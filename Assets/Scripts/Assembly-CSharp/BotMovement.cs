using System;
using UnityEngine;

public class BotMovement : MonoBehaviour
{
	[Serializable]
	public class AttackDistance
	{
		public float hitDitance;
	}

	[Header("REFERENCES")]
	public Transform headCamera;

	[Header("DISTANCES")]
	public float walkDistance = 5f;

	public float hitDistance = 3f;

	public AttackDistance[] attackDistances;

	[Header("ATTACK TIMES")]
	public float timeBetweenAttacks;

	private float lastTime_Attack = -100f;

	private float _timeBetweenAttacks;

	[Header("OTHER")]
	public float rotateSpeed = 10f;

	public bool canFly;

	public bool keepKinematic;

	public bool hasBigCollider;

	public CapsuleCollider bigCollider;

	[Header("DEBUG")]
	public bool invincible;

	public bool stay;

	public bool noDamage;

	private float health = 100f;

	private float m_health;

	private float runSpeed = 10f;

	private float walkSpeed = 5f;

	[HideInInspector]
	public MovementState currentMovementState;

	[HideInInspector]
	public BotInfo thisBotInfo;

	[HideInInspector]
	public BotMovement targetEnemy;

	[HideInInspector]
	public bool isPlayer;

	[HideInInspector]
	public bool isDead;

	[HideInInspector]
	public bool hasCameraOnHead;

	private Vector3 targetEnemyPos;

	private AllReferencesManager referencesManager;

	[Header("DEBUG")]
	public bool die;

	private float minTimeBetween_Fly = 5f;

	private float maxTimeBetween_Fly = 30f;

	private float timeBetween_Fly = 5f;

	private float lastTime_Fly = -100f;

	private float distanceFromTarget;

	private Vector2 playerV2;

	private Vector3 closestPoint;

	public Collider[] thisColliders { get; set; }

	private void Awake()
	{
		lastTime_Attack = -100f;
		lastTime_Fly = -100f;
		stay = false;
		invincible = false;
		noDamage = false;
		thisColliders = GetComponents<Collider>();
		referencesManager = GetComponent<AllReferencesManager>();
		MakeKinematic();
		if (noDamage)
		{
			referencesManager.botAttack.InitDamage(0f);
		}
	}

	private void Start()
	{
		m_health = health;
	}

	private void FixedUpdate()
	{
		if (CanPlay())
		{
			Move();
			if (targetEnemy != null)
			{
				RotateToTarget();
			}
		}
	}

	private void Update()
	{
		if (die)
		{
			die = false;
			Die(referencesManager.thisTR.forward * 5f, referencesManager.GetForce());
		}
		if (!CanPlay())
		{
			if (!isDead)
			{
				currentMovementState = MovementState.IDLE;
				Animate();
			}
			return;
		}
		if (currentMovementState != MovementState.FLY)
		{
			CheckForTarget();
			FindMovementState();
		}
		Animate();
	}

	public void InitHeath(float health)
	{
		this.health = health;
		m_health = health;
	}

	public void InitMovementSpeed(float movementSpeed)
	{
		runSpeed = movementSpeed;
		walkSpeed = movementSpeed / 2f;
	}

	public void InitAttackSpeed(float percent)
	{
		_timeBetweenAttacks = timeBetweenAttacks - timeBetweenAttacks * (percent - 1f);
	}

	private void FindMovementState()
	{
		if (canFly && Time.time - lastTime_Fly >= timeBetween_Fly && AllReferencesManager.GAME_CONTROLLER.GetLivingBots(!isPlayer) >= 10 && (float)AllReferencesManager.GAME_CONTROLLER.GetLivingBots(isPlayer) > 10f && Time.time - lastTime_Fly >= timeBetween_Fly)
		{
			StartFlying();
		}
		else if (targetEnemy != null)
		{
			thisBotInfo.closestEnemy.CalculateDistance();
			distanceFromTarget = thisBotInfo.closestEnemy.distance;
			if (distanceFromTarget <= hitDistance && targetEnemy.currentMovementState != MovementState.FLY)
			{
				if (Time.time - lastTime_Attack >= _timeBetweenAttacks)
				{
					currentMovementState = MovementState.ATTACK;
				}
				else
				{
					currentMovementState = MovementState.IDLE;
				}
			}
			else if (distanceFromTarget <= walkDistance)
			{
				currentMovementState = MovementState.MOVE_FORWARD;
			}
			else
			{
				currentMovementState = MovementState.RUN;
			}
		}
		else if (referencesManager.IsRanged())
		{
			currentMovementState = MovementState.IDLE;
		}
		else if (walkDistance > 0f)
		{
			currentMovementState = MovementState.MOVE_FORWARD;
		}
		else
		{
			currentMovementState = MovementState.RUN;
		}
	}

	private void Move()
	{
		if (!stay)
		{
			referencesManager.Move(currentMovementState);
		}
	}

	private void RotateToTarget()
	{
		if (!stay && currentMovementState != MovementState.FLY)
		{
			referencesManager.RotateToTarget(targetEnemy.referencesManager.thisRB.position);
		}
	}

	public void ReceiveDamage(float damage, Vector3 fromPosition, float force)
	{
		if (CanPlay() && !invincible && currentMovementState != MovementState.FLY)
		{
			m_health -= damage;
			if (m_health <= 0f)
			{
				Die(fromPosition, force);
			}
		}
	}

	public void Die(Vector3 fromPosition, float force)
	{
		if (!isDead)
		{
			isDead = true;
			if (hasCameraOnHead)
			{
				hasCameraOnHead = false;
			}
			currentMovementState = MovementState.DEAD;
			referencesManager.Animate();
			referencesManager.botAttack.Reset_Melee();
			AllReferencesManager.GAME_CONTROLLER.BotDied(thisBotInfo);
			for (int i = 0; i < thisColliders.Length; i++)
			{
				thisColliders[i].enabled = false;
			}
			MakeKinematic();
			referencesManager.Die(fromPosition, force);
		}
	}

	public void Begin()
	{
		referencesManager.InitializeWeapons();
		referencesManager.InitiazeMovement(referencesManager.thisRB, referencesManager.thisTR, runSpeed, walkSpeed, rotateSpeed);
		lastTime_Fly = Time.time;
		timeBetween_Fly = UnityEngine.Random.Range(0f, 10f);
	}

	public void ResetEverything()
	{
		ResetVariables();
		referencesManager.ResetEverything();
	}

	public void ResetVariables()
	{
		for (int i = 0; i < thisColliders.Length; i++)
		{
			thisColliders[i].enabled = true;
		}
		if (!keepKinematic)
		{
			referencesManager.thisRB.isKinematic = false;
		}
		referencesManager.thisRB.velocity = Vector3.zero;
		referencesManager.thisRB.angularVelocity = Vector3.zero;
		hasCameraOnHead = false;
		isDead = false;
		targetEnemy = null;
		m_health = health;
		currentMovementState = MovementState.IDLE;
	}

	public void MakeKinematic()
	{
		referencesManager.thisRB.isKinematic = true;
		referencesManager.thisRB.velocity = Vector3.zero;
		referencesManager.thisRB.angularVelocity = Vector3.zero;
	}

	public void StartFlying()
	{
		currentMovementState = MovementState.FLY;
		referencesManager.StartFlying();
	}

	public void StopFlying()
	{
		currentMovementState = MovementState.IDLE;
		lastTime_Fly = Time.time;
		timeBetween_Fly = UnityEngine.Random.Range(minTimeBetween_Fly, maxTimeBetween_Fly);
	}

	private void CheckForTarget()
	{
		BotInfo nearestTarget = AllReferencesManager.GAME_CONTROLLER.GetNearestTarget(base.transform.position, thisBotInfo);
		if (nearestTarget == null)
		{
			targetEnemy = null;
		}
		else
		{
			targetEnemy = nearestTarget.referencesManager.botMovement;
		}
	}

	private void Animate()
	{
		referencesManager.Animate();
	}

	public bool CanPutArrow()
	{
		return false;
	}

	public bool CanPlay()
	{
		return (AllReferencesManager.GAME_CONTROLLER.canPlay || currentMovementState == MovementState.FLY) && !isDead;
	}

	public Vector2 GetPosition(Vector3 enemyPos)
	{
		if (!hasBigCollider)
		{
			playerV2.Set(referencesManager.thisTR.position.x, referencesManager.thisTR.position.z);
		}
		else
		{
			enemyPos.y = bigCollider.center.y * referencesManager.thisTR.localScale.y;
			closestPoint = bigCollider.ClosestPointOnBounds(enemyPos);
			playerV2.Set(closestPoint.x, closestPoint.z);
		}
		return playerV2;
	}

	public void Attacked()
	{
		lastTime_Attack = Time.time;
	}

	public int GetAttackID()
	{
		if (attackDistances.Length == 0)
		{
			return -1;
		}
		int result = 0;
		int num = attackDistances.Length;
		for (int i = 0; i < num; i++)
		{
			if (distanceFromTarget <= attackDistances[i].hitDitance)
			{
				result = i + 1;
			}
		}
		return result;
	}
}
