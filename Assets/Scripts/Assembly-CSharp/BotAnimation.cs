using UnityEngine;

public class BotAnimation : MonoBehaviour
{
	private AllReferencesManager referecesManager;

	private Animator thisAnimator;

	private MovementState lastMovementState;

	private float lastTime_attackID = -100f;

	private float timeBetween_attackID = 1f;

	private void Awake()
	{
		referecesManager = GetComponent<AllReferencesManager>();
		thisAnimator = GetComponent<Animator>();
	}

	public void InitializeAtackSpeed(float attackSpeed)
	{
		thisAnimator.SetFloat(AnimationHash.ATTACK_SPEED, attackSpeed);
	}

	public void InitializeMovementSpeed(float movementSpeed)
	{
		thisAnimator.SetFloat(AnimationHash.MOVEMENT_SPEED, movementSpeed);
	}

	public void Animate()
	{
		if (referecesManager.botMovement.currentMovementState == lastMovementState)
		{
			if (referecesManager.botMovement.currentMovementState == MovementState.ATTACK)
			{
				Animation_AttackID();
			}
			return;
		}
		if (referecesManager.botMovement.currentMovementState == MovementState.IDLE)
		{
			Animation_Idle();
		}
		else if (referecesManager.botMovement.currentMovementState == MovementState.ATTACK)
		{
			Animation_Attack();
		}
		else if (referecesManager.botMovement.currentMovementState == MovementState.RUN)
		{
			Animation_Move(0);
		}
		else if (referecesManager.botMovement.currentMovementState == MovementState.MOVE_FORWARD)
		{
			Animation_Move(1);
		}
		else if (referecesManager.botMovement.currentMovementState == MovementState.FLY)
		{
			Animation_Fly();
		}
		lastMovementState = referecesManager.botMovement.currentMovementState;
	}

	private void Animation_Idle()
	{
		if (thisAnimator.isInitialized)
		{
			thisAnimator.SetBool(AnimationHash.ATTACK, false);
			thisAnimator.SetBool(AnimationHash.MOVE, false);
			if (referecesManager.botMovement.canFly)
			{
				thisAnimator.SetBool(AnimationHash.FLY, false);
			}
		}
	}

	private void Animation_AttackID()
	{
		if (Time.time - lastTime_attackID >= timeBetween_attackID)
		{
			lastTime_attackID = Time.time;
			int attackID = referecesManager.botMovement.GetAttackID();
			if (attackID != -1)
			{
				thisAnimator.SetInteger(AnimationHash.ATTACK_ID, attackID);
			}
		}
	}

	private void Animation_Attack()
	{
		Animation_AttackID();
		thisAnimator.SetBool(AnimationHash.ATTACK, true);
		thisAnimator.SetBool(AnimationHash.MOVE, false);
		if (referecesManager.botMovement.canFly)
		{
			thisAnimator.SetBool(AnimationHash.FLY, false);
		}
	}

	private void Animation_Move(int speed)
	{
		thisAnimator.SetBool(AnimationHash.MOVE, true);
		thisAnimator.SetInteger(AnimationHash.SPEED, speed);
		thisAnimator.SetBool(AnimationHash.ATTACK, false);
		if (referecesManager.botMovement.canFly)
		{
			thisAnimator.SetBool(AnimationHash.FLY, false);
		}
	}

	private void Animation_Fly()
	{
		thisAnimator.SetBool(AnimationHash.MOVE, false);
		thisAnimator.SetBool(AnimationHash.ATTACK, false);
		if (referecesManager.botMovement.canFly)
		{
			thisAnimator.SetBool(AnimationHash.FLY, true);
		}
	}

	public void ResetEverything()
	{
		thisAnimator.cullingMode = AnimatorCullingMode.CullUpdateTransforms;
		thisAnimator.enabled = true;
		thisAnimator.Rebind();
	}
}
