using System.Collections;
using UnityEngine;

public class BotAnimation_Chariot : MonoBehaviour
{
	public Animator[] humanAnimators;

	private int humanAnimatorsLength;

	private Animator thisAnimator;

	private MovementState lastMovementState;

	private AllReferencesManager referecesManager;

	[Header("ATTACK DELAY")]
	public bool attackDelay = true;

	public float minAttackDelay;

	public float maxAttackDelay = 0.5f;

	private float lastTime_attackID = -100f;

	private float timeBetween_attackID = 1f;

	private void Awake()
	{
		referecesManager = GetComponent<AllReferencesManager>();
		thisAnimator = GetComponent<Animator>();
		humanAnimatorsLength = humanAnimators.Length;
	}

	public void InitializeAtackSpeed(float attackSpeed)
	{
		thisAnimator.SetFloat(AnimationHash.ATTACK_SPEED, attackSpeed);
		for (int i = 0; i < humanAnimatorsLength; i++)
		{
			humanAnimators[i].SetFloat(AnimationHash.ATTACK_SPEED, attackSpeed);
		}
	}

	public void InitializeMovementSpeed(float movementSpeed)
	{
		thisAnimator.SetFloat(AnimationHash.MOVEMENT_SPEED, movementSpeed);
		for (int i = 0; i < humanAnimatorsLength; i++)
		{
			humanAnimators[i].SetFloat(AnimationHash.MOVEMENT_SPEED, movementSpeed);
		}
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
		if (!thisAnimator.isInitialized)
		{
			return;
		}
		for (int i = 0; i < humanAnimatorsLength; i++)
		{
			if (!humanAnimators[i].isInitialized)
			{
				return;
			}
		}
		thisAnimator.SetBool(AnimationHash.MOVE, false);
		StopCoroutine("HumanAttackDelay");
		for (int j = 0; j < humanAnimatorsLength; j++)
		{
			humanAnimators[j].SetBool(AnimationHash.ATTACK, false);
		}
	}

	private void Animation_AttackID()
	{
		if (!(Time.time - lastTime_attackID >= timeBetween_attackID))
		{
			return;
		}
		lastTime_attackID = Time.time;
		int attackID = referecesManager.botMovement.GetAttackID();
		if (attackID != -1)
		{
			for (int i = 0; i < humanAnimatorsLength; i++)
			{
				humanAnimators[i].SetInteger(AnimationHash.ATTACK_ID, attackID);
			}
		}
	}

	private void Animation_Attack()
	{
		Animation_AttackID();
		thisAnimator.SetBool(AnimationHash.MOVE, true);
		StopCoroutine("HumanAttackDelay");
		StartCoroutine("HumanAttackDelay");
	}

	private IEnumerator HumanAttackDelay()
	{
		for (int i = 0; i < humanAnimatorsLength; i++)
		{
			yield return new WaitForSeconds(Random.Range(minAttackDelay, maxAttackDelay));
			humanAnimators[i].SetBool(AnimationHash.ATTACK, true);
		}
	}

	private void Animation_Move(int speed)
	{
		thisAnimator.SetBool(AnimationHash.MOVE, true);
		thisAnimator.SetInteger(AnimationHash.SPEED, speed);
		for (int i = 0; i < humanAnimatorsLength; i++)
		{
			humanAnimators[i].SetBool(AnimationHash.ATTACK, false);
		}
	}

	private void Animation_Die()
	{
		thisAnimator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
		thisAnimator.SetBool(AnimationHash.MOVE, false);
		thisAnimator.SetTrigger(AnimationHash.DIE);
	}

	public void ResetEverything()
	{
		StopCoroutine("HumanAttackDelay");
		thisAnimator.cullingMode = AnimatorCullingMode.CullUpdateTransforms;
		thisAnimator.enabled = true;
		thisAnimator.Rebind();
		for (int i = 0; i < humanAnimatorsLength; i++)
		{
			humanAnimators[i].cullingMode = AnimatorCullingMode.CullUpdateTransforms;
			humanAnimators[i].enabled = true;
			humanAnimators[i].Rebind();
		}
	}

	public void DisableAnimators()
	{
		thisAnimator.enabled = false;
	}
}
