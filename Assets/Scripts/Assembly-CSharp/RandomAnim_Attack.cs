using System;
using UnityEngine;

public class RandomAnim_Attack : StateMachineBehaviour
{
	[Serializable]
	public class AttackAnimRange
	{
		public int minRange;

		public int maxRange;
	}

	public int range;

	public AttackAnimRange[] attackAnimRanges;

	private int random_id;

	private int attack_id;

	private int index;

	public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
	{
		if (attackAnimRanges.Length == 0)
		{
			random_id = UnityEngine.Random.Range(0, range);
		}
		else
		{
			attack_id = animator.GetInteger(AnimationHash.ATTACK_ID);
			if (attack_id == 0)
			{
				random_id = UnityEngine.Random.Range(0, range);
			}
			else
			{
				index = attack_id - 1;
				random_id = UnityEngine.Random.Range(attackAnimRanges[index].minRange, attackAnimRanges[index].maxRange);
			}
		}
		animator.SetInteger(AnimationHash.RANDOM_ATTACK_ID, random_id);
	}
}
