using System.Collections;
using UnityEngine;

public class WeaponExplosion : MonoBehaviour
{
	public float explosionRadius = 5f;

	public float explosionWait;

	public int attackSoundID = -1;

	public LayerMask bot1Layer;

	public LayerMask bot2Layer;

	public ParticleSystem explosionParticle;

	public ParticleSystem explosionParticlePlayer;

	public GameObject bombParent;

	public int penetration = 15;

	public bool dieFromExplosion = true;

	private bool exploded;

	private float damage;

	private float force;

	private bool isPlayer;

	private AllReferencesManager referencesManager;

	private BotMovement temp_bot;

	private Collider[] explosionColliders;

	private Transform parent;

	private void Awake()
	{
		referencesManager = GetComponent<AllReferencesManager>();
	}

	public void InitStats(float damage, float force)
	{
		this.damage = damage;
		this.force = force;
		explosionColliders = new Collider[penetration];
	}

	public void InitBot(AllReferencesManager referencesManager, Transform parent, bool isPlayer)
	{
		this.referencesManager = referencesManager;
		this.parent = parent;
		this.isPlayer = isPlayer;
		if (bombParent != null)
		{
			bombParent.SetActive(true);
		}
		exploded = false;
	}

	public void StopEverything()
	{
		StopCoroutine("Explode_CR");
		if (!exploded)
		{
			Explode();
		}
	}

	public void ResetEverything()
	{
		if (bombParent != null)
		{
			bombParent.SetActive(true);
		}
	}

	public void Attack()
	{
		if (!exploded)
		{
			StartCoroutine("Explode_CR");
		}
	}

	private IEnumerator Explode_CR()
	{
		yield return new WaitForSeconds(explosionWait);
		Explode();
	}

	private void Explode()
	{
		if (dieFromExplosion)
		{
			exploded = true;
		}
		int num = Physics.OverlapSphereNonAlloc(base.transform.position, explosionRadius, explosionColliders, (!isPlayer) ? bot1Layer : bot2Layer);
		for (int i = 0; i < num; i++)
		{
			Collider collider = explosionColliders[i];
			if (collider.CompareTag("Player"))
			{
				temp_bot = AllReferencesManager.GAME_CONTROLLER.GetBotMovement(collider.name);
				if (temp_bot != null && !temp_bot.isDead)
				{
					temp_bot.ReceiveDamage(damage, base.transform.position, force);
				}
			}
		}
		if (dieFromExplosion && !referencesManager.botMovement.isDead)
		{
			referencesManager.botMovement.Die(base.transform.position + base.transform.forward, force);
		}
		referencesManager.PlayAttackCameraShake();
		if (attackSoundID == -1)
		{
			referencesManager.PlayAttackAudio();
		}
		else
		{
			referencesManager.PlayAttackAudio(attackSoundID);
		}
		if (isPlayer && explosionParticlePlayer != null)
		{
			explosionParticlePlayer.Play();
		}
		else
		{
			explosionParticle.Play();
		}
		if (bombParent != null)
		{
			bombParent.SetActive(false);
		}
	}
}
