using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class RateInfo_Gui
{
	[Header("Panels")]
	public GameObject thisPanel;

	[Header("Canvas Groups")]
	public CanvasGroup thisCanvasGroup;

	[Header("Buttons")]
	public Button ok_Btn;

	public Button close_Btn;

	[Header("Texts")]
	public TextMeshProUGUI title_Txt;

	public TextMeshProUGUI description_Txt;
}
