using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchPanel_SetupArmy : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler, IEventSystemHandler
{
	public enum TouchType
	{
		NOTHING = 0,
		SIGLE = 1,
		DOUBLE = 2
	}

	private int firstFingerID = -2;

	private int secondFingerID = -2;

	private Vector3 lastPosFirstFinger;

	private Vector3 lastPosSecondFinger;

	private PackagesManager packagesManager;

	[Header("REFERENCES")]
	public Level_Manager levelManager;

	public Camera_SetupArmy cameraSetupArmy;

	[Header("VALUES")]
	public float speed = 1f;

	public float maxDiff = 10f;

	public float pcZoomSpeed = 10f;

	[Header("TUTORIAL")]
	public bool blockPutArmy;

	public bool blockZoom;

	public TutorialManager tutorialManager;

	[HideInInspector]
	public bool isTutorialStep_Zoom;

	[HideInInspector]
	public bool isTutorialStep_PutArmy;

	private bool isDelay;

	private bool didPlaceArmy;

	private bool inside;

	private void Awake()
	{
		if (GameObject.FindWithTag("PackagesManager") != null)
		{
			packagesManager = GameObject.FindGameObjectWithTag("PackagesManager").GetComponent<PackagesManager>();
		}
	}

	private void Update()
	{
		if (GetTouchType() == TouchType.SIGLE && !isDelay)
		{
			OnDrag(lastPosFirstFinger);
		}
		if (packagesManager != null && packagesManager.whichStore == App_Stores.Steam)
		{
			float axis = Input.GetAxis("Mouse ScrollWheel");
			if (axis != 0f)
			{
				cameraSetupArmy.Zoom(axis * pcZoomSpeed);
				CheckTutorialStep_Zoom();
			}
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (blockPutArmy && blockZoom)
		{
			return;
		}
		if (GetTouchType() == TouchType.NOTHING)
		{
			firstFingerID = eventData.pointerId;
			lastPosFirstFinger = eventData.position;
			if (isDelay)
			{
				StopDelay();
			}
			if (!blockPutArmy)
			{
				StartCoroutine("DelayFirstTouch", lastPosFirstFinger);
			}
		}
		else if (GetTouchType() == TouchType.SIGLE && !blockZoom)
		{
			secondFingerID = eventData.pointerId;
			lastPosSecondFinger = eventData.position;
			if (isDelay)
			{
				StopDelay();
			}
		}
	}

	private IEnumerator DelayFirstTouch(Vector3 pos)
	{
		isDelay = true;
		yield return new WaitForSeconds(0.1f);
		didPlaceArmy = levelManager.AddRemoveArmy(pos, true);
		isDelay = false;
		if (didPlaceArmy)
		{
			CheckTutorialStep_PutArmy();
		}
	}

	private void StopDelay()
	{
		isDelay = false;
		StopCoroutine("DelayFirstTouch");
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (isDelay || GetTouchType() == TouchType.NOTHING)
		{
			return;
		}
		if (GetTouchType() == TouchType.SIGLE)
		{
			if (eventData.pointerId == firstFingerID)
			{
				lastPosFirstFinger = eventData.position;
			}
		}
		else if (GetTouchType() == TouchType.DOUBLE)
		{
			if (eventData.pointerId == firstFingerID)
			{
				float num = Vector2.Distance(lastPosFirstFinger, lastPosSecondFinger);
				lastPosFirstFinger = eventData.position;
				float num2 = Vector2.Distance(lastPosFirstFinger, lastPosSecondFinger);
				float value = (num2 - num) * speed;
				value = Mathf.Clamp(value, 0f - maxDiff, maxDiff);
				cameraSetupArmy.Zoom(value);
				CheckTutorialStep_Zoom();
			}
			else if (eventData.pointerId == secondFingerID)
			{
				float num3 = Vector2.Distance(lastPosFirstFinger, lastPosSecondFinger);
				lastPosSecondFinger = eventData.position;
				float num4 = Vector2.Distance(lastPosFirstFinger, lastPosSecondFinger);
				float value2 = (num4 - num3) * speed;
				value2 = Mathf.Clamp(value2, 0f - maxDiff, maxDiff);
				cameraSetupArmy.Zoom(value2);
				CheckTutorialStep_Zoom();
			}
		}
	}

	private void OnDrag(Vector3 position)
	{
		if (IsInsideUI() && !blockPutArmy)
		{
			didPlaceArmy = levelManager.AddRemoveArmy(position, false);
			if (didPlaceArmy)
			{
				CheckTutorialStep_PutArmy();
			}
		}
	}

	private bool IsInsideUI()
	{
		return inside;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (GetTouchType() != TouchType.SIGLE || eventData.pointerId == firstFingerID)
		{
			inside = false;
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (GetTouchType() != TouchType.SIGLE || eventData.pointerId == firstFingerID)
		{
			inside = true;
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (GetTouchType() == TouchType.NOTHING)
		{
			return;
		}
		if (GetTouchType() == TouchType.SIGLE)
		{
			if (eventData.pointerId == firstFingerID)
			{
				firstFingerID = -2;
			}
		}
		else if (GetTouchType() == TouchType.DOUBLE && (eventData.pointerId == firstFingerID || eventData.pointerId == secondFingerID))
		{
			firstFingerID = -2;
			secondFingerID = -2;
		}
	}

	private TouchType GetTouchType()
	{
		if (firstFingerID == -2)
		{
			return TouchType.NOTHING;
		}
		if (secondFingerID == -2)
		{
			return TouchType.SIGLE;
		}
		return TouchType.DOUBLE;
	}

	private void CheckTutorialStep_PutArmy()
	{
		if (isTutorialStep_PutArmy)
		{
			tutorialManager.NextStep();
			isTutorialStep_PutArmy = false;
		}
	}

	private void CheckTutorialStep_Zoom()
	{
		if (isTutorialStep_Zoom)
		{
			tutorialManager.NextStep();
			isTutorialStep_Zoom = false;
		}
	}
}
