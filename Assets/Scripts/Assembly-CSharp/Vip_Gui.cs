using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Vip_Gui
{
	[Header("Canvas")]
	public Canvas thisCanvas;

	public GraphicRaycaster thisGraphicRaycaster;

	[Header("Canvas Groups")]
	public CanvasGroup thisCanvasGroup;

	[Header("Buttons")]
	public Button subscription_Btn;

	public Button privacyPolicy_Btn;

	public Button termsOfUse_Btn;

	public Button close_Btn;

	[Header("Texts")]
	public Text subscriptionButton_Text;

	public Text subscriptionInfo_Text;

	public void ResetAnimations()
	{
	}

	public void SetCanvas(bool state)
	{
		thisCanvas.enabled = state;
		thisGraphicRaycaster.enabled = state;
	}
}
