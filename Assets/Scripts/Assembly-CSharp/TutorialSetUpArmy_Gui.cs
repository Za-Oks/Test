using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class TutorialSetUpArmy_Gui
{
	[Header("Panels")]
	public GameObject thisPanel;

	public GameObject loadingPanel;

	public GameObject meleePanel;

	public GameObject rangedPanel;

	public GameObject cavalryPanel;

	public GameObject heavyPanel;

	public GameObject specialPanel;

	public GameObject epicPanel;

	[Header("RectTransforms")]
	public RectTransform Info_Rect;

	[Header("Canvas Groups")]
	public CanvasGroup thisCanvasGroup;

	[Header("Buttons")]
	public Button startGame_Btn;

	public Button placeRemoveArmy_Btn;

	public Button info_Btn;

	public Button skip_Btn;

	[Header("Toggles")]
	public Toggle Melee_Toggle;

	public Toggle Ranged_Toggle;

	public Toggle Cavalry_Toggle;

	public Toggle Heavy_Toggle;

	public Toggle Special_Toggle;

	public Toggle Epic_Toggle;

	[Header("Texts")]
	public TextMeshProUGUI gold_Txt;

	public TextMeshProUGUI armyNumber_Txt;

	[Header("Stas Texts")]
	public TextMeshProUGUI troopInfo_Txt;

	public TextMeshProUGUI troopHealth_Txt;

	public TextMeshProUGUI troopDamage_Txt;

	public TextMeshProUGUI troopAttackSpeed_Txt;

	public TextMeshProUGUI troopMovementSpeed_Txt;

	[Header("Values")]
	public Sprite placeArmy;

	public Sprite removeArmy;

	public Sprite inInfo;

	public Sprite outInfo;

	public Vector3 StartPosition { get; set; }

	public Vector3 LeftOutPosition { get; set; }

	public void InitializeValues()
	{
		CanvasScaler componentInChildren = Info_Rect.root.GetComponentInChildren<CanvasScaler>(true);
		Vector2 size = Info_Rect.rect.size;
		float num = 5f;
		float num2 = (float)Screen.width / componentInChildren.referenceResolution.x;
		StartPosition = new Vector3(Info_Rect.position.x, Info_Rect.position.y, Info_Rect.position.z);
		LeftOutPosition = new Vector3((0f - (size.x + num)) * num2, StartPosition.y, StartPosition.z);
		Info_Rect.parent.parent.gameObject.SetActive(true);
	}
}
