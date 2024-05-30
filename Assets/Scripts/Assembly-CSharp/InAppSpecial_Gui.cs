using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class InAppSpecial_Gui
{
	[Header("Canvas")]
	public Canvas thisCanvas;

	public GraphicRaycaster thisGraphicRaycaster;

	[Header("Canvas Groups")]
	public CanvasGroup thisCanvasGroup;

	[Header("In App Buttons")]
	public Button unlockSpecial_FireDragon_MiniGun_Btn;

	public Button UnlockSpecial_FireDragon_Rhino_Btn;

	public Button UnlockSpecial_FireDragon_Minigun_Rhino_Btn;

	public Button unlockSpecial_5000Gems_Btn;

	public Button unlockSpecial_40000Gems_Btn;

	[Header("Buttons")]
	public Button back_Btn;

	public void SetCanvas(bool state)
	{
		thisCanvas.enabled = state;
		thisGraphicRaycaster.enabled = state;
	}
}
