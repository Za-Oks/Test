using UnityEngine;

public class RankingCalculations
{
	public static float ReturnC(float gamesPlayed)
	{
		float num = 20f;
		float num2 = 60f;
		float num3 = 25f;
		float num4 = num2;
		float num5 = num3;
		float num6 = gamesPlayed / num;
		if (num6 >= 1f)
		{
			num6 = 1f;
		}
		return num4 + (num5 - num4) * num6;
	}

	public static float WinElo(float eloPlayer, float eloEnemy, float C)
	{
		float num = 1f / (1f + Mathf.Pow(10f, (eloEnemy - eloPlayer) / 400f));
		return eloPlayer + C * (1f - num);
	}

	public static float LoseElo(float eloPlayer, float eloEnemy, float C)
	{
		float num = 1f / (1f + Mathf.Pow(10f, (eloPlayer - eloEnemy) / 400f));
		return eloPlayer - C * (1f - num);
	}
}
