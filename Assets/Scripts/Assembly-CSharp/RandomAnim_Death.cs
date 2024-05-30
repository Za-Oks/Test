using UnityEngine;

public class RandomAnim_Death : StateMachineBehaviour
{
	public int range;

	private int random_id;

	public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
	{
		random_id = Random.Range(0, range);
		animator.SetInteger(AnimationHash.RANDOM_DEATH_ID, random_id);
	}
}
