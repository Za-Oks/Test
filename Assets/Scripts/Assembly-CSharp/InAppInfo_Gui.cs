using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class InAppInfo_Gui
{
	[Header("Panels")]
	public GameObject thisPanel;

	public GameObject rewardPanel;

	public GameObject infoAdsPanel;

	[Header("Canvas Groups")]
	public CanvasGroup thisCanvasGroup;

	[Header("Buttons")]
	public Button ok_Btn;

	public Button info_Reward_Btn;

	[Header("Texts")]
	public TextMeshProUGUI title_Txt;

	public TextMeshProUGUI description_Txt;

	public TextMeshProUGUI reward_Txt;
}
