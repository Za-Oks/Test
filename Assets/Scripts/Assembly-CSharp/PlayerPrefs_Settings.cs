using UnityEngine;

public class PlayerPrefs_Settings
{
	private const string SoundSetUp = "SoundSetUp";

	private const string MusicSetUp = "MusicSetUp";

	private const string TutorialCanShow = "TutorialCanShow";

	private const string CanShowRatePopUp = "CanShowRatePopUp";

	private const string LockedTopPanel = "LockedTopPanel";

	public static void Settings_Creation()
	{
		int num = 0;
		if (!PlayerPrefs.HasKey("SoundSetUp"))
		{
			SaveSoundSetUp(true);
			num++;
		}
		if (!PlayerPrefs.HasKey("MusicSetUp"))
		{
			SaveMusicSetUp(true);
			num++;
		}
		if (!PlayerPrefs.HasKey("TutorialCanShow"))
		{
			SaveTutorialCanShow(true);
			num++;
		}
		if (!PlayerPrefs.HasKey("CanShowRatePopUp"))
		{
			SaveCanShowRatePopUp(true);
			num++;
		}
		if (!PlayerPrefs.HasKey("LockedTopPanel"))
		{
			SaveLockedTopPanel(false);
			num++;
		}
		if (num == 5)
		{
			Debug.Log("PlayerPrefs Settings Created");
			PlayerPrefs.Save();
		}
	}

	public static void SaveSoundSetUp(bool status)
	{
		int value = (status ? 1 : 0);
		PlayerPrefs.SetInt("SoundSetUp", value);
	}

	public static bool LoadSoundSetUp()
	{
		return PlayerPrefs.GetInt("SoundSetUp") == 1;
	}

	public static void SaveMusicSetUp(bool status)
	{
		int value = (status ? 1 : 0);
		PlayerPrefs.SetInt("MusicSetUp", value);
	}

	public static bool LoadMusicSetUp()
	{
		return PlayerPrefs.GetInt("MusicSetUp") == 1;
	}

	public static void SaveTutorialCanShow(bool status)
	{
		int value = (status ? 1 : 0);
		PlayerPrefs.SetInt("TutorialCanShow", value);
	}

	public static bool LoadTutorialCanShow()
	{
		return PlayerPrefs.GetInt("TutorialCanShow") == 1;
	}

	public static void SaveCanShowRatePopUp(bool status)
	{
		int value = (status ? 1 : 0);
		PlayerPrefs.SetInt("CanShowRatePopUp", value);
	}

	public static bool LoadShowRatePopUp()
	{
		return PlayerPrefs.GetInt("CanShowRatePopUp") == 1;
	}

	public static void SaveLockedTopPanel(bool status)
	{
		int value = (status ? 1 : 0);
		PlayerPrefs.SetInt("LockedTopPanel", value);
	}

	public static bool LoadLockedTopPanel()
	{
		return PlayerPrefs.GetInt("LockedTopPanel") == 1;
	}
}
