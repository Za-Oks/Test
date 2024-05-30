using UnityEngine;

public class BotAnimation_Machine : MonoBehaviour
{
	public Animator humanAnimator;

	private Animator thisAnimator;

	private MovementState lastMovementState;

	private AllReferencesManager referecesManager;

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
		humanAnimator.SetFloat(AnimationHash.ATTACK_SPEED, attackSpeed);
	}

	public void InitializeMovementSpeed(float movementSpeed)
	{
		thisAnimator.SetFloat(AnimationHash.MOVEMENT_SPEED, movementSpeed);
		humanAnimator.SetFloat(AnimationHash.MOVEMENT_SPEED, movementSpeed);
	}

	public void Animate()
	{
		if (referecesManager.botMovement.currentMovementState != lastMovementState)
		{
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
			else if (referecesManager.botMovement.currentMovementState == MovementState.DEAD)
			{
				Animation_Die();
			}
			lastMovementState = referecesManager.botMovement.currentMovementState;
		}
	}

	private void Animation_Idle()
	{
		if (thisAnimator.isInitialized && humanAnimator.isInitialized)
		{
			thisAnimator.SetBool(AnimationHash.MOVE, false);
			humanAnimator.SetBool(AnimationHash.ATTACK, false);
			humanAnimator.SetBool(AnimationHash.MOVE, false);
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
				humanAnimator.SetInteger(AnimationHash.ATTACK_ID, attackID);
			}
		}
	}

	private void Animation_Attack()
	{
		Animation_AttackID();
		thisAnimator.SetBool(AnimationHash.MOVE, false);
		humanAnimator.SetBool(AnimationHash.ATTACK, true);
		humanAnimator.SetBool(AnimationHash.MOVE, false);
	}

	private void Animation_Move(int speed)
	{
		thisAnimator.SetBool(AnimationHash.MOVE, true);
		thisAnimator.SetInteger(AnimationHash.SPEED, speed);
		humanAnimator.SetBool(AnimationHash.MOVE, true);
		humanAnimator.SetInteger(AnimationHash.SPEED, speed);
		humanAnimator.SetBool(AnimationHash.ATTACK, false);
	}

	private void Animation_GetHit()
	{
		humanAnimator.SetBool(AnimationHash.ATTACK, false);
		humanAnimator.SetBool(AnimationHash.MOVE, false);
	}

	private void Animation_AttackMachine()
	{
		thisAnimator.SetBool(AnimationHash.MOVE, false);
		thisAnimator.SetTrigger(AnimationHash.ATTACK);
	}

	private void Animation_Die()
	{
		thisAnimator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
		thisAnimator.SetBool(AnimationHash.MOVE, false);
		thisAnimator.SetTrigger(AnimationHash.DIE);
	}

	public void ResetEverything()
	{
		thisAnimator.cullingMode = AnimatorCullingMode.CullUpdateTransforms;
		thisAnimator.enabled = true;
		thisAnimator.Rebind();
		humanAnimator.cullingMode = AnimatorCullingMode.CullUpdateTransforms;
		humanAnimator.enabled = true;
		humanAnimator.Rebind();
	}

	public void AttackMachine()
	{
		Animation_AttackMachine();
	}

	public void DisableAnimators()
	{
		thisAnimator.enabled = false;
	}
}
