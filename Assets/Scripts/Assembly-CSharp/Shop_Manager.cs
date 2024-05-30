using UnityEngine;

public class Shop_Manager : MonoBehaviour
{
	[Header("References")]
	public Ui_Manager ui_Manager;

	public Level_Manager level_Manager;

	public PlayerPrefs_Players playerPrefs_Players;

	public Audio_Manager audio_Manager;

	private GameObject[] Players;

	private int currentIndex;

	private void Start()
	{
		currentIndex = 0;
		PlayersSetUp();
		Choose_Handler(ChooseOption.None);
		Set_Player_Button_Active(currentIndex);
		SetShopGui(currentIndex);
	}

	public void PlayersSetUp()
	{
		int num = playerPrefs_Players.Players_SetUp.Length;
		Players = new GameObject[num];
		for (int i = 0; i < num; i++)
		{
			PlayersSetUp playersSetUp = playerPrefs_Players.Players_SetUp[i];
			GameObject gameObject = Object.Instantiate(playersSetUp.player);
			gameObject.SetActive(true);
			gameObject.transform.parent = level_Manager.ShopGOs.transform;
			gameObject.transform.localPosition = Vector3.zero;
			Players[i] = gameObject;
		}
	}

	public void Set_Player_Button_Active(int target)
	{
		for (int i = 0; i < Players.Length; i++)
		{
			Players[i].SetActive(false);
			playerPrefs_Players.Players_SetUp[i].bossInAppShop.gameObject.SetActive(false);
		}
		Players[target].SetActive(true);
		if (playerPrefs_Players.Players_SetUp[target].status == "locked")
		{
			playerPrefs_Players.Players_SetUp[target].bossInAppShop.gameObject.SetActive(true);
		}
	}

	private int Choose_Handler(ChooseOption chooseOption)
	{
		if (chooseOption == ChooseOption.Next && currentIndex != Players.Length - 1)
		{
			currentIndex++;
		}
		else if (chooseOption == ChooseOption.Previous && currentIndex != 0)
		{
			currentIndex--;
		}
		return currentIndex;
	}

	public void ChooseNext()
	{
		int num = Choose_Handler(ChooseOption.Next);
		SetShopGui(num);
		Set_Player_Button_Active(num);
	}

	public void ChoosePrevious()
	{
		int num = Choose_Handler(ChooseOption.Previous);
		SetShopGui(num);
		Set_Player_Button_Active(num);
	}

	private void SetShopGui(int target)
	{
		int price = playerPrefs_Players.Players_SetUp[target].price;
		string status = playerPrefs_Players.Players_SetUp[target].status;
		if (status.Equals("locked"))
		{
			ui_Manager.shop_Gui.buyPanel.gameObject.SetActive(true);
			ui_Manager.shop_Gui.price_Txt.text = price.ToString();
		}
		else if (status.Equals("unlocked"))
		{
			ui_Manager.shop_Gui.buyPanel.gameObject.SetActive(false);
		}
		if (target == 0)
		{
			ui_Manager.shop_Gui.previous_Btn.interactable = false;
			ui_Manager.shop_Gui.next_Btn.interactable = true;
		}
		else if (target == Players.Length - 1)
		{
			ui_Manager.shop_Gui.previous_Btn.interactable = true;
			ui_Manager.shop_Gui.next_Btn.interactable = false;
		}
		else
		{
			ui_Manager.shop_Gui.previous_Btn.interactable = true;
			ui_Manager.shop_Gui.next_Btn.interactable = true;
		}
		level_Manager.SetTroopBossValues(playerPrefs_Players.Players_SetUp[target].columnName);
	}

	public void CheckIfBossIsLocked()
	{
		string status = playerPrefs_Players.Players_SetUp[currentIndex].status;
		if (status.Equals("locked"))
		{
			ui_Manager.shop_Gui.buyPanel.gameObject.SetActive(true);
		}
		else if (status.Equals("unlocked"))
		{
			ui_Manager.shop_Gui.buyPanel.gameObject.SetActive(false);
		}
	}

	public void BuyPlayer()
	{
		int num = currentIndex;
		int num2 = PlayerPrefs_Saves.LoadConsumables();
		int price = playerPrefs_Players.Players_SetUp[num].price;
		if (num2 >= price)
		{
			MonoBehaviour.print("Player Bought");
			playerPrefs_Players.Players_SetUp[num].status = "unlocked";
			playerPrefs_Players.Players_SetUp[num].bossToggle.interactable = true;
			playerPrefs_Players.Players_SetUp[num].lock_Panel.SetActive(false);
			playerPrefs_Players.Players_SetUp[num].bossInAppShop.gameObject.SetActive(false);
			PlayerPrefs_Saves.SaveConsumables(-price);
			PlayerPrefs_Players.UnLockPlayer(playerPrefs_Players.Players_SetUp[num].columnName);
			PlayerPrefs.Save();
			SetShopGui(num);
			ui_Manager.SetConsumablesTexts();
			audio_Manager.BossBuySucceed();
		}
		else
		{
			ui_Manager.OpenToast("You need more gems");
			audio_Manager.BossBuyFailed();
		}
	}
}
