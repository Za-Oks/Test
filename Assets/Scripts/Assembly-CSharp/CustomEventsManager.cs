using System.Collections.Generic;
using UnityEngine.Analytics;

public static class CustomEventsManager
{
	public static void Send_LevelWon(string levelID)
	{
		Analytics.CustomEvent("Unique_Level_Won", new Dictionary<string, object> { 
		{
			"level_id",
			"ID_" + levelID
		} });
	}

	public static void Send_LevelLost(string levelID)
	{
		Analytics.CustomEvent("Unique_Level_Lost", new Dictionary<string, object> { 
		{
			"level_id",
			"ID_" + levelID
		} });
	}
}
