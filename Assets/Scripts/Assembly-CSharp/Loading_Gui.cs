using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Loading_Gui
{
	[Header("Canvas")]
	public Canvas thisCanvas;

	public GraphicRaycaster thisGraphicRaycaster;

	[Header("Canvas Groups")]
	public CanvasGroup thisCanvasGroup;

	[Header("Values")]
	public float Loading_Delay;

	public void SetCanvas(bool state)
	{
		thisCanvas.enabled = state;
		thisGraphicRaycaster.enabled = state;
	}
}
