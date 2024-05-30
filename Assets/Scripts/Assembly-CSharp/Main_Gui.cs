using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Main_Gui
{
	[Header("Canvas")]
	public Canvas thisCanvas;

	public GraphicRaycaster thisGraphicRaycaster;

	[Header("Panels")]
	public GameObject canvas;

	public GameObject canvas_Loading;

	public GameObject tutorialPanel;

	[Header("Canvas Groups")]
	public CanvasGroup thisCanvasGroup;

	[Header("Buttons")]
	public Button playLevels_Btn;

	public Button playEpicLevels_Btn;

	public Button playEpicLevelsInfo_Btn;

	public Button playCustom_Btn;

	public Button playMultiplayer_Btn;

	public Button openTutorialPanel_Btn;

	public Button closeTutorialPanel_Btn;

	public Button startTutorialPanel_Btn;

	public Button settings_Btn;

	public Button leaderboard_Btn;

	public Button shop_Btn;

	public Button upgradeStatsValues_Btn;

	public Button inApp_Btn;

	public Button vip_Btn;

	public Button crossPromotion_Btn;

	public void SetCanvas(bool state)
	{
		thisCanvas.enabled = state;
		thisGraphicRaycaster.enabled = state;
	}
}
