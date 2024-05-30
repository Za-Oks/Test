using DG.Tweening;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
	[Header("REFERENCES")]
	public CanvasGroup canvasGroup;

	[Header("CAMERA")]
	public Camera_SetupArmy camera_SetupArmy;

	[Header("JOYSTICK")]
	public GameObject joystickPanel;

	public GameObject joystickSteamPanel;

	public Joystick_Single joystick_Single;

	[Header("ZOOM")]
	public GameObject zoomPanel;

	public GameObject zoomSteamPanel;

	public TouchPanel_SetupArmy touchPanel_SetupArmy;

	[Header("PUT ARMY")]
	public GameObject putArmyPanel;

	[Header("START BATTLE")]
	public GameObject startBattlePanel;

	public TutorialUi_Manager uimanager;

	[Header("COMPLETED")]
	public GameObject completedPanel;

	[Header("FREE LOOK CAMERA")]
	public GameObject freeLookPanel;

	public GameObject freeLookSteamPanel;

	private int step;

	private PackagesManager packagesManager;

	private void Awake()
	{
		packagesManager = GameObject.FindWithTag("PackagesManager").GetComponent<PackagesManager>();
	}

	private void Start()
	{
		NextStep();
	}

	private void Update()
	{
	}

	public void NextStep()
	{
		canvasGroup.DOFade(0f, 0.5f).OnComplete(delegate
		{
			NextStepLogic();
			canvasGroup.DOFade(1f, 0.5f);
		});
	}

	private void NextStepLogic()
	{
		step++;
		if (step == 1)
		{
			StartStep_Joystick();
		}
		else if (step == 2)
		{
			StopStep_Joystick();
			StartStep_Zoom();
		}
		else if (step == 3)
		{
			StopStep_Zoom();
			StartStep_PutArmy();
		}
		else if (step == 4)
		{
			StopStep_PutArmy();
			StartStep_StartBattle();
		}
		else if (step == 5)
		{
			StopStep_StartBattle();
			StartStep_FreeLookCamera();
		}
		else if (step == 6)
		{
			StopStep_FreeLookCamera();
		}
		else if (step == 7)
		{
			StartStep_Completed();
			Invoke("Completed", 1.5f);
		}
	}

	private void Completed()
	{
		uimanager.LoadMainScene();
	}

	private void StartStep_Joystick()
	{
		if (packagesManager.whichStore != App_Stores.Steam)
		{
			joystickPanel.SetActive(true);
		}
		else
		{
			joystickSteamPanel.SetActive(true);
		}
		joystick_Single.isTutorialStep = true;
		camera_SetupArmy.InitValues();
		camera_SetupArmy.canPlay = true;
	}

	private void StopStep_Joystick()
	{
		joystick_Single.isTutorialStep = false;
		if (packagesManager.whichStore != App_Stores.Steam)
		{
			joystickPanel.SetActive(false);
		}
		else
		{
			joystickSteamPanel.SetActive(false);
		}
	}

	private void StartStep_Zoom()
	{
		if (packagesManager.whichStore != App_Stores.Steam)
		{
			zoomPanel.SetActive(true);
		}
		else
		{
			zoomSteamPanel.SetActive(true);
		}
		touchPanel_SetupArmy.blockZoom = false;
		touchPanel_SetupArmy.isTutorialStep_Zoom = true;
	}

	private void StopStep_Zoom()
	{
		touchPanel_SetupArmy.isTutorialStep_Zoom = false;
		if (packagesManager.whichStore != App_Stores.Steam)
		{
			zoomPanel.SetActive(false);
		}
		else
		{
			zoomSteamPanel.SetActive(false);
		}
	}

	private void StartStep_PutArmy()
	{
		putArmyPanel.SetActive(true);
		touchPanel_SetupArmy.blockPutArmy = false;
		touchPanel_SetupArmy.isTutorialStep_PutArmy = true;
	}

	private void StopStep_PutArmy()
	{
		touchPanel_SetupArmy.isTutorialStep_PutArmy = false;
		putArmyPanel.SetActive(false);
	}

	private void StartStep_StartBattle()
	{
		startBattlePanel.SetActive(true);
		uimanager.isTutorialStep = true;
	}

	private void StopStep_StartBattle()
	{
		uimanager.isTutorialStep = false;
		startBattlePanel.SetActive(false);
	}

	private void StartStep_Completed()
	{
		completedPanel.SetActive(true);
	}

	private void StopStep_Completed()
	{
		completedPanel.SetActive(false);
	}

	private void StartStep_FreeLookCamera()
	{
		if (packagesManager.whichStore != App_Stores.Steam)
		{
			freeLookPanel.SetActive(true);
		}
		else
		{
			freeLookSteamPanel.SetActive(true);
		}
		Invoke("NextStep", 3f);
	}

	private void StopStep_FreeLookCamera()
	{
		if (packagesManager.whichStore != App_Stores.Steam)
		{
			freeLookPanel.SetActive(false);
		}
		else
		{
			freeLookSteamPanel.SetActive(false);
		}
	}
}
