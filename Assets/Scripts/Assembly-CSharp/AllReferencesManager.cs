using UnityEngine;

public class AllReferencesManager : MonoBehaviour
{
	[Header("FLY")]
	public DetectGroundTarget detectGroundTarget;

	public GameObject fly_shadow;

	[Header("NOT COMMON")]
	public WeaponSideEffects weaponSideEffects;

	public Transform rotateTransform;

	public Transform[] secondaryRotateTransforms;

	[HideInInspector]
	public BotMovement botMovement;

	[HideInInspector]
	public HumanMovement humanMovement;

	[HideInInspector]
	public HorseMovement horseMovement;

	[HideInInspector]
	public ChariotMovement chariotMovement;

	[HideInInspector]
	public BotAttack botAttack;

	[HideInInspector]
	public BotAnimation botAnimation;

	[HideInInspector]
	public BotAnimation_Machine botAnimationMachine;

	[HideInInspector]
	public BotAnimation_Chariot botAnimationChariot;

	[HideInInspector]
	public BotRagdollDeath[] botRagdollDeath;

	[HideInInspector]
	public BotAudio botAudio;

	[HideInInspector]
	public BotCrash botCrash;

	[HideInInspector]
	public BotFly botFly;

	[HideInInspector]
	public Transform thisTR;

	[HideInInspector]
	public Rigidbody thisRB;

	public static GameController GAME_CONTROLLER;

	public static CrossPoolManager CROSS_POOL_MANAGER;

	public static Particles_Manager PARTICLES_MANAGER;

	public static Audio_Manager AUDIO_MANAGER;

	public static AudioBattleManager AUDIO_BATTLE_MANAGER;

	public static Ui_Manager UI_MANAGER;

	[HideInInspector]
	public int level = 1;

	private void Awake()
	{
		thisTR = GetComponent<Transform>();
		thisRB = GetComponent<Rigidbody>();
		botMovement = GetComponent<BotMovement>();
		humanMovement = GetComponent<HumanMovement>();
		horseMovement = GetComponent<HorseMovement>();
		chariotMovement = GetComponent<ChariotMovement>();
		botAttack = GetComponent<BotAttack>();
		botAnimation = GetComponent<BotAnimation>();
		botAnimationMachine = GetComponent<BotAnimation_Machine>();
		botAnimationChariot = GetComponent<BotAnimation_Chariot>();
		botRagdollDeath = GetComponents<BotRagdollDeath>();
		botAudio = GetComponent<BotAudio>();
		botCrash = GetComponent<BotCrash>();
		botFly = GetComponent<BotFly>();
		if (GAME_CONTROLLER == null)
		{
			GAME_CONTROLLER = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		}
		if (CROSS_POOL_MANAGER == null)
		{
			CROSS_POOL_MANAGER = GameObject.FindGameObjectWithTag("CrossPoolManager").GetComponent<CrossPoolManager>();
		}
		if (PARTICLES_MANAGER == null)
		{
			PARTICLES_MANAGER = GameObject.FindWithTag("Particles_Manager").GetComponent<Particles_Manager>();
		}
		if (PARTICLES_MANAGER == null)
		{
			AUDIO_MANAGER = GameObject.FindWithTag("Audio_Manager").GetComponent<Audio_Manager>();
		}
		if (AUDIO_BATTLE_MANAGER == null)
		{
			AUDIO_BATTLE_MANAGER = GameObject.FindWithTag("AudioBattleManager").GetComponent<AudioBattleManager>();
		}
		if (UI_MANAGER == null)
		{
			UI_MANAGER = GameObject.FindWithTag("Ui_Manager").GetComponent<Ui_Manager>();
		}
	}

	public void InitializeStats(StatsFinal stats, int level)
	{
		this.level = level;
		botMovement.InitHeath(stats.healthFinal);
		botMovement.InitMovementSpeed(stats.movementSpeedFinal);
		botMovement.InitAttackSpeed(stats.attackSpeedMultiplierFinal);
		if (!botMovement.noDamage)
		{
			botAttack.InitDamage(stats.damageFinal);
		}
		if (botAnimation != null)
		{
			botAnimation.InitializeAtackSpeed(stats.attackSpeedMultiplierFinal);
			botAnimation.InitializeMovementSpeed(stats.movementSpeedMultiplierFinal);
		}
		else if (botAnimationMachine != null)
		{
			botAnimationMachine.InitializeAtackSpeed(stats.attackSpeedMultiplierFinal);
			botAnimationMachine.InitializeMovementSpeed(stats.movementSpeedMultiplierFinal);
		}
		else if (botAnimationChariot != null)
		{
			botAnimationChariot.InitializeAtackSpeed(stats.attackSpeedMultiplierFinal);
			botAnimationChariot.InitializeMovementSpeed(stats.movementSpeedMultiplierFinal);
		}
	}

	public void InitializeWeapons()
	{
		botAttack.InitWeapons(botMovement.isPlayer);
		if (botCrash != null)
		{
			botCrash.InitBot(this, base.transform, botMovement.isPlayer);
		}
	}

	public void InitiazeMovement(Rigidbody thisRB, Transform thisTR, float runSpeed, float walkSpeed, float rotateSpeed)
	{
		if (humanMovement != null)
		{
			humanMovement.Initialize(thisRB, thisTR, runSpeed, walkSpeed, rotateSpeed, rotateTransform);
		}
		if (horseMovement != null)
		{
			horseMovement.Initialize(thisRB, thisTR, runSpeed, walkSpeed, rotateSpeed, rotateTransform);
		}
		if (chariotMovement != null)
		{
			chariotMovement.Initialize(thisRB, thisTR, runSpeed, walkSpeed, rotateSpeed, rotateTransform, secondaryRotateTransforms);
		}
	}

	public void Move(MovementState movementState)
	{
		if (humanMovement != null)
		{
			humanMovement.Move(movementState);
		}
		else if (horseMovement != null)
		{
			horseMovement.Move(movementState);
		}
		else if (chariotMovement != null)
		{
			chariotMovement.Move(movementState);
		}
	}

	public void RotateToTarget(Vector3 position)
	{
		if (humanMovement != null)
		{
			humanMovement.RotateToTarget(position);
		}
		else if (horseMovement != null)
		{
			horseMovement.RotateToTarget(position);
		}
		else if (chariotMovement != null)
		{
			chariotMovement.RotateToTarget(position);
		}
	}

	public void Animate()
	{
		if (botAnimation != null)
		{
			botAnimation.Animate();
		}
		else if (botAnimationMachine != null)
		{
			botAnimationMachine.Animate();
		}
		else if (botAnimationChariot != null)
		{
			botAnimationChariot.Animate();
		}
	}

	public void Die(Vector3 fromPosition, float force)
	{
		for (int i = 0; i < botRagdollDeath.Length; i++)
		{
			botRagdollDeath[i].Die(fromPosition, force);
		}
		StopWeaponSideEffects();
		if (botAttack.weaponExplosion != null)
		{
			botAttack.weaponExplosion.StopEverything();
		}
	}

	public void ResetEverything()
	{
		for (int i = 0; i < botRagdollDeath.Length; i++)
		{
			botRagdollDeath[i].ResetEverything();
		}
		ResetAnimator();
		if (botFly != null)
		{
			botFly.ResetEverything();
		}
		if (botAttack != null)
		{
			botAttack.ResetEverything();
		}
		if (humanMovement != null)
		{
			humanMovement.ResetEverything();
		}
		else if (horseMovement != null)
		{
			horseMovement.ResetEverything();
		}
		else if (chariotMovement != null)
		{
			chariotMovement.ResetEverything();
		}
	}

	public void DisableAnimators()
	{
		if (botAnimationMachine != null)
		{
			botAnimationMachine.DisableAnimators();
		}
		else if (botAnimationChariot != null)
		{
			botAnimationChariot.DisableAnimators();
		}
	}

	public void ResetAnimator()
	{
		if (botAnimation != null)
		{
			botAnimation.ResetEverything();
		}
		else if (botAnimationMachine != null)
		{
			botAnimationMachine.ResetEverything();
		}
		else if (botAnimationChariot != null)
		{
			botAnimationChariot.ResetEverything();
		}
	}

	public void PlayAttackParticle(int i)
	{
		botAttack.PlayAttackParticle(i);
	}

	public void PlayAttackAudio()
	{
		botAudio.PlayAudioAttack();
	}

	public void PlayAttackAudio(int id)
	{
		botAudio.PlayAudioAttack(id);
	}

	public void PlayAttackCameraShake()
	{
		botAttack.PlayAttackCameraShake();
	}

	public void StopWeaponSideEffects()
	{
		if (weaponSideEffects != null)
		{
			weaponSideEffects.StopEverything();
		}
	}

	public void StartFlying()
	{
		botFly.Begin();
	}

	public void FlyBegan()
	{
		detectGroundTarget.Begin(botMovement.isPlayer);
		botMovement.thisColliders[0].enabled = false;
		fly_shadow.SetActive(false);
	}

	public void FlyEnded()
	{
		detectGroundTarget.Stop();
		botMovement.StopFlying();
		botMovement.thisColliders[0].enabled = true;
		fly_shadow.SetActive(true);
	}

	public void Attacked()
	{
		botMovement.Attacked();
	}

	public void Crashed()
	{
		if (chariotMovement != null)
		{
			chariotMovement.Attacked();
		}
	}

	public bool IsRanged()
	{
		return chariotMovement == null && botAttack.weaponRanged != null;
	}

	public bool IsStationary()
	{
		if (humanMovement == null)
		{
			return false;
		}
		return humanMovement.isStationary;
	}

	public bool IsPlayer()
	{
		return botMovement.isPlayer;
	}

	public BotMovement GetTargetEnemy()
	{
		return botMovement.targetEnemy;
	}

	public float GetForce()
	{
		return botAttack.force;
	}

	public void PauseWeaponEffectAudio(bool pause)
	{
		if (weaponSideEffects != null)
		{
			weaponSideEffects.PauseSound(pause);
		}
	}
}
