using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ArmyInspector
{
	[Header("Values")]
	public GameObject GO;

	public Sprite SP;

	public Army army;

	public string name;

	public int price;

	public int counterMax;

	[HideInInspector]
	public int counterPlayer;

	[Header("References")]
	public Ui_Manager ui_Manager;

	public GameObject upgrade_Panel;

	public Toggle army_Toggle;

	private TextMeshProUGUI price_Txt;

	private TextMeshProUGUI counter_Txt;

	private Image percent_Img;

	private TextMeshProUGUI troopName_Txt;

	private TextMeshProUGUI troopLevel_Txt;

	private TextMeshProUGUI health_Txt;

	private TextMeshProUGUI damage_Txt;

	private TextMeshProUGUI attackSpeed_Txt;

	private TextMeshProUGUI movementSpeed_Txt;

	private TextMeshProUGUI priceUpgrade_Txt;

	[Header("Base Stats")]
	public float health;

	public float damage;

	public float attackSpeed;

	public float movementSpeed;

	public float hitDistance;

	public int penetration;

	[Header("Upgrade Stats Percent")]
	public StatsValues[] StatsValuesLvL;

	public string saveColumn { get; set; }

	public void InitTroopSaveStats()
	{
		saveColumn = army.ToString();
		if (!PlayerPrefs.HasKey(saveColumn))
		{
			PlayerPrefs.SetInt(saveColumn, 0);
		}
	}

	public void InitTroopSaveStatsBoss()
	{
		saveColumn = army.ToString() + "Boss";
		if (!PlayerPrefs.HasKey(saveColumn))
		{
			PlayerPrefs.SetInt(saveColumn, 0);
		}
	}

	public void InitToggleValues()
	{
		if (GO != null)
		{
			price_Txt = army_Toggle.transform.Find("Price_Bg").GetChild(0).GetComponent<TextMeshProUGUI>();
			counter_Txt = army_Toggle.transform.Find("BackgroundCounter_Img").GetChild(1).GetComponent<TextMeshProUGUI>();
			percent_Img = army_Toggle.transform.Find("BackgroundCounter_Img").GetChild(0).GetComponent<Image>();
			price_Txt.text = string.Empty + price;
			counter_Txt.text = string.Empty + 0;
			percent_Img.fillAmount = 0f;
		}
		if (GO == null)
		{
			GO = (GameObject)Resources.Load("ArcherTutorial");
		}
	}

	public void InitUpgradePanelValues()
	{
		if (!(upgrade_Panel == null))
		{
			Image panel_Img = upgrade_Panel.GetComponent<Image>();
			Image component = upgrade_Panel.transform.GetChild(0).GetComponent<Image>();
			troopName_Txt = upgrade_Panel.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
			troopLevel_Txt = upgrade_Panel.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
			health_Txt = upgrade_Panel.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
			damage_Txt = upgrade_Panel.transform.GetChild(4).GetComponent<TextMeshProUGUI>();
			attackSpeed_Txt = upgrade_Panel.transform.GetChild(5).GetComponent<TextMeshProUGUI>();
			movementSpeed_Txt = upgrade_Panel.transform.GetChild(6).GetComponent<TextMeshProUGUI>();
			priceUpgrade_Txt = upgrade_Panel.transform.GetChild(7).GetChild(0).GetComponent<TextMeshProUGUI>();
			SetUpgradeStatsTexts();
			Button component2 = upgrade_Panel.transform.GetChild(7).GetComponent<Button>();
			component2.onClick.AddListener(delegate
			{
				bool upgradeDone = UpgradeStats();
				SetUpgradeStatsTexts();
				ui_Manager.CheckUpgradeDone(upgradeDone, panel_Img);
			});
			component.sprite = SP;
		}
	}

	public void SetCounterValues(int tempValue, bool setPercent)
	{
		counterPlayer += tempValue;
		counter_Txt.text = string.Empty + counterPlayer;
		if (setPercent)
		{
			percent_Img.fillAmount = (float)counterPlayer / (float)counterMax;
		}
		else
		{
			percent_Img.fillAmount = 0f;
		}
		if (percent_Img.fillAmount == 1f)
		{
			counter_Txt.text = "Max";
		}
	}

	public void ResetCounterValues()
	{
		counterPlayer = 0;
		counter_Txt.text = string.Empty + counterPlayer;
		percent_Img.fillAmount = counterPlayer / counterMax;
	}

	public bool CanAddMoreTroops()
	{
		if (counterPlayer < counterMax)
		{
			return true;
		}
		return false;
	}

	public void SaveStatsLvL(int lvl)
	{
		PlayerPrefs.SetInt(saveColumn, lvl);
	}

	public void SaveMaxStatsLvL()
	{
		PlayerPrefs.SetInt(saveColumn, 2);
	}

	public int LoadStatsLvL()
	{
        //if (InAppManager.HAS_SUBSCRIPTION)
        //{
        //	if (army == Army.Guard)
        //	{
        //		return 2;
        //	}
        //	if (army == Army.Giant)
        //	{
        //		return 2;
        //	}
        //	if (army == Army.Hwacha)
        //	{
        //		return 2;
        //	}
        //}
        if (army == Army.Guard)
        {
            return 2;
        }
        if (army == Army.Giant)
        {
            return 2;
        }
        if (army == Army.Hwacha)
        {
            return 2;
        }
        return PlayerPrefs.GetInt(saveColumn);
	}

	public bool UpgradeStats()
	{
		bool result = false;
		int num = LoadStatsLvL();
		int num2 = StatsValuesLvL.Length - 1;
		if (num >= num2)
		{
			return result;
		}
		int num3 = PlayerPrefs_Saves.LoadConsumables();
		int num4 = StatsValuesLvL[num].price;
		if (num3 >= StatsValuesLvL[num].price)
		{
			result = true;
			int lvl = num + 1;
			SaveStatsLvL(lvl);
			PlayerPrefs_Saves.SaveConsumables(-num4);
			PlayerPrefs.Save();
		}
		return result;
	}

	public void SetUpgradeStatsTexts()
	{
		int num = LoadStatsLvL();
		int index = num + 1;
		int num2 = StatsValuesLvL.Length - 1;
		bool flag = false;
		if (num >= num2)
		{
			flag = true;
		}
		troopName_Txt.text = ReturnName();
		troopLevel_Txt.text = ReturnLevelInfo();
		StatsFinal statsFinal = ReturnFinalStats(num);
		int digits = 2;
		if (flag)
		{
			health_Txt.text = (float)Math.Round(statsFinal.healthFinal, digits) + " (MAX)";
			damage_Txt.text = (float)Math.Round(statsFinal.damageFinal, digits) + " (MAX)";
			attackSpeed_Txt.text = (float)Math.Round(statsFinal.attackSpeedFinal, digits) + " (MAX)";
			movementSpeed_Txt.text = (float)Math.Round(statsFinal.movementSpeedFinal, digits) + " (MAX)";
			priceUpgrade_Txt.text = "MAX";
			return;
		}
		StatsFinal statsFinal2 = ReturnFinalStats(index);
		float num3 = statsFinal2.healthFinal - statsFinal.healthFinal;
		float num4 = statsFinal2.damageFinal - statsFinal.damageFinal;
		float num5 = statsFinal2.attackSpeedFinal - statsFinal.attackSpeedFinal;
		float num6 = statsFinal2.movementSpeedFinal - statsFinal.movementSpeedFinal;
		num3 = (float)Math.Round(num3, 2);
		num4 = (float)Math.Round(num4, 2);
		num5 = (float)Math.Round(num5, 2);
		num6 = (float)Math.Round(num6, 2);
		health_Txt.text = ((float)Math.Round(statsFinal.healthFinal, digits)).ToString() + " (+" + num3 + ")";
		damage_Txt.text = ((float)Math.Round(statsFinal.damageFinal, digits)).ToString() + " (+" + num4 + ")";
		attackSpeed_Txt.text = (float)Math.Round(statsFinal.attackSpeedFinal, digits) + " (+" + num5 + ")";
		movementSpeed_Txt.text = ((float)Math.Round(statsFinal.movementSpeedFinal, digits)).ToString() + " (+" + num6 + ")";
		priceUpgrade_Txt.text = StatsValuesLvL[num].price + string.Empty;
	}

	public string ReturnName()
	{
		return name;
	}

	public string ReturnLevelInfo()
	{
		int num = LoadStatsLvL();
		return "LVL " + (num + 1);
	}

	public string ReturnTroopBossInfo()
	{
		return name;
	}

	public StatsFinal ReturnFinalStats(int index)
	{
		StatsFinal result = default(StatsFinal);
		result.healthFinal = health * (StatsValuesLvL[index].healthPercent / 100f);
		result.damageFinal = damage * (StatsValuesLvL[index].damagePercent / 100f);
		result.attackSpeedFinal = attackSpeed * (StatsValuesLvL[index].attackSpeedPercent / 100f);
		result.movementSpeedFinal = movementSpeed * (StatsValuesLvL[index].movementSpeedPercent / 100f);
		result.attackSpeedMultiplierFinal = StatsValuesLvL[index].attackSpeedPercent / 100f;
		result.movementSpeedMultiplierFinal = StatsValuesLvL[index].movementSpeedPercent / 100f;
		result.hitDistance = hitDistance;
		result.penetration = penetration;
		return result;
	}
}
