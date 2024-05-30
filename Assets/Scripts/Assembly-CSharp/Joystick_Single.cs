using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick_Single : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
{
	public bool ARROW_MODE_ENABLED;

	public bool hasScaler;

	public RectTransform canvas_trans;

	public RectTransform joystick_trans;

	public RectTransform handlerTrans;

	public bool isDraging;

	private Vector2 pointerPos;

	private float radius;

	private float realRadius;

	private float reduction;

	private int dragFingerID = -2;

	private Vector3 startPos;

	private Vector2 unityPos = Vector2.zero;

	private PackagesManager packagesManager;

	[Header("TUTORIAL")]
	public TutorialManager tutorialManager;

	[HideInInspector]
	public bool isTutorialStep;

	private void Awake()
	{
		if (GameObject.FindWithTag("PackagesManager") != null)
		{
			packagesManager = GameObject.FindGameObjectWithTag("PackagesManager").GetComponent<PackagesManager>();
		}
	}

	private void Start()
	{
		radius = joystick_trans.rect.size.x / 2f;
		Init();
		if (packagesManager != null && packagesManager.whichStore == App_Stores.Steam)
		{
			joystick_trans.GetComponent<Image>().enabled = false;
			handlerTrans.GetComponent<Image>().enabled = false;
		}
		else
		{
			ARROW_MODE_ENABLED = false;
		}
	}

	private void Update()
	{
		if (!ARROW_MODE_ENABLED)
		{
			return;
		}
		unityPos.Set(startPos.x, startPos.y);
		if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
		{
			dragFingerID = -3;
			isDraging = true;
			if (Input.GetKey(KeyCode.UpArrow))
			{
				unityPos += new Vector2(0f, radius);
				handlerTrans.position = CalculateHandlerPosition(startPos, unityPos);
			}
			if (Input.GetKey(KeyCode.DownArrow))
			{
				unityPos -= new Vector2(0f, radius);
				handlerTrans.position = CalculateHandlerPosition(startPos, unityPos);
			}
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				unityPos += new Vector2(0f - radius, 0f);
				handlerTrans.position = CalculateHandlerPosition(startPos, unityPos);
			}
			if (Input.GetKey(KeyCode.RightArrow))
			{
				unityPos += new Vector2(radius, 0f);
				handlerTrans.position = CalculateHandlerPosition(startPos, unityPos);
			}
			if (isTutorialStep)
			{
				tutorialManager.NextStep();
				isTutorialStep = false;
			}
		}
		else if (isDraging)
		{
			dragFingerID = -2;
			isDraging = false;
			StopCoroutine("JoystickOrigin");
			StartCoroutine("JoystickOrigin");
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (dragFingerID == -2)
		{
			dragFingerID = eventData.pointerId;
		}
		if (eventData.pointerId == dragFingerID)
		{
			isDraging = true;
			handlerTrans.position = CalculateHandlerPosition(startPos, eventData.position);
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (eventData.pointerId == dragFingerID)
		{
			Reset();
			dragFingerID = -2;
			isDraging = false;
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (eventData.pointerId == dragFingerID)
		{
			isDraging = true;
			handlerTrans.position = CalculateHandlerPosition(startPos, eventData.position);
			if (isTutorialStep)
			{
				tutorialManager.NextStep();
				isTutorialStep = false;
			}
		}
	}

	public void Init()
	{
		if (hasScaler)
		{
			startPos = handlerTrans.position;
			reduction = 1f - canvas_trans.localScale.x;
			realRadius = radius * (1f - reduction);
		}
		else
		{
			startPos = joystick_trans.position;
			realRadius = radius;
			handlerTrans.position = startPos;
		}
	}

	public bool CheckFingerID(int fingerID)
	{
		return fingerID == dragFingerID;
	}

	public bool HasFinger()
	{
		return dragFingerID != -2;
	}

	public float GetInputValue()
	{
		return Mathf.Clamp(Vector3.Magnitude(handlerTrans.position - startPos) / realRadius, 0f, 1f);
	}

	public Vector2 GetInputDirection()
	{
		return handlerTrans.position - startPos;
	}

	public float GetInputAngles()
	{
		float num = Vector2.Angle(Vector2.up, GetInputDirection());
		if (handlerTrans.position.x < startPos.x)
		{
			num = 360f - num;
		}
		return num;
	}

	public Quaternion GetToRotation()
	{
		return Quaternion.Euler(0f, GetInputAngles() + Camera.main.transform.rotation.eulerAngles.y, 0f);
	}

	private Vector2 CalculateHandlerPosition(Vector2 centerPoint, Vector2 endPoint)
	{
		Vector2 a = GetRealPos(centerPoint);
		Vector2 b = GetRealPos(endPoint);
		if (Vector2.Distance(a, b) > radius)
		{
			Vector2 vector = endPoint - centerPoint;
			vector.Normalize();
			return vector * realRadius + centerPoint;
		}
		return endPoint;
	}

	private Vector3 GetRealPos(Vector2 pos)
	{
		if (hasScaler)
		{
			return new Vector2(pos.x / (1f - reduction), pos.y / (1f - reduction));
		}
		return pos;
	}

	public void Reset()
	{
		handlerTrans.position = startPos;
		isDraging = false;
		dragFingerID = -2;
	}

	private IEnumerator JoystickOrigin()
	{
		if (Vector2.Distance(b: handlerTrans.position, a: startPos) > 0f)
		{
			float t = 0f;
			float animDuration = 0.3f;
			while (t < 1f)
			{
				t += Time.deltaTime / animDuration;
				handlerTrans.position = Vector3.Lerp(handlerTrans.position, startPos, t);
				yield return null;
			}
			handlerTrans.position = startPos;
		}
		yield return null;
	}
}
