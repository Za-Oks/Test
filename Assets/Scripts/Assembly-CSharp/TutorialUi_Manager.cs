using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialUi_Manager : MonoBehaviour
{
	[Header("References")]
	public Level_Manager level_Manager;

	public Audio_Manager audio_Manager;

	public GameController gc;

	public AudioBattleManager audioBattleManager;

	[Header("Main Guis")]
	public TutorialSetUpArmy_Gui tutorialSetUpArmy_Gui;

	public Game_Gui game_Gui;

	[Header("Tutorial")]
	public TutorialManager tutorialManager;

	[HideInInspector]
	public bool isTutorialStep;

	private float tempArmyTogglesAlpha = 0.5f;

	private float time = 0.25f;

	private bool infoState;

	private CustomCameraType currentCamera;

	private CustomCameraType lastCamera;

	private void Start()
	{
		InitCameras();
		CanvasInitialize();
	}

	private void CanvasInitialize()
	{
		tutorialSetUpArmy_Gui.InitializeValues();
		ResetInfoPanel();
		ResetPlaceRemoveBtn();
		InitArmiesUIs();
		tutorialSetUpArmy_Gui.startGame_Btn.onClick.AddListener(delegate
		{
			if (isTutorialStep)
			{
				StartGame();
			}
			audio_Manager.MenuClick();
		});
		tutorialSetUpArmy_Gui.placeRemoveArmy_Btn.onClick.AddListener(delegate
		{
			PlaceRemoveArmy();
			audio_Manager.MenuClick();
		});
		tutorialSetUpArmy_Gui.info_Btn.onClick.AddListener(delegate
		{
			InfoPanelAnimation();
			audio_Manager.MenuClick();
		});
		tutorialSetUpArmy_Gui.skip_Btn.onClick.AddListener(delegate
		{
			LoadMainScene();
			audio_Manager.MenuClick();
		});
		tutorialSetUpArmy_Gui.Melee_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			MeleeArmyUIs(status);
			audio_Manager.MenuClick();
		});
		tutorialSetUpArmy_Gui.Ranged_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			RangedArmyUIs(status);
			audio_Manager.MenuClick();
		});
		tutorialSetUpArmy_Gui.Cavalry_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			CavalryArmyUIs(status);
			audio_Manager.MenuClick();
		});
		tutorialSetUpArmy_Gui.Heavy_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			HeavyArmyUIs(status);
			audio_Manager.MenuClick();
		});
		tutorialSetUpArmy_Gui.Special_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			SpecialArmyUIs(status);
			audio_Manager.MenuClick();
		});
		tutorialSetUpArmy_Gui.Epic_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			EpicArmyUIs(status);
			audio_Manager.MenuClick();
		});
		level_Manager.AutoAction_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			AutoAction(status);
			audio_Manager.MenuClick();
		});
		level_Manager.Archer.army_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			if (status)
			{
				level_Manager.SetValuesArmyInspector(level_Manager.Archer);
				audio_Manager.MenuClick();
			}
		});
		level_Manager.CannonMan.army_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			if (status)
			{
				level_Manager.SetValuesArmyInspector(level_Manager.CannonMan);
				audio_Manager.MenuClick();
			}
		});
		level_Manager.Gladiator.army_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			if (status)
			{
				level_Manager.SetValuesArmyInspector(level_Manager.Gladiator);
				audio_Manager.MenuClick();
			}
		});
		level_Manager.Knight.army_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			if (status)
			{
				level_Manager.SetValuesArmyInspector(level_Manager.Knight);
				audio_Manager.MenuClick();
			}
		});
		level_Manager.Man.army_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			if (status)
			{
				level_Manager.SetValuesArmyInspector(level_Manager.Man);
				audio_Manager.MenuClick();
			}
		});
		level_Manager.CatapultMan.army_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			if (status)
			{
				level_Manager.SetValuesArmyInspector(level_Manager.CatapultMan);
				audio_Manager.MenuClick();
			}
		});
		level_Manager.Musketeer.army_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			if (status)
			{
				level_Manager.SetValuesArmyInspector(level_Manager.Musketeer);
				audio_Manager.MenuClick();
			}
		});
		level_Manager.AxeMan.army_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			if (status)
			{
				level_Manager.SetValuesArmyInspector(level_Manager.AxeMan);
				audio_Manager.MenuClick();
			}
		});
		level_Manager.ShieldMan.army_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			if (status)
			{
				level_Manager.SetValuesArmyInspector(level_Manager.ShieldMan);
				audio_Manager.MenuClick();
			}
		});
		level_Manager.SpearMan.army_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			if (status)
			{
				level_Manager.SetValuesArmyInspector(level_Manager.SpearMan);
				audio_Manager.MenuClick();
			}
		});
		level_Manager.Giant.army_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			if (status)
			{
				level_Manager.SetValuesArmyInspector(level_Manager.Giant);
				audio_Manager.MenuClick();
			}
		});
		level_Manager.Guard.army_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			if (status)
			{
				level_Manager.SetValuesArmyInspector(level_Manager.Guard);
				audio_Manager.MenuClick();
			}
		});
		level_Manager.Spartan.army_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			if (status)
			{
				level_Manager.SetValuesArmyInspector(level_Manager.Spartan);
				audio_Manager.MenuClick();
			}
		});
		level_Manager.Sentinel.army_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			if (status)
			{
				level_Manager.SetValuesArmyInspector(level_Manager.Sentinel);
				audio_Manager.MenuClick();
			}
		});
	}

	private void StartGame()
	{
		if (isTutorialStep)
		{
			isTutorialStep = false;
			tutorialManager.NextStep();
		}
		ChangeCamera(CustomCameraType.Best);
		level_Manager.SetPlayerQuad(false);
		level_Manager.SetEnemyQuad(false);
		level_Manager.ClosePlayerSpritesLvL();
		level_Manager.CloseEnemySpritesLvL();
		level_Manager.CloseLevelSpritesLvL();
		StopCoroutine("StartGame_CR");
		StartCoroutine("StartGame_CR");
	}

	private IEnumerator StartGame_CR()
	{
		tutorialSetUpArmy_Gui.thisCanvasGroup.alpha = 1f;
		tutorialSetUpArmy_Gui.thisCanvasGroup.interactable = false;
		float t = 0f;
		float lerpDuration = 0.5f;
		while (t < 1f)
		{
			t += Time.deltaTime / lerpDuration;
			tutorialSetUpArmy_Gui.thisCanvasGroup.alpha = Mathf.Lerp(1f, 0f, t);
			yield return null;
		}
		tutorialSetUpArmy_Gui.thisPanel.SetActive(false);
		game_Gui.thisCanvas.enabled = true;
		game_Gui.thisCanvasGroup.alpha = 1f;
		game_Gui.thisCanvasGroup.interactable = true;
		StartGameMode();
		gc.Begin();
	}

	private void StartGameMode()
	{
		if (level_Manager.gameMode == GameMode.Tutorial)
		{
			level_Manager.ResetBotList();
			level_Manager.ResetLevelsArmy();
			level_Manager.SpawnPlayerArmy();
			level_Manager.SpawnTutorialArmy();
			level_Manager.FinalizeBotList();
		}
	}

	private void InitArmiesUIs()
	{
		tutorialSetUpArmy_Gui.Melee_Toggle.GetComponent<CanvasGroup>().alpha = 1f;
		tutorialSetUpArmy_Gui.meleePanel.GetComponent<CanvasGroup>().alpha = 1f;
		tutorialSetUpArmy_Gui.meleePanel.SetActive(true);
		tutorialSetUpArmy_Gui.meleePanel.transform.SetAsLastSibling();
		tutorialSetUpArmy_Gui.Ranged_Toggle.GetComponent<CanvasGroup>().alpha = tempArmyTogglesAlpha;
		tutorialSetUpArmy_Gui.rangedPanel.GetComponent<CanvasGroup>().alpha = 0f;
		tutorialSetUpArmy_Gui.rangedPanel.SetActive(true);
		tutorialSetUpArmy_Gui.Cavalry_Toggle.GetComponent<CanvasGroup>().alpha = tempArmyTogglesAlpha;
		tutorialSetUpArmy_Gui.cavalryPanel.GetComponent<CanvasGroup>().alpha = 0f;
		tutorialSetUpArmy_Gui.cavalryPanel.SetActive(true);
		tutorialSetUpArmy_Gui.Heavy_Toggle.GetComponent<CanvasGroup>().alpha = tempArmyTogglesAlpha;
		tutorialSetUpArmy_Gui.heavyPanel.GetComponent<CanvasGroup>().alpha = 0f;
		tutorialSetUpArmy_Gui.heavyPanel.SetActive(true);
		tutorialSetUpArmy_Gui.Special_Toggle.GetComponent<CanvasGroup>().alpha = tempArmyTogglesAlpha;
		tutorialSetUpArmy_Gui.specialPanel.GetComponent<CanvasGroup>().alpha = 0f;
		tutorialSetUpArmy_Gui.specialPanel.SetActive(true);
		tutorialSetUpArmy_Gui.Epic_Toggle.GetComponent<CanvasGroup>().alpha = tempArmyTogglesAlpha;
		tutorialSetUpArmy_Gui.epicPanel.GetComponent<CanvasGroup>().alpha = 0f;
		tutorialSetUpArmy_Gui.epicPanel.SetActive(true);
		tutorialSetUpArmy_Gui.placeRemoveArmy_Btn.interactable = false;
	}

	private void ResetArmiesUIs()
	{
		tutorialSetUpArmy_Gui.Melee_Toggle.GetComponent<CanvasGroup>().alpha = tempArmyTogglesAlpha;
		tutorialSetUpArmy_Gui.meleePanel.GetComponent<CanvasGroup>().alpha = 0f;
		tutorialSetUpArmy_Gui.Ranged_Toggle.GetComponent<CanvasGroup>().alpha = tempArmyTogglesAlpha;
		tutorialSetUpArmy_Gui.rangedPanel.GetComponent<CanvasGroup>().alpha = 0f;
		tutorialSetUpArmy_Gui.Cavalry_Toggle.GetComponent<CanvasGroup>().alpha = tempArmyTogglesAlpha;
		tutorialSetUpArmy_Gui.cavalryPanel.GetComponent<CanvasGroup>().alpha = 0f;
		tutorialSetUpArmy_Gui.Heavy_Toggle.GetComponent<CanvasGroup>().alpha = tempArmyTogglesAlpha;
		tutorialSetUpArmy_Gui.heavyPanel.GetComponent<CanvasGroup>().alpha = 0f;
		tutorialSetUpArmy_Gui.Special_Toggle.GetComponent<CanvasGroup>().alpha = tempArmyTogglesAlpha;
		tutorialSetUpArmy_Gui.specialPanel.GetComponent<CanvasGroup>().alpha = 0f;
		tutorialSetUpArmy_Gui.Epic_Toggle.GetComponent<CanvasGroup>().alpha = tempArmyTogglesAlpha;
		tutorialSetUpArmy_Gui.epicPanel.GetComponent<CanvasGroup>().alpha = 0f;
	}

	private void MeleeArmyUIs(bool status)
	{
		if (status)
		{
			ResetArmiesUIs();
			tutorialSetUpArmy_Gui.Melee_Toggle.GetComponent<CanvasGroup>().alpha = 1f;
			tutorialSetUpArmy_Gui.meleePanel.GetComponent<CanvasGroup>().alpha = 1f;
			tutorialSetUpArmy_Gui.meleePanel.transform.SetAsLastSibling();
		}
	}

	private void RangedArmyUIs(bool status)
	{
		if (status)
		{
			ResetArmiesUIs();
			tutorialSetUpArmy_Gui.Ranged_Toggle.GetComponent<CanvasGroup>().alpha = 1f;
			tutorialSetUpArmy_Gui.rangedPanel.GetComponent<CanvasGroup>().alpha = 1f;
			tutorialSetUpArmy_Gui.rangedPanel.transform.SetAsLastSibling();
		}
	}

	private void CavalryArmyUIs(bool status)
	{
		if (status)
		{
			ResetArmiesUIs();
			tutorialSetUpArmy_Gui.Cavalry_Toggle.GetComponent<CanvasGroup>().alpha = 1f;
			tutorialSetUpArmy_Gui.cavalryPanel.GetComponent<CanvasGroup>().alpha = 1f;
			tutorialSetUpArmy_Gui.cavalryPanel.transform.SetAsLastSibling();
		}
	}

	private void HeavyArmyUIs(bool status)
	{
		if (status)
		{
			ResetArmiesUIs();
			tutorialSetUpArmy_Gui.Heavy_Toggle.GetComponent<CanvasGroup>().alpha = 1f;
			tutorialSetUpArmy_Gui.heavyPanel.GetComponent<CanvasGroup>().alpha = 1f;
			tutorialSetUpArmy_Gui.heavyPanel.transform.SetAsLastSibling();
		}
	}

	private void SpecialArmyUIs(bool status)
	{
		if (status)
		{
			ResetArmiesUIs();
			tutorialSetUpArmy_Gui.Special_Toggle.GetComponent<CanvasGroup>().alpha = 1f;
			tutorialSetUpArmy_Gui.specialPanel.GetComponent<CanvasGroup>().alpha = 1f;
			tutorialSetUpArmy_Gui.specialPanel.transform.SetAsLastSibling();
		}
	}

	private void EpicArmyUIs(bool status)
	{
		if (status)
		{
			ResetArmiesUIs();
			tutorialSetUpArmy_Gui.Epic_Toggle.GetComponent<CanvasGroup>().alpha = 1f;
			tutorialSetUpArmy_Gui.epicPanel.GetComponent<CanvasGroup>().alpha = 1f;
			tutorialSetUpArmy_Gui.epicPanel.transform.SetAsLastSibling();
		}
	}

	private void AutoAction(bool status)
	{
		if (status)
		{
			tutorialSetUpArmy_Gui.placeRemoveArmy_Btn.interactable = false;
		}
		else
		{
			tutorialSetUpArmy_Gui.placeRemoveArmy_Btn.interactable = true;
		}
	}

	private void PlaceRemoveArmy()
	{
		switch (level_Manager.ReturnPlaceAction())
		{
		case PlaceAction.Place:
			tutorialSetUpArmy_Gui.placeRemoveArmy_Btn.image.sprite = tutorialSetUpArmy_Gui.removeArmy;
			level_Manager.SetPlaceAction(PlaceAction.Remove);
			break;
		case PlaceAction.Remove:
			tutorialSetUpArmy_Gui.placeRemoveArmy_Btn.image.sprite = tutorialSetUpArmy_Gui.placeArmy;
			level_Manager.SetPlaceAction(PlaceAction.Place);
			break;
		}
	}

	private void ResetPlaceRemoveBtn()
	{
		tutorialSetUpArmy_Gui.placeRemoveArmy_Btn.image.sprite = tutorialSetUpArmy_Gui.placeArmy;
		level_Manager.SetPlaceAction(PlaceAction.Place);
	}

	public void SetPlaceRemoveImg(PlaceAction tempAction)
	{
		switch (tempAction)
		{
		case PlaceAction.Place:
			tutorialSetUpArmy_Gui.placeRemoveArmy_Btn.image.sprite = tutorialSetUpArmy_Gui.placeArmy;
			break;
		case PlaceAction.Remove:
			tutorialSetUpArmy_Gui.placeRemoveArmy_Btn.image.sprite = tutorialSetUpArmy_Gui.removeArmy;
			break;
		}
	}

	private void ResetInfoPanel()
	{
		infoState = true;
		tutorialSetUpArmy_Gui.Info_Rect.position = tutorialSetUpArmy_Gui.StartPosition;
		tutorialSetUpArmy_Gui.info_Btn.GetComponent<Image>().sprite = tutorialSetUpArmy_Gui.outInfo;
	}

	private void InfoPanelAnimation()
	{
		float duration = 0.4f;
		if (infoState)
		{
			tutorialSetUpArmy_Gui.Info_Rect.DOKill();
			tutorialSetUpArmy_Gui.Info_Rect.DOMove(tutorialSetUpArmy_Gui.LeftOutPosition, duration).SetEase(Ease.OutCirc);
			tutorialSetUpArmy_Gui.info_Btn.GetComponent<Image>().sprite = tutorialSetUpArmy_Gui.inInfo;
			infoState = false;
		}
		else
		{
			tutorialSetUpArmy_Gui.Info_Rect.DOKill();
			tutorialSetUpArmy_Gui.Info_Rect.DOMove(tutorialSetUpArmy_Gui.StartPosition, duration).SetEase(Ease.OutCirc);
			tutorialSetUpArmy_Gui.info_Btn.GetComponent<Image>().sprite = tutorialSetUpArmy_Gui.outInfo;
			infoState = true;
		}
	}

	public void InfoPanelOpen()
	{
		if (!infoState)
		{
			tutorialSetUpArmy_Gui.Info_Rect.DOKill();
			tutorialSetUpArmy_Gui.Info_Rect.DOMove(tutorialSetUpArmy_Gui.StartPosition, time).SetEase(Ease.OutCirc);
			tutorialSetUpArmy_Gui.info_Btn.GetComponent<Image>().sprite = tutorialSetUpArmy_Gui.outInfo;
			infoState = true;
		}
	}

	public void InfoPanelClose()
	{
		if (infoState)
		{
			tutorialSetUpArmy_Gui.Info_Rect.DOKill();
			tutorialSetUpArmy_Gui.Info_Rect.DOMove(tutorialSetUpArmy_Gui.LeftOutPosition, time).SetEase(Ease.OutCirc);
			tutorialSetUpArmy_Gui.info_Btn.GetComponent<Image>().sprite = tutorialSetUpArmy_Gui.inInfo;
			infoState = false;
		}
	}

	public void LoadMainScene()
	{
		tutorialSetUpArmy_Gui.loadingPanel.SetActive(true);
		SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Single);
	}

	public void ChangeCamera(CustomCameraType type)
	{
		if (currentCamera != CustomCameraType.Head)
		{
			lastCamera = currentCamera;
		}
		currentCamera = type;
		ResetJoysticks();
		switch (type)
		{
		case CustomCameraType.Best:
			game_Gui.cameraBest.ResetEverything();
			game_Gui.cameraHeadPanel.SetActive(false);
			game_Gui.cameraSetup.canPlay = false;
			game_Gui.cameraSetup_GameObject.SetActive(false);
			game_Gui.cameraHead.canPlay = false;
			game_Gui.cameraHead_GameObject.SetActive(false);
			AnimateCameraBest();
			break;
		case CustomCameraType.Setup:
			game_Gui.cameraSetup.ResetEverything();
			game_Gui.cameraSetup.ResetCameraPosition();
			game_Gui.cameraBestPanel.SetActive(false);
			game_Gui.cameraHeadPanel.SetActive(false);
			game_Gui.cameraBest.canPlay = false;
			game_Gui.cameraHead.canPlay = false;
			game_Gui.cameraHead_GameObject.SetActive(false);
			if (lastCamera == CustomCameraType.Best)
			{
				AnimateCameraSetup();
				break;
			}
			game_Gui.cameraBest_GameObject.SetActive(false);
			game_Gui.cameraSetup_JoystickMove.Reset();
			game_Gui.cameraSetup.canPlay = true;
			game_Gui.cameraSetup_GameObject.SetActive(true);
			break;
		case CustomCameraType.Head:
			game_Gui.cameraBestPanel.SetActive(false);
			game_Gui.cameraHeadPanel.SetActive(true);
			game_Gui.cameraHead_GameObject.SetActive(true);
			game_Gui.cameraHead.canPlay = true;
			game_Gui.cameraSetup.canPlay = false;
			game_Gui.cameraSetup_GameObject.SetActive(false);
			game_Gui.cameraBest.canPlay = false;
			game_Gui.cameraBest_GameObject.SetActive(false);
			game_Gui.cameraBest.ResetEverything();
			game_Gui.cameraSetup.ResetEverything();
			break;
		case CustomCameraType.Nothing:
			game_Gui.cameraBestPanel.SetActive(false);
			game_Gui.cameraHeadPanel.SetActive(false);
			game_Gui.cameraBest.canPlay = false;
			game_Gui.cameraBest_GameObject.SetActive(false);
			game_Gui.cameraHead.canPlay = false;
			game_Gui.cameraHead_GameObject.SetActive(false);
			game_Gui.cameraSetup.canPlay = false;
			game_Gui.cameraSetup_GameObject.SetActive(false);
			game_Gui.cameraBest.ResetEverything();
			game_Gui.cameraSetup.ResetEverything();
			break;
		}
	}

	private void AnimateCameraBest()
	{
		Vector3 vector = new Vector3(0f, 15f, -35f);
		Vector3 endValue = new Vector3(30f, 0f, 0f);
		game_Gui.cameraBest_GameObject.transform.position = game_Gui.cameraSetup_Camera.transform.position;
		game_Gui.cameraBest_GameObject.transform.rotation = game_Gui.cameraSetup_Camera.transform.rotation;
		game_Gui.cameraBest_GameObject.SetActive(true);
		float cameraAnimationTime = GetCameraAnimationTime(game_Gui.cameraBest_GameObject.transform.position, vector);
		game_Gui.cameraBest_GameObject.transform.DOMove(vector, cameraAnimationTime).SetEase(Ease.OutCirc);
		game_Gui.cameraBest_GameObject.transform.DORotate(endValue, cameraAnimationTime).SetEase(Ease.OutCirc).OnComplete(delegate
		{
			AnimateCameraComplete();
		});
	}

	private void AnimateCameraSetup()
	{
		Vector3 position = game_Gui.cameraSetup_Camera.transform.position;
		Vector3 eulerAngles = game_Gui.cameraSetup_Camera.transform.rotation.eulerAngles;
		float cameraAnimationTime = GetCameraAnimationTime(game_Gui.cameraBest_GameObject.transform.position, position);
		game_Gui.cameraBest_GameObject.transform.DOMove(position, cameraAnimationTime);
		game_Gui.cameraBest_GameObject.transform.DORotate(eulerAngles, cameraAnimationTime).OnComplete(delegate
		{
			game_Gui.cameraBest_GameObject.SetActive(false);
			game_Gui.cameraSetup_JoystickMove.Reset();
			game_Gui.cameraSetup.canPlay = true;
			game_Gui.cameraSetup_GameObject.SetActive(true);
		});
	}

	private void AnimateCameraComplete()
	{
		game_Gui.cameraBestPanel.SetActive(true);
		game_Gui.cameraBest_JoystickMove.Reset();
		game_Gui.cameraBest.canPlay = true;
	}

	private float GetCameraAnimationTime(Vector3 from, Vector3 to)
	{
		float num = 30f;
		return Vector3.Distance(from, to) / num;
	}

	private void ResetJoysticks()
	{
		game_Gui.cameraBest_JoystickMove.Reset();
	}

	private void InitCameras()
	{
		game_Gui.cameraBest.InitValues();
		game_Gui.cameraSetup.InitValues();
	}
}
