using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class MultiplayerConnect_Gui
{
	[Header("Canvas")]
	public Canvas thisCanvas;

	public GraphicRaycaster thisGraphicRaycaster;

	[Header("Panels")]
	public GameObject counterPanel;

	public GameObject rankingPanel;

	public GameObject infoPanel;

	public GameObject loadingPanel;

	public GameObject exclamationImg;

	[Header("Canvas Groups")]
	public CanvasGroup thisCanvasGroup;

	public CanvasGroup nickNameCanvasGroup;

	public CanvasGroup userNameCanvasGroup;

	public CanvasGroup passwordCanvasGroup;

	[Header("Buttons")]
	public Button ready_Btn;

	public Button dailyReward_Btn;

	public Button back_Btn;

	public Button cancel_Btn;

	public Button showPassword_Btn;

	public Button info_Btn;

	[Header("Images")]
	public Image[] StarImages;

	public Image showPassword_Image;

	[Header("Texts")]
	public InputField nickNameInput;

	public InputField userNameInput;

	public InputField passwordInput;

	public TextMeshProUGUI rankingText;

	public TextMeshProUGUI priceToPlay_Txt;

	public TextMeshProUGUI consumables_Txt;

	public TextMeshProUGUI dailyRewardWait_Txt;

	[Header("Values")]
	public Sprite star_On;

	public Sprite star_Off;

	public Sprite showPassOn;

	public Sprite showPassOff;

	public int priceToPlay;

	public void InitValues()
	{
		priceToPlay_Txt.text = "-" + priceToPlay;
		PlayerPrefs_Saves.LoadConsumables();
	}

	public void SetCanvas(bool state)
	{
		thisCanvas.enabled = state;
		thisGraphicRaycaster.enabled = state;
	}
}
