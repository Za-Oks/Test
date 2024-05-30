using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Bullet_Cannon : MonoBehaviour
{
	public bool canBeGrounded;

	private float force = 5f;

	private float damage = 10f;

	private float speed = 1f;

	private bool isPlayer;

	private Transform thisParent;

	private SphereCollider thisSphereCol;

	private BoxCollider thisBocCol;

	private Quaternion startRot;

	private Transform thisTR;

	private Rigidbody thisRB;

	private BotMovement temp_bot;

	private GameController gc;

	public bool dontRotate;

	public bool grounded;

	private bool stoped = true;

	public float delayStopTime = 1f;

	private bool delayStopStarted;

	private void Awake()
	{
		thisRB = GetComponent<Rigidbody>();
		thisTR = GetComponent<Transform>();
		thisSphereCol = GetComponent<SphereCollider>();
		thisBocCol = GetComponent<BoxCollider>();
		gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}

	private void Update()
	{
		if (!stoped && !delayStopStarted && (thisTR.position.y < -50f || thisRB.velocity.y <= 0.1f))
		{
			StartCoroutine("DelayStop");
		}
	}

	private void FixedUpdate()
	{
		if (dontRotate)
		{
			thisRB.MoveRotation(startRot);
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		if (grounded)
		{
			return;
		}
		if (other.gameObject.CompareTag("Player"))
		{
			temp_bot = AllReferencesManager.GAME_CONTROLLER.GetBotMovement(other.gameObject.name);
			if (temp_bot != null && other.transform != thisParent && !temp_bot.isDead && temp_bot.isPlayer != isPlayer)
			{
				temp_bot.ReceiveDamage(damage, thisParent.position, force);
			}
		}
		else if (other.gameObject.CompareTag("Ground") && !delayStopStarted)
		{
			StartCoroutine("DelayStop");
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
		startRot = thisRB.rotation;
		delayStopStarted = false;
		grounded = false;
		if (thisSphereCol != null)
		{
			thisSphereCol.enabled = true;
		}
		else if (thisBocCol != null)
		{
			thisBocCol.enabled = true;
		}
		stoped = false;
		thisRB.velocity = thisTR.forward * speed;
	}

	private void Stop()
	{
		StopCoroutine("DelayStop");
		grounded = true;
		stoped = true;
		thisTR.DOScale(0f, 0.5f).OnComplete(delegate
		{
			base.gameObject.SetActive(false);
		}).SetDelay(2f)
			.SetEase(Ease.Linear);
	}

	private IEnumerator DelayStop()
	{
		delayStopStarted = true;
		yield return new WaitForSeconds(delayStopTime);
		Stop();
	}

	public void ResetEverything()
	{
		delayStopStarted = false;
		grounded = false;
		stoped = true;
		thisTR.DOKill();
		base.gameObject.SetActive(false);
	}
}
