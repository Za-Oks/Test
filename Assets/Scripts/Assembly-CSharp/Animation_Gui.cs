using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Animation_Gui
{
	[Header("Canvas")]
	public Canvas thisCanvas;

	public GraphicRaycaster thisGraphicRaycaster;

	[Header("Canvas Groups")]
	public CanvasGroup thisCanvasGroup;

	public void SetCanvas(bool state)
	{
		thisCanvas.enabled = state;
		thisGraphicRaycaster.enabled = state;
	}
}
