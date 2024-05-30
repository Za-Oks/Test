using System.Collections;
using UnityEngine;

public class ChariotMovement : BasicMovement
{
	private bool attacked;

	private WaitForSeconds wait_3s = new WaitForSeconds(3f);

	public override void Initialize(Rigidbody thisRB, Transform thisTR, float runSpeed, float walkSpeed, float rotateSpeed, Transform rotateTransform, Transform[] secondaryRotateTransforms)
	{
		base.Initialize(thisRB, thisTR, runSpeed, walkSpeed, rotateSpeed, rotateTransform, secondaryRotateTransforms);
	}

	public override void Move(MovementState currentMovementState)
	{
		if (currentMovementState != 0)
		{
			if (attacked && !AllReferencesManager.GAME_CONTROLLER.IsInsideBounds(thisTR.position.x, thisTR.position.z))
			{
				StopCoroutine("ResetAttacked");
				attacked = false;
			}
			base.Move(currentMovementState);
		}
	}

	public override void RotateToTarget(Vector3 targetEnemyPos)
	{
		base.RotateToTarget(targetEnemyPos, false);
		if (!attacked)
		{
			base.RotateToTarget(targetEnemyPos);
		}
	}

	public void Attacked()
	{
		attacked = true;
		StopCoroutine("ResetAttacked");
		StartCoroutine("ResetAttacked");
	}

	private IEnumerator ResetAttacked()
	{
		yield return wait_3s;
		attacked = false;
	}
}
