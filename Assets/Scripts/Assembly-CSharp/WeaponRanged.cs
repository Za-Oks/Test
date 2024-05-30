using System.Collections;
using UnityEngine;

public class WeaponRanged : MonoBehaviour
{
	public enum RangedWeaponType
	{
		BOW = 0,
		CANNON = 1,
		CATAPULT = 2,
		GUN = 3,
		SENTINEL = 4,
		CROSSBOW = 5,
		BALLISTA = 6,
		SHURIKEN = 7,
		MACHINE_GUN = 8,
		FIRE_BALL = 9,
		HWACHA = 10,
		ORGAN_GUN = 11,
		JAVELLIN = 12
	}

	[Header("REFERENCES")]
	public Transform projectileStartTransform_Level1;

	public Transform projectileStartTransform_Level2;

	public Transform projectileStartTransform_Level3;

	private Transform startPos;

	[Header("VALUES")]
	public RangedWeaponType rangedWeaponType;

	public float minDist = 20f;

	public float maxDist = 30f;

	public bool shouldChangeSpeed = true;

	public float minSpeed = 20f;

	public float maxSpeed = 40f;

	public bool shouldRotate;

	public float minRot = -10f;

	public float maxRot = -25f;

	public bool shouldFollowWeaponRotation;

	[Header("NOT COMMON")]
	public Transform particleTransform;

	public Transform reloadTransform;

	public float[] mutlipleAttacks_RotY;

	public bool muteAudioAndShake;

	[Header("MULTI PROJECTILE START")]
	public bool hasMultiStartTransforms;

	public float timeBetweenShoot = 0.1f;

	public Transform[] multiProjectileStartTransforms;

	private int nextMultiProjectileIndex;

	private float damage = 10f;

	private float force = 5f;

	private int level = 1;

	private int penetration = 1;

	private Transform parent;

	private Quaternion tempRot;

	private Vector3 tempDir;

	private BasicPoolManager poolManager_ArrowBow;

	private BasicPoolManager poolManager_BulletCannon;

	private BasicPoolManager poolManager_BulletCatapult;

	private BasicPoolManager poolManager_Bullet;

	private BasicPoolManager poolManager_ArrowSentinelBlue;

	private BasicPoolManager poolManager_ArrowSentinelRed;

	private BasicPoolManager poolManager_ArrowCrossbow;

	private BasicPoolManager poolManager_SpearBallistaRed;

	private BasicPoolManager poolManager_SpearBallistaBlue;

	private BasicPoolManager poolManager_Shuriken;

	private BasicPoolManager poolManager_FireballBlue;

	private BasicPoolManager poolManager_FireballRed;

	private BasicPoolManager poolManager_Jevallin;

	private MultiProjectilePoolManager poolManager_Hwacha;

	private Particles_Manager particles_Manager;

	private Arrow arrow;

	private Arrow_Bomb arrow_bomb;

	private Bullet bullet;

	private Bullet_Cannon bulletCannon;

	private WeaponSideEffects weaponSideEffects;

	private float speed;

	private float t;

	private bool isPlayer;

	private Vector3 target;

	private void Awake()
	{
		poolManager_ArrowBow = GameObject.FindWithTag("PoolManager_ArrowBow").GetComponent<BasicPoolManager>();
		poolManager_BulletCannon = GameObject.FindWithTag("PoolManager_BulletCannon").GetComponent<BasicPoolManager>();
		poolManager_BulletCatapult = GameObject.FindWithTag("PoolManager_BulletCatapult").GetComponent<BasicPoolManager>();
		poolManager_Bullet = GameObject.FindWithTag("PoolManager_BulletPistol").GetComponent<BasicPoolManager>();
		poolManager_ArrowSentinelBlue = GameObject.FindWithTag("PoolManager_ArrowSentinelBlue").GetComponent<BasicPoolManager>();
		poolManager_ArrowSentinelRed = GameObject.FindWithTag("PoolManager_ArrowSentinelRed").GetComponent<BasicPoolManager>();
		poolManager_ArrowCrossbow = GameObject.FindWithTag("PoolManager_ArrowCrossbow").GetComponent<BasicPoolManager>();
		poolManager_SpearBallistaRed = GameObject.FindWithTag("PoolManager_SpearBallistaRed").GetComponent<BasicPoolManager>();
		poolManager_SpearBallistaBlue = GameObject.FindWithTag("PoolManager_SpearBallistaBlue").GetComponent<BasicPoolManager>();
		poolManager_Shuriken = GameObject.FindWithTag("PoolManager_Shuriken").GetComponent<BasicPoolManager>();
		poolManager_FireballBlue = GameObject.FindWithTag("PoolManager_FireballBlue").GetComponent<BasicPoolManager>();
		poolManager_FireballRed = GameObject.FindWithTag("PoolManager_FireballRed").GetComponent<BasicPoolManager>();
		poolManager_Hwacha = GameObject.FindWithTag("PoolManager_Hwacha").GetComponent<MultiProjectilePoolManager>();
		poolManager_Jevallin = GameObject.FindWithTag("PoolManager_Javellin").GetComponent<BasicPoolManager>();
		particles_Manager = GameObject.FindWithTag("Particles_Manager").GetComponent<Particles_Manager>();
		weaponSideEffects = GetComponent<WeaponSideEffects>();
	}

	private void OnEnable()
	{
		if (projectileStartTransform_Level1 != null)
		{
			projectileStartTransform_Level1.gameObject.SetActive(false);
		}
		if (projectileStartTransform_Level2 != null)
		{
			projectileStartTransform_Level2.gameObject.SetActive(false);
		}
		if (projectileStartTransform_Level3 != null)
		{
			projectileStartTransform_Level3.gameObject.SetActive(false);
		}
		if (reloadTransform != null && rangedWeaponType != RangedWeaponType.JAVELLIN)
		{
			reloadTransform.gameObject.SetActive(false);
		}
	}

	public void InitBot(Transform parent, bool isPlayer)
	{
		this.parent = parent;
		this.isPlayer = isPlayer;
	}

	public void InitStats(float damage, float force, int level)
	{
		this.damage = damage;
		this.force = force;
		this.level = level;
		InitStartPos();
	}

	public void InitStats(float damage, float force, int level, int penetration)
	{
		this.penetration = penetration;
		InitStats(damage, force, level);
	}

	private void InitStartPos()
	{
		if (hasMultiStartTransforms)
		{
			startPos = multiProjectileStartTransforms[0];
		}
		else if (level == 2 && projectileStartTransform_Level2 != null)
		{
			startPos = projectileStartTransform_Level2;
			projectileStartTransform_Level2.gameObject.SetActive(true);
			projectileStartTransform_Level1.gameObject.SetActive(false);
			if (projectileStartTransform_Level3 != null)
			{
				projectileStartTransform_Level3.gameObject.SetActive(false);
			}
		}
		else if (level == 3 && projectileStartTransform_Level3 != null)
		{
			startPos = projectileStartTransform_Level3;
			projectileStartTransform_Level3.gameObject.SetActive(true);
			projectileStartTransform_Level1.gameObject.SetActive(false);
			if (projectileStartTransform_Level2 != null)
			{
				projectileStartTransform_Level2.gameObject.SetActive(false);
			}
		}
		else
		{
			startPos = projectileStartTransform_Level1;
			projectileStartTransform_Level1.gameObject.SetActive(true);
			if (projectileStartTransform_Level2 != null)
			{
				projectileStartTransform_Level2.gameObject.SetActive(false);
			}
			if (projectileStartTransform_Level3 != null)
			{
				projectileStartTransform_Level3.gameObject.SetActive(false);
			}
		}
		if (particleTransform == null)
		{
			particleTransform = startPos;
		}
		if (reloadTransform == null)
		{
			reloadTransform = startPos;
		}
		if (reloadTransform != null && rangedWeaponType != RangedWeaponType.JAVELLIN)
		{
			reloadTransform.gameObject.SetActive(false);
		}
	}

	private IEnumerator MutliProjectileAttack_CR()
	{
		WaitForSeconds wait = new WaitForSeconds(timeBetweenShoot);
		int length = multiProjectileStartTransforms.Length;
		for (int i = 0; i < length; i++)
		{
			nextMultiProjectileIndex = i;
			multiProjectileStartTransforms[i].gameObject.SetActive(false);
			AttackLogic();
			yield return wait;
		}
	}

	public void Attack(Vector3 target)
	{
		this.target = target;
		if (!hasMultiStartTransforms)
		{
			AttackLogic();
			return;
		}
		if (rangedWeaponType == RangedWeaponType.HWACHA)
		{
			particles_Manager.PlayHwachaParticle(particleTransform.position);
		}
		StartCoroutine("MutliProjectileAttack_CR");
	}

	private void AttackLogic()
	{
		if (mutlipleAttacks_RotY.Length == 0)
		{
			ThroughProjectile(0f);
			return;
		}
		for (int i = 0; i < mutlipleAttacks_RotY.Length; i++)
		{
			ThroughProjectile(mutlipleAttacks_RotY[i]);
		}
	}

	public void StraightAttack()
	{
		target = startPos.position + startPos.forward * maxDist;
		ThroughProjectile(0f);
	}

	private void ThroughProjectile(float yAngle)
	{
		if (weaponSideEffects != null)
		{
			weaponSideEffects.DoSideEffects_OnStart();
		}
		if (!shouldFollowWeaponRotation)
		{
			tempDir = target - startPos.position;
			tempRot = Quaternion.LookRotation(tempDir, Vector3.up);
			if (shouldChangeSpeed || shouldRotate)
			{
				float num = Vector3.Distance(base.transform.position, target);
				t = (num - minDist) / (maxDist - minDist);
				if (shouldChangeSpeed)
				{
					speed = Mathf.Lerp(minSpeed, maxSpeed, t);
				}
				if (shouldRotate)
				{
					tempRot = Quaternion.Euler(new Vector3(Mathf.Lerp(minRot, maxRot, t), tempRot.eulerAngles.y + yAngle, tempRot.eulerAngles.z));
				}
			}
			if (!shouldChangeSpeed)
			{
				speed = maxSpeed;
			}
			if (!shouldRotate)
			{
				tempRot = Quaternion.Euler(new Vector3(0f, tempRot.eulerAngles.y + yAngle, tempRot.eulerAngles.z));
			}
		}
		else
		{
			if (shouldChangeSpeed)
			{
				float num2 = Vector3.Distance(base.transform.position, target);
				t = (num2 - minDist) / (maxDist - minDist);
				speed = Mathf.Lerp(minSpeed, maxSpeed, t);
			}
			else
			{
				speed = maxSpeed;
			}
			tempRot = startPos.rotation;
		}
		arrow = null;
		arrow_bomb = null;
		bullet = null;
		bulletCannon = null;
		PoolProjectile();
		if (arrow != null)
		{
			arrow.InitStats(damage, speed, force);
			arrow.InitBot(parent, isPlayer);
			arrow.Begin();
		}
		else if (arrow_bomb != null)
		{
			arrow_bomb.InitStats(damage, speed, force, penetration);
			arrow_bomb.InitBot(parent, isPlayer);
			arrow_bomb.Begin();
		}
		else if (bullet != null)
		{
			bullet.InitStats(damage, speed, force);
			bullet.InitBot(parent, isPlayer);
			bullet.Begin();
		}
		else if (bulletCannon != null)
		{
			bulletCannon.InitStats(damage, speed, force);
			bulletCannon.InitBot(parent, isPlayer);
			bulletCannon.Begin();
		}
	}

	private void PoolProjectile()
	{
		if (rangedWeaponType == RangedWeaponType.BOW)
		{
			arrow = poolManager_ArrowBow.Trigger(startPos.position, tempRot, level).arrow;
		}
		else if (rangedWeaponType == RangedWeaponType.CANNON)
		{
			bulletCannon = poolManager_BulletCannon.Trigger(startPos.position, tempRot, level).bulletCannon;
			particles_Manager.PlayCannonParticle(particleTransform);
		}
		else if (rangedWeaponType == RangedWeaponType.CATAPULT)
		{
			bulletCannon = poolManager_BulletCatapult.Trigger(startPos.position, tempRot, level, startPos.lossyScale).bulletCannon;
			startPos.gameObject.SetActive(false);
		}
		else if (rangedWeaponType == RangedWeaponType.GUN)
		{
			bullet = poolManager_Bullet.Trigger(startPos.position, tempRot, level).bullet;
			particles_Manager.PlayPistolParticle(particleTransform);
		}
		else if (rangedWeaponType == RangedWeaponType.SENTINEL)
		{
			if (isPlayer)
			{
				arrow = poolManager_ArrowSentinelBlue.Trigger(startPos.position, tempRot, level).arrow;
			}
			else
			{
				arrow = poolManager_ArrowSentinelRed.Trigger(startPos.position, tempRot, level).arrow;
			}
		}
		else if (rangedWeaponType == RangedWeaponType.CROSSBOW)
		{
			arrow = poolManager_ArrowCrossbow.Trigger(startPos.position, tempRot, level).arrow;
		}
		else if (rangedWeaponType == RangedWeaponType.BALLISTA)
		{
			if (isPlayer)
			{
				bulletCannon = poolManager_SpearBallistaBlue.Trigger(startPos.position, tempRot, level).bulletCannon;
			}
			else
			{
				bulletCannon = poolManager_SpearBallistaRed.Trigger(startPos.position, tempRot, level).bulletCannon;
			}
		}
		else if (rangedWeaponType == RangedWeaponType.SHURIKEN)
		{
			arrow = poolManager_Shuriken.Trigger(startPos.position, tempRot, level).arrow;
		}
		else if (rangedWeaponType == RangedWeaponType.MACHINE_GUN)
		{
			bullet = poolManager_Bullet.Trigger(startPos.position, tempRot, level).bullet;
			particles_Manager.PlayMachineGunParticle(particleTransform);
		}
		else if (rangedWeaponType == RangedWeaponType.FIRE_BALL)
		{
			if (isPlayer)
			{
				arrow_bomb = poolManager_FireballBlue.Trigger(startPos.position, tempRot, level).arrow_bomb;
			}
			else
			{
				arrow_bomb = poolManager_FireballRed.Trigger(startPos.position, tempRot, level).arrow_bomb;
			}
		}
		else if (rangedWeaponType == RangedWeaponType.HWACHA)
		{
			arrow = poolManager_Hwacha.Trigger(startPos.position, tempRot, nextMultiProjectileIndex).arrow;
			startPos.gameObject.SetActive(false);
		}
		else if (rangedWeaponType == RangedWeaponType.ORGAN_GUN)
		{
			bulletCannon = poolManager_BulletCannon.Trigger(startPos.position, tempRot, level, startPos.localScale).bulletCannon;
			particles_Manager.PlayCannonParticle(particleTransform);
		}
		else if (rangedWeaponType == RangedWeaponType.JAVELLIN)
		{
			bulletCannon = poolManager_Jevallin.Trigger(startPos.position, tempRot, level).bulletCannon;
		}
		if (reloadTransform != null)
		{
			reloadTransform.gameObject.SetActive(false);
		}
	}

	public void Reload()
	{
		if (rangedWeaponType == RangedWeaponType.CATAPULT)
		{
			reloadTransform.gameObject.SetActive(true);
		}
		else if (rangedWeaponType == RangedWeaponType.SENTINEL)
		{
			reloadTransform.gameObject.SetActive(true);
		}
		else if (rangedWeaponType == RangedWeaponType.BOW)
		{
			reloadTransform.gameObject.SetActive(true);
		}
		else if (rangedWeaponType == RangedWeaponType.CROSSBOW)
		{
			reloadTransform.gameObject.SetActive(true);
		}
		else if (rangedWeaponType == RangedWeaponType.BALLISTA)
		{
			reloadTransform.gameObject.SetActive(true);
		}
		else if (rangedWeaponType == RangedWeaponType.HWACHA)
		{
			for (int i = 0; i < multiProjectileStartTransforms.Length; i++)
			{
				multiProjectileStartTransforms[i].gameObject.SetActive(true);
			}
		}
		else if (rangedWeaponType == RangedWeaponType.JAVELLIN)
		{
			reloadTransform.gameObject.SetActive(true);
		}
	}
}
