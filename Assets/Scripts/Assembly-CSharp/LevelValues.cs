using System;
using UnityEngine;

[Serializable]
public class LevelValues
{
	[Header("Values")]
	public LevelSetUp levelSetUp;

	public int availableGold;

	public int availableArmy;

	public LevelDesign design;

	[Header("Boss Values")]
	public bool isBossLevel;

	public int bossReward;

	[Header("Epic Values")]
	public int epic_Reward_First_Time;

	public int epic_Reward;

	private float percentBonusGold = 0.2f;

	private float percentBonusArmy = 0.2f;

	public int AvailableGold(bool useBonusGold, GameMode tempMode)
	{
		if (useBonusGold && tempMode == GameMode.Levels)
		{
			return availableGold + BonusGoldValue();
		}
		return availableGold;
	}

	public int AvailableArmy(bool useBonusArmy, GameMode tempMode)
	{
		if (useBonusArmy && tempMode == GameMode.Levels)
		{
			return availableArmy + BonusArmyValue();
		}
		return availableArmy;
	}

	public int BonusGoldValue()
	{
		int num = 50;
		int num2 = (int)(percentBonusGold * (float)availableGold);
		if (num2 < num)
		{
			num2 = num;
		}
		return num2;
	}

	public int BonusArmyValue()
	{
		int num = 3;
		int num2 = (int)(percentBonusArmy * (float)availableArmy);
		if (num2 < num)
		{
			num2 = num;
		}
		return num2;
	}
}
