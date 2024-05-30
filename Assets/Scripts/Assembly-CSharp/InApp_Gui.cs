using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class InApp_Gui
{
	[Header("Canvas")]
	public Canvas thisCanvas;

	public GraphicRaycaster thisGraphicRaycaster;

	[Header("Canvas Groups")]
	public CanvasGroup thisCanvasGroup;

	[Header("In App Buttons")]
	public Button unlockCombo_SpartanSentinel;

	public Button unlockCombo_SpartanSentinelSamurai;

	public Button unlockCombo_SpartanSentinelSamuraiFireDragon;

	public Button unlockCombo_SpartanSentinelSamuraiFireDragonMinigun;

	public Button unlockCombo_SpartanSentinelSamuraiFireDragonMinigunRhino;

	public Button unlockCombo_SpartanSentinelSamuraiFireDragonMinigunRhinoGuardian;

	public Button unlockCombo_SpartanSentinelSamuraiFireDragonMinigunRhinoGuardianWolf;

	public Button unlock_Spartan;

	public Button unlock_Sentinel;

	public Button unlock_Samurai;

	public Button unlock_FireDragon;

	public Button unlock_Minigun;

	public Button unlock_Rhino;

	public Button unlock_Guardian;

	public Button unlock_Wolf;

	public Button gempack_2500;

	public Button gempack_7000;

	public Button gempack_15000;

	public Button gempack_50000;

	public Button noAds_Btn;

	[Header("Buttons")]
	public Button restorePurchases_Btn;

	public Button back_Btn;

	public void SetCanvas(bool state)
	{
		thisCanvas.enabled = state;
		thisGraphicRaycaster.enabled = state;
	}
}
