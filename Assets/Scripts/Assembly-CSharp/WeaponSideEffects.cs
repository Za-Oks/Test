using DG.Tweening;
using UnityEngine;

public class WeaponSideEffects : MonoBehaviour
{
	[Header("SCALE")]
	public bool scale;

	public Transform m_parent;

	public Vector3 scaleTarget;

	public float scaleTime;

	[Header("SOUND")]
	public bool playSound;

	public AudioSource sound;

	public bool autoStop = true;

	public float time;

	private bool soundPaused;

	[Header("PARTICLE")]
	public bool playParticle;

	public ParticleSystem particlePlayer;

	public ParticleSystem particleEnemy;

	private float damage;

	private float force;

	private bool isPlayer;

	private AllReferencesManager referencesManager;

	private BotMovement temp_bot;

	public void InitStats(float damage, float force)
	{
		this.damage = damage;
		this.force = force;
	}

	public void InitBot(AllReferencesManager referencesManager, bool isPlayer)
	{
		this.referencesManager = referencesManager;
		this.isPlayer = isPlayer;
	}

	public void DoSideEffects_OnStart()
	{
		if (scale)
		{
			m_parent.DOScale(scaleTarget, scaleTime).SetLoops(2, LoopType.Yoyo);
		}
		if (playSound && !sound.isPlaying && !soundPaused)
		{
			sound.Play();
			if (autoStop)
			{
				Invoke("StopSound", time);
			}
		}
		if (playParticle)
		{
			if (isPlayer)
			{
				particlePlayer.Play();
			}
			else
			{
				particleEnemy.Play();
			}
		}
	}

	private void StopSound()
	{
		sound.Stop();
	}

	public void PauseSound(bool pause)
	{
		if (!playSound)
		{
			return;
		}
		if (pause)
		{
			if (sound.isPlaying)
			{
				soundPaused = true;
				sound.Pause();
			}
		}
		else if (soundPaused)
		{
			soundPaused = false;
			sound.UnPause();
		}
	}

	public void StopEverything()
	{
		if (playSound)
		{
			soundPaused = false;
			sound.Stop();
		}
		if (playParticle)
		{
			particlePlayer.Stop();
			particleEnemy.Stop();
		}
	}
}
