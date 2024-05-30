using UnityEngine;

public class PlayerPrefs_Account
{
	private const string AccountCreated = "AccountCreated";

	private const string NickName = "NickName";

	private const string UserName = "UserName";

	private const string Password = "Password";

	private const string Ranking = "Ranking";

	public static void Account_Creation()
	{
		int num = 0;
		if (!PlayerPrefs.HasKey("AccountCreated"))
		{
			SaveAccountCreated(false);
			num++;
		}
		if (!PlayerPrefs.HasKey("NickName"))
		{
			SaveNickName(string.Empty);
			num++;
		}
		if (!PlayerPrefs.HasKey("UserName"))
		{
			SaveUserName(string.Empty);
			num++;
		}
		if (!PlayerPrefs.HasKey("Password"))
		{
			SavePassword(string.Empty);
			num++;
		}
		if (!PlayerPrefs.HasKey("Ranking"))
		{
			SaveRanking(1000);
			num++;
		}
		if (num == 5)
		{
			Debug.Log("PlayerPrefs Account Created");
			PlayerPrefs.Save();
		}
	}

	public static void SaveAccountCreated(bool status)
	{
		int value = (status ? 1 : 0);
		PlayerPrefs.SetInt("AccountCreated", value);
	}

	public static bool LoadAccountCreated()
	{
		return PlayerPrefs.GetInt("AccountCreated") == 1;
	}

	public static void SaveNickName(string nickName)
	{
		PlayerPrefs.SetString("NickName", nickName);
	}

	public static string LoadNickName()
	{
		return PlayerPrefs.GetString("NickName");
	}

	public static void SaveUserName(string userName)
	{
		PlayerPrefs.SetString("UserName", userName);
	}

	public static string LoadUsername()
	{
		return PlayerPrefs.GetString("UserName");
	}

	public static void SavePassword(string password)
	{
		PlayerPrefs.SetString("Password", password);
	}

	public static string LoadPassword()
	{
		return PlayerPrefs.GetString("Password");
	}

	public static void SaveRanking(int value)
	{
		PlayerPrefs.SetInt("Ranking", value);
	}

	public static int LoadRanking()
	{
		return PlayerPrefs.GetInt("Ranking");
	}
}
