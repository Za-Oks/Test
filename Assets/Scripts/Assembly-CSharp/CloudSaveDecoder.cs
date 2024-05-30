using System;
using System.Text;
using UnityEngine;

public class CloudSaveDecoder : MonoBehaviour
{
	private PlayerPrefs_Players playerPrefs_Players;

	private Level_Manager levelManager;

	private Ui_Manager ui_Manager;

	private string dataString = string.Empty;

	private byte[] data;

	private const string CATEGORY = "|CAT|";

	private const string CATEGORY_UPGRADES = "|UPG|";

	private const string CATEGORY_EPICS = "|EPI|";

	private const string CATEGORY_LEVELS = "|LEV|";

	private const string KEY = "|KEY|";

	private const string VALUE = "|VAL|";

	private ArmyInspector[] army;

	private string[] categories;

	private string[] category_upgrades;

	private string[] category_epics;

	private string[] category_levels;

	private void Awake()
	{
		levelManager = GameObject.FindGameObjectWithTag("Level_Manager").GetComponent<Level_Manager>();
		ui_Manager = GameObject.FindGameObjectWithTag("Ui_Manager").GetComponent<Ui_Manager>();
		playerPrefs_Players = GameObject.FindGameObjectWithTag("PlayerPrefs_Players").GetComponent<PlayerPrefs_Players>();
	}

	private void Start()
	{
		Init();
	}

	private void Init()
	{
		army = new ArmyInspector[28]
		{
			levelManager.Archer, levelManager.CannonMan, levelManager.Gladiator, levelManager.Knight, levelManager.Man, levelManager.CatapultMan, levelManager.Musketeer, levelManager.AxeMan, levelManager.ShieldMan, levelManager.SpearMan,
			levelManager.Giant, levelManager.Guard, levelManager.Ballista, levelManager.Crossbow, levelManager.WarElephant, levelManager.Kamikaze, levelManager.Ninja, levelManager.M16, levelManager.AK47, levelManager.MageTower,
			levelManager.Chariot, levelManager.Camel, levelManager.Pirate, levelManager.Hwacha, levelManager.Mage, levelManager.BearRider, levelManager.OrganGun, levelManager.TrebuchetMan
		};
	}

	public byte[] EncodeEverything()
	{
		dataString = string.Empty;
		Encode_Upgrades();
		Encode_Epics();
		Encode_Levels();
		Encode_EpicLevels();
		data = Encoding.UTF8.GetBytes(dataString);
		return data;
	}

	private void Encode_Upgrades()
	{
		dataString += "|CAT||UPG|";
		for (int i = 0; i < army.Length; i++)
		{
			string text = dataString;
			dataString = text + "|KEY|" + army[i].saveColumn + "|VAL|" + army[i].LoadStatsLvL();
		}
	}

	private void Encode_Epics()
	{
		dataString += "|CAT||EPI|";
		for (int i = 0; i < playerPrefs_Players.Players_SetUp.Length; i++)
		{
			string text = dataString;
			dataString = text + "|KEY|" + playerPrefs_Players.Players_SetUp[i].columnName + "|VAL|" + playerPrefs_Players.Players_SetUp[i].status;
		}
	}

	private void Encode_Levels()
	{
		dataString = dataString + "|CAT||LEV||KEY|" + levelManager.CountCompletedLevels();
	}

	private void Encode_EpicLevels()
	{
		dataString = dataString + "|CAT||LEV||KEY|" + levelManager.CountCompletedEpicLevels();
	}

	public void DecodeEverything(byte[] data)
	{
		this.data = data;
		dataString = Encoding.UTF8.GetString(data);
		categories = dataString.Split(new string[1] { "|CAT|" }, StringSplitOptions.RemoveEmptyEntries);
		category_upgrades = null;
		category_epics = null;
		category_levels = null;
		Decode_Categories();
		Decode_Upgrades();
		Decode_Epics();
		Decode_Levels();
		Decode_EpicLevels();
		levelManager.RefreshUpgrades();
		levelManager.RefreshBosses();
	}

	private void Decode_Categories()
	{
		string[] array = categories;
		foreach (string text in array)
		{
			if (text.StartsWith("|UPG|"))
			{
				string text2 = text.Replace("|UPG|", string.Empty);
				category_upgrades = text2.Split(new string[1] { "|KEY|" }, StringSplitOptions.RemoveEmptyEntries);
			}
			else if (text.StartsWith("|EPI|"))
			{
				string text2 = text.Replace("|EPI|", string.Empty);
				category_epics = text2.Split(new string[1] { "|KEY|" }, StringSplitOptions.RemoveEmptyEntries);
			}
			else if (text.StartsWith("|LEV|"))
			{
				string text2 = text.Replace("|LEV|", string.Empty);
				category_levels = text2.Split(new string[1] { "|KEY|" }, StringSplitOptions.RemoveEmptyEntries);
			}
		}
	}

	private void Decode_Upgrades()
	{
		if (category_upgrades != null)
		{
			string[] array = category_upgrades;
			foreach (string text in array)
			{
				string[] array2 = text.Split(new string[1] { "|VAL|" }, StringSplitOptions.RemoveEmptyEntries);
				if (array2.Length == 2)
				{
					int result = 0;
					if (int.TryParse(array2[1], out result))
					{
						levelManager.CloudSaveArmy(array2[0], result);
					}
					else
					{
						MonoBehaviour.print("ERROR");
					}
				}
				else
				{
					MonoBehaviour.print("ERROR");
				}
			}
		}
		else
		{
			MonoBehaviour.print("ERROR");
		}
	}

	private void Decode_Epics()
	{
		if (category_epics != null)
		{
			string[] array = category_epics;
			foreach (string text in array)
			{
				string[] array2 = text.Split(new string[1] { "|VAL|" }, StringSplitOptions.RemoveEmptyEntries);
				if (array2.Length == 2)
				{
					if (array2[1] == "unlocked")
					{
						levelManager.CloudSaveUnlockEpic(array2[0]);
					}
				}
				else
				{
					MonoBehaviour.print("ERROR");
				}
			}
		}
		else
		{
			MonoBehaviour.print("ERROR");
		}
	}

	private void Decode_Levels()
	{
		if (category_levels != null)
		{
			if (category_levels.Length > 0)
			{
				int result = 0;
				if (int.TryParse(category_levels[0], out result))
				{
					MonoBehaviour.print("LOAD LEVELS: " + result);
					if (result > 0)
					{
						levelManager.RefreshLevelsCompleted(result);
					}
				}
			}
			else
			{
				MonoBehaviour.print("ERROR");
			}
		}
		else
		{
			MonoBehaviour.print("ERROR");
		}
	}

	private void Decode_EpicLevels()
	{
		if (category_levels != null)
		{
			if (category_levels.Length > 0)
			{
				int result = 0;
				if (int.TryParse(category_levels[0], out result))
				{
					MonoBehaviour.print("LOAD LEVELS: " + result);
					if (result > 0)
					{
						levelManager.RefreshEpicLevelsCompleted(result);
					}
				}
			}
			else
			{
				MonoBehaviour.print("ERROR");
			}
		}
		else
		{
			MonoBehaviour.print("ERROR");
		}
	}
}
