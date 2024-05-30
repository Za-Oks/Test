using UnityEngine;

public class Camera_SetupArmy : MonoBehaviour
{
	[Header("REFERENCES")]
	public Joystick_Single joystickMove;

	public Transform cameraTrans;

	[Header("BOUNDS")]
	public Vector3 maxBounds;

	public Vector3 minBounds;

	public float maxZoomY;

	public float minZoomY;

	[Header("SPEED")]
	public float speed = 1f;

	[Header("OTHER")]
	public Vector3 cameraStartPos;

	[HideInInspector]
	public bool canPlay;

	private Transform thisTrans;

	private Vector3 startPos;

	private Quaternion startRot;

	private Vector3 joystickMoveDir;

	private Vector3 moveDir;

	private Vector3 tempPos;

	private void Update()
	{
		if (canPlay && joystickMove.isDraging)
		{
			joystickMoveDir = joystickMove.GetInputDirection().normalized;
			moveDir.Set(joystickMoveDir.x, 0f, joystickMoveDir.y);
			tempPos = thisTrans.position + thisTrans.TransformDirection(moveDir) * speed * joystickMove.GetInputValue() * Time.deltaTime;
			tempPos.Set(Mathf.Clamp(tempPos.x, minBounds.x, maxBounds.x), Mathf.Clamp(tempPos.y, minBounds.y, maxBounds.y), Mathf.Clamp(tempPos.z, minBounds.z, maxBounds.z));
			thisTrans.position = tempPos;
		}
	}

	public void Zoom(float amount)
	{
		Vector3 vector = cameraTrans.position + cameraTrans.forward * amount;
		if (vector.y < maxZoomY && vector.y > minZoomY)
		{
			cameraTrans.position += cameraTrans.forward * amount;
		}
	}

	public void InitValues()
	{
		thisTrans = GetComponent<Transform>();
		startPos = thisTrans.position;
		startRot = thisTrans.rotation;
	}

	public void ResetEverything()
	{
		thisTrans.position = startPos;
		thisTrans.rotation = startRot;
	}

	public void ResetCameraPosition()
	{
		cameraTrans.localPosition = cameraStartPos;
	}
}
