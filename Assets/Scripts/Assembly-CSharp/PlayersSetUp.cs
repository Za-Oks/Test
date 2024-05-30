using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PlayersSetUp
{
	[Header("Values")]
	public string columnName;

	public string status;

	public int price;

	[Header("Component Values")]
	public GameObject player;

	public Toggle bossToggle;

	public Button bossInAppShop;

	[HideInInspector]
	public GameObject lock_Panel;

	[HideInInspector]
	public Button lockClick_Btn;

	[HideInInspector]
	public Image toggle_Img;

	public int index { get; set; }

	public void InitValues(Ui_Manager ui_Manager, Audio_Manager audio_Manager)
	{
		lock_Panel = bossToggle.transform.Find("Lock_Panel").gameObject;
		lockClick_Btn = lock_Panel.transform.Find("LockClick_Btn").GetComponent<Button>();
		toggle_Img = bossToggle.GetComponent<Image>();
		lockClick_Btn.onClick.AddListener(delegate
		{
			ui_Manager.FailBuyShop(toggle_Img);
			ui_Manager.OpenToast("Locked");
			audio_Manager.BossBuyFailed();
		});
	}
}
