using UnityEngine;

public class HumanMovement : BasicMovement
{
	[Header("NOT COMMON")]
	public bool isStationary;

	public override void Initialize(Rigidbody thisRB, Transform thisTR, float runSpeed, float walkSpeed, float rotateSpeed, Transform rotateTransform)
	{
		base.Initialize(thisRB, thisTR, runSpeed, walkSpeed, rotateSpeed, rotateTransform);
	}

	public override void Move(MovementState currentMovementState)
	{
		if (!isStationary && (currentMovementState == MovementState.MOVE_FORWARD || currentMovementState == MovementState.RUN))
		{
			base.Move(currentMovementState);
		}
	}

	public override void RotateToTarget(Vector3 targetEnemyPos)
	{
		base.RotateToTarget(targetEnemyPos);
	}
}
