using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class LeaderBoard_Gui
{
	public class LeaderBoardPlayer
	{
		public TextMeshProUGUI number_Txt;

		public TextMeshProUGUI name_Txt;

		public TextMeshProUGUI trophy_Txt;

		public Text battlesWon_Txt;

		public Text battlesLost_Txt;
	}

	[Header("Canvas")]
	public Canvas thisCanvas;

	public GraphicRaycaster thisGraphicRaycaster;

	[Header("Panels")]
	public GameObject namesPanel;

	public GameObject loadingPanel;

	[Header("Canvas Groups")]
	public CanvasGroup thisCanvasGroup;

	[Header("Buttons")]
	public Button back_Btn;

	[Header("Values")]
	public Transform[] LeaderBoardPanels;

	public LeaderBoardPlayer[] LeaderBoardPlayers;

	public void InitLeaderBoardPlayers()
	{
		int num = LeaderBoardPanels.Length;
		LeaderBoardPlayers = new LeaderBoardPlayer[num];
		for (int i = 0; i < num; i++)
		{
			LeaderBoardPlayer leaderBoardPlayer = new LeaderBoardPlayer();
			leaderBoardPlayer.number_Txt = LeaderBoardPanels[i].Find("Number_Txt").GetComponent<TextMeshProUGUI>();
			leaderBoardPlayer.name_Txt = LeaderBoardPanels[i].Find("Name_Txt").GetComponent<TextMeshProUGUI>();
			leaderBoardPlayer.trophy_Txt = LeaderBoardPanels[i].Find("Background_Trophy").Find("Trophy_Txt").GetComponent<TextMeshProUGUI>();
			leaderBoardPlayer.battlesWon_Txt = LeaderBoardPanels[i].Find("BattlesWon_Txt").GetComponent<Text>();
			leaderBoardPlayer.battlesLost_Txt = LeaderBoardPanels[i].Find("BattlesLost_Txt").GetComponent<Text>();
			LeaderBoardPlayers[i] = leaderBoardPlayer;
		}
	}

	public void SetCanvas(bool state)
	{
		thisCanvas.enabled = state;
		thisGraphicRaycaster.enabled = state;
	}
}
