using System;
using UnityEngine;

public class PlayerPrefs_Saves
{
	private const string Consumables = "Consumables";

	private const string BestScore = "BestScore";

	private const string LevelsCompleted = "LevelsCompleted";

	private const string EpicLevelsCompleted = "EpicLevelsCompleted";

	private const string HasBoughtIAP = "HasBoughtIAP";

	private const string CanShowAds = "CanShowAds";

	private const string LockedPlayers = "LockedPlayers";

	private const string LockedSpartan = "LockedSpartan";

	private const string LockedSentinel = "LockedSentinel";

	private const string LockedSamurai = "LockedSamurai";

	private const string LockedFireDragon = "LockedFireDragon";

	private const string LockedMinigun = "LockedMinigun";

	private const string LockedRhino = "LockedRhino";

	private const string LockedGuardian = "LockedGuardian";

	private const string LockedWolf = "LockedWolf";

	private const string RunOnlyOnce = "RunOnlyOnce";

	private const string FacebookReward = "FacebookReward";

	private const string TwitterReward = "TwitterReward";

	private const string DateInstallation = "DateInstallation";

	private const string DailyReferenceTime = "DailyReferenceTime";

	private const string DailyWinsMultiplayer = "DailyWinsMultiplayer";

	private const string TermsAccepted = "TermsAccepted";

	private const string SubscriptionAppleExpirationDate = "SubscriptionAppleExpirationDate";

	private const string HasSubscriptionGoogle = "HasSubscriptionGoogle";

	private const string SubscriptionPurchaseDate = "SubscriptionPurchaseDate";

	private const string SubscriptionCollectedDays = "SubscriptionCollectedDays";

	private const string LastDayOpen = "LastDayOpen";

	public static void Saves_Creation()
	{
		int num = 0;
		if (!PlayerPrefs.HasKey("Consumables"))
		{
			PlayerPrefs.SetInt("Consumables", 0);
			num++;
		}
		if (!PlayerPrefs.HasKey("BestScore"))
		{
			PlayerPrefs.SetInt("BestScore", 0);
			num++;
		}
		if (!PlayerPrefs.HasKey("LevelsCompleted"))
		{
			PlayerPrefs.SetInt("LevelsCompleted", 0);
			num++;
		}
		if (!PlayerPrefs.HasKey("EpicLevelsCompleted"))
		{
			PlayerPrefs.SetInt("EpicLevelsCompleted", 0);
			num++;
		}
		if (!PlayerPrefs.HasKey("HasBoughtIAP"))
		{
			SaveHasBoughtIAP(false);
			num++;
		}
		if (!PlayerPrefs.HasKey("CanShowAds"))
		{
			SaveCanShowAds(true);
			num++;
		}
		if (!PlayerPrefs.HasKey("LockedPlayers"))
		{
			SaveLockedPlayers(true);
			num++;
		}
		if (!PlayerPrefs.HasKey("LockedSpartan"))
		{
			SaveLockedSpartan(true);
			num++;
		}
		if (!PlayerPrefs.HasKey("LockedSentinel"))
		{
			SaveLockedSentinel(true);
			num++;
		}
		if (!PlayerPrefs.HasKey("LockedSamurai"))
		{
			SaveLockedSamurai(true);
			num++;
		}
		if (!PlayerPrefs.HasKey("LockedFireDragon"))
		{
			SaveLockedFireDragon(true);
			num++;
		}
		if (!PlayerPrefs.HasKey("LockedMinigun"))
		{
			SaveLockedMinigun(true);
			num++;
		}
		if (!PlayerPrefs.HasKey("LockedRhino"))
		{
			SaveLockedRhino(true);
			num++;
		}
		if (!PlayerPrefs.HasKey("LockedGuardian"))
		{
			SaveLockedGuardian(true);
			num++;
		}
		if (!PlayerPrefs.HasKey("LockedWolf"))
		{
			SaveLockedWolf(true);
			num++;
		}
		if (!PlayerPrefs.HasKey("RunOnlyOnce"))
		{
			SaveRunOnlyOnce(true);
			num++;
		}
		if (!PlayerPrefs.HasKey("FacebookReward"))
		{
			SaveFacebookReward(true);
			num++;
		}
		if (!PlayerPrefs.HasKey("TwitterReward"))
		{
			SaveTwitterReward(true);
			num++;
		}
		if (!PlayerPrefs.HasKey("DateInstallation"))
		{
			SaveDateInstallation(DateTime.Now);
			num++;
		}
		if (!PlayerPrefs.HasKey("DailyReferenceTime"))
		{
			SaveDailyReferenceTime(new DateTime(2000, 1, 1));
			num++;
		}
		if (!PlayerPrefs.HasKey("DailyWinsMultiplayer"))
		{
			SaveDailyWinsMultiplayer(0);
			num++;
		}
		if (!PlayerPrefs.HasKey("SubscriptionAppleExpirationDate"))
		{
			SaveSubscriptionAppleExpirationDate(DateTime.MinValue.ToUniversalTime());
			num++;
		}
		if (!PlayerPrefs.HasKey("HasSubscriptionGoogle"))
		{
			SaveHasGoogleSubscription(false);
			num++;
		}
		if (!PlayerPrefs.HasKey("SubscriptionPurchaseDate"))
		{
			SaveSubscriptionPurchaseDate(DateTime.Now.ToUniversalTime());
			num++;
		}
		if (!PlayerPrefs.HasKey("SubscriptionCollectedDays"))
		{
			SaveSubscriptionCollectedDays(0);
			num++;
		}
		if (!PlayerPrefs.HasKey("LastDayOpen"))
		{
			SaveLastDayOpen(DateTime.MinValue);
			num++;
		}
		if (num == 26)
		{
			Debug.Log("PlayerPrefs Saves Created");
			PlayerPrefs.Save();
		}
	}

	public static void SaveConsumables(int newConsumables)
	{
		int num = LoadConsumables();
		num += newConsumables;
		PlayerPrefs.SetInt("Consumables", num);
	}

	public static int LoadConsumables()
	{
		return PlayerPrefs.GetInt("Consumables");
	}

	public static bool SaveBestScore(int score)
	{
		bool result = false;
		if (score > LoadBestScore())
		{
			PlayerPrefs.SetInt("BestScore", score);
			result = true;
		}
		return result;
	}

	public static int LoadBestScore()
	{
		return PlayerPrefs.GetInt("BestScore");
	}

	public static void SaveLevelsCompleted(int levelsIndex)
	{
		if (levelsIndex > LoadLevelsCompleted())
		{
			PlayerPrefs.SetInt("LevelsCompleted", levelsIndex);
		}
	}

	public static int LoadLevelsCompleted()
	{
		return PlayerPrefs.GetInt("LevelsCompleted");
	}

	public static void SaveEpicLevelsCompleted(int levelsIndex)
	{
		if (levelsIndex > LoadEpicLevelsCompleted())
		{
			PlayerPrefs.SetInt("EpicLevelsCompleted", levelsIndex);
		}
	}

	public static int LoadEpicLevelsCompleted()
	{
		return PlayerPrefs.GetInt("EpicLevelsCompleted");
	}

	public static void SaveHasBoughtIAP(bool status)
	{
		int value = (status ? 1 : 0);
		PlayerPrefs.SetInt("HasBoughtIAP", value);
	}

	public static bool LoadHasBoughtIAP()
	{
		return PlayerPrefs.GetInt("HasBoughtIAP") == 1;
	}

	public static void SaveCanShowAds(bool status)
	{
		int value = (status ? 1 : 0);
		PlayerPrefs.SetInt("CanShowAds", value);
	}

	public static bool LoadCanShowAds()
	{
		return PlayerPrefs.GetInt("CanShowAds") == 1;
	}

	public static void SaveLockedPlayers(bool status)
	{
		int value = (status ? 1 : 0);
		PlayerPrefs.SetInt("LockedPlayers", value);
	}

	public static bool LoadLockedPlayers()
	{
		return PlayerPrefs.GetInt("LockedPlayers") == 1;
	}

	public static void SaveLockedSpartan(bool status)
	{
		int value = (status ? 1 : 0);
		PlayerPrefs.SetInt("LockedSpartan", value);
	}

	public static bool LoadLockedSpartan()
	{
		return PlayerPrefs.GetInt("LockedSpartan") == 1;
	}

	public static void SaveLockedSentinel(bool status)
	{
		int value = (status ? 1 : 0);
		PlayerPrefs.SetInt("LockedSentinel", value);
		PlayerPrefs.Save();
	}

	public static bool LoadLockedSentinel()
	{
		return PlayerPrefs.GetInt("LockedSentinel") == 1;
	}

	public static void SaveLockedSamurai(bool status)
	{
		int value = (status ? 1 : 0);
		PlayerPrefs.SetInt("LockedSamurai", value);
	}

	public static bool LoadLockedSamurai()
	{
		return PlayerPrefs.GetInt("LockedSamurai") == 1;
	}

	public static void SaveLockedFireDragon(bool status)
	{
		int value = (status ? 1 : 0);
		PlayerPrefs.SetInt("LockedFireDragon", value);
	}

	public static bool LoadLockedFireDragon()
	{
		return PlayerPrefs.GetInt("LockedFireDragon") == 1;
	}

	public static void SaveLockedMinigun(bool status)
	{
		int value = (status ? 1 : 0);
		PlayerPrefs.SetInt("LockedMinigun", value);
	}

	public static bool LoadLockedMinigun()
	{
		return PlayerPrefs.GetInt("LockedMinigun") == 1;
	}

	public static void SaveLockedRhino(bool status)
	{
		int value = (status ? 1 : 0);
		PlayerPrefs.SetInt("LockedRhino", value);
	}

	public static bool LoadLockedRhino()
	{
		return PlayerPrefs.GetInt("LockedRhino") == 1;
	}

	public static void SaveLockedGuardian(bool status)
	{
		int value = (status ? 1 : 0);
		PlayerPrefs.SetInt("LockedGuardian", value);
	}

	public static bool LoadLockedGuardian()
	{
		return PlayerPrefs.GetInt("LockedGuardian") == 1;
	}

	public static void SaveLockedWolf(bool status)
	{
		int value = (status ? 1 : 0);
		PlayerPrefs.SetInt("LockedWolf", value);
	}

	public static bool LoadLockedWolf()
	{
		return PlayerPrefs.GetInt("LockedWolf") == 1;
	}

	public static void SaveRunOnlyOnce(bool status)
	{
		int value = (status ? 1 : 0);
		PlayerPrefs.SetInt("RunOnlyOnce", value);
	}

	public static bool LoadRunOnlyOnce()
	{
		return PlayerPrefs.GetInt("RunOnlyOnce") == 1;
	}

	public static void SaveFacebookReward(bool status)
	{
		int value = (status ? 1 : 0);
		PlayerPrefs.SetInt("FacebookReward", value);
	}

	public static bool LoadFacebookReward()
	{
		return PlayerPrefs.GetInt("FacebookReward") == 1;
	}

	public static void SaveTwitterReward(bool status)
	{
		int value = (status ? 1 : 0);
		PlayerPrefs.SetInt("TwitterReward", value);
	}

	public static bool LoadTwitterReward()
	{
		return PlayerPrefs.GetInt("TwitterReward") == 1;
	}

	public static void SaveDateInstallation(DateTime tempDate)
	{
		PlayerPrefs.SetString("DateInstallation", tempDate.ToString());
	}

	public static string LoadDateInstallation()
	{
		return PlayerPrefs.GetString("DateInstallation");
	}

	public static void SaveDailyReferenceTime(DateTime tempDate)
	{
		PlayerPrefs.SetString("DailyReferenceTime", tempDate.ToString());
	}

	public static string LoadDailyReferenceTime()
	{
		return PlayerPrefs.GetString("DailyReferenceTime");
	}

	public static void SaveDailyWinsMultiplayer(int value)
	{
		PlayerPrefs.SetInt("DailyWinsMultiplayer", value);
	}

	public static int LoadDailyWinsMultiplayer()
	{
		return PlayerPrefs.GetInt("DailyWinsMultiplayer");
	}

	public static void CreateTermsAccepted()
	{
		if (!PlayerPrefs.HasKey("TermsAccepted"))
		{
			SaveTermsAccepted(false);
		}
	}

	public static void SaveTermsAccepted(bool status)
	{
		int value = (status ? 1 : 0);
		PlayerPrefs.SetInt("TermsAccepted", value);
	}

	public static bool LoadTermsAccepted()
	{
		return PlayerPrefs.GetInt("TermsAccepted") == 1;
	}

	public static void SaveSubscriptionAppleExpirationDate(DateTime tempDate)
	{
		PlayerPrefs.SetString("SubscriptionAppleExpirationDate", tempDate.ToString());
	}

	public static DateTime LoadSubscriptionAppleExpirationDate()
	{
		DateTime result = DateTime.MinValue.ToUniversalTime();
		DateTime.TryParse(PlayerPrefs.GetString("SubscriptionAppleExpirationDate"), out result);
		return result;
	}

	public static void SaveHasGoogleSubscription(bool has)
	{
		PlayerPrefs.SetInt("HasSubscriptionGoogle", has ? 1 : 0);
	}

	public static bool LoadHasGoogleSubscription()
	{
		return PlayerPrefs.GetInt("HasSubscriptionGoogle") == 1;
	}

	public static void SaveSubscriptionPurchaseDate(DateTime tempDate)
	{
		PlayerPrefs.SetString("SubscriptionPurchaseDate", tempDate.ToString());
	}

	public static DateTime LoadSubscriptionPurchaseDate()
	{
		DateTime result = DateTime.Now.ToUniversalTime();
		DateTime.TryParse(PlayerPrefs.GetString("SubscriptionPurchaseDate"), out result);
		return result;
	}

	public static void SaveSubscriptionCollectedDays(int days)
	{
		PlayerPrefs.SetInt("SubscriptionCollectedDays", days);
	}

	public static int LoadSubscriptionCollectedDays()
	{
		return PlayerPrefs.GetInt("SubscriptionCollectedDays");
	}

	public static void SaveLastDayOpen(DateTime tempDate)
	{
		PlayerPrefs.SetString("LastDayOpen", tempDate.ToString());
	}

	public static DateTime LoadLastDayOpen()
	{
		DateTime result = DateTime.MinValue;
		DateTime.TryParse(PlayerPrefs.GetString("LastDayOpen"), out result);
		return result;
	}
}
