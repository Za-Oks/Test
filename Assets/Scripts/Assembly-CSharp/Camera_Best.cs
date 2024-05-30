using UnityEngine;

public class Camera_Best : MonoBehaviour
{
	[Header("REFERENCES")]
	public Joystick_Single joystickMove;

	[Header("BOUNDS")]
	public Vector3 maxBounds;

	public Vector3 minBounds;

	[Header("SPEED")]
	public float speed = 1f;

	public float rotateSpeed = 1f;

	public bool canPlay;

	private Transform thisTrans;

	private Vector3 startPos;

	private Quaternion startRot;

	private Vector3 joystickMoveDir;

	private Vector3 moveDir;

	private Vector3 rotateDir;

	private Vector3 tempPos;

	private Vector3 tempEuler;

	private Quaternion tempRot;

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

	public void Rotate(Vector2 amount)
	{
		if (canPlay)
		{
			rotateDir.Set(0f - amount.y, amount.x, 0f);
			tempRot = thisTrans.rotation * Quaternion.Euler(rotateDir * rotateSpeed);
			tempEuler = tempRot.eulerAngles;
			tempRot = Quaternion.Euler(ClampAngle(tempEuler.x, -60f, 60f), tempEuler.y, 0f);
			thisTrans.rotation = tempRot;
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
		if (thisTrans == null)
		{
			InitValues();
		}
		thisTrans.position = startPos;
		thisTrans.rotation = startRot;
	}

	private float ClampAngle(float angle, float min, float max)
	{
		if (angle < 90f || angle > 270f)
		{
			if (angle > 180f)
			{
				angle -= 360f;
			}
			if (max > 180f)
			{
				max -= 360f;
			}
			if (min > 180f)
			{
				min -= 360f;
			}
		}
		angle = Mathf.Clamp(angle, min, max);
		if (angle < 0f)
		{
			angle += 360f;
		}
		return angle;
	}
}
