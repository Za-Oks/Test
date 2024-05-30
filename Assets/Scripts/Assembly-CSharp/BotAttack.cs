using UnityEngine;

public class BotAttack : MonoBehaviour
{
	[Header("STATS")]
	public float force = 5f;

	public int penetration = 1;

	[Header("MELEE WEAPONS")]
	public WeaponMelee weaponMelee1;

	public WeaponMelee weaponMelee2;

	public bool playAudioFromAnimationEvents;

	[Header("RANGED WEAPON")]
	public WeaponRanged weaponRanged;

	public WeaponRanged weaponRanged2;

	public WeaponRanged weaponRanged3;

	public WeaponRanged weaponRanged4;

	public WeaponRanged weaponRanged5;

	[Header("OTHER WEAPONS")]
	public WeaponExplosion weaponExplosion;

	[Header("OTHER")]
	public bool shakeCamera;

	[Header("NOT COMMON")]
	public AttackParticleType particleType;

	private AllReferencesManager referencesManager;

	private float damage = 10f;

	private LayerMask layerWeapon1;

	private LayerMask layerWeapon2;

	private BotMovement enemyTarget;

	private void Awake()
	{
		referencesManager = GetComponent<AllReferencesManager>();
		layerWeapon1 = LayerMask.NameToLayer("Weapon");
		layerWeapon2 = LayerMask.NameToLayer("Weapon2");
	}

	public void InitDamage(float damage)
	{
		this.damage = damage;
	}

	public void InitWeapons(bool isPlayer)
	{
		LayerMask layerMask = ((!referencesManager.IsPlayer()) ? layerWeapon1 : layerWeapon2);
		if (weaponMelee1 != null)
		{
			weaponMelee1.gameObject.layer = layerMask;
			weaponMelee1.InitStats(damage, penetration, force);
			weaponMelee1.InitBot(referencesManager, base.transform, isPlayer, particleType, playAudioFromAnimationEvents);
		}
		if (weaponMelee2 != null)
		{
			weaponMelee2.gameObject.layer = layerMask;
			weaponMelee2.InitStats(damage, penetration, force);
			weaponMelee2.InitBot(referencesManager, base.transform, isPlayer, particleType, playAudioFromAnimationEvents);
		}
		if (weaponRanged != null)
		{
			weaponRanged.gameObject.layer = layerMask;
			weaponRanged.InitStats(damage, force, referencesManager.level + 1, penetration);
			weaponRanged.InitBot(base.transform, isPlayer);
		}
		if (weaponRanged2 != null)
		{
			weaponRanged2.gameObject.layer = layerMask;
			weaponRanged2.InitStats(damage, force, referencesManager.level + 1, penetration);
			weaponRanged2.InitBot(base.transform, isPlayer);
		}
		if (weaponRanged3 != null)
		{
			weaponRanged3.gameObject.layer = layerMask;
			weaponRanged3.InitStats(damage, force, referencesManager.level + 1, penetration);
			weaponRanged3.InitBot(base.transform, isPlayer);
		}
		if (weaponRanged4 != null)
		{
			weaponRanged4.gameObject.layer = layerMask;
			weaponRanged4.InitStats(damage, force, referencesManager.level + 1, penetration);
			weaponRanged4.InitBot(base.transform, isPlayer);
		}
		if (weaponRanged5 != null)
		{
			weaponRanged5.gameObject.layer = layerMask;
			weaponRanged5.InitStats(damage, force, referencesManager.level + 1, penetration);
			weaponRanged5.InitBot(base.transform, isPlayer);
		}
		if (weaponExplosion != null)
		{
			weaponExplosion.gameObject.layer = layerMask;
			weaponExplosion.InitStats(damage, force);
			weaponExplosion.InitBot(referencesManager, base.transform, isPlayer);
		}
	}

	public void Attack_Melee()
	{
		Attack_Melee1();
		Attack_Melee2();
	}

	public void Attack_Melee1()
	{
		if (weaponMelee1 != null)
		{
			weaponMelee1.Attack();
		}
	}

	public void Attack_Melee2()
	{
		if (weaponMelee2 != null)
		{
			weaponMelee2.Attack();
		}
	}

	public void Reset_Melee()
	{
		Reset_Melee1();
		Reset_Melee2();
	}

	public void Reset_Melee1()
	{
		if (weaponMelee1 != null)
		{
			weaponMelee1.ResetWeapon();
		}
	}

	public void Reset_Melee2()
	{
		if (weaponMelee2 != null)
		{
			weaponMelee2.ResetWeapon();
		}
	}

	public void Attack_Ranged()
	{
		Attack_Ranged1();
		Attack_Ranged2();
		Attack_Ranged3();
		Attack_Ranged4();
		Attack_Ranged5();
	}

	public void Attack_Ranged1()
	{
		if (weaponRanged != null)
		{
			enemyTarget = referencesManager.GetTargetEnemy();
			if (enemyTarget != null)
			{
				weaponRanged.Attack(enemyTarget.transform.position);
			}
			else
			{
				weaponRanged.StraightAttack();
			}
			if (!weaponRanged.muteAudioAndShake)
			{
				PlayAttackCameraShake();
				referencesManager.PlayAttackAudio();
			}
		}
	}

	public void Attack_Ranged2()
	{
		if (weaponRanged2 != null)
		{
			enemyTarget = referencesManager.GetTargetEnemy();
			if (enemyTarget != null)
			{
				weaponRanged2.Attack(enemyTarget.transform.position);
			}
			else
			{
				weaponRanged2.StraightAttack();
			}
			if (!weaponRanged2.muteAudioAndShake)
			{
				PlayAttackCameraShake();
				referencesManager.PlayAttackAudio();
			}
		}
	}

	public void Attack_Ranged3()
	{
		if (weaponRanged3 != null)
		{
			enemyTarget = referencesManager.GetTargetEnemy();
			if (enemyTarget != null)
			{
				weaponRanged3.Attack(enemyTarget.transform.position);
			}
			else
			{
				weaponRanged3.StraightAttack();
			}
			if (!weaponRanged3.muteAudioAndShake)
			{
				PlayAttackCameraShake();
				referencesManager.PlayAttackAudio();
			}
		}
	}

	public void Attack_Ranged4()
	{
		if (weaponRanged4 != null)
		{
			enemyTarget = referencesManager.GetTargetEnemy();
			if (enemyTarget != null)
			{
				weaponRanged4.Attack(enemyTarget.transform.position);
			}
			else
			{
				weaponRanged4.StraightAttack();
			}
			if (!weaponRanged4.muteAudioAndShake)
			{
				PlayAttackCameraShake();
				referencesManager.PlayAttackAudio();
			}
		}
	}

	public void Attack_Ranged5()
	{
		if (weaponRanged5 != null)
		{
			enemyTarget = referencesManager.GetTargetEnemy();
			if (enemyTarget != null)
			{
				weaponRanged5.Attack(enemyTarget.transform.position);
			}
			else
			{
				weaponRanged5.StraightAttack();
			}
			if (!weaponRanged5.muteAudioAndShake)
			{
				PlayAttackCameraShake();
				referencesManager.PlayAttackAudio();
			}
		}
	}

	public void Reload()
	{
		Reload1();
		Reload2();
	}

	public void Reload1()
	{
		if (weaponRanged != null)
		{
			weaponRanged.Reload();
		}
	}

	public void Reload2()
	{
		if (weaponRanged2 != null)
		{
			weaponRanged2.Reload();
		}
	}

	public void Reload3()
	{
		if (weaponRanged3 != null)
		{
			weaponRanged3.Reload();
		}
	}

	public void Reload4()
	{
		if (weaponRanged4 != null)
		{
			weaponRanged4.Reload();
		}
	}

	public void Reload5()
	{
		if (weaponRanged5 != null)
		{
			weaponRanged5.Reload();
		}
	}

	public void Attack_Explosion()
	{
		if (weaponExplosion != null)
		{
			weaponExplosion.Attack();
		}
	}

	public void PlayAttackParticle(int i)
	{
		if (i == 0 && weaponMelee1 != null)
		{
			weaponMelee1.PlayAttackParticle();
		}
		else if (i == 1 && weaponMelee2 != null)
		{
			weaponMelee2.PlayAttackParticle();
		}
	}

	public void PlayAttackCameraShake()
	{
		if (shakeCamera)
		{
			AllReferencesManager.GAME_CONTROLLER.ShakeCamera(base.transform.position);
		}
	}

	public void ResetEverything()
	{
		if (weaponExplosion != null)
		{
			weaponExplosion.ResetEverything();
		}
	}
}
