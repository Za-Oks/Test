using UnityEngine;

public class Arrow : MonoBehaviour
{
	public float rotateSpeed = 1f;

	public bool isMultiTarget;

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

	private void Awake()
	{
		thisRB = GetComponent<Rigidbody>();
		thisTR = GetComponent<Transform>();
		boxColl = GetComponent<BoxCollider>();
		sphereColl = GetComponent<SphereCollider>();
		gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}

	private void FixedUpdate()
	{
		if (!stoped)
		{
			if (thisRB.position.y <= 0f)
			{
				Stop();
				return;
			}
			thisRB.MovePosition(thisRB.position + thisTR.forward * speed * 0.05f);
			thisRB.MoveRotation(Quaternion.Euler(rotateDir * rotateSpeed * 0.05f) * thisRB.rotation);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			if (!(other.transform != thisParent))
			{
				return;
			}
			temp_bot = AllReferencesManager.GAME_CONTROLLER.GetBotMovement(other.name);
			if (temp_bot != null && !temp_bot.isDead && temp_bot.isPlayer != isPlayer)
			{
				temp_bot.ReceiveDamage(damage, thisParent.position, force);
				if (!isMultiTarget)
				{
					Stop();
				}
			}
		}
		else if (other.CompareTag("Ground"))
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
		EnableCollider(true);
		stoped = false;
	}

	private void Stop()
	{
		stoped = true;
		base.gameObject.SetActive(false);
	}

	private void PutArrowToGround()
	{
		stoped = true;
		EnableCollider(false);
		GroundPosition();
	}

	private void GroundPosition()
	{
		base.transform.position = base.transform.position + base.transform.forward;
	}

	private void PutArrowToShield(Transform trans)
	{
		stoped = true;
		EnableCollider(false);
		ShieldPosition(trans.position);
		thisTR.SetParent(trans, true);
	}

	private void ShieldPosition(Vector3 parentPos)
	{
		float num = Vector3.Distance(base.transform.position, parentPos);
		float num2 = 1f;
		float num3 = num - num2;
		base.transform.position = base.transform.position + base.transform.forward * num3;
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
