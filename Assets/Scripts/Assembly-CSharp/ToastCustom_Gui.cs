using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ToastCustom_Gui
{
	[Header("Canvas")]
	public Canvas thisCanvas;

	public GraphicRaycaster thisGraphicRaycaster;

	[Header("Canvas Groups")]
	public CanvasGroup thisCanvasGroup;

	[Header("Texts")]
	public TextMeshProUGUI toastMessage_Txt;

	public void SetCanvas(bool state)
	{
		thisCanvas.enabled = state;
		thisGraphicRaycaster.enabled = state;
	}
}
