using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UpgradeStatsValues_Gui
{
	[Header("Canvas")]
	public Canvas thisCanvas;

	public GraphicRaycaster thisGraphicRaycaster;

	[Header("Canvas Groups")]
	public CanvasGroup thisCanvasGroup;

	[Header("Shop Default Items")]
	public GameObject[] defaultItems;

	[Header("Shop Mask Items")]
	public ScrollRect maskScroll;

	public RectTransform scrollView;

	public RectTransform panel1_Row1;

	public RectTransform panel2_Row1;

	public RectTransform panel3_Row1;

	public RectTransform panel1_Row2;

	[Header("Buttons")]
	public Button back_Btn;

	[Header("Texts")]
	public TextMeshProUGUI title_Txt;

	public TextMeshProUGUI consumables_Txt;

	[Header("Values")]
	public Color failBuyStart;

	public Color failBuyTarget;

	public Vector2 Size { get; set; }

	public float DiffY { get; set; }

	public float panel1PosX { get; set; }

	public float panel2PosX { get; set; }

	public float panel3PosX { get; set; }

	public float panelY { get; set; }

	public void InitializeButtonsValues()
	{
		Size = panel1_Row1.rect.size;
		DiffY = Mathf.Abs(panel1_Row2.localPosition.y - panel1_Row1.localPosition.y);
		panel1PosX = panel1_Row1.localPosition.x;
		panel2PosX = panel2_Row1.localPosition.x;
		panel3PosX = panel3_Row1.localPosition.x;
		panelY = panel1_Row1.localPosition.y;
	}

	public void CloseDefault_Items()
	{
		GameObject[] array = defaultItems;
		foreach (GameObject gameObject in array)
		{
			gameObject.SetActive(false);
		}
	}

	public void SetCanvas(bool state)
	{
		thisCanvas.enabled = state;
		thisGraphicRaycaster.enabled = state;
	}
}
