using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Settings_Gui
{
	[Header("Canvas")]
	public Canvas thisCanvas;

	public GraphicRaycaster thisGraphicRaycaster;

	[Header("Panels")]
	public GameObject settingsContent;

	public GameObject levelstatsContent;

	public GameObject cloudSavePanel;

	public GameObject cloudSaveFirstPanel;

	public GameObject cloudSaveWaitPanel;

	[Header("Canvas Groups")]
	public CanvasGroup thisCanvasGroup;

	[Header("Buttons")]
	public Button facebook_Btn;

	public Button twitter_Btn;

	public Button achievemetns_Btn;

	public Button leaderboards_Btn;

	public Button back_Btn;

	public Button cloud_Btn;

	public Button cloudBack_Btn;

	public Button cloudSave_Btn;

	public Button cloudLoad_Btn;

	public Button terms_Btn;

	[Header("Toggles")]
	public Toggle settings_Toggle;

	public Toggle levelstats_Toggle;

	public Toggle music_Toggle;

	public Toggle sound_Toggle;

	[Header("Texts")]
	public TextMeshProUGUI social_Txt;

	[Header("Values")]
	public Sprite musicIsOn;

	public Sprite musicIsOff;

	public Sprite soundIsOn;

	public Sprite soundIsOff;

	public void SetCanvas(bool state)
	{
		thisCanvas.enabled = state;
		thisGraphicRaycaster.enabled = state;
	}
}
