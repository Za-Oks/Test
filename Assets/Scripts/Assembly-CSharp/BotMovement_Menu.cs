using UnityEngine;

public class BotMovement_Menu : MonoBehaviour
{
	public MenuCharacter thisCharacter;

	private Animator thisAnimator;

	private AudioBattleManager audioManager;

	private BasicPoolManager poolManager_Bullet;

	private BasicPoolManager poolManager_ArrowBow;

	private Particles_Manager particles_Manager;

	private Vector3 tempDir;

	private Quaternion tempRot;

	[Header("RANGED")]
	public Transform startPos;

	public Transform target;

	public Transform reloadTransform;

	private float soundDistance = 20f;

	private void Awake()
	{
		thisAnimator = GetComponent<Animator>();
		audioManager = GameObject.FindGameObjectWithTag("AudioBattleManager").GetComponent<AudioBattleManager>();
		poolManager_Bullet = GameObject.FindGameObjectWithTag("PoolManager_BulletPistol").GetComponent<BasicPoolManager>();
		poolManager_ArrowBow = GameObject.FindGameObjectWithTag("PoolManager_ArrowBow").GetComponent<BasicPoolManager>();
		particles_Manager = GameObject.FindWithTag("Particles_Manager").GetComponent<Particles_Manager>();
	}

	public void AnimationEvent_AttackRanged()
	{
		Arrow arrow = null;
		Bullet bullet = null;
		if (thisCharacter == MenuCharacter.MUSKETEER)
		{
			tempDir = target.position + Vector3.up * 1.5f - startPos.position;
			tempRot = Quaternion.LookRotation(tempDir, Vector3.up);
			bullet = poolManager_Bullet.Trigger(startPos.position, tempRot, 1).bullet;
			particles_Manager.PlayPistolParticle(startPos);
			audioManager.PlayAudio_FireMusket(base.transform.position + Vector3.forward * soundDistance);
		}
		else if (thisCharacter == MenuCharacter.ARCHER)
		{
			tempDir = target.position + Vector3.up * 3f - startPos.position;
			tempRot = Quaternion.LookRotation(tempDir, Vector3.up);
			arrow = poolManager_ArrowBow.Trigger(startPos.position, tempRot, 1).arrow;
			reloadTransform.gameObject.SetActive(false);
			audioManager.PlayAudio_FireArrow(base.transform.position + Vector3.forward * soundDistance);
		}
		if (arrow != null)
		{
			arrow.InitStats(0f, 30f, 0f);
			arrow.InitBot(base.transform, false);
			arrow.Begin();
		}
		else if (bullet != null)
		{
			bullet.InitStats(0f, 50f, 0f);
			bullet.InitBot(base.transform, true);
			bullet.Begin();
		}
	}

	public void AnimationEvent_Reload()
	{
		reloadTransform.gameObject.SetActive(true);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("WeaponMenu"))
		{
			if (thisCharacter == MenuCharacter.AXE_MAN || thisCharacter == MenuCharacter.SHIELD_MAN)
			{
				audioManager.PlayAudio_Sword(base.transform.position + Vector3.forward * soundDistance);
			}
			thisAnimator.SetTrigger("GetHit");
		}
	}
}
