using DG.Tweening;
using UnityEngine;

public class Audio_Manager : MonoBehaviour
{
	[Header("UI")]
	public AudioSource MenuClick_Audio;

	public AudioSource MenuBack_Audio;

	public AudioSource AddTroop_Audio;

	public AudioSource RemoveTroop_Audio;

	public AudioSource FailAddTroop_Audio;

	public AudioSource InApp_Audio;

	public AudioSource Reward_Audio;

	public AudioSource UpgradeSucceed_Audio;

	public AudioSource UpgradeFailed_Audio;

	public AudioSource BossBuySucceed_Audio;

	public AudioSource BossBuyFailed_Audio;

	[Header("Music")]
	public AudioSource BackGroundMusicMenu_Audio;

	public AudioSource BackGroundMusicFight_Audio;

	public AudioSource BackGroundMusicFightEnd_Audio;

	[Header("Values")]
	[Range(0f, 1f)]
	public float backgroundMusicVolume;

	private float time = 1f;

	private BackgroundMusic lastType;

	private void Awake()
	{
		Audio_Init();
		InitBackGroundMusic();
	}

	private void Audio_Init()
	{
		bool flag = PlayerPrefs_Settings.LoadSoundSetUp();
		bool flag2 = PlayerPrefs_Settings.LoadMusicSetUp();
		if (flag)
		{
			SetAudioListernerVolume(1f);
		}
		else
		{
			SetAudioListernerVolume(0f);
		}
		if (flag2)
		{
			SetBackGroundMusicVolume(backgroundMusicVolume);
		}
		else
		{
			SetBackGroundMusicVolume(0f);
		}
	}

	public void SetAudioListernerVolume(float value)
	{
		AudioListener.volume = value;
	}

	public void MenuClick()
	{
		MenuClick_Audio.Play();
	}

	public void MenuBack()
	{
		MenuBack_Audio.Play();
	}

	public void AddTroop()
	{
		AddTroop_Audio.Play();
	}

	public void RemoveTroop()
	{
		RemoveTroop_Audio.Play();
	}

	public void FailAddTroop()
	{
		FailAddTroop_Audio.Play();
	}

	public void InApp()
	{
		InApp_Audio.Play();
	}

	public void Reward()
	{
		Reward_Audio.Play();
	}

	public void UpgradeSucceed()
	{
		UpgradeSucceed_Audio.Play();
	}

	public void UpgradeFailed()
	{
		UpgradeFailed_Audio.Play();
	}

	public void BossBuySucceed()
	{
		BossBuySucceed_Audio.Play();
	}

	public void BossBuyFailed()
	{
		BossBuyFailed_Audio.Play();
	}

	private void InitBackGroundMusic()
	{
		BackGroundMusicMenu_Audio.ignoreListenerVolume = true;
		BackGroundMusicMenu_Audio.Play();
		BackGroundMusicFight_Audio.ignoreListenerVolume = true;
		BackGroundMusicFightEnd_Audio.ignoreListenerVolume = true;
	}

	public void SetBackGroundMusicVolume(float value)
	{
		BackGroundMusicMenu_Audio.volume = value;
		BackGroundMusicFight_Audio.volume = value;
		BackGroundMusicFightEnd_Audio.volume = value;
	}

	public bool CanPlayBackgroundMusic(BackgroundMusic type)
	{
		return lastType != type;
	}

	public void SwitchBackgroundMusic(BackgroundMusic type)
	{
		if (!CanPlayBackgroundMusic(type))
		{
			return;
		}
		lastType = type;
		if (!PlayerPrefs_Settings.LoadMusicSetUp())
		{
			return;
		}
		BackGroundMusicMenu_Audio.DOKill();
		BackGroundMusicFight_Audio.DOKill();
		BackGroundMusicFightEnd_Audio.DOKill();
		switch (type)
		{
		case BackgroundMusic.MENU:
			if (BackGroundMusicFight_Audio.isPlaying)
			{
				SwitchBackgroundMusic(BackGroundMusicFight_Audio, BackGroundMusicMenu_Audio);
				BackGroundMusicFightEnd_Audio.Stop();
			}
			else if (BackGroundMusicFightEnd_Audio.isPlaying)
			{
				SwitchBackgroundMusic(BackGroundMusicFightEnd_Audio, BackGroundMusicMenu_Audio);
				BackGroundMusicFight_Audio.Stop();
			}
			break;
		case BackgroundMusic.FIGHT:
			SwitchBackgroundMusic(BackGroundMusicMenu_Audio, BackGroundMusicFight_Audio);
			BackGroundMusicFightEnd_Audio.Stop();
			break;
		default:
			if (!BackGroundMusicFightEnd_Audio.isPlaying)
			{
				SwitchBackgroundMusic(BackGroundMusicFight_Audio, BackGroundMusicFightEnd_Audio);
			}
			BackGroundMusicMenu_Audio.Stop();
			break;
		}
	}

	private void SwitchBackgroundMusic(AudioSource from, AudioSource to)
	{
		to.time = from.time;
		from.DOFade(0f, time).OnComplete(delegate
		{
			from.Stop();
		});
		to.volume = 0f;
		to.DOFade(backgroundMusicVolume, time);
		to.Play();
	}
}
