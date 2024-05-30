using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Shop_Gui
{
	[Header("Canvas")]
	public Canvas thisCanvas;

	public GraphicRaycaster thisGraphicRaycaster;

	[Header("Panels")]
	public GameObject buyPanel;

	public GameObject infoBossPanel;

	[Header("Canvas Groups")]
	public CanvasGroup thisCanvasGroup;

	[Header("Buttons")]
	public Button next_Btn;

	public Button previous_Btn;

	public Button buy_Btn;

	public Button infoBoss_Btn;

	public Button infoBossBack_Btn;

	public Button facebookReward_Btn;

	public Button twitterReward_Btn;

	public Button back_Btn;

	[Header("Boss in-app Buttons")]
	public Button spartanInApp_Btn;

	public Button sentinelnInApp_Btn;

	public Button samuraiInApp_Btn;

	public Button fireDragonInApp_Btn;

	public Button minigunInApp_Btn;

	public Button rhinoInApp_Btn;

	public Button guardianInApp_Btn;

	public Button wolfInApp_Btn;

	[Header("Boss Info in-app Texts")]
	public TextMeshProUGUI troopBossInfo_Txt;

	public TextMeshProUGUI troopBossHealth_Txt;

	public TextMeshProUGUI troopBossDamage_Txt;

	public TextMeshProUGUI troopBossAttackSpeed_Txt;

	public TextMeshProUGUI troopBossMovementSpeed_Txt;

	public TextMeshProUGUI troopBossHitDistance_Txt;

	public TextMeshProUGUI troopBossPenetration_Txt;

	[Header("Texts")]
	public TextMeshProUGUI consumables_Txt;

	public TextMeshProUGUI price_Txt;

	[Header("Values")]
	public int rewardSocialMedia;

	public void SetCanvas(bool state)
	{
		thisCanvas.enabled = state;
		thisGraphicRaycaster.enabled = state;
	}
}
