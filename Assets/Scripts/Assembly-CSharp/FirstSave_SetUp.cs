using UnityEngine;

public class FirstSave_SetUp : MonoBehaviour
{
	[Header("References")]
	public PlayerPrefs_Players playerPrefs_Players;

	private void Awake()
	{
		PlayerPrefs_Settings.Settings_Creation();
		PlayerPrefs_Account.Account_Creation();
		PlayerPrefs_Saves.Saves_Creation();
		playerPrefs_Players.PlayersSave_Creation();
		playerPrefs_Players.PlayersLoad_Status();
	}
}
