using System.Collections;
using UnityEngine;

public class Arrow_Bomb : MonoBehaviour
{
	public float rotateSpeed = 1f;

	public float explosionRadius = 5f;

	public float explosionTime = 0.5f;

	public LayerMask bot1Layer;

	public LayerMask bot2Layer;

	public ParticleSystem explosionParticle;

	public GameObject bombParent;

	public AudioBattleType audioType;

	private bool exploded;

	private float force = 5f;

	private float damage = 10f;

	private float speed = 1f;

	private bool isPlayer;

	private Rigidbody thisRB;

	private Transform thisTR;

	private Vector3 rotateDir;

	private Transform thisParent;

	private BoxCollider boxColl;

	private SphereCollider sphereColl;

	private BotMovement temp_bot;

	private GameController gc;

	private bool stoped = true;

	private Collider[] explosionColliders = new Collider[15];

	private WaitForSeconds wait_sec;

	private void Awake()
	{
		thisRB = GetComponent<Rigidbody>();
		thisTR = GetComponent<Transform>();
		boxColl = GetComponent<BoxCollider>();
		sphereColl = GetComponent<SphereCollider>();
		gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		wait_sec = new WaitForSeconds(explosionTime);
	}

	private void Update()
	{
		if (!stoped)
		{
			if (thisTR.position.y <= 0f)
			{
				Explode();
				return;
			}
			thisTR.Translate(Vector3.forward * speed * Time.deltaTime);
			thisTR.rotation = Quaternion.Euler(rotateDir * rotateSpeed * Time.deltaTime) * thisTR.rotation;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			if (other.transform != thisParent)
			{
				temp_bot = AllReferencesManager.GAME_CONTROLLER.GetBotMovement(other.name);
				if (temp_bot != null && !temp_bot.isDead && temp_bot.isPlayer != isPlayer)
				{
					Explode();
				}
			}
		}
		else if (other.CompareTag("Ground"))
		{
			Explode();
		}
	}

	private void Explode()
	{
		if (exploded)
		{
			return;
		}
		exploded = true;
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
		explosionParticle.Play();
		if (audioType == AudioBattleType.FIRE_EXPLOSION)
		{
			AllReferencesManager.AUDIO_BATTLE_MANAGER.PlayAudio_FireExplosion(base.transform.position);
		}
		if (bombParent != null)
		{
			bombParent.SetActive(false);
		}
		stoped = true;
		StartCoroutine("DelayStop");
	}

	private IEnumerator DelayStop()
	{
		yield return wait_sec;
		Stop();
	}

	public void InitStats(float damage, float speed, float force, int penetration)
	{
		this.damage = damage;
		this.speed = speed;
		this.force = force;
		explosionColliders = new Collider[penetration];
	}

	public void InitBot(Transform parent, bool isPlayer)
	{
		thisParent = parent;
		this.isPlayer = isPlayer;
		if (isPlayer)
		{
			base.gameObject.layer = LayerMask.NameToLayer("Weapon2");
		}
		else
		{
			base.gameObject.layer = LayerMask.NameToLayer("Weapon");
		}
	}

	public void Begin()
	{
		rotateDir = -Vector3.Cross(thisTR.forward, Vector3.up).normalized;
		EnableCollider(true);
		exploded = false;
		stoped = false;
		if (bombParent != null)
		{
			bombParent.SetActive(true);
		}
	}

	private void Stop()
	{
		StopCoroutine("DelayStop");
		base.gameObject.SetActive(false);
	}

	private void EnableCollider(bool enable)
	{
		if (boxColl != null)
		{
			boxColl.enabled = enable;
		}
		else if (sphereColl != null)
		{
			sphereColl.enabled = enable;
		}
	}

	public void ResetEverything()
	{
		Stop();
	}
}
