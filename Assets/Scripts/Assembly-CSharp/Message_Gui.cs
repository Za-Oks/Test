using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Message_Gui
{
	[Header("Canvas")]
	public Canvas thisCanvas;

	public GraphicRaycaster thisGraphicRaycaster;

	[Header("Canvas Groups")]
	public CanvasGroup thisCanvasGroup;

	[Header("Buttons")]
	public Button ok_Btn;

	[Header("Texts")]
	public TextMeshProUGUI Message_Txt;

	public void SetCanvas(bool state)
	{
		thisCanvas.enabled = state;
		thisGraphicRaycaster.enabled = state;
	}
}
