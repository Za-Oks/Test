using UnityEngine;

public class BasicMovement : MonoBehaviour
{
	protected AllReferencesManager referencesManager;

	protected Rigidbody thisRB;

	protected Transform thisTR;

	protected Transform rotateTransform;

	protected Transform[] secondaryRotateTransforms;

	private Transform tempRotateTransform;

	private float runSpeed;

	private float walkSpeed;

	private float rotateSpeed;

	private int rotateSpeedSign = 1;

	public bool skipRotateFrame = true;

	private Vector3 targetPos;

	private bool rotateFlag;

	private void Awake()
	{
		referencesManager = GetComponent<AllReferencesManager>();
	}

	public virtual void Initialize(Rigidbody thisRB, Transform thisTR, float runSpeed, float walkSpeed, float rotateSpeed)
	{
		Initialize(thisRB, thisTR, runSpeed, walkSpeed, rotateSpeed, null, null);
	}

	public virtual void Initialize(Rigidbody thisRB, Transform thisTR, float runSpeed, float walkSpeed, float rotateSpeed, Transform rotateTransform)
	{
		Initialize(thisRB, thisTR, runSpeed, walkSpeed, rotateSpeed, rotateTransform, null);
	}

	public virtual void Initialize(Rigidbody thisRB, Transform thisTR, float runSpeed, float walkSpeed, float rotateSpeed, Transform rotateTransform, Transform[] secondaryRotateTransforms)
	{
		if (skipRotateFrame)
		{
			rotateSpeed *= 2f;
		}
		this.thisRB = thisRB;
		this.thisTR = thisTR;
		this.rotateTransform = rotateTransform;
		this.secondaryRotateTransforms = secondaryRotateTransforms;
		this.runSpeed = runSpeed;
		this.walkSpeed = walkSpeed;
		this.rotateSpeed = rotateSpeed;
	}

	public virtual void Move(MovementState currentMovementState)
	{
		if (!(thisRB == null) && !(thisTR == null))
		{
			float num = runSpeed;
			if (currentMovementState == MovementState.MOVE_FORWARD)
			{
				num = walkSpeed;
			}
			targetPos = thisRB.position + thisTR.forward * num * 0.008f;
			thisRB.MovePosition(targetPos);
		}
	}

	public virtual void RotateToTarget(Vector3 targetEnemyPos)
	{
		if (skipRotateFrame)
		{
			rotateFlag = !rotateFlag;
			if (rotateFlag)
			{
				return;
			}
		}
		RotateToTarget(targetEnemyPos, true);
	}

	public virtual void RotateToTarget(Vector3 targetEnemyPos, bool mainTransform)
	{
		if (thisRB == null || thisTR == null)
		{
			return;
		}
		if (mainTransform)
		{
			tempRotateTransform = rotateTransform;
			RotateLogic(targetEnemyPos);
			return;
		}
		for (int i = 0; i < secondaryRotateTransforms.Length; i++)
		{
			tempRotateTransform = secondaryRotateTransforms[i];
			RotateLogic(targetEnemyPos);
		}
	}

	private void RotateLogic(Vector3 targetEnemyPos)
	{
		if (tempRotateTransform == null)
		{
			targetEnemyPos.y = thisRB.position.y;
			if (ShouldLookAt(tempRotateTransform, targetEnemyPos))
			{
				thisRB.rotation = Quaternion.LookRotation(targetEnemyPos - thisRB.position, Vector3.up);
				return;
			}
			FixRotateSign(targetEnemyPos);
			thisRB.MoveRotation(thisRB.rotation * Quaternion.Euler(Vector3.up * rotateSpeed * rotateSpeedSign * 0.05f * 1f));
		}
		else
		{
			targetEnemyPos.y = tempRotateTransform.position.y;
			if (ShouldLookAt(tempRotateTransform, targetEnemyPos))
			{
				tempRotateTransform.rotation = Quaternion.LookRotation(targetEnemyPos - tempRotateTransform.position, Vector3.up);
				return;
			}
			FixRotateSign(targetEnemyPos);
			tempRotateTransform.rotation *= Quaternion.Euler(Vector3.up * rotateSpeed * rotateSpeedSign * 0.05f * 1f);
		}
	}

	private bool ShouldLookAt(Transform whatTransform, Vector3 position)
	{
		if (whatTransform == null)
		{
			return Vector3.Dot(thisTR.forward, (position - thisTR.position).normalized) >= 0.99f;
		}
		return Vector3.Dot(whatTransform.forward, (position - whatTransform.position).normalized) >= 0.99f;
	}

	private void FixRotateSign(Vector3 position)
	{
		if (tempRotateTransform == null)
		{
			rotateSpeedSign = ((Vector3.Dot(thisTR.right.normalized, (position - thisTR.position).normalized) > 0f) ? 1 : (-1));
		}
		else
		{
			rotateSpeedSign = ((Vector3.Dot(tempRotateTransform.right.normalized, (position - tempRotateTransform.position).normalized) > 0f) ? 1 : (-1));
		}
	}

	public void ResetEverything()
	{
		if (rotateTransform != null)
		{
			rotateTransform.localRotation = Quaternion.identity;
		}
		if (secondaryRotateTransforms != null)
		{
			for (int i = 0; i < secondaryRotateTransforms.Length; i++)
			{
				secondaryRotateTransforms[i].localRotation = Quaternion.identity;
			}
		}
	}
}
