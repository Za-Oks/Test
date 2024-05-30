using UnityEngine;

public class AnimationEventListener : MonoBehaviour
{
	[Header("NOT COMMON")]
	public AllReferencesManager referencesManager;

	public int attackRangedWeapon;

	private void Awake()
	{
		if (referencesManager == null)
		{
			referencesManager = GetComponent<AllReferencesManager>();
		}
	}

	public void AnimationEvent_AttackMelee()
	{
		referencesManager.botAttack.Attack_Melee();
		if (referencesManager.horseMovement != null)
		{
			referencesManager.horseMovement.Attacked();
		}
		referencesManager.Attacked();
	}

	public void AnimationEvent_AttackMelee1()
	{
		referencesManager.botAttack.Attack_Melee1();
		referencesManager.Attacked();
	}

	public void AnimationEvent_AttackMelee2()
	{
		referencesManager.botAttack.Attack_Melee2();
		referencesManager.Attacked();
	}

	public void AnimationEvent_AttackRanged()
	{
		if (attackRangedWeapon == 0)
		{
			referencesManager.botAttack.Attack_Ranged();
			referencesManager.Attacked();
		}
		else if (attackRangedWeapon == 1)
		{
			AnimationEvent_AttackRanged1();
		}
		else if (attackRangedWeapon == 2)
		{
			AnimationEvent_AttackRanged2();
		}
		else if (attackRangedWeapon == 3)
		{
			AnimationEvent_AttackRanged3();
		}
		else if (attackRangedWeapon == 4)
		{
			AnimationEvent_AttackRanged4();
		}
		else if (attackRangedWeapon == 5)
		{
			AnimationEvent_AttackRanged5();
		}
	}

	public void AnimationEvent_AttackRanged1()
	{
		referencesManager.botAttack.Attack_Ranged1();
		referencesManager.Attacked();
	}

	public void AnimationEvent_AttackRanged2()
	{
		referencesManager.botAttack.Attack_Ranged2();
		referencesManager.Attacked();
	}

	public void AnimationEvent_AttackRanged3()
	{
		referencesManager.botAttack.Attack_Ranged3();
		referencesManager.Attacked();
	}

	public void AnimationEvent_AttackRanged4()
	{
		referencesManager.botAttack.Attack_Ranged4();
		referencesManager.Attacked();
	}

	public void AnimationEvent_AttackRanged5()
	{
		referencesManager.botAttack.Attack_Ranged5();
		referencesManager.Attacked();
	}

	public void AnimationEvent_ResetMelee()
	{
		referencesManager.botAttack.Reset_Melee();
	}

	public void AnimationEvent_ResetMelee1()
	{
		referencesManager.botAttack.Reset_Melee1();
	}

	public void AnimationEvent_ResetMelee2()
	{
		referencesManager.botAttack.Reset_Melee2();
	}

	public void AnimationEvent_AttackMachine()
	{
		referencesManager.botAnimationMachine.AttackMachine();
	}

	public void AnimationEvent_Reload()
	{
		referencesManager.botAttack.Reload();
	}

	public void AnimationEvent_Reload1()
	{
		referencesManager.botAttack.Reload1();
	}

	public void AnimationEvent_Reload2()
	{
		referencesManager.botAttack.Reload2();
	}

	public void AnimationEvent_AttackExplosion()
	{
		referencesManager.botAttack.Attack_Explosion();
		referencesManager.Attacked();
	}

	public void AnimationEvent_DisableAnimators()
	{
		referencesManager.DisableAnimators();
	}

	public void AnimationEvent_AttackLanded(int i)
	{
		referencesManager.PlayAttackParticle(i);
		referencesManager.PlayAttackAudio();
		referencesManager.PlayAttackCameraShake();
	}

	public void AnimationEvent_StopSideEffects()
	{
		referencesManager.StopWeaponSideEffects();
	}
}
