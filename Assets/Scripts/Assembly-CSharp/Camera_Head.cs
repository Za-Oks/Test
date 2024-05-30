using UnityEngine;

public class Camera_Head : MonoBehaviour
{
	[Header("REFERENCES")]
	public Joystick_Single joystick;

	[Header("SPEED")]
	public float rotateSpeed = 1f;

	[HideInInspector]
	public bool canPlay;

	private Transform thisTr;

	private Transform target;

	public float speed1;

	public float speed2;

	private Vector3 joystickRotateDir;

	private Vector3 rotateDir;

	private Quaternion tempRot;

	private Vector3 tempEuler;

	private Quaternion startRot;

	private void Update()
	{
		if (canPlay && joystick.isDraging)
		{
			joystickRotateDir = joystick.GetInputDirection().normalized;
			rotateDir.Set(0f - joystickRotateDir.y, joystickRotateDir.x, 0f);
			tempRot = target.localRotation * Quaternion.Euler(rotateDir * rotateSpeed * joystick.GetInputValue() * Time.deltaTime);
			tempEuler = tempRot.eulerAngles;
			tempRot = Quaternion.Euler(ClampAngle(tempEuler.x, -20f, 20f), ClampAngle(tempEuler.y, -20f, 20f), 0f);
			target.localRotation = tempRot;
		}
	}

	private void LateUpdate()
	{
		if ((bool)target)
		{
			thisTr.position = Vector3.MoveTowards(thisTr.position, target.position, Time.deltaTime * speed1);
			thisTr.rotation = Quaternion.RotateTowards(thisTr.rotation, target.rotation, Time.deltaTime * speed2);
		}
	}

	public void Init(Transform target)
	{
		if (!thisTr)
		{
			thisTr = GetComponent<Transform>();
		}
		this.target = target;
		thisTr.position = target.position;
		thisTr.rotation = target.rotation;
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
