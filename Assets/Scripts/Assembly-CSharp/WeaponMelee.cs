using UnityEngine;

public class WeaponMelee : MonoBehaviour
{
	[Header("NOT COMMON")]
	public int attackSoundID = -1;

	private float damage = 10f;

	private int penetration = 1;

	private float force = 5f;

	private bool isPlayer;

	private int countPene;

	private Transform parent;

	private Collider coll;

	private GameController gc;

	private AllReferencesManager referencesManager;

	private WeaponSideEffects weaponSideEffects;

	private BotMovement temp_bot;

	private AttackParticleType attackParticleType;

	private bool playAudioFromAnimationEvents;

	private void Awake()
	{
		gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		coll = GetComponent<BoxCollider>();
		if (coll == null)
		{
			coll = GetComponent<SphereCollider>();
		}
		weaponSideEffects = GetComponent<WeaponSideEffects>();
	}

	private void Start()
	{
		if (coll != null)
		{
			coll.enabled = false;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!other.CompareTag("Player") || !(other.transform != parent))
		{
			return;
		}
		temp_bot = AllReferencesManager.GAME_CONTROLLER.GetBotMovement(other.name);
		if (temp_bot != null && isPlayer != temp_bot.isPlayer && !temp_bot.isDead)
		{
			temp_bot.ReceiveDamage(damage, parent.position, force);
			if (++countPene >= penetration)
			{
				ResetWeapon();
			}
			if (!playAudioFromAnimationEvents)
			{
				PlayAttackAudio();
			}
		}
	}

	public void InitStats(float damage, int penetration, float force)
	{
		this.damage = damage;
		this.penetration = penetration;
		this.force = force;
		if (weaponSideEffects != null)
		{
			weaponSideEffects.InitStats(damage, force);
		}
	}

	public void InitBot(AllReferencesManager referencesManager, Transform parent, bool isPlayer, AttackParticleType particleType, bool playAudioFromAnimationEvents)
	{
		this.referencesManager = referencesManager;
		this.parent = parent;
		this.isPlayer = isPlayer;
		attackParticleType = particleType;
		this.playAudioFromAnimationEvents = playAudioFromAnimationEvents;
		if (weaponSideEffects != null)
		{
			weaponSideEffects.InitBot(referencesManager, isPlayer);
		}
	}

	public void Attack()
	{
		countPene = 0;
		if (coll != null)
		{
			coll.enabled = true;
		}
		if (weaponSideEffects != null)
		{
			weaponSideEffects.DoSideEffects_OnStart();
		}
	}

	public void ResetWeapon()
	{
		if (coll != null)
		{
			coll.enabled = false;
		}
	}

	public void PlayAttackParticle()
	{
		if (attackParticleType == AttackParticleType.GIANT)
		{
			Vector3 position = base.transform.position;
			position.y = 0.2f;
			AllReferencesManager.PARTICLES_MANAGER.PlayGiantParticle(position);
		}
	}

	public void PlayAttackAudio()
	{
		if (attackSoundID == -1)
		{
			referencesManager.PlayAttackAudio();
		}
		else
		{
			referencesManager.PlayAttackAudio(attackSoundID);
		}
	}
}
