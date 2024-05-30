using UnityEngine;

public class PlayerPrefs_Players : MonoBehaviour
{
	[Header("References")]
	public Ui_Manager ui_Manager;

	public Audio_Manager audio_Manager;

	[Header("Players")]
	public PlayersSetUp[] Players_SetUp;

	public SortOrder SortOrder;

	public void PlayersSave_Creation()
	{
		int num = 0;
		for (int i = 0; i < Players_SetUp.Length; i++)
		{
			Players_SetUp[i].InitValues(ui_Manager, audio_Manager);
			string columnName = Players_SetUp[i].columnName;
			string status = Players_SetUp[i].status;
			if (!PlayerPrefs.HasKey(columnName))
			{
				PlayerPrefs.SetString(columnName, status);
				num++;
			}
		}
		if (num == Players_SetUp.Length)
		{
			Debug.Log("PlayerPrefs Players Created");
			PlayerPrefs.Save();
		}
	}

	public void PlayersLoad_Status()
	{
		for (int i = 0; i < Players_SetUp.Length; i++)
		{
			string columnName = Players_SetUp[i].columnName;
			if (PlayerPrefs.HasKey(columnName))
			{
				Players_SetUp[i].index = i;
				Players_SetUp[i].status = PlayerPrefs.GetString(columnName);
				if (Players_SetUp[i].status == "unlocked")
				{
					Players_SetUp[i].bossToggle.interactable = true;
					Players_SetUp[i].lock_Panel.SetActive(false);
					Players_SetUp[i].bossInAppShop.gameObject.SetActive(false);
				}
				else
				{
					Players_SetUp[i].bossToggle.interactable = false;
				}
			}
		}
	}

	public static void UnLockPlayer(string columnName)
	{
		PlayerPrefs.SetString(columnName, "unlocked");
	}

	public void Sort()
	{
		if (SortOrder == SortOrder.Ascending)
		{
			SortAscending();
		}
		else if (SortOrder == SortOrder.Descending)
		{
			SortDescending();
		}
	}

	private void SortAscending()
	{
		for (int i = 0; i < Players_SetUp.Length; i++)
		{
			for (int j = i + 1; j < Players_SetUp.Length; j++)
			{
				if (Players_SetUp[i].price > Players_SetUp[j].price)
				{
					PlayersSetUp playersSetUp = Players_SetUp[i];
					Players_SetUp[i] = Players_SetUp[j];
					Players_SetUp[j] = playersSetUp;
				}
			}
		}
	}

	private void SortDescending()
	{
		for (int i = 0; i < Players_SetUp.Length; i++)
		{
			for (int j = i + 1; j < Players_SetUp.Length; j++)
			{
				if (Players_SetUp[i].price < Players_SetUp[j].price)
				{
					PlayersSetUp playersSetUp = Players_SetUp[i];
					Players_SetUp[i] = Players_SetUp[j];
					Players_SetUp[j] = playersSetUp;
				}
			}
		}
	}
}
