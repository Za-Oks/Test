using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float rotateSpeed = 1f;

	private float force = 5f;

	private float damage = 10f;

	private float speed = 1f;

	private bool isPlayer;

	private Transform thisTR;

	private Vector3 rotateDir;

	private Transform thisParent;

	private BoxCollider thisCol;

	private BotMovement temp_bot;

	private GameController gc;

	private bool stoped = true;

	private void Awake()
	{
		thisTR = GetComponent<Transform>();
		thisCol = GetComponent<BoxCollider>();
		gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}

	private void Update()
	{
		if (!stoped)
		{
			if (thisTR.position.y <= 0f)
			{
				Stop();
				return;
			}
			thisTR.position += thisTR.forward * speed * Time.deltaTime;
			thisTR.Rotate(rotateDir * rotateSpeed * Time.deltaTime, Space.World);
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
					temp_bot.ReceiveDamage(damage, thisParent.position, force);
					Stop();
				}
			}
		}
		else if (other.CompareTag("Ground") || other.CompareTag("Barrier"))
		{
			Stop();
		}
	}

	public void InitStats(float damage, float speed, float force)
	{
		this.damage = damage;
		this.speed = speed;
		this.force = force;
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
		thisCol.enabled = true;
		stoped = false;
	}

	private void Stop()
	{
		stoped = true;
		base.gameObject.SetActive(false);
	}

	public void ResetEverything()
	{
		Stop();
	}
}
