using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class GameOver_Gui
{
	[Header("Canvas")]
	public Canvas thisCanvas;

	public GraphicRaycaster thisGraphicRaycaster;

	[Header("Panels")]
	public GameObject starsPanel;

	public GameObject trophiesParentPanel;

	public GameObject trophiesLoadingPanel;

	public GameObject trophiesPanel;

	[Header("RectTransforms")]
	public RectTransform thisRect;

	public RectTransform resultsRect;

	[Header("Canvas Groups")]
	public CanvasGroup thisCanvasGroup;

	public CanvasGroup battleCompletedCanvasGroup;

	[Header("Buttons")]
	public Button restart_Btn;

	public Button menu_Btn;

	public Button rate_Btn;

	public Button share_Btn;

	public Button facebook_Btn;

	public Button twitter_Btn;

	public Button consumables_Btn;

	[Header("Texts")]
	public TextMeshProUGUI result_Txt;

	public TextMeshProUGUI consumables_Txt;

	public TextMeshProUGUI consumablesReward_Txt;

	public TextMeshProUGUI consumablesRewardButton_Txt;

	public TextMeshProUGUI trophies_Txt;

	[Header("Images")]
	public Image scorePanelImage;

	[Header("Sprites")]
	public Sprite nextLvL_image;

	public Sprite playAgain_image;

	[Header("Values")]
	[Range(0f, 1f)]
	public float alphaAdButtons;

	public Color32 normal;

	public Color32 redTeamWin;

	public Color32 blueTeamWin;

	private int minReward = 100;

	private int maxReward = 350;

	[HideInInspector]
	public int finalReward;

	public void SetRewardNumber(int reward, int rewardExtra)
	{
		finalReward = reward + rewardExtra;
		if (finalReward < minReward)
		{
			finalReward = minReward;
		}
		else if (finalReward > maxReward)
		{
			finalReward = maxReward;
		}
		consumablesRewardButton_Txt.text = "+" + finalReward;
	}

	public void SetCanvas(bool state)
	{
		thisCanvas.enabled = state;
		thisGraphicRaycaster.enabled = state;
	}
}
