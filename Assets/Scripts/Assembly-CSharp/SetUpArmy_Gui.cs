using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class SetUpArmy_Gui
{
	[Header("Canvas")]
	public Canvas thisCanvas;

	public GraphicRaycaster thisGraphicRaycaster;

	[Header("Panels")]
	public GameObject LockedPanel;

	public GameObject PlayLevels_UIs;

	public GameObject PlayCustom_UIs;

	public GameObject PlayMultiplayer_UIs;

	public GameObject Levels_UIs;

	public GameObject EpicLevels_UIs;

	public GameObject meleePanel;

	public GameObject rangedPanel;

	public GameObject cavalryPanel;

	public GameObject heavyPanel;

	public GameObject specialPanel;

	public GameObject epicPanel;

	public GameObject preventTapPanel;

	[Header("Canvas Groups")]
	public CanvasGroup thisCanvasGroup;

	[Header("Buttons")]
	public Button setEnemyArmy_Btn;

	public Button startGame_Btn;

	public Button placeRemoveArmy_Btn;

	public Button playFirstIncomplete_Btn;

	public Button nextLevel_Btn;

	public Button previousLevel_Btn;

	public Button resetArmy_Btn;

	public Button changeLevelDesign_Btn;

	public Button info_Reward_Btn;

	public Button back_Btn;

	[Header("Buttons Bonus")]
	public Button bonusGold_Btn;

	public Button bonusArmy_Btn;

	[Header("Toggles")]
	public Toggle Melee_Toggle;

	public Toggle Ranged_Toggle;

	public Toggle Cavalry_Toggle;

	public Toggle Heavy_Toggle;

	public Toggle Special_Toggle;

	public Toggle Epic_Toggle;

	[Header("Images")]
	public Image levelDesign_Img;

	[Header("Texts")]
	public TextMeshProUGUI level_Txt;

	public TextMeshProUGUI gold_Txt;

	public TextMeshProUGUI armyNumber_Txt;

	public TextMeshProUGUI epicGold_Txt;

	public TextMeshProUGUI epicArmyNumber_Txt;

	public TextMeshProUGUI bonusGold_Txt;

	public TextMeshProUGUI bonusArmy_Txt;

	public TextMeshProUGUI armyBlueNumber_Txt;

	public TextMeshProUGUI armyRedNumber_Txt;

	public TextMeshProUGUI armyBlueGold_Txt;

	public TextMeshProUGUI armyRedGold_Txt;

	[Header("Multiplayer Stuff")]
	public TextMeshProUGUI multiPlayerName_Txt;

	public TextMeshProUGUI multiEnemyName_Txt;

	public TextMeshProUGUI multiAvailableBlueGold_Txt;

	public TextMeshProUGUI multiAvailableRedGold_Txt;

	public TextMeshProUGUI multiBlueArmy_Txt;

	public TextMeshProUGUI multiRedArmy_Txt;

	public TextMeshProUGUI timer_Txt;

	public Image bg;

	public Image timer_Img;

	public Sprite playerReadyBG;

	public Sprite enemyReadyBG;

	public Sprite bothReadyBG;

	public Sprite normalBG;

	[Header("Values")]
	public GameObject bonusGoldChecked;

	public GameObject bonusArmyChecked;

	public GameObject bonusGoldNA;

	public GameObject bonusArmyNA;

	public Sprite placeArmy;

	public Sprite removeArmy;

	public void SetCanvas(bool state)
	{
		thisCanvas.enabled = state;
		thisGraphicRaycaster.enabled = state;
	}
}
