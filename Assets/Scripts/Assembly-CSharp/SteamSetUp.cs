using System;
using TMPro;
using UnityEngine;

[Serializable]
public class SteamSetUp
{
	[Header("GOs Inactive")]
	public GameObject[] GOsInactive;

	[Header("GOs Active")]
	public GameObject[] GOsActive;

	[Header("Replace Texts")]
	public TextMeshProUGUI ReplaceGold_Txt;

	public TextMeshProUGUI ReplaceArmyNumber_Txt;
}
