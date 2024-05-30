using UnityEngine;

public static class AnimationHash
{
	public static int DIE = Animator.StringToHash("Die");

	public static int MOVE = Animator.StringToHash("Move");

	public static int ATTACK = Animator.StringToHash("Attack");

	public static int SPEED = Animator.StringToHash("Speed");

	public static int RANDOM_ATTACK_ID = Animator.StringToHash("RandomAttackID");

	public static int RANDOM_DEATH_ID = Animator.StringToHash("RandomDeathID");

	public static int ATTACK_ID = Animator.StringToHash("AttackID");

	public static int FLY = Animator.StringToHash("Fly");

	public static int FLY_ATTACK = Animator.StringToHash("Fly Attack");

	public static int ATTACK_SPEED = Animator.StringToHash("AttackSpeedMultiplier");

	public static int MOVEMENT_SPEED = Animator.StringToHash("MovementSpeedMultiplier");
}
