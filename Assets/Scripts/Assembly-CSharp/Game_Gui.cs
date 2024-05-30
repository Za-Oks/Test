using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Game_Gui
{
	[Header("Canvas")]
	public Canvas thisCanvas;

	public GraphicRaycaster thisGraphicRaycaster;

	[Header("Panels")]
	public GameObject versusPanel;

	public GameObject cameraBestPanel;

	public GameObject cameraHeadPanel;

	public GameObject surrenderPanel;

	[Header("Canvas Groups")]
	public CanvasGroup thisCanvasGroup;

	[Header("Buttons")]
	public Button pause_Btn;

	public Button surrender_Btn;

	public Button confirm_surrender_Btn;

	public Button refuse_surrender_Btn;

	[Header("Texts")]
	public TextMeshProUGUI playerVS_Txt;

	public TextMeshProUGUI enemyVS_Txt;

	[Header("Joysticks")]
	public Joystick_Single cameraBest_JoystickMove;

	public Joystick_Single cameraSetup_JoystickMove;

	[Header("GameObjects")]
	public GameObject cameraBest_GameObject;

	public GameObject cameraBestChild_GameObject;

	public GameObject cameraHead_GameObject;

	public GameObject cameraSetup_GameObject;

	public GameObject cameraSetup_Camera;

	[Header("Scripts")]
	public Camera_Best cameraBest;

	public Camera_Head cameraHead;

	public Camera_SetupArmy cameraSetup;

	[Header("Values")]
	public int rateLevelIndex;

	public Color colorHeadCamera;

	public void SetCanvas(bool state)
	{
		thisCanvas.enabled = state;
		thisGraphicRaycaster.enabled = state;
	}
}
