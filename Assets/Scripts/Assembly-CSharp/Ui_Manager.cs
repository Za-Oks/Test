using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ui_Manager : MonoBehaviour
{
	private PackagesManager packagesManager;

	 

	public FeaturesManager featuresManager;

	public SteamSetUp steamSetUp;

	[Header("References")]
	public TopPanel_Manager topPanel_Manager;

	public InfoPanel_Manager infoPanel_Manager;

	public Level_Manager level_Manager;

	public Shop_Manager shop_Manager;

	public Audio_Manager audio_Manager;

	public ShareManager shareManager;

	public PlayerPrefs_Players playerPrefs_Players;

	public GameController gc;

	public ConnectionScreen connectionScreen;

	public MultiplayerController multiplayerController;

	public LeadeboardConnection leadeboardConnection;

	[Header("Main Guis")]
	public Main_Gui main_Gui;

	public Settings_Gui settings_Gui;

	public InApp_Gui inApp_Gui;

	public InAppSpecial_Gui inAppSpecial_Gui;

	public SetUpArmy_Gui setUpArmy_Gui;

	public Game_Gui game_Gui;

	public Shop_Gui shop_Gui;

	public UpgradeStatsValues_Gui upgradeStatsValues_Gui;

	public MultiplayerConnect_Gui multiplayerConnect_Gui;

	public LevelStats_Gui levelStats_Gui;

	public GameOver_Gui gameOver_Gui;

	public Pause_Gui pause_Gui;

	public LeaderBoard_Gui leaderBoard_Gui;

	public InAppInfo_Gui inAppInfo_Gui;

	public Vip_Gui vip_Gui;

	public RateInfo_Gui rateInfo_Gui;

	public Message_Gui message_Gui;

	public ToastCustom_Gui toastCustom_Gui;

	public Animation_Gui animation_Gui;

	public Loading_Gui loading_Gui;

	public RectTransform[] AspectRatioRects;

	 

	 

	[HideInInspector]
	public bool multiplayerStarted;

	private string playerName = "Player";

	private string enemyName = "Player";

	private bool isVIPEnabled;

	private bool canRate;

	private float tempArmyTogglesAlpha = 0.5f;

	private CustomCameraType currentCamera;

	private CustomCameraType lastCamera;

	private DateTime referenceTime;

	private DateTime currentDate;

	private TimeSpan difference;

	private List<Image> ResultImages = new List<Image>();

	private WaitForSeconds waitTimeAnimation = new WaitForSeconds(3f);

	private WhichGuiIsOpen whichGuiIsOpen = WhichGuiIsOpen.CanQuit;

	private int roundsCounter = 2;

	private int roundsAds = 2;

	private float lastTimeInterstitial = -300f;

	private float timeBetweenInterstitial = 300f;

	public LastPressed_AdButton Last_AdButton { get; set; }

	private void Awake()
	{
		if (GameObject.FindWithTag("PackagesManager") != null)
		{
			packagesManager = GameObject.FindWithTag("PackagesManager").GetComponent<PackagesManager>();
			AmazonSetUp();
			SteamSetUp();
		}
		InitCameras();
		ChangeCamera(CustomCameraType.Nothing);
		DOTween.SetTweensCapacity(2500, 2500);
		if (PlayerPrefs_Saves.LoadRunOnlyOnce())
		{
			PlayerPrefs_Saves.SaveConsumables(250);
			PlayerPrefs_Saves.SaveRunOnlyOnce(false);
			PlayerPrefs.Save();
		}
		if (PlayerPrefs_Saves.LoadFacebookReward())
		{
			shop_Gui.facebookReward_Btn.gameObject.SetActive(true);
		}
		else
		{
			shop_Gui.facebookReward_Btn.gameObject.SetActive(false);
		}
		if (PlayerPrefs_Saves.LoadTwitterReward())
		{
			shop_Gui.twitterReward_Btn.gameObject.SetActive(true);
		}
		else
		{
			shop_Gui.twitterReward_Btn.gameObject.SetActive(false);
		}
		InitInAppProducts();
	}

	private void Start()
	{
		SetWhichGuiIsOpen(WhichGuiIsOpen.CanQuit);
		if (!level_Manager.IsProfile && !level_Manager.CreateLevels)
		{
			Loading();
		}
		CanvasInitialize();
		SetAspectRatioRects();
		CheckProfileOrBalance();
		SetCreateArmyLevel();
		SetStateRestoreBtn();
		if (!PlayerPrefs_Saves.LoadLockedPlayers())
		{
			UnlockAllPlayers(false);
		}
		if (!PlayerPrefs_Saves.LoadLockedSpartan())
		{
			UnlockSpartan(false);
		}
		if (!PlayerPrefs_Saves.LoadLockedSentinel())
		{
			UnlockSentinel(false);
		}
		if (!PlayerPrefs_Saves.LoadLockedSamurai())
		{
			UnlockSamurai(false);
		}
		if (!PlayerPrefs_Saves.LoadLockedFireDragon())
		{
			UnlockFireDragon(false);
		}
		if (!PlayerPrefs_Saves.LoadLockedMinigun())
		{
			UnlockMinigun(false);
		}
		if (!PlayerPrefs_Saves.LoadLockedRhino())
		{
			UnlockRhino(false);
		}
		if (!PlayerPrefs_Saves.LoadLockedGuardian())
		{
			UnlockGuardian(false);
		}
		if (!PlayerPrefs_Saves.LoadLockedWolf())
		{
			UnlockWolf(false);
		}
		//if (InAppManager.HAS_SUBSCRIPTION)
		//{
		//	EnableVIP();
		//}
	}

 

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Android_BackButton();
		}
	}

	private void SetCreateArmyLevel()
	{
		if (level_Manager.CreateLevels)
		{
			main_Gui.SetCanvas(false);
			setUpArmy_Gui.SetCanvas(true);
			setUpArmy_Gui.setEnemyArmy_Btn.gameObject.SetActive(true);
			setUpArmy_Gui.PlayLevels_UIs.SetActive(false);
			setUpArmy_Gui.PlayCustom_UIs.SetActive(false);
			setUpArmy_Gui.PlayMultiplayer_UIs.SetActive(false);
			level_Manager.SetPlayerQuad(false);
			level_Manager.SetEnemyQuad(true);
			level_Manager.ResetLevelsArmy();
			level_Manager.SetMenuGOs(false);
			level_Manager.SetLevelDesignCreate();
			ChangeCamera(CustomCameraType.Setup);
		}
	}

	private void CheckProfileOrBalance()
	{
		if (level_Manager.IsProfile)
		{
			level_Manager.SetMenuGOs(false);
			level_Manager.ProfileArmy();
			level_Manager.SetLevelDesign(LevelDesign.Forest);
			main_Gui.SetCanvas(false);
			game_Gui.SetCanvas(true);
			ChangeCamera(CustomCameraType.Best);
		}
		else if (level_Manager.IsBalance)
		{
			level_Manager.SetMenuGOs(false);
			level_Manager.SpawnBalance();
			level_Manager.SetLevelDesign(LevelDesign.Forest);
			main_Gui.SetCanvas(false);
			game_Gui.SetCanvas(true);
			ChangeCamera(CustomCameraType.Best);
		}
	}

	public void SetAnchors(RectTransform tempRect)
	{
		RectTransform component = tempRect.parent.GetComponent<RectTransform>();
		Vector2 zero = Vector2.zero;
		Vector2 anchorMin = new Vector2(tempRect.anchorMin.x + tempRect.offsetMin.x / component.rect.width, tempRect.anchorMin.y + tempRect.offsetMin.y / component.rect.height);
		Vector2 anchorMax = new Vector2(tempRect.anchorMax.x + tempRect.offsetMax.x / component.rect.width, tempRect.anchorMax.y + tempRect.offsetMax.y / component.rect.height);
		tempRect.anchorMin = anchorMin;
		tempRect.anchorMax = anchorMax;
		Vector2 offsetMin = (tempRect.offsetMax = zero);
		tempRect.offsetMin = offsetMin;
	}

	private void SetAspectRatioRects()
	{
		for (int i = 0; i < AspectRatioRects.Length; i++)
		{
			SetAspectRatioSize(AspectRatioRects[i]);
		}
	}

	private void SetAspectRatioSize(RectTransform tempRect)
	{
		Vector2 size = tempRect.rect.size;
		float num = 0f;
		num = ((!(size.x < size.y)) ? size.y : size.x);
		tempRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, num);
		tempRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, num);
	}

	public void SetConsumablesTexts()
	{
		shop_Gui.consumables_Txt.text = string.Empty + PlayerPrefs_Saves.LoadConsumables();
		upgradeStatsValues_Gui.consumables_Txt.text = string.Empty + PlayerPrefs_Saves.LoadConsumables();
		multiplayerConnect_Gui.consumables_Txt.text = string.Empty + PlayerPrefs_Saves.LoadConsumables();
		gameOver_Gui.consumables_Txt.text = string.Empty + PlayerPrefs_Saves.LoadConsumables();
	}

	public void SetAvailableTexts()
	{
		string text = string.Empty;
		bool flag = false;
		if (level_Manager.gameMode == GameMode.Levels)
		{
			text = "Level " + (level_Manager.levelIndex + 1);
			flag = level_Manager.LevelsValues[level_Manager.levelIndex].isBossLevel;
		}
		else if (level_Manager.gameMode == GameMode.EpicLevels)
		{
			text = "Epic " + (level_Manager.epicLevelIndex + 1);
			flag = true;
		}
		if (flag)
		{
			setUpArmy_Gui.level_Txt.color = Color.red;
		}
		else
		{
			setUpArmy_Gui.level_Txt.color = Color.white;
		}
		setUpArmy_Gui.level_Txt.text = text;
		setUpArmy_Gui.gold_Txt.text = string.Empty + level_Manager.availableGold;
		setUpArmy_Gui.armyNumber_Txt.text = string.Empty + level_Manager.CountActiveArmyPlayer() + "/" + level_Manager.maxArmy;
		setUpArmy_Gui.epicGold_Txt.text = string.Empty + level_Manager.availableGold;
		setUpArmy_Gui.epicArmyNumber_Txt.text = string.Empty + level_Manager.CountActiveArmyPlayer() + "/" + level_Manager.maxArmy;
		setUpArmy_Gui.bonusGold_Txt.text = "+" + level_Manager.bonusGoldValue;
		setUpArmy_Gui.bonusArmy_Txt.text = "+" + level_Manager.bonusArmyValue;
		setUpArmy_Gui.armyBlueNumber_Txt.text = string.Empty + level_Manager.CountActiveArmyPlayer() + "/" + level_Manager.customArmyNumber;
		setUpArmy_Gui.armyRedNumber_Txt.text = string.Empty + level_Manager.CountActiveArmyEnemy() + "/" + level_Manager.customArmyNumber;
		setUpArmy_Gui.multiAvailableBlueGold_Txt.text = string.Empty + level_Manager.tempUserAvailableGold;
		setUpArmy_Gui.multiAvailableRedGold_Txt.text = string.Empty + level_Manager.tempEnemyAvailableGold;
	}

	public void SetAvailableGoldTextsMultiplayer()
	{
		if (level_Manager.isPlayer)
		{
			setUpArmy_Gui.multiAvailableBlueGold_Txt.text = string.Empty + level_Manager.tempUserAvailableGold;
			setUpArmy_Gui.multiAvailableRedGold_Txt.text = string.Empty + level_Manager.tempEnemyAvailableGold;
		}
		else
		{
			setUpArmy_Gui.multiAvailableBlueGold_Txt.text = string.Empty + level_Manager.tempEnemyAvailableGold;
			setUpArmy_Gui.multiAvailableRedGold_Txt.text = string.Empty + level_Manager.tempUserAvailableGold;
		}
	}

	public void SetArmiesTextsMultiplayer()
	{
		setUpArmy_Gui.multiBlueArmy_Txt.text = string.Empty + level_Manager.CountActiveArmyPlayer() + "/" + level_Manager.multiArmyNumber;
		setUpArmy_Gui.multiRedArmy_Txt.text = string.Empty + level_Manager.CountActiveArmyEnemy() + "/" + level_Manager.multiArmyNumber;
	}

	public void SetArmiesGoldCustom(int tempPrice, PlaceAction tempAction, float xPosition)
	{
		int num = int.Parse(setUpArmy_Gui.armyBlueGold_Txt.text);
		int num2 = int.Parse(setUpArmy_Gui.armyRedGold_Txt.text);
		if (xPosition < 0f)
		{
			switch (tempAction)
			{
			case PlaceAction.Place:
				num += tempPrice;
				break;
			case PlaceAction.Remove:
				num -= tempPrice;
				break;
			}
		}
		else
		{
			switch (tempAction)
			{
			case PlaceAction.Place:
				num2 += tempPrice;
				break;
			case PlaceAction.Remove:
				num2 -= tempPrice;
				break;
			}
		}
		setUpArmy_Gui.armyBlueGold_Txt.text = string.Empty + num;
		setUpArmy_Gui.armyRedGold_Txt.text = string.Empty + num2;
	}

	private void ResetBlueArmyGoldCustom()
	{
		setUpArmy_Gui.armyBlueGold_Txt.text = string.Empty + 0;
	}

	private void ResetRedArmyGoldCustom()
	{
		setUpArmy_Gui.armyRedGold_Txt.text = string.Empty + 0;
	}

	private void AmazonSetUp()
	{
		if (packagesManager.whichStore == App_Stores.Amazon)
		{
			main_Gui.openTutorialPanel_Btn.gameObject.SetActive(false);
		}
	}

	private void SteamSetUp()
	{
		if (packagesManager.whichStore == App_Stores.Steam)
		{
			main_Gui.upgradeStatsValues_Btn.transform.position = main_Gui.shop_Btn.transform.position;
			main_Gui.shop_Btn.GetComponent<RectTransform>().position = main_Gui.inApp_Btn.transform.position;
			gameOver_Gui.menu_Btn.GetComponent<RectTransform>().position = new Vector3(Screen.width / 2, gameOver_Gui.menu_Btn.GetComponent<RectTransform>().position.y, 0f);
			settings_Gui.social_Txt.GetComponent<RectTransform>().position = new Vector3(Screen.width / 2, settings_Gui.social_Txt.GetComponent<RectTransform>().position.y, 0f);
			settings_Gui.facebook_Btn.GetComponent<RectTransform>().position = new Vector3(settings_Gui.sound_Toggle.GetComponent<RectTransform>().position.x, settings_Gui.facebook_Btn.GetComponent<RectTransform>().position.y, 0f);
			settings_Gui.twitter_Btn.GetComponent<RectTransform>().position = new Vector3(settings_Gui.music_Toggle.GetComponent<RectTransform>().position.x, settings_Gui.twitter_Btn.GetComponent<RectTransform>().position.y, 0f);
			SetAnchors(settings_Gui.social_Txt.GetComponent<RectTransform>());
			SetAnchors(settings_Gui.facebook_Btn.GetComponent<RectTransform>());
			SetAnchors(settings_Gui.twitter_Btn.GetComponent<RectTransform>());
			int num = steamSetUp.GOsInactive.Length;
			for (int i = 0; i < num; i++)
			{
				steamSetUp.GOsInactive[i].SetActive(false);
			}
			int num2 = steamSetUp.GOsActive.Length;
			for (int j = 0; j < num2; j++)
			{
				steamSetUp.GOsActive[j].SetActive(true);
			}
			setUpArmy_Gui.gold_Txt = steamSetUp.ReplaceGold_Txt;
			setUpArmy_Gui.armyNumber_Txt = steamSetUp.ReplaceArmyNumber_Txt;
		}
	}

	private void SetFastest()
	{
	}

	private void SetFantastic()
	{
	}

	private void CanvasInitialize()
	{
		InitToggles();
		EpicLevelsBtnsLogic();
		SetCrossPromotionState(false);
		main_Gui.playLevels_Btn.onClick.AddListener(delegate
		{
			level_Manager.gameMode = GameMode.Levels;
			OpenSetUpArmy();
			level_Manager.ResetArmyUsedValues();
			audio_Manager.MenuClick();
		});
		main_Gui.playEpicLevels_Btn.onClick.AddListener(delegate
		{
			level_Manager.gameMode = GameMode.EpicLevels;
			OpenSetUpArmy();
			level_Manager.ResetArmyUsedValues();
			audio_Manager.MenuClick();
		});
		main_Gui.playEpicLevelsInfo_Btn.onClick.AddListener(delegate
		{
			EpicLevelsInfo_Btn();
			audio_Manager.MenuClick();
		});
		main_Gui.playCustom_Btn.onClick.AddListener(delegate
		{
			level_Manager.gameMode = GameMode.Custom;
			OpenSetUpArmy();
			level_Manager.ResetArmyUsedValues();
			audio_Manager.MenuClick();
		});
		main_Gui.playMultiplayer_Btn.onClick.AddListener(delegate
		{
			CheckCanPlayMultiplayer();
			level_Manager.ResetArmyUsedValues();
			audio_Manager.MenuClick();
		});
		main_Gui.openTutorialPanel_Btn.onClick.AddListener(delegate
		{
			OpenTutorialPanel();
			audio_Manager.MenuClick();
		});
		main_Gui.closeTutorialPanel_Btn.onClick.AddListener(delegate
		{
			CloseTutorialPanel();
			audio_Manager.MenuClick();
		});
		main_Gui.startTutorialPanel_Btn.onClick.AddListener(delegate
		{
			StartTutorial();
			audio_Manager.MenuClick();
		});
		main_Gui.settings_Btn.onClick.AddListener(delegate
		{
			OpenSettings();
			audio_Manager.MenuClick();
		});
		main_Gui.leaderboard_Btn.onClick.AddListener(delegate
		{
			OpenLeaderBoard();
			audio_Manager.MenuClick();
		});
		main_Gui.shop_Btn.onClick.AddListener(delegate
		{
			OpenShop();
			audio_Manager.MenuClick();
		});
		main_Gui.upgradeStatsValues_Btn.onClick.AddListener(delegate
		{
			OpenUpgradeStatsValues();
			audio_Manager.MenuClick();
		});
		main_Gui.inApp_Btn.onClick.AddListener(delegate
		{
			OpenInAppGui();
			audio_Manager.MenuClick();
		});
		main_Gui.vip_Btn.onClick.AddListener(delegate
		{
			OpenVipPanel();
			audio_Manager.MenuClick();
		});
		ResetSettingsPanels();
		settings_Gui.facebook_Btn.onClick.AddListener(delegate
		{
			LinkFacebook();
			audio_Manager.MenuClick();
		});
		settings_Gui.twitter_Btn.onClick.AddListener(delegate
		{
			LinkTwitter();
			audio_Manager.MenuClick();
		});
		settings_Gui.back_Btn.onClick.AddListener(delegate
		{
			CloseSettings();
			audio_Manager.MenuBack();
		});
		settings_Gui.settings_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			SettingPanel(status);
			audio_Manager.MenuClick();
		});
		settings_Gui.levelstats_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			LevelstatsPanel(status);
			audio_Manager.MenuClick();
		});
		settings_Gui.music_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			HandleMusic(status);
			audio_Manager.MenuClick();
		});
		settings_Gui.sound_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			HandleSound(status);
			audio_Manager.MenuClick();
		});
		settings_Gui.terms_Btn.onClick.AddListener(delegate
		{
			Application.OpenURL("http://rappidstudios.com/index.php/privacy-policy");
			audio_Manager.MenuClick();
		});
		inApp_Gui.unlockCombo_SpartanSentinel.onClick.AddListener(delegate
		{
			Purchase_Combo_Spartan_Sentinel();
			audio_Manager.MenuClick();
		});
		inApp_Gui.unlockCombo_SpartanSentinelSamurai.onClick.AddListener(delegate
		{
			Purchase_Combo_Spartan_Sentinel_Samurai();
			audio_Manager.MenuClick();
		});
		inApp_Gui.unlockCombo_SpartanSentinelSamuraiFireDragon.onClick.AddListener(delegate
		{
			Purchase_Combo_Spartan_Sentinel_Samurai_FireDragon();
			audio_Manager.MenuClick();
		});
		inApp_Gui.unlockCombo_SpartanSentinelSamuraiFireDragonMinigun.onClick.AddListener(delegate
		{
			Purchase_Combo_Spartan_Sentinel_Samurai_FireDragon_Minigun();
			audio_Manager.MenuClick();
		});
		inApp_Gui.unlockCombo_SpartanSentinelSamuraiFireDragonMinigunRhino.onClick.AddListener(delegate
		{
			Purchase_Combo_Spartan_Sentinel_Samurai_FireDragon_Minigun_Rhino();
			audio_Manager.MenuClick();
		});
		inApp_Gui.unlockCombo_SpartanSentinelSamuraiFireDragonMinigunRhinoGuardian.onClick.AddListener(delegate
		{
			Purchase_Combo_Spartan_Sentinel_Samurai_FireDragon_Minigun_Rhino_Guardian();
			audio_Manager.MenuClick();
		});
		inApp_Gui.unlockCombo_SpartanSentinelSamuraiFireDragonMinigunRhinoGuardianWolf.onClick.AddListener(delegate
		{
			Purchase_Combo_Spartan_Sentinel_Samurai_FireDragon_Minigun_Rhino_Guardian_Wolf();
			audio_Manager.MenuClick();
		});
		inApp_Gui.unlock_Spartan.onClick.AddListener(delegate
		{
			PurchaseSpartan();
			audio_Manager.MenuClick();
		});
		inApp_Gui.unlock_Sentinel.onClick.AddListener(delegate
		{
			PurchaseSentinel();
			audio_Manager.MenuClick();
		});
		inApp_Gui.unlock_Samurai.onClick.AddListener(delegate
		{
			PurchaseSamurai();
			audio_Manager.MenuClick();
		});
		inApp_Gui.unlock_FireDragon.onClick.AddListener(delegate
		{
			PurchaseFireDragon();
			audio_Manager.MenuClick();
		});
		inApp_Gui.unlock_Minigun.onClick.AddListener(delegate
		{
			PurchaseMinigun();
			audio_Manager.MenuClick();
		});
		inApp_Gui.unlock_Rhino.onClick.AddListener(delegate
		{
			PurchaseRhino();
			audio_Manager.MenuClick();
		});
		inApp_Gui.unlock_Guardian.onClick.AddListener(delegate
		{
			PurchaseGuardian();
			audio_Manager.MenuClick();
		});
		inApp_Gui.unlock_Wolf.onClick.AddListener(delegate
		{
			PurchaseWolf();
			audio_Manager.MenuClick();
		});
		inApp_Gui.gempack_2500.onClick.AddListener(delegate
		{
			PurchaseGempack_2500();
			audio_Manager.MenuClick();
		});
		inApp_Gui.gempack_7000.onClick.AddListener(delegate
		{
			PurchaseGempack_7000();
			audio_Manager.MenuClick();
		});
		inApp_Gui.gempack_15000.onClick.AddListener(delegate
		{
			PurchaseGempack_15000();
			audio_Manager.MenuClick();
		});
		inApp_Gui.gempack_50000.onClick.AddListener(delegate
		{
			PurchaseGempack_50000();
			audio_Manager.MenuClick();
		});
		inApp_Gui.noAds_Btn.onClick.AddListener(delegate
		{
			PurchaseNoAds();
			audio_Manager.MenuClick();
		});
		inApp_Gui.restorePurchases_Btn.onClick.AddListener(delegate
		{
			RestorePurchases();
			audio_Manager.MenuClick();
		});
		inApp_Gui.back_Btn.onClick.AddListener(delegate
		{
			CloseInAppGui();
			audio_Manager.MenuBack();
		});
		SetInAppSpecialButtons();
		inAppSpecial_Gui.unlockSpecial_FireDragon_MiniGun_Btn.onClick.AddListener(delegate
		{
			Purchase_SpecialOffer_FireDragon_Minigun();
			audio_Manager.MenuClick();
		});
		inAppSpecial_Gui.UnlockSpecial_FireDragon_Rhino_Btn.onClick.AddListener(delegate
		{
			Purchase_SpecialOffer_FireDragon_Rhino();
			audio_Manager.MenuClick();
		});
		inAppSpecial_Gui.UnlockSpecial_FireDragon_Minigun_Rhino_Btn.onClick.AddListener(delegate
		{
			Purchase_SpecialOffer_FireDragon_Minigun_Rhino();
			audio_Manager.MenuClick();
		});
		inAppSpecial_Gui.unlockSpecial_5000Gems_Btn.onClick.AddListener(delegate
		{
			Purchase_SpecialOffer_Gempack_5000();
			audio_Manager.MenuClick();
		});
		inAppSpecial_Gui.unlockSpecial_40000Gems_Btn.onClick.AddListener(delegate
		{
			Purchase_SpecialOffer_Gempack_40000();
			audio_Manager.MenuClick();
		});
		inAppSpecial_Gui.back_Btn.onClick.AddListener(delegate
		{
			CloseInAppSpecialGui();
			audio_Manager.MenuBack();
		});
		ResetPlaceRemoveBtn();
		InitArmiesUIs();
		SetLevelButtons();
		SetAvailableTexts();
		setUpArmy_Gui.setEnemyArmy_Btn.onClick.AddListener(delegate
		{
			level_Manager.CreateLevel();
		});
		setUpArmy_Gui.startGame_Btn.onClick.AddListener(delegate
		{
			CheckCanStartGame();
			audio_Manager.MenuClick();
		});
		setUpArmy_Gui.placeRemoveArmy_Btn.onClick.AddListener(delegate
		{
			PlaceRemoveArmy();
			audio_Manager.MenuClick();
		});
		setUpArmy_Gui.playFirstIncomplete_Btn.onClick.AddListener(delegate
		{
			PlayFirstIncompleteLevel();
			audio_Manager.MenuClick();
		});
		setUpArmy_Gui.resetArmy_Btn.onClick.AddListener(delegate
		{
			ResetArmies();
			audio_Manager.MenuClick();
		});
		setUpArmy_Gui.changeLevelDesign_Btn.onClick.AddListener(delegate
		{
			level_Manager.ChangeLevelDesignCustom();
			ResetArmies();
			audio_Manager.MenuClick();
		});
		setUpArmy_Gui.info_Reward_Btn.onClick.AddListener(delegate
		{
			EpicLevelInfoReward();
			audio_Manager.MenuClick();
		});
		setUpArmy_Gui.back_Btn.onClick.AddListener(delegate
		{
			CloseSetUpArmy();
			audio_Manager.MenuBack();
		});
		setUpArmy_Gui.bonusGold_Btn.onClick.AddListener(delegate
		{
			Last_AdButton = LastPressed_AdButton.BonusGold;
			AdLogic();
			audio_Manager.MenuClick();
		});
		setUpArmy_Gui.bonusArmy_Btn.onClick.AddListener(delegate
		{
			Last_AdButton = LastPressed_AdButton.BonusArmy;
			AdLogic();
			audio_Manager.MenuClick();
		});
		setUpArmy_Gui.Melee_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			MeleeArmyUIs(status);
			audio_Manager.MenuClick();
		});
		setUpArmy_Gui.Ranged_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			RangedArmyUIs(status);
			audio_Manager.MenuClick();
		});
		setUpArmy_Gui.Cavalry_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			CavalryArmyUIs(status);
			audio_Manager.MenuClick();
		});
		setUpArmy_Gui.Heavy_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			HeavyArmyUIs(status);
			audio_Manager.MenuClick();
		});
		setUpArmy_Gui.Special_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			SpecialArmyUIs(status);
			audio_Manager.MenuClick();
		});
		setUpArmy_Gui.Epic_Toggle.onValueChanged.AddListener(delegate(bool status)
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
		level_Manager.Ballista.army_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			if (status)
			{
				level_Manager.SetValuesArmyInspector(level_Manager.Ballista);
				audio_Manager.MenuClick();
			}
		});
		level_Manager.Crossbow.army_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			if (status)
			{
				level_Manager.SetValuesArmyInspector(level_Manager.Crossbow);
				audio_Manager.MenuClick();
			}
		});
		level_Manager.WarElephant.army_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			if (status)
			{
				level_Manager.SetValuesArmyInspector(level_Manager.WarElephant);
				audio_Manager.MenuClick();
			}
		});
		level_Manager.Kamikaze.army_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			if (status)
			{
				level_Manager.SetValuesArmyInspector(level_Manager.Kamikaze);
				audio_Manager.MenuClick();
			}
		});
		level_Manager.Ninja.army_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			if (status)
			{
				level_Manager.SetValuesArmyInspector(level_Manager.Ninja);
				audio_Manager.MenuClick();
			}
		});
		level_Manager.AK47.army_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			if (status)
			{
				level_Manager.SetValuesArmyInspector(level_Manager.AK47);
				audio_Manager.MenuClick();
			}
		});
		level_Manager.M16.army_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			if (status)
			{
				level_Manager.SetValuesArmyInspector(level_Manager.M16);
				audio_Manager.MenuClick();
			}
		});
		level_Manager.MageTower.army_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			if (status)
			{
				level_Manager.SetValuesArmyInspector(level_Manager.MageTower);
				audio_Manager.MenuClick();
			}
		});
		level_Manager.Chariot.army_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			if (status)
			{
				level_Manager.SetValuesArmyInspector(level_Manager.Chariot);
				audio_Manager.MenuClick();
			}
		});
		level_Manager.Camel.army_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			if (status)
			{
				level_Manager.SetValuesArmyInspector(level_Manager.Camel);
				audio_Manager.MenuClick();
			}
		});
		level_Manager.Pirate.army_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			if (status)
			{
				level_Manager.SetValuesArmyInspector(level_Manager.Pirate);
				audio_Manager.MenuClick();
			}
		});
		level_Manager.Hwacha.army_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			if (status)
			{
				level_Manager.SetValuesArmyInspector(level_Manager.Hwacha);
				audio_Manager.MenuClick();
			}
		});
		level_Manager.Mage.army_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			if (status)
			{
				level_Manager.SetValuesArmyInspector(level_Manager.Mage);
				audio_Manager.MenuClick();
			}
		});
		level_Manager.BearRider.army_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			if (status)
			{
				level_Manager.SetValuesArmyInspector(level_Manager.BearRider);
				audio_Manager.MenuClick();
			}
		});
		level_Manager.OrganGun.army_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			if (status)
			{
				level_Manager.SetValuesArmyInspector(level_Manager.OrganGun);
				audio_Manager.MenuClick();
			}
		});
		level_Manager.TrebuchetMan.army_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			if (status)
			{
				level_Manager.SetValuesArmyInspector(level_Manager.TrebuchetMan);
				audio_Manager.MenuClick();
			}
		});
		level_Manager.Berseker.army_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			if (status)
			{
				level_Manager.SetValuesArmyInspector(level_Manager.Berseker);
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
		level_Manager.Samurai.army_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			if (status)
			{
				level_Manager.SetValuesArmyInspector(level_Manager.Samurai);
				audio_Manager.MenuClick();
			}
		});
		level_Manager.FireDragon.army_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			if (status)
			{
				level_Manager.SetValuesArmyInspector(level_Manager.FireDragon);
				audio_Manager.MenuClick();
			}
		});
		level_Manager.Minigun.army_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			if (status)
			{
				level_Manager.SetValuesArmyInspector(level_Manager.Minigun);
				audio_Manager.MenuClick();
			}
		});
		level_Manager.Rhino.army_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			if (status)
			{
				level_Manager.SetValuesArmyInspector(level_Manager.Rhino);
				audio_Manager.MenuClick();
			}
		});
		level_Manager.Guardian.army_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			if (status)
			{
				level_Manager.SetValuesArmyInspector(level_Manager.Guardian);
				audio_Manager.MenuClick();
			}
		});
		level_Manager.Wolf.army_Toggle.onValueChanged.AddListener(delegate(bool status)
		{
			if (status)
			{
				level_Manager.SetValuesArmyInspector(level_Manager.Wolf);
				audio_Manager.MenuClick();
			}
		});
		game_Gui.pause_Btn.onClick.AddListener(delegate
		{
			Pause();
			audio_Manager.MenuClick();
		});
		game_Gui.surrender_Btn.onClick.AddListener(delegate
		{
			OpenSurrenderPanel();
			audio_Manager.MenuClick();
		});
		game_Gui.confirm_surrender_Btn.onClick.AddListener(delegate
		{
			CloseSurrenderPanel();
			Surrender();
			audio_Manager.MenuClick();
		});
		game_Gui.refuse_surrender_Btn.onClick.AddListener(delegate
		{
			CloseSurrenderPanel();
			audio_Manager.MenuClick();
		});
		InitSocialMediaButtons();
		shop_Gui.next_Btn.onClick.AddListener(delegate
		{
			shop_Manager.ChooseNext();
			audio_Manager.MenuClick();
		});
		shop_Gui.previous_Btn.onClick.AddListener(delegate
		{
			shop_Manager.ChoosePrevious();
			audio_Manager.MenuClick();
		});
		shop_Gui.buy_Btn.onClick.AddListener(delegate
		{
			shop_Manager.BuyPlayer();
		});
		shop_Gui.infoBoss_Btn.onClick.AddListener(delegate
		{
			OpenInfoBossPanel();
			audio_Manager.MenuClick();
		});
		shop_Gui.infoBossBack_Btn.onClick.AddListener(delegate
		{
			CloseInfoBossPanel();
			audio_Manager.MenuClick();
		});
		shop_Gui.facebookReward_Btn.onClick.AddListener(delegate
		{
			LinkFacebookReward();
			audio_Manager.MenuClick();
		});
		shop_Gui.twitterReward_Btn.onClick.AddListener(delegate
		{
			LinkTwitterReward();
			audio_Manager.MenuClick();
		});
		shop_Gui.back_Btn.onClick.AddListener(delegate
		{
			CloseShop();
			audio_Manager.MenuBack();
		});
		shop_Gui.spartanInApp_Btn.onClick.AddListener(delegate
		{
			PurchaseSpartan();
			audio_Manager.MenuClick();
		});
		shop_Gui.sentinelnInApp_Btn.onClick.AddListener(delegate
		{
			PurchaseSentinel();
			audio_Manager.MenuClick();
		});
		shop_Gui.samuraiInApp_Btn.onClick.AddListener(delegate
		{
			PurchaseSamurai();
			audio_Manager.MenuClick();
		});
		shop_Gui.fireDragonInApp_Btn.onClick.AddListener(delegate
		{
			PurchaseFireDragon();
			audio_Manager.MenuClick();
		});
		shop_Gui.minigunInApp_Btn.onClick.AddListener(delegate
		{
			PurchaseMinigun();
			audio_Manager.MenuClick();
		});
		shop_Gui.rhinoInApp_Btn.onClick.AddListener(delegate
		{
			PurchaseRhino();
			audio_Manager.MenuClick();
		});
		shop_Gui.guardianInApp_Btn.onClick.AddListener(delegate
		{
			PurchaseGuardian();
			audio_Manager.MenuClick();
		});
		shop_Gui.wolfInApp_Btn.onClick.AddListener(delegate
		{
			PurchaseWolf();
			audio_Manager.MenuClick();
		});
		upgradeStatsValues_Gui.back_Btn.onClick.AddListener(delegate
		{
			CloseUpgradeStatsValues();
			audio_Manager.MenuBack();
		});
		multiplayerConnect_Gui.InitValues();
		InitLogin();
		SetDateTimeVariables();
		SetStarImages();
		SetDailyRewardPanels();
		multiplayerConnect_Gui.ready_Btn.onClick.AddListener(delegate
		{
			ReadyPressed();
			audio_Manager.MenuClick();
		});
		multiplayerConnect_Gui.dailyReward_Btn.onClick.AddListener(delegate
		{
			GetDailyReward();
			audio_Manager.Reward();
		});
		multiplayerConnect_Gui.cancel_Btn.onClick.AddListener(delegate
		{
			CancelPressed();
			audio_Manager.MenuBack();
		});
		multiplayerConnect_Gui.showPassword_Btn.onClick.AddListener(delegate
		{
			ShowPassword();
			audio_Manager.MenuBack();
		});
		multiplayerConnect_Gui.info_Btn.onClick.AddListener(delegate
		{
			OpenRateInfoPanel("Log in", "After logging in, you won't be able to use a different account.");
		});
		multiplayerConnect_Gui.back_Btn.onClick.AddListener(delegate
		{
			CloseMultiplayerConnect();
			audio_Manager.MenuBack();
		});
		levelStats_Gui.InitializeImagesValues();
		levelStats_Gui.CloseDefault_Items();
		LevelStats_Initialize();
		ConsumableBtnAnimation();
		gameOver_Gui.restart_Btn.onClick.AddListener(delegate
		{
			Restart();
			audio_Manager.MenuClick();
		});
		gameOver_Gui.menu_Btn.onClick.AddListener(delegate
		{
			MainMenu();
			audio_Manager.MenuClick();
		});
		gameOver_Gui.rate_Btn.onClick.AddListener(delegate
		{
			Rate();
			audio_Manager.MenuClick();
		});
		gameOver_Gui.share_Btn.onClick.AddListener(delegate
		{
			shareManager.Share(level_Manager.levelIndex + 1);
			audio_Manager.MenuClick();
		});
		gameOver_Gui.facebook_Btn.onClick.AddListener(delegate
		{
			LinkFacebook();
			audio_Manager.MenuClick();
		});
		gameOver_Gui.twitter_Btn.onClick.AddListener(delegate
		{
			LinkTwitter();
			audio_Manager.MenuClick();
		});
		gameOver_Gui.consumables_Btn.onClick.AddListener(delegate
		{
			Last_AdButton = LastPressed_AdButton.EarnConsumableGameOver;
			AdLogic();
			audio_Manager.MenuClick();
		});
		pause_Gui.resume_Btn.onClick.AddListener(delegate
		{
			Resume();
			audio_Manager.MenuClick();
		});
		pause_Gui.surrender_Btn.onClick.AddListener(delegate
		{
			Surrender();
			audio_Manager.MenuClick();
		});
		pause_Gui.menu_Btn.onClick.AddListener(delegate
		{
			MainMenu();
			audio_Manager.MenuClick();
		});
		leaderBoard_Gui.InitLeaderBoardPlayers();
		leaderBoard_Gui.back_Btn.onClick.AddListener(delegate
		{
			CloseLeaderBoard();
			audio_Manager.MenuBack();
		});
		inAppInfo_Gui.ok_Btn.onClick.AddListener(delegate
		{
			CloseInAppInfoPanel();
			audio_Manager.MenuBack();
		});
		inAppInfo_Gui.info_Reward_Btn.onClick.AddListener(delegate
		{
			if (level_Manager.epicLevelIndex == level_Manager.CountCompletedEpicLevels())
			{
				OpenToast("First Time Win Reward - " + level_Manager.EpicLevelsValues[level_Manager.epicLevelIndex].epic_Reward_First_Time);
			}
			else
			{
				OpenToast("Regular Win Reward - " + level_Manager.EpicLevelsValues[level_Manager.epicLevelIndex].epic_Reward);
			}
			audio_Manager.MenuClick();
		});
		//vip_Gui.subscription_Btn.onClick.AddListener(delegate
		//{
		//	inAppManager.PurchaseMembership();
		//	audio_Manager.MenuBack();
		//});
		vip_Gui.privacyPolicy_Btn.onClick.AddListener(delegate
		{
			OpenPrivacyPolicy();
			audio_Manager.MenuBack();
		});
		vip_Gui.termsOfUse_Btn.onClick.AddListener(delegate
		{
			OpenTermsOfUse();
			audio_Manager.MenuBack();
		});
		vip_Gui.close_Btn.onClick.AddListener(delegate
		{
			CloseVipPanel();
			audio_Manager.MenuBack();
		});
		rateInfo_Gui.ok_Btn.onClick.AddListener(delegate
		{
			CloseRateInfoPanel();
			if (canRate)
			{
				PlayerPrefs_Settings.SaveCanShowRatePopUp(false);
				canRate = false;
				Rate();
			}
			audio_Manager.MenuBack();
		});
		rateInfo_Gui.close_Btn.onClick.AddListener(delegate
		{
			CloseRateInfoPanel();
			if (canRate)
			{
				PlayerPrefs_Settings.SaveCanShowRatePopUp(false);
				canRate = false;
			}
			audio_Manager.MenuBack();
		});
		message_Gui.ok_Btn.onClick.AddListener(delegate
		{
			CloseMessage();
			audio_Manager.MenuClick();
		});
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

	private void InitInAppProducts()
	{
		InAppProducts.Combo_Spartan_Sentinel.Init(inApp_Gui.unlockCombo_SpartanSentinel.transform);
		InAppProducts.Combo_Spartan_Sentinel_Samurai.Init(inApp_Gui.unlockCombo_SpartanSentinelSamurai.transform);
		InAppProducts.Combo_Spartan_Sentinel_Samurai_Fire_Dragon.Init(inApp_Gui.unlockCombo_SpartanSentinelSamuraiFireDragon.transform);
		InAppProducts.Combo_Spartan_Sentinel_Samurai_Fire_Dragon_Minigun.Init(inApp_Gui.unlockCombo_SpartanSentinelSamuraiFireDragonMinigun.transform);
		InAppProducts.Combo_Spartan_Sentinel_Samurai_Fire_Dragon_Minigun_Rhino.Init(inApp_Gui.unlockCombo_SpartanSentinelSamuraiFireDragonMinigunRhino.transform);
		InAppProducts.Combo_Spartan_Sentinel_Samurai_Fire_Dragon_Minigun_Rhino_Guardian.Init(inApp_Gui.unlockCombo_SpartanSentinelSamuraiFireDragonMinigunRhinoGuardian.transform);
		InAppProducts.Combo_Spartan_Sentinel_Samurai_Fire_Dragon_Minigun_Rhino_Guardian_Wolf.Init(inApp_Gui.unlockCombo_SpartanSentinelSamuraiFireDragonMinigunRhinoGuardianWolf.transform);
		InAppProducts.Spartan.Init(inApp_Gui.unlock_Spartan.transform, shop_Gui.spartanInApp_Btn.transform);
		InAppProducts.Sentinel.Init(inApp_Gui.unlock_Sentinel.transform, shop_Gui.sentinelnInApp_Btn.transform);
		InAppProducts.Samurai.Init(inApp_Gui.unlock_Samurai.transform, shop_Gui.samuraiInApp_Btn.transform);
		InAppProducts.Minigun.Init(inApp_Gui.unlock_Minigun.transform, shop_Gui.minigunInApp_Btn.transform);
		InAppProducts.FireDragon.Init(inApp_Gui.unlock_FireDragon.transform, shop_Gui.fireDragonInApp_Btn.transform);
		InAppProducts.Rhino.Init(inApp_Gui.unlock_Rhino.transform, shop_Gui.rhinoInApp_Btn.transform);
		InAppProducts.Guardian.Init(inApp_Gui.unlock_Guardian.transform, shop_Gui.guardianInApp_Btn.transform);
		InAppProducts.Wolf.Init(inApp_Gui.unlock_Wolf.transform, shop_Gui.wolfInApp_Btn.transform);
		InAppProducts.Gempack_2500.Init(inApp_Gui.gempack_2500.transform);
		InAppProducts.Gempack_7000.Init(inApp_Gui.gempack_7000.transform);
		InAppProducts.Gempack_15000.Init(inApp_Gui.gempack_15000.transform);
		InAppProducts.Gempack_50000.Init(inApp_Gui.gempack_50000.transform);
		InAppProducts.NoAds.Init(inApp_Gui.noAds_Btn.transform);
		InAppProducts.SF_Fire_Dragon_Minigun.Init(inAppSpecial_Gui.unlockSpecial_FireDragon_MiniGun_Btn.transform);
		InAppProducts.SF_Fire_Dragon_Rhino.Init(inAppSpecial_Gui.UnlockSpecial_FireDragon_Rhino_Btn.transform);
		InAppProducts.SF_Fire_Dragon_Minigun_Rhino.Init(inAppSpecial_Gui.UnlockSpecial_FireDragon_Minigun_Rhino_Btn.transform);
		InAppProducts.SF_Gem_5000.Init(inAppSpecial_Gui.unlockSpecial_5000Gems_Btn.transform);
		InAppProducts.SF_Gem_40000.Init(inAppSpecial_Gui.unlockSpecial_40000Gems_Btn.transform);
	}

	public void SetupInAppProduct(InAppProduct_Info inApp_Info, string price, string title, string descr)
	{
		title = title.Replace(" (Epic Battle Simulator 2)", string.Empty);
		inApp_Info.descr = descr;
		inApp_Info.title = title;
		inApp_Info.priceInApp_Text.text = price;
		inApp_Info.infoInApp_Button.interactable = true;
		if (inApp_Info.priceShop_Text != null)
		{
			inApp_Info.priceShop_Text.text = price;
		}
	}

	public void InfoButtonClicked(InAppProduct_Info inapp)
	{
		OpenInAppInfoPanel(inapp.title, inapp.descr, false, true);
		audio_Manager.MenuClick();
	}

	public void RestorePurchases()
	{
		if (packagesManager.whichStore == App_Stores.IOS)
		{
			//inAppManager.RestorePurchases();
			LoadFromCloud();
		}
	}

	public void SetStateRestoreBtn()
	{
		if (GameObject.FindWithTag("PackagesManager") != null)
		{
			if (packagesManager.whichStore == App_Stores.IOS)
			{
				inApp_Gui.restorePurchases_Btn.gameObject.SetActive(true);
			}
			else
			{
				inApp_Gui.restorePurchases_Btn.gameObject.SetActive(false);
			}
		}
	}

	public void UnlockNoAds(bool playSound)
	{
		PlayerPrefs_Saves.SaveCanShowAds(false);
		PlayerPrefs.Save();
		level_Manager.SetBonusGold(true);
		level_Manager.SetBonusArmy(true);
		if (playSound)
		{
			audio_Manager.InApp();
		}
	}

	public void UnlockAllPlayers(bool playSound)
	{
		for (int i = 0; i < playerPrefs_Players.Players_SetUp.Length; i++)
		{
			playerPrefs_Players.Players_SetUp[i].status = "unlocked";
			playerPrefs_Players.Players_SetUp[i].bossToggle.interactable = true;
			playerPrefs_Players.Players_SetUp[i].lock_Panel.SetActive(false);
		}
		PlayerPrefs_Saves.SaveLockedPlayers(false);
		PlayerPrefs.Save();
		if (playSound)
		{
			audio_Manager.InApp();
		}
	}

	public void UnlockSpartan(bool playSound)
	{
		string text = "Spartan";
		for (int i = 0; i < playerPrefs_Players.Players_SetUp.Length; i++)
		{
			if (playerPrefs_Players.Players_SetUp[i].columnName == text)
			{
				playerPrefs_Players.Players_SetUp[i].status = "unlocked";
				playerPrefs_Players.Players_SetUp[i].bossToggle.interactable = true;
				playerPrefs_Players.Players_SetUp[i].lock_Panel.SetActive(false);
				playerPrefs_Players.Players_SetUp[i].bossInAppShop.gameObject.SetActive(false);
				break;
			}
		}
		shop_Gui.buyPanel.SetActive(false);
		PlayerPrefs_Saves.SaveLockedSpartan(false);
		PlayerPrefs.Save();
		if (playSound)
		{
			audio_Manager.InApp();
		}
	}

	public void UnlockSentinel(bool playSound)
	{
		string text = "Sentinel";
		for (int i = 0; i < playerPrefs_Players.Players_SetUp.Length; i++)
		{
			if (playerPrefs_Players.Players_SetUp[i].columnName == text)
			{
				playerPrefs_Players.Players_SetUp[i].status = "unlocked";
				playerPrefs_Players.Players_SetUp[i].bossToggle.interactable = true;
				playerPrefs_Players.Players_SetUp[i].lock_Panel.SetActive(false);
				playerPrefs_Players.Players_SetUp[i].bossInAppShop.gameObject.SetActive(false);
				break;
			}
		}
		shop_Gui.buyPanel.SetActive(false);
		PlayerPrefs_Saves.SaveLockedSentinel(false);
		PlayerPrefs.Save();
		if (playSound)
		{
			audio_Manager.InApp();
		}
	}

	public void UnlockSamurai(bool playSound)
	{
		string text = "SamuraiNEW";
		for (int i = 0; i < playerPrefs_Players.Players_SetUp.Length; i++)
		{
			if (playerPrefs_Players.Players_SetUp[i].columnName == text)
			{
				playerPrefs_Players.Players_SetUp[i].status = "unlocked";
				playerPrefs_Players.Players_SetUp[i].bossToggle.interactable = true;
				playerPrefs_Players.Players_SetUp[i].lock_Panel.SetActive(false);
				playerPrefs_Players.Players_SetUp[i].bossInAppShop.gameObject.SetActive(false);
				break;
			}
		}
		shop_Gui.buyPanel.SetActive(false);
		PlayerPrefs_Saves.SaveLockedSamurai(false);
		PlayerPrefs.Save();
		if (playSound)
		{
			audio_Manager.InApp();
		}
	}

	public void UnlockFireDragon(bool playSound)
	{
		string text = "FireDragonNEW";
		for (int i = 0; i < playerPrefs_Players.Players_SetUp.Length; i++)
		{
			if (playerPrefs_Players.Players_SetUp[i].columnName == text)
			{
				playerPrefs_Players.Players_SetUp[i].status = "unlocked";
				playerPrefs_Players.Players_SetUp[i].bossToggle.interactable = true;
				playerPrefs_Players.Players_SetUp[i].lock_Panel.SetActive(false);
				playerPrefs_Players.Players_SetUp[i].bossInAppShop.gameObject.SetActive(false);
				break;
			}
		}
		shop_Gui.buyPanel.SetActive(false);
		PlayerPrefs_Saves.SaveLockedFireDragon(false);
		PlayerPrefs.Save();
		if (playSound)
		{
			audio_Manager.InApp();
		}
	}

	public void UnlockMinigun(bool playSound)
	{
		string text = "Minigun";
		for (int i = 0; i < playerPrefs_Players.Players_SetUp.Length; i++)
		{
			if (playerPrefs_Players.Players_SetUp[i].columnName == text)
			{
				playerPrefs_Players.Players_SetUp[i].status = "unlocked";
				playerPrefs_Players.Players_SetUp[i].bossToggle.interactable = true;
				playerPrefs_Players.Players_SetUp[i].lock_Panel.SetActive(false);
				playerPrefs_Players.Players_SetUp[i].bossInAppShop.gameObject.SetActive(false);
				break;
			}
		}
		shop_Gui.buyPanel.SetActive(false);
		PlayerPrefs_Saves.SaveLockedMinigun(false);
		PlayerPrefs.Save();
		if (playSound)
		{
			audio_Manager.InApp();
		}
	}

	public void UnlockRhino(bool playSound)
	{
		string text = "Rhino";
		for (int i = 0; i < playerPrefs_Players.Players_SetUp.Length; i++)
		{
			if (playerPrefs_Players.Players_SetUp[i].columnName == text)
			{
				playerPrefs_Players.Players_SetUp[i].status = "unlocked";
				playerPrefs_Players.Players_SetUp[i].bossToggle.interactable = true;
				playerPrefs_Players.Players_SetUp[i].lock_Panel.SetActive(false);
				playerPrefs_Players.Players_SetUp[i].bossInAppShop.gameObject.SetActive(false);
				break;
			}
		}
		shop_Gui.buyPanel.SetActive(false);
		PlayerPrefs_Saves.SaveLockedRhino(false);
		PlayerPrefs.Save();
		if (playSound)
		{
			audio_Manager.InApp();
		}
	}

	public void UnlockGuardian(bool playSound)
	{
		string text = "Guardian";
		for (int i = 0; i < playerPrefs_Players.Players_SetUp.Length; i++)
		{
			if (playerPrefs_Players.Players_SetUp[i].columnName == text)
			{
				playerPrefs_Players.Players_SetUp[i].status = "unlocked";
				playerPrefs_Players.Players_SetUp[i].bossToggle.interactable = true;
				playerPrefs_Players.Players_SetUp[i].lock_Panel.SetActive(false);
				playerPrefs_Players.Players_SetUp[i].bossInAppShop.gameObject.SetActive(false);
				break;
			}
		}
		shop_Gui.buyPanel.SetActive(false);
		PlayerPrefs_Saves.SaveLockedGuardian(false);
		PlayerPrefs.Save();
		if (playSound)
		{
			audio_Manager.InApp();
		}
	}

	public void UnlockWolf(bool playSound)
	{
		string text = "Wolf";
		for (int i = 0; i < playerPrefs_Players.Players_SetUp.Length; i++)
		{
			if (playerPrefs_Players.Players_SetUp[i].columnName == text)
			{
				playerPrefs_Players.Players_SetUp[i].status = "unlocked";
				playerPrefs_Players.Players_SetUp[i].bossToggle.interactable = true;
				playerPrefs_Players.Players_SetUp[i].lock_Panel.SetActive(false);
				playerPrefs_Players.Players_SetUp[i].bossInAppShop.gameObject.SetActive(false);
				break;
			}
		}
		shop_Gui.buyPanel.SetActive(false);
		PlayerPrefs_Saves.SaveLockedWolf(false);
		PlayerPrefs.Save();
		if (playSound)
		{
			audio_Manager.InApp();
		}
	}

	public void UnlockWolfSubscription(bool playSound)
	{
		string text = "Wolf";
		for (int i = 0; i < playerPrefs_Players.Players_SetUp.Length; i++)
		{
			if (playerPrefs_Players.Players_SetUp[i].columnName == text)
			{
				playerPrefs_Players.Players_SetUp[i].status = "unlocked";
				playerPrefs_Players.Players_SetUp[i].bossToggle.interactable = true;
				playerPrefs_Players.Players_SetUp[i].lock_Panel.SetActive(false);
				playerPrefs_Players.Players_SetUp[i].bossInAppShop.gameObject.SetActive(false);
				break;
			}
		}
		shop_Gui.buyPanel.SetActive(false);
		if (playSound)
		{
			audio_Manager.InApp();
		}
	}

	public void Unlock_Spartan_Sentinel(bool playSound)
	{
		UnlockSpartan(false);
		UnlockSentinel(false);
		if (playSound)
		{
			audio_Manager.InApp();
		}
	}

	public void Unlock_Spartan_Sentinel_Samurai(bool playSound)
	{
		UnlockSpartan(false);
		UnlockSentinel(false);
		UnlockSamurai(false);
		if (playSound)
		{
			audio_Manager.InApp();
		}
	}

	public void Unlock_Spartan_Sentinel_Samurai_FireDragon(bool playSound)
	{
		UnlockSpartan(false);
		UnlockSentinel(false);
		UnlockSamurai(false);
		UnlockFireDragon(false);
		if (playSound)
		{
			audio_Manager.InApp();
		}
	}

	public void Unlock_Spartan_Sentinel_Samurai_FireDragon_Minigun(bool playSound)
	{
		UnlockSpartan(false);
		UnlockSentinel(false);
		UnlockSamurai(false);
		UnlockFireDragon(false);
		UnlockMinigun(false);
		if (playSound)
		{
			audio_Manager.InApp();
		}
	}

	public void Unlock_Spartan_Sentinel_Samurai_FireDragon_Minigun_Rhino(bool playSound)
	{
		UnlockSpartan(false);
		UnlockSentinel(false);
		UnlockSamurai(false);
		UnlockFireDragon(false);
		UnlockMinigun(false);
		UnlockRhino(false);
		if (playSound)
		{
			audio_Manager.InApp();
		}
	}

	public void Unlock_Spartan_Sentinel_Samurai_FireDragon_Minigun_Rhino_Guardian(bool playSound)
	{
		UnlockSpartan(false);
		UnlockSentinel(false);
		UnlockSamurai(false);
		UnlockFireDragon(false);
		UnlockMinigun(false);
		UnlockRhino(false);
		UnlockGuardian(false);
		if (playSound)
		{
			audio_Manager.InApp();
		}
	}

	public void Unlock_Spartan_Sentinel_Samurai_FireDragon_Minigun_Rhino_Guardian_Wolf(bool playSound)
	{
		UnlockSpartan(false);
		UnlockSentinel(false);
		UnlockSamurai(false);
		UnlockFireDragon(false);
		UnlockMinigun(false);
		UnlockRhino(false);
		UnlockGuardian(false);
		UnlockWolf(false);
		if (playSound)
		{
			audio_Manager.InApp();
		}
	}

	public void Unlock_All()
	{
		UnlockSpartan(false);
		UnlockSentinel(false);
		UnlockSamurai(false);
		UnlockFireDragon(false);
		UnlockMinigun(false);
		UnlockRhino(false);
		UnlockGuardian(false);
		UnlockWolf(false);
	}

	public void UnlockGems(int amount, bool playSound)
	{
		PlayerPrefs_Saves.SaveConsumables(amount);
		PlayerPrefs.Save();
		if (playSound)
		{
			audio_Manager.InApp();
		}
	}

	public void Unlock_SF_FireDragon_Minigun(bool playSound)
	{
		UnlockFireDragon(false);
		UnlockMinigun(false);
		if (playSound)
		{
			audio_Manager.InApp();
		}
	}

	public void Unlock_SF_FireDragon_Rhino(bool playSound)
	{
		UnlockFireDragon(false);
		UnlockRhino(false);
		if (playSound)
		{
			audio_Manager.InApp();
		}
	}

	public void Unlock_SF_FireDragon_Minigun_Rhino(bool playSound)
	{
		UnlockFireDragon(false);
		UnlockMinigun(false);
		UnlockRhino(false);
		if (playSound)
		{
			audio_Manager.InApp();
		}
	}

	public void PurchaseGempack_2500()
	{
		//inAppManager.PurchaseGempack_2500();
	}

	public void PurchaseGempack_7000()
	{
		//inAppManager.PurchaseGempack_7000();
	}

	public void PurchaseGempack_15000()
	{
		//inAppManager.PurchaseGempack_15000();
	}

	public void PurchaseGempack_50000()
	{
		//inAppManager.PurchaseGempack_50000();
	}

	public void PurchaseNoAds()
	{
		//inAppManager.PurchaseNoAds();
	}

	public void PurchaseSpartan()
	{
		//inAppManager.Purchase_Spartan();
	}

	public void PurchaseSentinel()
	{
		//inAppManager.Purchase_Sentinel();
	}

	public void PurchaseSamurai()
	{
		//inAppManager.Purchase_Samurai();
	}

	public void PurchaseFireDragon()
	{
		//inAppManager.Purchase_FireDragon();
	}

	public void PurchaseMinigun()
	{
		//inAppManager.Purchase_Minigun();
	}

	public void PurchaseRhino()
	{
		//inAppManager.Purchase_Rhino();
	}

	public void PurchaseGuardian()
	{
		//inAppManager.Purchase_Guardian();
	}

	public void PurchaseWolf()
	{
		//inAppManager.Purchase_Wolf();
	}

	public void Purchase_Combo_Spartan_Sentinel()
	{
		//inAppManager.Purchase_Combo_Spartan_Sentinel();
	}

	public void Purchase_Combo_Spartan_Sentinel_Samurai()
	{
		//inAppManager.Purchase_Combo_Spartan_Sentinel_Samurai();
	}

	public void Purchase_Combo_Spartan_Sentinel_Samurai_FireDragon()
	{
		//inAppManager.Purchase_Combo_Spartan_Sentinel_Samurai_FireDragon();
	}

	public void Purchase_Combo_Spartan_Sentinel_Samurai_FireDragon_Minigun()
	{
		//inAppManager.Purchase_Combo_Spartan_Sentinel_Samurai_FireDragon_Minigun();
	}

	public void Purchase_Combo_Spartan_Sentinel_Samurai_FireDragon_Minigun_Rhino()
	{
		//inAppManager.Purchase_Combo_Spartan_Sentinel_Samurai_FireDragon_Minigun_Rhino();
	}

	public void Purchase_Combo_Spartan_Sentinel_Samurai_FireDragon_Minigun_Rhino_Guardian()
	{
		//inAppManager.Purchase_Combo_Spartan_Sentinel_Samurai_FireDragon_Minigun_Rhino_Guardian();
	}

	public void Purchase_Combo_Spartan_Sentinel_Samurai_FireDragon_Minigun_Rhino_Guardian_Wolf()
	{
		//inAppManager.Purchase_Combo_Spartan_Sentinel_Samurai_FireDragon_Minigun_Rhino_Guardian_Wolf();
	}

	public void Purchase_SpecialOffer_FireDragon_Minigun()
	{
		//inAppManager.Purchase_SF_FireDragon_Minigun();
	}

	public void Purchase_SpecialOffer_FireDragon_Rhino()
	{
		//inAppManager.Purchase_SF_FireDragon_Rhino();
	}

	public void Purchase_SpecialOffer_FireDragon_Minigun_Rhino()
	{
		//inAppManager.Purchase_SF_FireDragon_Minigun_Rhino();
	}

	public void Purchase_SpecialOffer_Gempack_5000()
	{
	//inAppManager.Purchase_SF_Gempack_5000();
	}

	public void Purchase_SpecialOffer_Gempack_40000()
	{
		//inAppManager.Purchase_SF_Gempack_40000();
	}

	public void EnableVIP()
	{
		if (!isVIPEnabled)
		{
			isVIPEnabled = true;
			DateTime dateTime = DateTime.Now.ToUniversalTime();
			if (packagesManager.whichStore == App_Stores.IOS)
			{
				dateTime = PlayerPrefs_Saves.LoadSubscriptionAppleExpirationDate();
			}
			DateTime dateTime2 = PlayerPrefs_Saves.LoadSubscriptionPurchaseDate();
			DateTime dateTime3 = DateTime.Now.ToUniversalTime();
			DateTime dateTime4 = ((dateTime.CompareTo(dateTime3) <= 0) ? dateTime : dateTime3);
			int num = (dateTime4 - dateTime2).Days + 1;
			num -= PlayerPrefs_Saves.LoadSubscriptionCollectedDays();
			int amount = num * 200;
			//InAppManager.DebugIt(string.Concat("expirationDate: ", dateTime, ", purchaseDate: ", dateTime2, ", nowDate: ", dateTime3, ", endDate: ", dateTime4, ", daysToCollect: ", num, ", LoadSubscriptionCollectedDays: ", PlayerPrefs_Saves.LoadSubscriptionCollectedDays()));
			PlayerPrefs_Saves.SaveSubscriptionCollectedDays(PlayerPrefs_Saves.LoadSubscriptionCollectedDays() + num);
			PlayerPrefs.Save();
			UnlockGems(amount, false);
			level_Manager.RefreshUpgrades();
			UnlockWolfSubscription(false);
			CloseVipPanel();
		}
	}

	public void DisableVIP()
	{
		if (isVIPEnabled)
		{
			isVIPEnabled = false;
		}
	}

	public void LinkFacebookReward()
	{
		shop_Gui.facebookReward_Btn.gameObject.SetActive(false);
		PlayerPrefs_Saves.SaveConsumables(shop_Gui.rewardSocialMedia);
		PlayerPrefs_Saves.SaveFacebookReward(false);
		PlayerPrefs.Save();
		Application.OpenURL("https://www.facebook.com/rappidstudios/");
		Invoke("SetConsumablesTexts", 0.5f);
	}

	public void LinkFacebook()
	{
		Application.OpenURL("https://www.facebook.com/rappidstudios/");
	}

	public void LinkTwitterReward()
	{
		shop_Gui.twitterReward_Btn.gameObject.SetActive(false);
		PlayerPrefs_Saves.SaveConsumables(shop_Gui.rewardSocialMedia);
		PlayerPrefs_Saves.SaveTwitterReward(false);
		PlayerPrefs.Save();
		Application.OpenURL("https://twitter.com/rappidstudios/");
		Invoke("SetConsumablesTexts", 0.5f);
	}

	public void LinkTwitter()
	{
		Application.OpenURL("https://twitter.com/rappidstudios/");
	}

	private void ShowAchievements()
	{
		if (packagesManager.whichStore == App_Stores.Google || packagesManager.whichStore == App_Stores.IOS)
		{
			featuresManager.alManager.ShowAchievements();
		}
		else
		{
			MessagePopUp();
		}
	}

	public void Rate()
	{
		if (packagesManager.whichStore == App_Stores.Amazon)
		{
			Application.OpenURL(packagesManager.AmazonUrlRate);
		}
		else if (packagesManager.whichStore == App_Stores.Google)
		{
			Application.OpenURL(packagesManager.GoogleUrlRate);
		}
		else if (packagesManager.whichStore == App_Stores.IOS)
		{
			Application.OpenURL(packagesManager.IOSUrlRate);
		}
	}

	public void OpenRatePopUp(int Level)
	{
		if (Level >= game_Gui.rateLevelIndex && PlayerPrefs_Settings.LoadShowRatePopUp())
		{
			canRate = true;
			string title = "Do you Like this Game?";
			string descr = "Please take a moment to rate it.\nThank you!";
			OpenRateInfoPanel(title, descr);
		}
	}

	public void MessagePopUp()
	{
		string title = "Not Available !";
		string descr = "Coming Soon !";
		OpenRateInfoPanel(title, descr);
	}

	public void OpenConnectScreen()
	{
		multiplayerConnect_Gui.loadingPanel.SetActive(true);
	}

	public void CloseConnectScreen()
	{
		multiplayerConnect_Gui.loadingPanel.SetActive(false);
	}

	public void UpdateRequiredPopUp()
	{
		string title = "Update found!";
		string descr = "You need to update the app first!";
		OpenRateInfoPanel(title, descr);
	}

	public void ServerDownPopUp()
	{
		string title = "Server is down";
		string descr = "The server is down for maintenance.\nPlease try again later.";
		OpenRateInfoPanel(title, descr);
	}

	public void UnknownErrorPopUp()
	{
		string title = "Unknown error";
		string descr = "Please check your internet connection or try again later.";
		OpenRateInfoPanel(title, descr);
	}

	public void WrongDataPopUp()
	{
		string title = "Wrong data";
		string descr = "Username and password don't match, try again.";
		OpenRateInfoPanel(title, descr);
	}

	public void OpenPrivacyPolicy()
	{
		Application.OpenURL("https://rappidstudios.com/index.php/privacy-policy");
	}

	public void OpenTermsOfUse()
	{
		Application.OpenURL("https://rappidstudios.com/terms-of-use");
	}

	public void MultiplayerSetTime(int time)
	{
		setUpArmy_Gui.timer_Txt.text = time + string.Empty;
		setUpArmy_Gui.timer_Img.fillAmount = (80f - (float)time) / 80f;
		if (time == 0)
		{
			MultiplayerPlayerReady();
			MultiplayerEnemyReady();
		}
	}

	public void MultiplayerEnemyClearAmy()
	{
		if (level_Manager.isPlayer)
		{
			level_Manager.ResetEnemyArmy();
		}
		else
		{
			level_Manager.ResetPlayerArmy();
		}
		level_Manager.ResetEnemyMultiplayer();
	}

	public void MultiplayerEnemyReady()
	{
		if (!level_Manager.isEnemyReady)
		{
			level_Manager.isEnemyReady = true;
			setUpArmy_Gui.bg.sprite = (level_Manager.isPlayerReady ? setUpArmy_Gui.bothReadyBG : ((!level_Manager.isPlayer) ? setUpArmy_Gui.playerReadyBG : setUpArmy_Gui.enemyReadyBG));
			if (level_Manager.isPlayerReady && !multiplayerStarted)
			{
				StartCoroutine("MultiplayerStartGame");
			}
		}
	}

	public void MultiplayerPlayerReady()
	{
		if (!level_Manager.isPlayerReady)
		{
			ReadyMultiplayer();
			level_Manager.isPlayerReady = true;
			multiplayerController.Send_Ready();
			setUpArmy_Gui.bg.sprite = (level_Manager.isEnemyReady ? setUpArmy_Gui.bothReadyBG : ((!level_Manager.isPlayer) ? setUpArmy_Gui.enemyReadyBG : setUpArmy_Gui.playerReadyBG));
			if (level_Manager.isEnemyReady && !multiplayerStarted)
			{
				StartCoroutine("MultiplayerStartGame");
			}
		}
	}

	public IEnumerator MultiplayerStartGame()
	{
		multiplayerStarted = true;
		yield return new WaitForSeconds(1f);
		StartGame();
	}

	private void ReadyMultiplayer()
	{
		setUpArmy_Gui.startGame_Btn.interactable = false;
		setUpArmy_Gui.resetArmy_Btn.interactable = false;
		if (level_Manager.isPlayer)
		{
			level_Manager.SetPlayerQuad(false);
		}
		else
		{
			level_Manager.SetEnemyQuad(false);
		}
	}

	private void ResetReadyMultiplayer()
	{
		setUpArmy_Gui.startGame_Btn.interactable = true;
		setUpArmy_Gui.resetArmy_Btn.interactable = true;
	}

	private void PreventResetMultiplayer()
	{
		if (level_Manager.gameMode == GameMode.Multiplayer)
		{
			setUpArmy_Gui.resetArmy_Btn.interactable = false;
		}
	}

	private void OpenSetUpArmy()
	{
		StopCoroutine("OpenSetUpArmy_CR");
		StartCoroutine("OpenSetUpArmy_CR");
	}

	private void CloseSetUpArmy()
	{
		if (level_Manager.gameMode == GameMode.Multiplayer)
		{
			StopCoroutine("MultiplayerStartGame");
		}
		StopCoroutine("CloseSetUpArmy_CR");
		StartCoroutine("CloseSetUpArmy_CR");
	}

	private IEnumerator OpenSetUpArmy_CR()
	{
		SetWhichGuiIsOpen(WhichGuiIsOpen.Between);
		animation_Gui.SetCanvas(true);
		animation_Gui.thisCanvasGroup.alpha = 0f;
		float t2 = 0f;
		float lerpDuration = 0.25f;
		while (t2 < 1f)
		{
			t2 += Time.deltaTime / lerpDuration;
			animation_Gui.thisCanvasGroup.alpha = Mathf.Lerp(0f, 1f, t2);
			yield return null;
		}
		main_Gui.SetCanvas(false);
		level_Manager.SetMenuGOs(false);
		SetUpArmyPanel();
		OpenSetUpArmyPanel();
		ChangeCamera(CustomCameraType.Setup);
		t2 = 0f;
		while (t2 < 1f)
		{
			t2 += Time.deltaTime / lerpDuration;
			animation_Gui.thisCanvasGroup.alpha = Mathf.Lerp(1f, 0f, t2);
			yield return null;
		}
		animation_Gui.SetCanvas(false);
		SetWhichGuiIsOpen(WhichGuiIsOpen.SetUpArmy_Gui);
	}

	private IEnumerator CloseSetUpArmy_CR()
	{
		SetWhichGuiIsOpen(WhichGuiIsOpen.Between);
		animation_Gui.SetCanvas(true);
		animation_Gui.thisCanvasGroup.alpha = 0f;
		float t2 = 0f;
		float lerpDuration = 0.25f;
		while (t2 < 1f)
		{
			t2 += Time.deltaTime / lerpDuration;
			animation_Gui.thisCanvasGroup.alpha = Mathf.Lerp(0f, 1f, t2);
			yield return null;
		}
		main_Gui.SetCanvas(true);
		level_Manager.SetMenuGOs(true);
		level_Manager.SetLevelDesign(LevelDesign.None);
		EpicLevelsBtnsLogic();
		CloseSetUpArmyPanel();
		ChangeCamera(CustomCameraType.Nothing);
		t2 = 0f;
		while (t2 < 1f)
		{
			t2 += Time.deltaTime / lerpDuration;
			animation_Gui.thisCanvasGroup.alpha = Mathf.Lerp(1f, 0f, t2);
			yield return null;
		}
		animation_Gui.SetCanvas(false);
		SetWhichGuiIsOpen(WhichGuiIsOpen.CanQuit);
	}

	private void SetUpArmyPanel()
	{
		level_Manager.ResetLevelsArmy();
		level_Manager.ResetPlayerArmy();
		level_Manager.ResetEnemyArmy();
		if (level_Manager.gameMode == GameMode.Levels)
		{
			setUpArmy_Gui.PlayLevels_UIs.SetActive(true);
			setUpArmy_Gui.PlayCustom_UIs.SetActive(false);
			setUpArmy_Gui.PlayMultiplayer_UIs.SetActive(false);
			setUpArmy_Gui.Levels_UIs.SetActive(true);
			setUpArmy_Gui.EpicLevels_UIs.SetActive(false);
			setUpArmy_Gui.back_Btn.interactable = true;
			setUpArmy_Gui.changeLevelDesign_Btn.interactable = false;
			setUpArmy_Gui.changeLevelDesign_Btn.GetComponent<CanvasGroup>().alpha = 0.6f;
			level_Manager.isPlayer = true;
			level_Manager.SetPlayerQuad(true);
			level_Manager.SetEnemyQuad(false);
			level_Manager.SetTargetLevel();
			level_Manager.GetAvailableValues();
			level_Manager.SetLevelDesign(level_Manager.LevelsValues[level_Manager.levelIndex].design);
		}
		else if (level_Manager.gameMode == GameMode.EpicLevels)
		{
			setUpArmy_Gui.PlayLevels_UIs.SetActive(true);
			setUpArmy_Gui.PlayCustom_UIs.SetActive(false);
			setUpArmy_Gui.PlayMultiplayer_UIs.SetActive(false);
			setUpArmy_Gui.Levels_UIs.SetActive(false);
			setUpArmy_Gui.EpicLevels_UIs.SetActive(true);
			setUpArmy_Gui.back_Btn.interactable = true;
			setUpArmy_Gui.changeLevelDesign_Btn.interactable = false;
			setUpArmy_Gui.changeLevelDesign_Btn.GetComponent<CanvasGroup>().alpha = 0.6f;
			level_Manager.isPlayer = true;
			level_Manager.SetPlayerQuad(true);
			level_Manager.SetEnemyQuad(false);
			level_Manager.SetTargetEpicLevel();
			level_Manager.GetAvailableValuesEpic();
			level_Manager.SetLevelDesign(level_Manager.EpicLevelsValues[level_Manager.epicLevelIndex].design);
		}
		else if (level_Manager.gameMode == GameMode.Custom)
		{
			setUpArmy_Gui.PlayLevels_UIs.SetActive(false);
			setUpArmy_Gui.PlayCustom_UIs.SetActive(true);
			setUpArmy_Gui.PlayMultiplayer_UIs.SetActive(false);
			setUpArmy_Gui.back_Btn.interactable = true;
			setUpArmy_Gui.changeLevelDesign_Btn.interactable = true;
			setUpArmy_Gui.changeLevelDesign_Btn.GetComponent<CanvasGroup>().alpha = 1f;
			level_Manager.isPlayer = true;
			level_Manager.SetPlayerQuad(true);
			level_Manager.SetEnemyQuad(true);
			level_Manager.SetLevelDesign(level_Manager.customDesign);
			ResetBlueArmyGoldCustom();
			ResetRedArmyGoldCustom();
		}
		else if (level_Manager.gameMode == GameMode.Multiplayer)
		{
			setUpArmy_Gui.PlayLevels_UIs.SetActive(false);
			setUpArmy_Gui.PlayCustom_UIs.SetActive(false);
			setUpArmy_Gui.PlayMultiplayer_UIs.SetActive(true);
			setUpArmy_Gui.resetArmy_Btn.interactable = false;
			setUpArmy_Gui.back_Btn.interactable = false;
			setUpArmy_Gui.changeLevelDesign_Btn.interactable = false;
			setUpArmy_Gui.changeLevelDesign_Btn.GetComponent<CanvasGroup>().alpha = 0.6f;
			level_Manager.SetPlayerQuad(level_Manager.isPlayer);
			level_Manager.SetEnemyQuad(!level_Manager.isPlayer);
			multiplayerConnect_Gui.SetCanvas(false);
		}
		ResetPlaceRemoveBtn();
		ResetReadyMultiplayer();
		PreventResetMultiplayer();
		SetAvailableTexts();
		SetLevelButtons();
	}

	private void CheckCanPlayMultiplayer()
	{
		int num = level_Manager.CountCompletedLevels();
		int num2 = level_Manager.CountCompletedEpicLevels();
		int num3 = 0;
		int num4 = 0;
		if (num >= num3 && num2 >= num4)
		{
			level_Manager.gameMode = GameMode.Multiplayer;
			level_Manager.ResetMultiplayer();
			OpenMultiplayerConnect();
		}
	}

	private void InitToggles()
	{
		if (PlayerPrefs_Settings.LoadSoundSetUp())
		{
			settings_Gui.sound_Toggle.isOn = true;
			settings_Gui.sound_Toggle.GetComponent<Image>().sprite = settings_Gui.soundIsOn;
		}
		else
		{
			settings_Gui.sound_Toggle.isOn = false;
			settings_Gui.sound_Toggle.GetComponent<Image>().sprite = settings_Gui.soundIsOff;
		}
		if (PlayerPrefs_Settings.LoadMusicSetUp())
		{
			settings_Gui.music_Toggle.isOn = true;
			settings_Gui.music_Toggle.GetComponent<Image>().sprite = settings_Gui.musicIsOn;
		}
		else
		{
			settings_Gui.music_Toggle.isOn = false;
			settings_Gui.music_Toggle.GetComponent<Image>().sprite = settings_Gui.musicIsOff;
		}
	}

	private void HandleMusic(bool status)
	{
		if (status)
		{
			audio_Manager.SetBackGroundMusicVolume(audio_Manager.backgroundMusicVolume);
			settings_Gui.music_Toggle.GetComponent<Image>().sprite = settings_Gui.musicIsOn;
			PlayerPrefs_Settings.SaveMusicSetUp(true);
		}
		else
		{
			audio_Manager.SetBackGroundMusicVolume(0f);
			settings_Gui.music_Toggle.GetComponent<Image>().sprite = settings_Gui.musicIsOff;
			PlayerPrefs_Settings.SaveMusicSetUp(false);
		}
	}

	private void HandleSound(bool status)
	{
		if (status)
		{
			audio_Manager.SetAudioListernerVolume(1f);
			settings_Gui.sound_Toggle.GetComponent<Image>().sprite = settings_Gui.soundIsOn;
			PlayerPrefs_Settings.SaveSoundSetUp(true);
		}
		else
		{
			audio_Manager.SetAudioListernerVolume(0f);
			settings_Gui.sound_Toggle.GetComponent<Image>().sprite = settings_Gui.soundIsOff;
			PlayerPrefs_Settings.SaveSoundSetUp(false);
		}
	}

	private void EpicLevelsBtnsLogic()
	{
		int num = level_Manager.CountCompletedLevels();
		int unlockEpicLevelsIndex = level_Manager.unlockEpicLevelsIndex;
		if (num >= unlockEpicLevelsIndex)
		{
			main_Gui.playEpicLevels_Btn.gameObject.SetActive(true);
			main_Gui.playEpicLevelsInfo_Btn.gameObject.SetActive(false);
		}
		else
		{
			main_Gui.playEpicLevels_Btn.gameObject.SetActive(false);
			main_Gui.playEpicLevelsInfo_Btn.gameObject.SetActive(true);
		}
	}

	private void EpicLevelsInfo_Btn()
	{
		OpenInAppInfoPanel("Epic Levels Locked", "In order to unlock  (Epic Levels)  you have to complete at least  (" + level_Manager.unlockEpicLevelsIndex + ")  Levels", false, false);
	}

	private void OpenTutorialPanel()
	{
		SetWhichGuiIsOpen(WhichGuiIsOpen.Tutorial_Gui);
		main_Gui.tutorialPanel.SetActive(true);
	}

	private void CloseTutorialPanel()
	{
		SetWhichGuiIsOpen(WhichGuiIsOpen.CanQuit);
		main_Gui.tutorialPanel.SetActive(false);
	}

	private void StartTutorial()
	{
		loading_Gui.SetCanvas(true);
		SceneManager.LoadSceneAsync("TutorialScene", LoadSceneMode.Single);
	}

	public void SetCanvasLoading(bool state)
	{
	}

	public void SetCrossPromotionState(bool state)
	{
		state = false;
		main_Gui.crossPromotion_Btn.gameObject.SetActive(state);
	}

	private void OpenSettings()
	{
		StopCoroutine("OpenSettings_CR");
		StartCoroutine("OpenSettings_CR");
	}

	private void CloseSettings()
	{
		StopCoroutine("CloseSettings_CR");
		StartCoroutine("CloseSettings_CR");
	}

	private IEnumerator OpenSettings_CR()
	{
		animation_Gui.SetCanvas(true);
		animation_Gui.thisCanvasGroup.alpha = 0f;
		float t2 = 0f;
		float lerpDuration = 0.25f;
		while (t2 < 1f)
		{
			t2 += Time.deltaTime / lerpDuration;
			animation_Gui.thisCanvasGroup.alpha = Mathf.Lerp(0f, 1f, t2);
			yield return null;
		}
		main_Gui.SetCanvas(false);
		settings_Gui.SetCanvas(true);
		level_Manager.SetMenuTroops(false);
		t2 = 0f;
		while (t2 < 1f)
		{
			t2 += Time.deltaTime / lerpDuration;
			animation_Gui.thisCanvasGroup.alpha = Mathf.Lerp(1f, 0f, t2);
			yield return null;
		}
		animation_Gui.SetCanvas(false);
		SetWhichGuiIsOpen(WhichGuiIsOpen.Settings_Gui);
	}

	private IEnumerator CloseSettings_CR()
	{
		animation_Gui.SetCanvas(true);
		animation_Gui.thisCanvasGroup.alpha = 0f;
		float t2 = 0f;
		float lerpDuration = 0.25f;
		while (t2 < 1f)
		{
			t2 += Time.deltaTime / lerpDuration;
			animation_Gui.thisCanvasGroup.alpha = Mathf.Lerp(0f, 1f, t2);
			yield return null;
		}
		main_Gui.SetCanvas(true);
		settings_Gui.SetCanvas(false);
		level_Manager.SetMenuTroops(true);
		t2 = 0f;
		while (t2 < 1f)
		{
			t2 += Time.deltaTime / lerpDuration;
			animation_Gui.thisCanvasGroup.alpha = Mathf.Lerp(1f, 0f, t2);
			yield return null;
		}
		animation_Gui.SetCanvas(false);
		SetWhichGuiIsOpen(WhichGuiIsOpen.CanQuit);
	}

	public void OpenCloudSavePanel()
	{
		if (packagesManager.whichStore == App_Stores.Google || packagesManager.whichStore == App_Stores.IOS)
		{
			settings_Gui.cloudSavePanel.SetActive(true);
			SetWhichGuiIsOpen(WhichGuiIsOpen.CloudSave_Gui);
		}
		else
		{
			MessagePopUp();
		}
	}

	public void CloseCloudSavePanel()
	{
		settings_Gui.cloudSavePanel.SetActive(false);
		SetWhichGuiIsOpen(WhichGuiIsOpen.Settings_Gui);
	}

	private void ResetSettingsPanels()
	{
		settings_Gui.settingsContent.SetActive(true);
		settings_Gui.levelstatsContent.SetActive(false);
		settings_Gui.settings_Toggle.GetComponent<CanvasGroup>().alpha = 1f;
		settings_Gui.levelstats_Toggle.GetComponent<CanvasGroup>().alpha = 0.4f;
	}

	private void SettingPanel(bool status)
	{
		if (status)
		{
			settings_Gui.settingsContent.SetActive(true);
			settings_Gui.levelstatsContent.SetActive(false);
			settings_Gui.settings_Toggle.GetComponent<CanvasGroup>().alpha = 1f;
			settings_Gui.levelstats_Toggle.GetComponent<CanvasGroup>().alpha = 0.4f;
		}
	}

	private void LevelstatsPanel(bool status)
	{
		if (status)
		{
			settings_Gui.settingsContent.SetActive(false);
			settings_Gui.levelstatsContent.SetActive(true);
			settings_Gui.settings_Toggle.GetComponent<CanvasGroup>().alpha = 0.4f;
			settings_Gui.levelstats_Toggle.GetComponent<CanvasGroup>().alpha = 1f;
			SetLevelsStats();
		}
	}

	private void OpenInAppGui()
	{
		StopCoroutine("OpenInAppGui_CR");
		StartCoroutine("OpenInAppGui_CR");
	}

	private void CloseInAppGui()
	{
		StopCoroutine("CloseInAppGui_CR");
		StartCoroutine("CloseInAppGui_CR");
	}

	private IEnumerator OpenInAppGui_CR()
	{
		SetWhichGuiIsOpen(WhichGuiIsOpen.Between);
		animation_Gui.SetCanvas(true);
		animation_Gui.thisCanvasGroup.alpha = 0f;
		inApp_Gui.SetCanvas(true);
		inApp_Gui.thisCanvasGroup.alpha = 0f;
		float t2 = 0f;
		float lerpDuration = 0.3f;
		t2 = 0f;
		while (t2 < 1f)
		{
			t2 += Time.deltaTime / lerpDuration;
			inApp_Gui.thisCanvasGroup.alpha = Mathf.Lerp(0f, 1f, t2);
			yield return null;
		}
		main_Gui.SetCanvas(false);
		animation_Gui.SetCanvas(false);
		level_Manager.SetMenuTroops(false);
		SetWhichGuiIsOpen(WhichGuiIsOpen.InApp_Gui);
	}

	private IEnumerator CloseInAppGui_CR()
	{
		SetWhichGuiIsOpen(WhichGuiIsOpen.Between);
		level_Manager.SetMenuTroops(true);
		yield return null;
		main_Gui.SetCanvas(true);
		main_Gui.thisCanvasGroup.alpha = 1f;
		inApp_Gui.SetCanvas(false);
		SetWhichGuiIsOpen(WhichGuiIsOpen.CanQuit);
	}

	private void OpenInAppSpecialGui()
	{
		StopCoroutine("OpenInAppSpecialGui_CR");
		StartCoroutine("OpenInAppSpecialGui_CR");
	}

	private void CloseInAppSpecialGui()
	{
		StopCoroutine("CloseInAppSpecialGui_CR");
		StartCoroutine("CloseInAppSpecialGui_CR");
	}

	private IEnumerator OpenInAppSpecialGui_CR()
	{
		SetWhichGuiIsOpen(WhichGuiIsOpen.Between);
		animation_Gui.SetCanvas(true);
		animation_Gui.thisCanvasGroup.alpha = 0f;
		float t2 = 0f;
		float lerpDuration = 0.25f;
		while (t2 < 1f)
		{
			t2 += Time.deltaTime / lerpDuration;
			animation_Gui.thisCanvasGroup.alpha = Mathf.Lerp(0f, 1f, t2);
			yield return null;
		}
		main_Gui.SetCanvas(false);
		inAppSpecial_Gui.SetCanvas(true);
		level_Manager.SetMenuTroops(false);
		t2 = 0f;
		while (t2 < 1f)
		{
			t2 += Time.deltaTime / lerpDuration;
			animation_Gui.thisCanvasGroup.alpha = Mathf.Lerp(1f, 0f, t2);
			yield return null;
		}
		animation_Gui.SetCanvas(false);
		SetWhichGuiIsOpen(WhichGuiIsOpen.InAppSpecial_Gui);
	}

	private IEnumerator CloseInAppSpecialGui_CR()
	{
		SetWhichGuiIsOpen(WhichGuiIsOpen.Between);
		animation_Gui.SetCanvas(true);
		animation_Gui.thisCanvasGroup.alpha = 0f;
		float t2 = 0f;
		float lerpDuration = 0.25f;
		while (t2 < 1f)
		{
			t2 += Time.deltaTime / lerpDuration;
			animation_Gui.thisCanvasGroup.alpha = Mathf.Lerp(0f, 1f, t2);
			yield return null;
		}
		main_Gui.SetCanvas(true);
		inAppSpecial_Gui.SetCanvas(false);
		level_Manager.SetMenuTroops(true);
		t2 = 0f;
		while (t2 < 1f)
		{
			t2 += Time.deltaTime / lerpDuration;
			animation_Gui.thisCanvasGroup.alpha = Mathf.Lerp(1f, 0f, t2);
			yield return null;
		}
		animation_Gui.SetCanvas(false);
		SetWhichGuiIsOpen(WhichGuiIsOpen.CanQuit);
	}

	private void SetInAppSpecialButtons()
	{
	}

	private void OpenSetUpArmyPanel()
	{
		CacheAds();
		IsRewardedVideo_Available();
		setUpArmy_Gui.SetCanvas(true);
		topPanel_Manager.ResetTopPanelPanel();
		infoPanel_Manager.ResetInfoPanel();
	}

	private void CloseSetUpArmyPanel()
	{
		setUpArmy_Gui.SetCanvas(false);
		if (level_Manager.gameMode == GameMode.Multiplayer)
		{
			setUpArmy_Gui.bg.sprite = setUpArmy_Gui.normalBG;
			level_Manager.isPlayerReady = false;
			level_Manager.isEnemyReady = false;
			multiplayerStarted = false;
			CloseMessage();
		}
	}

	private void ResetArmies()
	{
		if (level_Manager.gameMode == GameMode.Levels)
		{
			level_Manager.GetAvailableValues();
			level_Manager.ResetPlayerArmy();
		}
		else if (level_Manager.gameMode == GameMode.EpicLevels)
		{
			level_Manager.GetAvailableValuesEpic();
			level_Manager.ResetPlayerArmy();
		}
		else if (level_Manager.gameMode == GameMode.Custom)
		{
			level_Manager.ResetLevelsArmy();
			level_Manager.ResetPlayerArmy();
			level_Manager.ResetEnemyArmy();
			ResetBlueArmyGoldCustom();
			ResetRedArmyGoldCustom();
		}
		else if (level_Manager.gameMode == GameMode.Multiplayer)
		{
			level_Manager.ResetLevelsArmy();
			if (level_Manager.isPlayer)
			{
				level_Manager.ResetPlayerArmy();
			}
			else
			{
				level_Manager.ResetEnemyArmy();
			}
			level_Manager.ResetPlayerMultiplayer();
			multiplayerController.Send_ClearArmy();
		}
		if (level_Manager.gameMode == GameMode.Levels)
		{
			level_Manager.SetLevelDesign(level_Manager.LevelsValues[level_Manager.levelIndex].design);
		}
		else if (level_Manager.gameMode == GameMode.EpicLevels)
		{
			level_Manager.SetLevelDesign(level_Manager.EpicLevelsValues[level_Manager.epicLevelIndex].design);
		}
		else if (level_Manager.gameMode == GameMode.Custom)
		{
			level_Manager.SetLevelDesignCustom();
		}
		else if (level_Manager.gameMode == GameMode.Multiplayer)
		{
			level_Manager.SetLevelDesignMultiplayer();
		}
		SetLevelButtons();
		SetAvailableTexts();
	}

	private void PlayFirstIncompleteLevel()
	{
		if (level_Manager.gameMode == GameMode.Levels)
		{
			level_Manager.SetTargetLevel();
			level_Manager.GetAvailableValues();
			level_Manager.SetLevelDesign(level_Manager.LevelsValues[level_Manager.levelIndex].design);
		}
		else if (level_Manager.gameMode == GameMode.EpicLevels)
		{
			level_Manager.SetTargetEpicLevel();
			level_Manager.GetAvailableValuesEpic();
			level_Manager.SetLevelDesign(level_Manager.EpicLevelsValues[level_Manager.epicLevelIndex].design);
		}
		SetLevelButtons();
		SetAvailableTexts();
	}

	public void CheckCanOpenLockPanel()
	{
		int targetIndex = 0;
		if (level_Manager.gameMode == GameMode.Levels)
		{
			targetIndex = level_Manager.levelIndex;
		}
		else if (level_Manager.gameMode == GameMode.EpicLevels)
		{
			targetIndex = level_Manager.epicLevelIndex;
		}
		bool flag = CheckCanStartLevel(targetIndex);
		setUpArmy_Gui.LockedPanel.SetActive(!flag);
	}

	private void CheckCanStartGame()
	{
		if (level_Manager.gameMode == GameMode.Levels)
		{
			int levelIndex = level_Manager.levelIndex;
			if (CheckCanStartLevel(levelIndex))
			{
				if (level_Manager.CountActiveArmyPlayer() > 0)
				{
					level_Manager.lastPlayedLevelIndex = level_Manager.levelIndex;
					StartGame();
				}
				else
				{
					OpenMessage("Add Some Troops First!");
				}
			}
			else
			{
				level_Manager.SetTargetLevel();
				level_Manager.GetAvailableValues();
				level_Manager.SetLevelDesign(level_Manager.LevelsValues[level_Manager.levelIndex].design);
				OpenMessage("First Win All Previous Levels");
				SetLevelButtons();
				SetAvailableTexts();
			}
		}
		else if (level_Manager.gameMode == GameMode.EpicLevels)
		{
			int epicLevelIndex = level_Manager.epicLevelIndex;
			if (CheckCanStartLevel(epicLevelIndex))
			{
				if (level_Manager.CountActiveArmyPlayer() > 0)
				{
					level_Manager.lastPlayedEpicLevelIndex = level_Manager.epicLevelIndex;
					StartGame();
				}
				else
				{
					OpenMessage("Add Some Troops First!");
				}
			}
			else
			{
				level_Manager.SetTargetEpicLevel();
				level_Manager.GetAvailableValuesEpic();
				level_Manager.SetLevelDesign(level_Manager.EpicLevelsValues[level_Manager.epicLevelIndex].design);
				OpenMessage("First Win All Previous Epic Levels");
				SetLevelButtons();
				SetAvailableTexts();
			}
		}
		else if (level_Manager.gameMode == GameMode.Custom)
		{
			if (level_Manager.CountActiveArmyPlayer() > 0 && level_Manager.CountActiveArmyEnemy() > 0)
			{
				StartGame();
			}
			else if (level_Manager.CountActiveArmyPlayer() == 0 && level_Manager.CountActiveArmyEnemy() == 0)
			{
				OpenMessage("Add Some Troops First!");
			}
			else if (level_Manager.CountActiveArmyPlayer() == 0)
			{
				OpenMessage("Blue Team Has No Troops!");
			}
			else if (level_Manager.CountActiveArmyEnemy() == 0)
			{
				OpenMessage("Red Team Has No Troops!");
			}
		}
		else if (level_Manager.gameMode == GameMode.Multiplayer)
		{
			bool flag = false;
			if (level_Manager.isPlayer && level_Manager.CountActiveArmyPlayer() > 0)
			{
				flag = true;
			}
			else if (!level_Manager.isPlayer && level_Manager.CountActiveArmyEnemy() > 0)
			{
				flag = true;
			}
			if (flag)
			{
				MultiplayerPlayerReady();
			}
			else
			{
				OpenMessage("Add Some Troops First!");
			}
		}
	}

	private bool CheckCanStartLevel(int targetIndex)
	{
		bool result = true;
		int num = 0;
		if (level_Manager.gameMode == GameMode.Levels)
		{
			num = PlayerPrefs_Saves.LoadLevelsCompleted();
		}
		else if (level_Manager.gameMode == GameMode.EpicLevels)
		{
			num = PlayerPrefs_Saves.LoadEpicLevelsCompleted();
		}
		if (targetIndex > num)
		{
			result = false;
		}
		return result;
	}

	private void StartGame()
	{
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
		SetWhichGuiIsOpen(WhichGuiIsOpen.Between);
		setUpArmy_Gui.thisCanvasGroup.alpha = 1f;
		setUpArmy_Gui.thisCanvasGroup.interactable = false;
		float t2 = 0f;
		float lerpDuration2 = 0.5f;
		while (t2 < 1f)
		{
			t2 += Time.deltaTime / lerpDuration2;
			setUpArmy_Gui.thisCanvasGroup.alpha = Mathf.Lerp(1f, 0f, t2);
			yield return null;
		}
		StartGameMode();
		CloseSetUpArmyPanel();
		setUpArmy_Gui.thisCanvasGroup.alpha = 1f;
		setUpArmy_Gui.thisCanvasGroup.interactable = true;
		CloseSurrenderPanel();
		if (level_Manager.gameMode == GameMode.Multiplayer)
		{
			game_Gui.pause_Btn.gameObject.SetActive(false);
			game_Gui.surrender_Btn.gameObject.SetActive(true);
		}
		else
		{
			game_Gui.pause_Btn.gameObject.SetActive(true);
			game_Gui.surrender_Btn.gameObject.SetActive(false);
		}
		game_Gui.SetCanvas(true);
		game_Gui.thisCanvasGroup.alpha = 0f;
		game_Gui.thisCanvasGroup.interactable = true;
		game_Gui.pause_Btn.interactable = false;
		level_Manager.UnlockSetUI(true);
		if (level_Manager.gameMode == GameMode.Multiplayer)
		{
			game_Gui.versusPanel.gameObject.SetActive(true);
			if (level_Manager.isPlayer)
			{
				game_Gui.playerVS_Txt.text = playerName;
				game_Gui.enemyVS_Txt.text = enemyName;
			}
			else
			{
				game_Gui.playerVS_Txt.text = enemyName;
				game_Gui.enemyVS_Txt.text = playerName;
			}
		}
		else
		{
			game_Gui.versusPanel.gameObject.SetActive(false);
		}
		t2 = 0f;
		lerpDuration2 = 0.4f;
		while (t2 < 1f)
		{
			t2 += Time.deltaTime / lerpDuration2;
			game_Gui.thisCanvasGroup.alpha = Mathf.Lerp(0f, 1f, t2);
			yield return null;
		}
		game_Gui.pause_Btn.interactable = true;
		gc.Begin();
		SetWhichGuiIsOpen(WhichGuiIsOpen.Game_Gui);
	}

	private void StartGameMode()
	{
		if (level_Manager.gameMode == GameMode.Levels)
		{
			level_Manager.ResetBotList();
			level_Manager.ResetLevelsArmy();
			level_Manager.SpawnPlayerArmy();
			level_Manager.SpawnLevelsArmy();
			level_Manager.FinalizeBotList();
		}
		else if (level_Manager.gameMode == GameMode.EpicLevels)
		{
			level_Manager.ResetBotList();
			level_Manager.ResetLevelsArmy();
			level_Manager.SpawnPlayerArmy();
			level_Manager.SpawnEpicLevelsArmy();
			level_Manager.FinalizeBotList();
		}
		else if (level_Manager.gameMode == GameMode.Custom)
		{
			level_Manager.ResetBotList();
			level_Manager.SpawnPlayerArmy();
			level_Manager.SpawnEnemyArmy();
			level_Manager.FinalizeBotList();
		}
		else if (level_Manager.gameMode == GameMode.Multiplayer)
		{
			level_Manager.ResetBotList();
			level_Manager.SpawnPlayerArmy();
			level_Manager.SpawnEnemyArmy();
			level_Manager.FinalizeBotList();
		}
	}

	private void SetLevelButtons()
	{
		setUpArmy_Gui.previousLevel_Btn.interactable = true;
		setUpArmy_Gui.nextLevel_Btn.interactable = true;
		if (level_Manager.gameMode == GameMode.Levels)
		{
			int levelIndex = level_Manager.levelIndex;
			int num = level_Manager.LevelsValues.Length - 1;
			if (levelIndex == 0)
			{
				setUpArmy_Gui.previousLevel_Btn.interactable = false;
			}
			if (levelIndex == num)
			{
				setUpArmy_Gui.nextLevel_Btn.interactable = false;
			}
		}
		else if (level_Manager.gameMode == GameMode.EpicLevels)
		{
			int epicLevelIndex = level_Manager.epicLevelIndex;
			int num2 = level_Manager.EpicLevelsValues.Length - 1;
			if (epicLevelIndex == 0)
			{
				setUpArmy_Gui.previousLevel_Btn.interactable = false;
			}
			if (epicLevelIndex == num2)
			{
				setUpArmy_Gui.nextLevel_Btn.interactable = false;
			}
		}
	}

	public void NextLevel()
	{
		if (setUpArmy_Gui.nextLevel_Btn.interactable)
		{
			if (level_Manager.gameMode == GameMode.Levels)
			{
				level_Manager.NextLevel();
				level_Manager.GetAvailableValues();
				level_Manager.SetLevelDesign(level_Manager.LevelsValues[level_Manager.levelIndex].design);
			}
			else if (level_Manager.gameMode == GameMode.EpicLevels)
			{
				level_Manager.NextEpicLevel();
				level_Manager.GetAvailableValuesEpic();
				level_Manager.SetLevelDesign(level_Manager.EpicLevelsValues[level_Manager.epicLevelIndex].design);
			}
			level_Manager.ResetPlayerArmy();
			SetLevelButtons();
			SetAvailableTexts();
			audio_Manager.MenuClick();
		}
	}

	public void PreviousLevel()
	{
		if (setUpArmy_Gui.previousLevel_Btn.interactable)
		{
			if (level_Manager.gameMode == GameMode.Levels)
			{
				level_Manager.PreviousLevel();
				level_Manager.GetAvailableValues();
				level_Manager.SetLevelDesign(level_Manager.LevelsValues[level_Manager.levelIndex].design);
			}
			else if (level_Manager.gameMode == GameMode.EpicLevels)
			{
				level_Manager.PreviousEpicLevel();
				level_Manager.GetAvailableValuesEpic();
				level_Manager.SetLevelDesign(level_Manager.EpicLevelsValues[level_Manager.epicLevelIndex].design);
			}
			level_Manager.ResetPlayerArmy();
			SetLevelButtons();
			SetAvailableTexts();
			audio_Manager.MenuClick();
		}
	}

	private void InitArmiesUIs()
	{
		setUpArmy_Gui.Melee_Toggle.GetComponent<CanvasGroup>().alpha = 1f;
		setUpArmy_Gui.meleePanel.GetComponent<CanvasGroup>().alpha = 1f;
		setUpArmy_Gui.meleePanel.SetActive(true);
		setUpArmy_Gui.meleePanel.transform.SetAsLastSibling();
		setUpArmy_Gui.Ranged_Toggle.GetComponent<CanvasGroup>().alpha = tempArmyTogglesAlpha;
		setUpArmy_Gui.rangedPanel.GetComponent<CanvasGroup>().alpha = 0f;
		setUpArmy_Gui.rangedPanel.SetActive(true);
		setUpArmy_Gui.Cavalry_Toggle.GetComponent<CanvasGroup>().alpha = tempArmyTogglesAlpha;
		setUpArmy_Gui.cavalryPanel.GetComponent<CanvasGroup>().alpha = 0f;
		setUpArmy_Gui.cavalryPanel.SetActive(true);
		setUpArmy_Gui.Heavy_Toggle.GetComponent<CanvasGroup>().alpha = tempArmyTogglesAlpha;
		setUpArmy_Gui.heavyPanel.GetComponent<CanvasGroup>().alpha = 0f;
		setUpArmy_Gui.heavyPanel.SetActive(true);
		setUpArmy_Gui.Special_Toggle.GetComponent<CanvasGroup>().alpha = tempArmyTogglesAlpha;
		setUpArmy_Gui.specialPanel.GetComponent<CanvasGroup>().alpha = 0f;
		setUpArmy_Gui.specialPanel.SetActive(true);
		setUpArmy_Gui.Epic_Toggle.GetComponent<CanvasGroup>().alpha = tempArmyTogglesAlpha;
		setUpArmy_Gui.epicPanel.GetComponent<CanvasGroup>().alpha = 0f;
		setUpArmy_Gui.epicPanel.SetActive(true);
		setUpArmy_Gui.placeRemoveArmy_Btn.interactable = false;
	}

	private void ResetArmiesUIs()
	{
		setUpArmy_Gui.Melee_Toggle.GetComponent<CanvasGroup>().alpha = tempArmyTogglesAlpha;
		setUpArmy_Gui.meleePanel.GetComponent<CanvasGroup>().alpha = 0f;
		setUpArmy_Gui.Ranged_Toggle.GetComponent<CanvasGroup>().alpha = tempArmyTogglesAlpha;
		setUpArmy_Gui.rangedPanel.GetComponent<CanvasGroup>().alpha = 0f;
		setUpArmy_Gui.Cavalry_Toggle.GetComponent<CanvasGroup>().alpha = tempArmyTogglesAlpha;
		setUpArmy_Gui.cavalryPanel.GetComponent<CanvasGroup>().alpha = 0f;
		setUpArmy_Gui.Heavy_Toggle.GetComponent<CanvasGroup>().alpha = tempArmyTogglesAlpha;
		setUpArmy_Gui.heavyPanel.GetComponent<CanvasGroup>().alpha = 0f;
		setUpArmy_Gui.Special_Toggle.GetComponent<CanvasGroup>().alpha = tempArmyTogglesAlpha;
		setUpArmy_Gui.specialPanel.GetComponent<CanvasGroup>().alpha = 0f;
		setUpArmy_Gui.Epic_Toggle.GetComponent<CanvasGroup>().alpha = tempArmyTogglesAlpha;
		setUpArmy_Gui.epicPanel.GetComponent<CanvasGroup>().alpha = 0f;
	}

	private void MeleeArmyUIs(bool status)
	{
		if (status)
		{
			ResetArmiesUIs();
			setUpArmy_Gui.Melee_Toggle.GetComponent<CanvasGroup>().alpha = 1f;
			setUpArmy_Gui.meleePanel.GetComponent<CanvasGroup>().alpha = 1f;
			setUpArmy_Gui.meleePanel.transform.SetAsLastSibling();
		}
	}

	private void RangedArmyUIs(bool status)
	{
		if (status)
		{
			ResetArmiesUIs();
			setUpArmy_Gui.Ranged_Toggle.GetComponent<CanvasGroup>().alpha = 1f;
			setUpArmy_Gui.rangedPanel.GetComponent<CanvasGroup>().alpha = 1f;
			setUpArmy_Gui.rangedPanel.transform.SetAsLastSibling();
		}
	}

	private void CavalryArmyUIs(bool status)
	{
		if (status)
		{
			ResetArmiesUIs();
			setUpArmy_Gui.Cavalry_Toggle.GetComponent<CanvasGroup>().alpha = 1f;
			setUpArmy_Gui.cavalryPanel.GetComponent<CanvasGroup>().alpha = 1f;
			setUpArmy_Gui.cavalryPanel.transform.SetAsLastSibling();
		}
	}

	private void HeavyArmyUIs(bool status)
	{
		if (status)
		{
			ResetArmiesUIs();
			setUpArmy_Gui.Heavy_Toggle.GetComponent<CanvasGroup>().alpha = 1f;
			setUpArmy_Gui.heavyPanel.GetComponent<CanvasGroup>().alpha = 1f;
			setUpArmy_Gui.heavyPanel.transform.SetAsLastSibling();
		}
	}

	private void SpecialArmyUIs(bool status)
	{
		if (status)
		{
			ResetArmiesUIs();
			setUpArmy_Gui.Special_Toggle.GetComponent<CanvasGroup>().alpha = 1f;
			setUpArmy_Gui.specialPanel.GetComponent<CanvasGroup>().alpha = 1f;
			setUpArmy_Gui.specialPanel.transform.SetAsLastSibling();
		}
	}

	private void EpicArmyUIs(bool status)
	{
		if (status)
		{
			ResetArmiesUIs();
			setUpArmy_Gui.Epic_Toggle.GetComponent<CanvasGroup>().alpha = 1f;
			setUpArmy_Gui.epicPanel.GetComponent<CanvasGroup>().alpha = 1f;
			setUpArmy_Gui.epicPanel.transform.SetAsLastSibling();
		}
	}

	private void AutoAction(bool status)
	{
		if (status)
		{
			setUpArmy_Gui.placeRemoveArmy_Btn.interactable = false;
		}
		else
		{
			setUpArmy_Gui.placeRemoveArmy_Btn.interactable = true;
		}
	}

	private void PlaceRemoveArmy()
	{
		switch (level_Manager.ReturnPlaceAction())
		{
		case PlaceAction.Place:
			setUpArmy_Gui.placeRemoveArmy_Btn.image.sprite = setUpArmy_Gui.removeArmy;
			level_Manager.SetPlaceAction(PlaceAction.Remove);
			break;
		case PlaceAction.Remove:
			setUpArmy_Gui.placeRemoveArmy_Btn.image.sprite = setUpArmy_Gui.placeArmy;
			level_Manager.SetPlaceAction(PlaceAction.Place);
			break;
		}
	}

	private void ResetPlaceRemoveBtn()
	{
		setUpArmy_Gui.placeRemoveArmy_Btn.image.sprite = setUpArmy_Gui.placeArmy;
		level_Manager.SetPlaceAction(PlaceAction.Place);
	}

	public void SetPlaceRemoveImg(PlaceAction tempAction)
	{
		switch (tempAction)
		{
		case PlaceAction.Place:
			setUpArmy_Gui.placeRemoveArmy_Btn.image.sprite = setUpArmy_Gui.placeArmy;
			break;
		case PlaceAction.Remove:
			setUpArmy_Gui.placeRemoveArmy_Btn.image.sprite = setUpArmy_Gui.removeArmy;
			break;
		}
	}

	private void EpicLevelInfoReward()
	{
		OpenInAppInfoPanel("Epic Level " + (level_Manager.epicLevelIndex + 1), "Welcome to the Epic Battle Arena and congratulations for making it here, only a few can reach this honored place. Prepare yourself to battle against some of the most advanced and sophisticated armies and claim lavish rewards. Best of Luck!", true, false);
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
		setUpArmy_Gui.preventTapPanel.SetActive(true);
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
			setUpArmy_Gui.preventTapPanel.SetActive(false);
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

	private void OpenSurrenderPanel()
	{
		game_Gui.surrenderPanel.SetActive(true);
	}

	private void CloseSurrenderPanel()
	{
		game_Gui.surrenderPanel.SetActive(false);
	}

	private void Pause()
	{
		ResetJoysticks();
		SetWhichGuiIsOpen(WhichGuiIsOpen.Pause_Gui);
		pause_Gui.SetCanvas(true);
		game_Gui.SetCanvas(false);
		gc.Pause(true);
		Time.timeScale = 0f;
	}

	private void Resume()
	{
		SetWhichGuiIsOpen(WhichGuiIsOpen.Game_Gui);
		pause_Gui.SetCanvas(false);
		game_Gui.SetCanvas(true);
		gc.Pause(false);
		Time.timeScale = 1f;
	}

	private void RestartPause()
	{
		SetWhichGuiIsOpen(WhichGuiIsOpen.Game_Gui);
		pause_Gui.SetCanvas(false);
		game_Gui.SetCanvas(true);
		Time.timeScale = 1f;
	}

	private void MainMenu()
	{
		audio_Manager.SwitchBackgroundMusic(BackgroundMusic.MENU);
		SetWhichGuiIsOpen(WhichGuiIsOpen.CanQuit);
		gameOver_Gui.SetCanvas(false);
		pause_Gui.SetCanvas(false);
		game_Gui.SetCanvas(false);
		main_Gui.SetCanvas(true);
		gc.ResetEverything();
		level_Manager.SetMenuGOs(true);
		level_Manager.SetLevelDesign(LevelDesign.None);
		EpicLevelsBtnsLogic();
		ChangeCamera(CustomCameraType.Nothing);
		Time.timeScale = 1f;
	}

	private void Surrender()
	{
		SetWhichGuiIsOpen(WhichGuiIsOpen.Game_Gui);
		pause_Gui.SetCanvas(false);
		game_Gui.SetCanvas(true);
		Time.timeScale = 1f;
		BattleResult result = BattleResult.Defeat;
		if (level_Manager.gameMode == GameMode.Custom)
		{
			result = BattleResult.Draw;
		}
		gc.RushGameOver(result);
	}

	private void OpenShop()
	{
		SetConsumablesTexts();
		CacheAds();
		IsRewardedVideo_Available();
		shop_Manager.CheckIfBossIsLocked();
		StopCoroutine("OpenShop_CR");
		StartCoroutine("OpenShop_CR");
	}

	private void CloseShop()
	{
		StopCoroutine("CloseShop_CR");
		StartCoroutine("CloseShop_CR");
	}

	private IEnumerator OpenShop_CR()
	{
		animation_Gui.SetCanvas(true);
		animation_Gui.thisCanvasGroup.alpha = 0f;
		float t2 = 0f;
		float lerpDuration = 0.3f;
		while (t2 < 1f)
		{
			t2 += Time.deltaTime / lerpDuration;
			animation_Gui.thisCanvasGroup.alpha = Mathf.Lerp(0f, 1f, t2);
			yield return null;
		}
		main_Gui.SetCanvas(false);
		shop_Gui.SetCanvas(true);
		level_Manager.SetMenuGOs(false);
		level_Manager.SetShopGOs(true);
		t2 = 0f;
		while (t2 < 1f)
		{
			t2 += Time.deltaTime / lerpDuration;
			animation_Gui.thisCanvasGroup.alpha = Mathf.Lerp(1f, 0f, t2);
			yield return null;
		}
		animation_Gui.SetCanvas(false);
		SetWhichGuiIsOpen(WhichGuiIsOpen.Shop_Gui);
	}

	private IEnumerator CloseShop_CR()
	{
		animation_Gui.SetCanvas(true);
		animation_Gui.thisCanvasGroup.alpha = 0f;
		float t2 = 0f;
		float lerpDuration = 0.3f;
		while (t2 < 1f)
		{
			t2 += Time.deltaTime / lerpDuration;
			animation_Gui.thisCanvasGroup.alpha = Mathf.Lerp(0f, 1f, t2);
			yield return null;
		}
		main_Gui.SetCanvas(true);
		shop_Gui.SetCanvas(false);
		level_Manager.SetMenuGOs(true);
		level_Manager.SetShopGOs(false);
		t2 = 0f;
		while (t2 < 1f)
		{
			t2 += Time.deltaTime / lerpDuration;
			animation_Gui.thisCanvasGroup.alpha = Mathf.Lerp(1f, 0f, t2);
			yield return null;
		}
		animation_Gui.SetCanvas(false);
		SetWhichGuiIsOpen(WhichGuiIsOpen.CanQuit);
	}

	public void OpenInfoBossPanel()
	{
		shop_Gui.infoBossPanel.SetActive(true);
		SetWhichGuiIsOpen(WhichGuiIsOpen.InfoBoss_Gui);
	}

	public void CloseInfoBossPanel()
	{
		shop_Gui.infoBossPanel.SetActive(false);
		SetWhichGuiIsOpen(WhichGuiIsOpen.Shop_Gui);
	}

	private void InitSocialMediaButtons()
	{
	}

	public void FailBuyShop(Image tempImage)
	{
		StartCoroutine(FailBuyShop_CR(tempImage));
	}

	private IEnumerator FailBuyShop_CR(Image tempImage)
	{
		Color startColor = Color.white;
		Color targetColor = Color.red;
		float t2 = 0f;
		float lerpDuration = 0.15f;
		while (t2 < 1f)
		{
			t2 += Time.deltaTime / lerpDuration;
			tempImage.color = Color.Lerp(startColor, targetColor, t2);
			yield return null;
		}
		t2 = 0f;
		while (t2 < 1f)
		{
			t2 += Time.deltaTime / lerpDuration;
			tempImage.color = Color.Lerp(targetColor, startColor, t2);
			yield return null;
		}
	}

	private void OpenUpgradeStatsValues()
	{
		SetConsumablesTexts();
		StopCoroutine("OpenUpgradeStatsValues_CR");
		StartCoroutine("OpenUpgradeStatsValues_CR");
	}

	private void CloseUpgradeStatsValues()
	{
		StopCoroutine("CloseUpgradeStatsValues_CR");
		StartCoroutine("CloseUpgradeStatsValues_CR");
	}

	private IEnumerator OpenUpgradeStatsValues_CR()
	{
		SetWhichGuiIsOpen(WhichGuiIsOpen.Between);
		animation_Gui.SetCanvas(true);
		animation_Gui.thisCanvasGroup.alpha = 0f;
		upgradeStatsValues_Gui.SetCanvas(true);
		upgradeStatsValues_Gui.thisCanvasGroup.alpha = 0f;
		float t2 = 0f;
		float lerpDuration = 0.3f;
		t2 = 0f;
		while (t2 < 1f)
		{
			t2 += Time.deltaTime / lerpDuration;
			upgradeStatsValues_Gui.thisCanvasGroup.alpha = Mathf.Lerp(0f, 1f, t2);
			yield return null;
		}
		main_Gui.SetCanvas(false);
		animation_Gui.SetCanvas(false);
		level_Manager.SetMenuTroops(false);
		SetWhichGuiIsOpen(WhichGuiIsOpen.UpgradeStatsValues_Gui);
	}

	private IEnumerator CloseUpgradeStatsValues_CR()
	{
		SetWhichGuiIsOpen(WhichGuiIsOpen.Between);
		level_Manager.SetMenuTroops(true);
		yield return null;
		main_Gui.SetCanvas(true);
		main_Gui.thisCanvasGroup.alpha = 1f;
		upgradeStatsValues_Gui.SetCanvas(false);
		SetWhichGuiIsOpen(WhichGuiIsOpen.CanQuit);
	}

	public void CheckUpgradeDone(bool upgradeDone, Image tempImage)
	{
		if (upgradeDone)
		{
			SetConsumablesTexts();
			audio_Manager.UpgradeSucceed();
		}
		else
		{
			StartCoroutine(FailBuy_CR(tempImage));
			audio_Manager.UpgradeFailed();
		}
	}

	private IEnumerator FailBuy_CR(Image tempImage)
	{
		Color startColor = upgradeStatsValues_Gui.failBuyStart;
		Color targetColor = upgradeStatsValues_Gui.failBuyTarget;
		float t2 = 0f;
		float lerpDuration = 0.15f;
		while (t2 < 1f)
		{
			t2 += Time.deltaTime / lerpDuration;
			tempImage.color = Color.Lerp(startColor, targetColor, t2);
			yield return null;
		}
		t2 = 0f;
		while (t2 < 1f)
		{
			t2 += Time.deltaTime / lerpDuration;
			tempImage.color = Color.Lerp(targetColor, startColor, t2);
			yield return null;
		}
	}

	public void ReadyPressed()
	{
		if (multiplayerConnect_Gui.nickNameInput.text.Length < 3)
		{
			OpenMessage("Nickname must be more than 3 characters");
			return;
		}
		if (multiplayerConnect_Gui.userNameInput.text.Length < 4)
		{
			OpenMessage("Username must be more than 4 characters");
			return;
		}
		if (multiplayerConnect_Gui.passwordInput.text.Length < 4)
		{
			OpenMessage("Password must be more than 4 characters");
			return;
		}
		connectionScreen.Connect();
		OpenConnectScreen();
		SetWhichGuiIsOpen(WhichGuiIsOpen.Between);
	}

	public void CancelPressed()
	{
		connectionScreen.Abort();
		CloseConnectScreen();
		SetWhichGuiIsOpen(WhichGuiIsOpen.MultiplayerConnect_Gui);
	}

	public void OpenMultiplayerSetupArmy(string playerName, string enemyName, bool tempIsPlayer, int environment)
	{
		MultiplayerSetTime(80);
		CloseConnectScreen();
		this.playerName = playerName;
		this.enemyName = enemyName;
		level_Manager.isPlayer = tempIsPlayer;
		level_Manager.ChangeLevelDesignMultiplayer((LevelDesign)environment);
		level_Manager.SetLevelDesignMultiplayer();
		setUpArmy_Gui.multiPlayerName_Txt.text = ((!level_Manager.isPlayer) ? enemyName : playerName);
		setUpArmy_Gui.multiEnemyName_Txt.text = ((!level_Manager.isPlayer) ? playerName : enemyName);
		OpenSetUpArmy();
	}

	public void Disconnected(int i)
	{
		connectionScreen.Disconnect(20 + i);
		gc.ResetEverything();
		CloseSetUpArmy();
		CloseConnectScreen();
		game_Gui.SetCanvas(false);
		multiplayerConnect_Gui.SetCanvas(false);
		main_Gui.SetCanvas(true);
		SetWhichGuiIsOpen(WhichGuiIsOpen.CanQuit);
	}

	private void OpenMultiplayerConnectInstantly()
	{
		ResetShowPassword();
		if (PlayerPrefs_Account.LoadAccountCreated())
		{
			multiplayerConnect_Gui.rankingPanel.SetActive(true);
			multiplayerConnect_Gui.rankingText.text = PlayerPrefs_Account.LoadRanking() + string.Empty;
		}
		SetConsumablesTexts();
		SetDailyReward_Btn();
		main_Gui.SetCanvas(false);
		multiplayerConnect_Gui.SetCanvas(true);
		SetWhichGuiIsOpen(WhichGuiIsOpen.MultiplayerConnect_Gui);
	}

	private void OpenMultiplayerConnect()
	{
		ResetShowPassword();
		if (PlayerPrefs_Account.LoadAccountCreated())
		{
			multiplayerConnect_Gui.rankingPanel.SetActive(true);
			multiplayerConnect_Gui.rankingText.text = PlayerPrefs_Account.LoadRanking() + string.Empty;
		}
		SetConsumablesTexts();
		SetDailyReward_Btn();
		StopCoroutine("OpenMultiplayerConnect_CR");
		StartCoroutine("OpenMultiplayerConnect_CR");
	}

	private IEnumerator OpenMultiplayerConnect_CR()
	{
		animation_Gui.SetCanvas(true);
		animation_Gui.thisCanvasGroup.alpha = 0f;
		float t2 = 0f;
		float lerpDuration = 0.25f;
		while (t2 < 1f)
		{
			t2 += Time.deltaTime / lerpDuration;
			animation_Gui.thisCanvasGroup.alpha = Mathf.Lerp(0f, 1f, t2);
			yield return null;
		}
		main_Gui.SetCanvas(false);
		multiplayerConnect_Gui.SetCanvas(true);
		t2 = 0f;
		while (t2 < 1f)
		{
			t2 += Time.deltaTime / lerpDuration;
			animation_Gui.thisCanvasGroup.alpha = Mathf.Lerp(1f, 0f, t2);
			yield return null;
		}
		animation_Gui.SetCanvas(false);
		SetWhichGuiIsOpen(WhichGuiIsOpen.MultiplayerConnect_Gui);
	}

	private void CloseMultiplayerConnect()
	{
		StopCoroutine("CloseMultiplayerConnect_CR");
		StartCoroutine("CloseMultiplayerConnect_CR");
	}

	private IEnumerator CloseMultiplayerConnect_CR()
	{
		animation_Gui.SetCanvas(true);
		animation_Gui.thisCanvasGroup.alpha = 0f;
		float t2 = 0f;
		float lerpDuration = 0.25f;
		while (t2 < 1f)
		{
			t2 += Time.deltaTime / lerpDuration;
			animation_Gui.thisCanvasGroup.alpha = Mathf.Lerp(0f, 1f, t2);
			yield return null;
		}
		main_Gui.SetCanvas(true);
		multiplayerConnect_Gui.SetCanvas(false);
		t2 = 0f;
		while (t2 < 1f)
		{
			t2 += Time.deltaTime / lerpDuration;
			animation_Gui.thisCanvasGroup.alpha = Mathf.Lerp(1f, 0f, t2);
			yield return null;
		}
		animation_Gui.SetCanvas(false);
		SetWhichGuiIsOpen(WhichGuiIsOpen.CanQuit);
	}

	private void InitLogin()
	{
		if (PlayerPrefs_Account.LoadAccountCreated())
		{
			float alpha = 0.6f;
			multiplayerConnect_Gui.userNameCanvasGroup.interactable = false;
			multiplayerConnect_Gui.passwordCanvasGroup.interactable = false;
			multiplayerConnect_Gui.passwordCanvasGroup.alpha = alpha;
			multiplayerConnect_Gui.userNameCanvasGroup.alpha = alpha;
			multiplayerConnect_Gui.nickNameInput.text = PlayerPrefs_Account.LoadNickName();
			multiplayerConnect_Gui.userNameInput.text = PlayerPrefs_Account.LoadUsername();
			multiplayerConnect_Gui.passwordInput.text = PlayerPrefs_Account.LoadPassword();
		}
	}

	public void AccountCreated()
	{
		float alpha = 0.6f;
		multiplayerConnect_Gui.userNameCanvasGroup.interactable = false;
		multiplayerConnect_Gui.passwordCanvasGroup.interactable = false;
		multiplayerConnect_Gui.passwordCanvasGroup.alpha = alpha;
		multiplayerConnect_Gui.userNameCanvasGroup.alpha = alpha;
		string text = multiplayerConnect_Gui.nickNameInput.text;
		string text2 = multiplayerConnect_Gui.userNameInput.text;
		string text3 = multiplayerConnect_Gui.passwordInput.text;
		PlayerPrefs_Account.SaveAccountCreated(true);
		PlayerPrefs_Account.SaveNickName(text);
		PlayerPrefs_Account.SaveUserName(text2);
		PlayerPrefs_Account.SavePassword(text3);
		PlayerPrefs.Save();
		multiplayerConnect_Gui.rankingPanel.SetActive(true);
		multiplayerConnect_Gui.rankingText.text = PlayerPrefs_Account.LoadRanking() + string.Empty;
	}

	private void ResetShowPassword()
	{
		multiplayerConnect_Gui.passwordInput.inputType = InputField.InputType.Password;
		multiplayerConnect_Gui.showPassword_Image.sprite = multiplayerConnect_Gui.showPassOff;
	}

	private void ShowPassword()
	{
		if (multiplayerConnect_Gui.passwordInput.inputType == InputField.InputType.Password)
		{
			multiplayerConnect_Gui.passwordInput.inputType = InputField.InputType.Standard;
			multiplayerConnect_Gui.showPassword_Image.sprite = multiplayerConnect_Gui.showPassOn;
		}
		else
		{
			multiplayerConnect_Gui.passwordInput.inputType = InputField.InputType.Password;
			multiplayerConnect_Gui.showPassword_Image.sprite = multiplayerConnect_Gui.showPassOff;
		}
		multiplayerConnect_Gui.passwordInput.ForceLabelUpdate();
	}

	private void SetDateTimeVariables()
	{
		referenceTime = DateTime.Parse(PlayerPrefs_Saves.LoadDailyReferenceTime());
		currentDate = DateTime.Now;
		difference = currentDate.Subtract(referenceTime);
	}

	private void SetDailyReward_Btn()
	{
		int num = PlayerPrefs_Saves.LoadDailyWinsMultiplayer();
		if (multiplayerConnect_Gui.infoPanel.activeSelf && num >= 5)
		{
			multiplayerConnect_Gui.dailyReward_Btn.interactable = true;
		}
		else
		{
			multiplayerConnect_Gui.dailyReward_Btn.interactable = false;
		}
	}

	private void GetDailyReward()
	{
		PlayerPrefs_Saves.SaveConsumables(500);
		PlayerPrefs_Saves.SaveDailyReferenceTime(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0));
		PlayerPrefs_Saves.SaveDailyWinsMultiplayer(0);
		PlayerPrefs.Save();
		SetRewardPanels(true);
		multiplayerConnect_Gui.dailyReward_Btn.interactable = false;
		SetConsumablesTexts();
		SetDateTimeVariables();
		SetStarImages();
		SetCounterTimeText();
	}

	private void SetDailyRewardPanels()
	{
		StopCoroutine("SetDailyRewardPanels_CR");
		StartCoroutine("SetDailyRewardPanels_CR");
	}

	private IEnumerator SetDailyRewardPanels_CR()
	{
		WaitForSeconds waitTime = new WaitForSeconds(1f);
		while (true)
		{
			currentDate = DateTime.Now;
			difference = currentDate.Subtract(referenceTime);
			if (difference.TotalHours >= 24.0)
			{
				if (multiplayerConnect_Gui.counterPanel.activeSelf)
				{
					SetRewardPanels(false);
				}
			}
			else
			{
				SetCounterTimeText();
			}
			yield return waitTime;
		}
	}

	private void SetRewardPanels(bool state)
	{
		multiplayerConnect_Gui.counterPanel.SetActive(state);
		multiplayerConnect_Gui.infoPanel.SetActive(!state);
		multiplayerConnect_Gui.exclamationImg.SetActive(!state);
	}

	private void SetCounterTimeText()
	{
		TimeSpan timeSpan = referenceTime.AddDays(1.0).Subtract(currentDate);
		if (timeSpan.Ticks >= 0)
		{
			multiplayerConnect_Gui.dailyRewardWait_Txt.text = "Next In:\n" + new DateTime(timeSpan.Ticks).ToString("HH:mm:ss");
		}
	}

	private void CheckAddDailyWinMultiplayer()
	{
		if (multiplayerConnect_Gui.infoPanel.activeSelf)
		{
			int value = PlayerPrefs_Saves.LoadDailyWinsMultiplayer() + 1;
			PlayerPrefs_Saves.SaveDailyWinsMultiplayer(value);
			PlayerPrefs.Save();
			SetStarImages();
		}
	}

	private void SetStarImages()
	{
		int num = PlayerPrefs_Saves.LoadDailyWinsMultiplayer();
		int num2 = multiplayerConnect_Gui.StarImages.Length;
		for (int i = 0; i < num2; i++)
		{
			multiplayerConnect_Gui.StarImages[i].sprite = multiplayerConnect_Gui.star_Off;
		}
		for (int j = 0; j < num && j != num2; j++)
		{
			multiplayerConnect_Gui.StarImages[j].sprite = multiplayerConnect_Gui.star_On;
		}
	}

	private void LevelStats_Initialize()
	{
		int num = level_Manager.LevelsValues.Length;
		Vector2 size = levelStats_Gui.scrollView.rect.size;
		int num2 = 2;
		float num3 = 2f * Mathf.Abs(levelStats_Gui.imgY);
		int num4 = num;
		num4 /= num2;
		if (num % num2 == 0)
		{
			num4--;
		}
		for (int i = 0; i < num4; i++)
		{
			num3 += levelStats_Gui.imgDiffY;
		}
		if (num3 <= size.y)
		{
			num3 = size.y;
		}
		levelStats_Gui.scrollView.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
		levelStats_Gui.scrollView.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, num3);
		int num5 = 0;
		float x = 0f;
		float num6 = levelStats_Gui.imgY;
		for (int j = 0; j < num; j++)
		{
			if (j % num2 == 0)
			{
				x = levelStats_Gui.img1PosX;
			}
			else if (j % num2 == 1)
			{
				x = levelStats_Gui.img2PosX;
			}
			if (num5 == num2)
			{
				num6 -= levelStats_Gui.imgDiffY;
				num5 = 0;
			}
			GameObject gameObject = UnityEngine.Object.Instantiate(levelStats_Gui.img1_Row1.gameObject);
			gameObject.transform.SetParent(levelStats_Gui.scrollView.transform, true);
			gameObject.name = "Image " + j;
			gameObject.SetActive(true);
			Text component = gameObject.transform.GetChild(0).GetComponent<Text>();
			Image component2 = gameObject.transform.GetChild(1).GetComponent<Image>();
			component.text = "Level " + (j + 1);
			ResultImages.Add(component2);
			RectTransform component3 = gameObject.GetComponent<RectTransform>();
			component3.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, levelStats_Gui.img_Size.x);
			component3.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, levelStats_Gui.img_Size.y);
			component3.localPosition = new Vector3(x, num6, 0f);
			num5++;
		}
	}

	private void SetLevelsStats()
	{
		int num = level_Manager.LevelsValues.Length;
		int num2 = PlayerPrefs_Saves.LoadLevelsCompleted();
		for (int i = 0; i < num; i++)
		{
			if (i < num2)
			{
				ResultImages[i].sprite = levelStats_Gui.completed;
			}
			else
			{
				ResultImages[i].sprite = levelStats_Gui.incompleted;
			}
		}
	}

	private void SetEpicLevelsStats()
	{
		int num = level_Manager.EpicLevelsValues.Length;
		int num2 = PlayerPrefs_Saves.LoadEpicLevelsCompleted();
		for (int i = 0; i < num; i++)
		{
			if (i < num2)
			{
				ResultImages[i].sprite = levelStats_Gui.completed;
			}
			else
			{
				ResultImages[i].sprite = levelStats_Gui.incompleted;
			}
		}
	}

	public void SaveGameOver(BattleResult tempResult)
	{
		if (level_Manager.gameMode != GameMode.Custom && level_Manager.gameMode != GameMode.Multiplayer && tempResult == BattleResult.Win)
		{
			if (level_Manager.gameMode == GameMode.Levels)
			{
				int levelsIndex = level_Manager.levelIndex + 1;
				PlayerPrefs_Saves.SaveLevelsCompleted(levelsIndex);
			}
			else if (level_Manager.gameMode != GameMode.EpicLevels)
			{
			}
		}
	}

	public void SetGameOver(BattleResult tempResult)
	{
		if (level_Manager.gameMode == GameMode.Levels || level_Manager.gameMode == GameMode.EpicLevels)
		{
			if (tempResult == BattleResult.Win)
			{
				level_Manager.ResetArmyUsedValues();
			}
			if ((level_Manager.GetBonusGold() || level_Manager.GetBonusArmy()) && PlayerPrefs_Saves.LoadCanShowAds())
			{
				level_Manager.ResetArmyUsedValues();
			}
		}
		level_Manager.SetBonusGold(false);
		level_Manager.SetBonusArmy(false);
		ResetAdButtons();
		CacheAds();
		IsRewardedVideo_Available();
		level_Manager.battleResult = tempResult;
		int levelIndex = level_Manager.levelIndex;
		int epicLevelIndex = level_Manager.epicLevelIndex;
		if (level_Manager.gameMode == GameMode.Levels)
		{
			gameOver_Gui.trophiesParentPanel.SetActive(false);
			switch (tempResult)
			{
			case BattleResult.Win:
				gameOver_Gui.share_Btn.gameObject.SetActive(true);
				gameOver_Gui.starsPanel.SetActive(true);
				gameOver_Gui.result_Txt.text = "Level " + (levelIndex + 1) + " Passed!";
				gameOver_Gui.result_Txt.color = gameOver_Gui.normal;
				gameOver_Gui.restart_Btn.image.sprite = gameOver_Gui.nextLvL_image;
				CustomEventsManager.Send_LevelWon("Level " + levelIndex);
				if (packagesManager.whichStore == App_Stores.Google || packagesManager.whichStore == App_Stores.IOS)
				{
					featuresManager.alManager.Synchronize();
				}
				break;
			case BattleResult.Draw:
			case BattleResult.Defeat:
				gameOver_Gui.share_Btn.gameObject.SetActive(false);
				gameOver_Gui.starsPanel.SetActive(false);
				gameOver_Gui.result_Txt.text = "Level " + (levelIndex + 1) + " Failed!";
				gameOver_Gui.result_Txt.color = gameOver_Gui.redTeamWin;
				gameOver_Gui.restart_Btn.image.sprite = gameOver_Gui.playAgain_image;
				CustomEventsManager.Send_LevelLost("Level " + levelIndex);
				break;
			}
		}
		else if (level_Manager.gameMode == GameMode.EpicLevels)
		{
			gameOver_Gui.trophiesParentPanel.SetActive(false);
			switch (tempResult)
			{
			case BattleResult.Win:
				gameOver_Gui.share_Btn.gameObject.SetActive(true);
				gameOver_Gui.starsPanel.SetActive(true);
				gameOver_Gui.result_Txt.text = "Epic Level " + (epicLevelIndex + 1) + " Passed!";
				gameOver_Gui.result_Txt.color = gameOver_Gui.normal;
				gameOver_Gui.restart_Btn.image.sprite = gameOver_Gui.nextLvL_image;
				CustomEventsManager.Send_LevelWon("Epic Level " + epicLevelIndex);
				break;
			case BattleResult.Draw:
			case BattleResult.Defeat:
				gameOver_Gui.share_Btn.gameObject.SetActive(false);
				gameOver_Gui.starsPanel.SetActive(false);
				gameOver_Gui.result_Txt.text = "Epic Level " + (epicLevelIndex + 1) + " Failed!";
				gameOver_Gui.result_Txt.color = gameOver_Gui.redTeamWin;
				gameOver_Gui.restart_Btn.image.sprite = gameOver_Gui.playAgain_image;
				CustomEventsManager.Send_LevelLost("Epic Level " + levelIndex);
				break;
			}
		}
		else if (level_Manager.gameMode == GameMode.Custom)
		{
			gameOver_Gui.trophiesParentPanel.SetActive(false);
			gameOver_Gui.share_Btn.gameObject.SetActive(false);
			gameOver_Gui.starsPanel.SetActive(true);
			gameOver_Gui.restart_Btn.image.sprite = gameOver_Gui.playAgain_image;
			switch (tempResult)
			{
			case BattleResult.Win:
				gameOver_Gui.result_Txt.text = "Blue Team Won The Battle!";
				gameOver_Gui.result_Txt.color = gameOver_Gui.blueTeamWin;
				break;
			case BattleResult.Draw:
				gameOver_Gui.result_Txt.text = "The Battle Was A Draw!";
				gameOver_Gui.result_Txt.color = gameOver_Gui.normal;
				break;
			case BattleResult.Defeat:
				gameOver_Gui.result_Txt.text = "Red Team Won The Battle!";
				gameOver_Gui.result_Txt.color = gameOver_Gui.redTeamWin;
				break;
			}
		}
		else
		{
			if (level_Manager.gameMode != GameMode.Multiplayer)
			{
				return;
			}
			gameOver_Gui.trophiesParentPanel.SetActive(true);
			gameOver_Gui.trophiesLoadingPanel.SetActive(true);
			gameOver_Gui.trophiesPanel.SetActive(false);
			gameOver_Gui.share_Btn.gameObject.SetActive(false);
			gameOver_Gui.starsPanel.SetActive(true);
			gameOver_Gui.restart_Btn.image.sprite = gameOver_Gui.playAgain_image;
			switch (tempResult)
			{
			case BattleResult.Win:
				gameOver_Gui.result_Txt.text = playerName + " Wins The Battle!";
				if (level_Manager.isPlayer)
				{
					gameOver_Gui.result_Txt.color = gameOver_Gui.blueTeamWin;
				}
				else
				{
					gameOver_Gui.result_Txt.color = gameOver_Gui.redTeamWin;
				}
				CheckAddDailyWinMultiplayer();
				multiplayerController.Send_Won();
				break;
			case BattleResult.Draw:
				gameOver_Gui.result_Txt.text = "The Battle Was A Draw!";
				gameOver_Gui.result_Txt.color = gameOver_Gui.normal;
				multiplayerController.Send_Draw();
				break;
			case BattleResult.Defeat:
				gameOver_Gui.result_Txt.text = enemyName + " Wins The Battle!";
				if (level_Manager.isPlayer)
				{
					gameOver_Gui.result_Txt.color = gameOver_Gui.redTeamWin;
				}
				else
				{
					gameOver_Gui.result_Txt.color = gameOver_Gui.blueTeamWin;
				}
				multiplayerController.Send_Lost();
				break;
			}
		}
	}

	public void SetConsumablesReward(BattleResult tempResult)
	{
		int levelIndex = level_Manager.levelIndex;
		int epicLevelIndex = level_Manager.epicLevelIndex;
		int num = 20 + levelIndex * 2;
		int num2 = 10;
		int num3 = 10;
		int num4 = 10;
		int num5 = 150;
		int num6 = 25;
		int num7 = 0;
		int num8 = 0;
		if (level_Manager.gameMode == GameMode.Levels)
		{
			switch (tempResult)
			{
			case BattleResult.Win:
				num7 = num;
				num8 = 100;
				break;
			case BattleResult.Draw:
			case BattleResult.Defeat:
				num7 = num2;
				num8 = 50;
				break;
			}
			if (level_Manager.LevelsValues[levelIndex].isBossLevel && tempResult == BattleResult.Win)
			{
				num7 = level_Manager.LevelsValues[levelIndex].bossReward;
				num8 = 50;
			}
		}
		else if (level_Manager.gameMode == GameMode.EpicLevels)
		{
			switch (tempResult)
			{
			case BattleResult.Win:
				num7 = num;
				num8 = 100;
				break;
			case BattleResult.Draw:
			case BattleResult.Defeat:
				num7 = num2;
				num8 = 50;
				break;
			}
			if (level_Manager.EpicLevelsValues[epicLevelIndex].isBossLevel && tempResult == BattleResult.Win)
			{
				if (epicLevelIndex == level_Manager.CountCompletedEpicLevels())
				{
					int levelsIndex = level_Manager.epicLevelIndex + 1;
					PlayerPrefs_Saves.SaveEpicLevelsCompleted(levelsIndex);
					num7 = level_Manager.EpicLevelsValues[epicLevelIndex].epic_Reward_First_Time;
					num8 = 50;
				}
				else
				{
					num7 = level_Manager.EpicLevelsValues[epicLevelIndex].epic_Reward;
					num8 = 50;
				}
			}
		}
		else if (level_Manager.gameMode == GameMode.Custom)
		{
			switch (tempResult)
			{
			case BattleResult.Win:
				num7 = num3;
				num8 = 0;
				break;
			case BattleResult.Draw:
			case BattleResult.Defeat:
				num7 = num4;
				num8 = 0;
				break;
			}
		}
		else if (level_Manager.gameMode == GameMode.Multiplayer)
		{
			switch (tempResult)
			{
			case BattleResult.Win:
				num7 = num5;
				num8 = 100;
				break;
			case BattleResult.Draw:
			case BattleResult.Defeat:
				num7 = num6;
				num8 = 125;
				break;
			}
		}
		//if (InAppManager.HAS_SUBSCRIPTION)
		//{
		//	num7 *= 2;
		//	num8 *= 2;
		//}
		gameOver_Gui.SetRewardNumber(num7, num8);
		gameOver_Gui.consumablesReward_Txt.text = "+" + num7;
		PlayerPrefs_Saves.SaveConsumables(num7);
		PlayerPrefs.Save();
	}

	public void GameOverBegin()
	{
		SetWhichGuiIsOpen(WhichGuiIsOpen.Between);
		CloseSurrenderPanel();
		float duration = 0.35f;
		game_Gui.thisCanvasGroup.interactable = false;
		game_Gui.thisCanvasGroup.DOFade(0f, duration).SetEase(Ease.InCirc);
		game_Gui.cameraBest.canPlay = false;
		ResetJoysticks();
		gameOver_Gui.battleCompletedCanvasGroup.gameObject.SetActive(true);
		gameOver_Gui.battleCompletedCanvasGroup.alpha = 0f;
		gameOver_Gui.battleCompletedCanvasGroup.DOFade(1f, duration).SetEase(Ease.InOutSine).OnComplete(delegate
		{
			BattleCompletedFadeOut();
		});
	}

	private void BattleCompletedFadeOut()
	{
		float duration = 0.35f;
		float delay = 0.7f;
		gameOver_Gui.battleCompletedCanvasGroup.alpha = 1f;
		level_Manager.UnlockSetUI(false);
		gameOver_Gui.battleCompletedCanvasGroup.DOFade(0f, duration).SetDelay(delay).SetEase(Ease.InOutSine)
			.OnComplete(delegate
			{
				IntersitialAdLogic();
			});
	}

	public void IntersitialAdClosed()
	{
		GameOverEnd();
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary.Add("af_content_type", "Interstitial");
		AppsFlyer.trackRichEvent("ad_watched", dictionary);
	}

	private void GameOverEnd()
	{
		gameOver_Gui.battleCompletedCanvasGroup.alpha = 0f;
		gameOver_Gui.battleCompletedCanvasGroup.gameObject.SetActive(false);
		game_Gui.SetCanvas(false);
		float duration = 1.2f;
		gameOver_Gui.SetCanvas(true);
		gameOver_Gui.thisCanvasGroup.interactable = true;
		gameOver_Gui.thisCanvasGroup.alpha = 0f;
		gameOver_Gui.thisCanvasGroup.DOFade(1f, duration).SetEase(Ease.OutCirc);
		gameOver_Gui.thisRect.localScale = Vector3.one * 1.2f;
		gameOver_Gui.thisRect.DOScale(Vector3.one, duration).SetEase(Ease.OutCirc);
		gameOver_Gui.resultsRect.localScale = new Vector3(1f, 0f, 1f);
		gameOver_Gui.resultsRect.DOScale(Vector3.one, duration).SetEase(Ease.OutElastic);
	}

	public void OpenTrophies(int trophies)
	{
		gameOver_Gui.trophies_Txt.text = ((trophies <= 0) ? string.Empty : "+") + trophies;
		gameOver_Gui.trophiesLoadingPanel.SetActive(false);
		gameOver_Gui.trophiesPanel.SetActive(true);
		PlayerPrefs_Account.SaveRanking(PlayerPrefs_Account.LoadRanking() + trophies);
		PlayerPrefs.Save();
	}

	private void Restart()
	{
		gameOver_Gui.thisCanvasGroup.interactable = false;
		StopCoroutine("Restart_CR");
		StartCoroutine("Restart_CR");
	}

	private IEnumerator Restart_CR()
	{
		animation_Gui.SetCanvas(true);
		animation_Gui.thisCanvasGroup.alpha = 0f;
		float t2 = 0f;
		float lerpDuration = 0.25f;
		while (t2 < 1f)
		{
			t2 += Time.deltaTime / lerpDuration;
			animation_Gui.thisCanvasGroup.alpha = Mathf.Lerp(0f, 1f, t2);
			yield return null;
		}
		gameOver_Gui.SetCanvas(false);
		OpenSetUpArmyPanel();
		gc.ResetEverything();
		Restart_GameMode();
		t2 = 0f;
		while (t2 < 1f)
		{
			t2 += Time.deltaTime / lerpDuration;
			animation_Gui.thisCanvasGroup.alpha = Mathf.Lerp(1f, 0f, t2);
			yield return null;
		}
		animation_Gui.SetCanvas(false);
		SetWhichGuiIsOpen(WhichGuiIsOpen.SetUpArmy_Gui);
	}

	private void Restart_GameMode()
	{
		if (level_Manager.gameMode == GameMode.Levels)
		{
			level_Manager.SetPlayerQuad(true);
			level_Manager.SetEnemyQuad(false);
			if (level_Manager.battleResult == BattleResult.Win)
			{
				int levelIndex = level_Manager.levelIndex;
				int num = level_Manager.LevelsValues.Length - 1;
				if (levelIndex <= num)
				{
					level_Manager.SetTargetLevel();
					level_Manager.GetAvailableValues();
					level_Manager.SetLevelDesign(level_Manager.LevelsValues[level_Manager.levelIndex].design);
					SetLevelButtons();
					SetAvailableTexts();
				}
			}
			else
			{
				level_Manager.GetAvailableValues();
				level_Manager.SetLevelDesign(level_Manager.LevelsValues[level_Manager.levelIndex].design);
				level_Manager.SpawnArmyUsedValues();
				SetAvailableTexts();
			}
			ChangeCamera(CustomCameraType.Setup);
		}
		else if (level_Manager.gameMode == GameMode.EpicLevels)
		{
			level_Manager.SetPlayerQuad(true);
			level_Manager.SetEnemyQuad(false);
			if (level_Manager.battleResult == BattleResult.Win)
			{
				int epicLevelIndex = level_Manager.epicLevelIndex;
				int num2 = level_Manager.EpicLevelsValues.Length - 1;
				if (epicLevelIndex <= num2)
				{
					level_Manager.SetTargetEpicLevel();
					level_Manager.GetAvailableValuesEpic();
					level_Manager.SetLevelDesign(level_Manager.EpicLevelsValues[level_Manager.epicLevelIndex].design);
					SetLevelButtons();
					SetAvailableTexts();
				}
			}
			else
			{
				level_Manager.GetAvailableValuesEpic();
				level_Manager.SetLevelDesign(level_Manager.EpicLevelsValues[level_Manager.epicLevelIndex].design);
				level_Manager.SpawnArmyUsedValues();
				SetAvailableTexts();
			}
			ChangeCamera(CustomCameraType.Setup);
		}
		else if (level_Manager.gameMode == GameMode.Custom)
		{
			level_Manager.SetPlayerQuad(true);
			level_Manager.SetEnemyQuad(true);
			level_Manager.SpawnArmyUsedValues();
			ChangeCamera(CustomCameraType.Setup);
		}
		else if (level_Manager.gameMode == GameMode.Multiplayer)
		{
			level_Manager.ResetMultiplayer();
			level_Manager.SetPlayerQuad(level_Manager.isPlayer);
			level_Manager.SetEnemyQuad(!level_Manager.isPlayer);
			level_Manager.gameMode = GameMode.Multiplayer;
			MainMenu();
			CloseSetUpArmyPanel();
			OpenMultiplayerConnectInstantly();
			ReadyPressed();
		}
		SetAvailableTexts();
	}

	private void ConsumableBtnAnimation()
	{
		StartCoroutine(ConsumableBtnAnimation_CR());
	}

	private IEnumerator ConsumableBtnAnimation_CR()
	{
		while (true)
		{
			if (gameOver_Gui.thisCanvas.enabled)
			{
				float duration = 1f;
				gameOver_Gui.consumables_Btn.transform.DOShakePosition(duration, new Vector3(8f, 8f, 0f), 10, 90f, false, false).SetEase(Ease.OutCirc);
			}
			yield return waitTimeAnimation;
		}
	}

	private void OpenLeaderBoard()
	{
		StopCoroutine("OpenLeaderBoard_CR");
		StartCoroutine("OpenLeaderBoard_CR");
	}

	public void CloseLeaderBoard()
	{
		StopCoroutine("OpenLeaderBoard_CR");
		StopCoroutine("CloseLeaderBoard_CR");
		StartCoroutine("CloseLeaderBoard_CR");
	}

	private IEnumerator OpenLeaderBoard_CR()
	{
		animation_Gui.SetCanvas(true);
		animation_Gui.thisCanvasGroup.alpha = 0f;
		leaderBoard_Gui.loadingPanel.SetActive(true);
		leaderBoard_Gui.namesPanel.SetActive(false);
		leadeboardConnection.Connect();
		float t2 = 0f;
		float lerpDuration = 0.25f;
		while (t2 < 1f)
		{
			t2 += Time.deltaTime / lerpDuration;
			animation_Gui.thisCanvasGroup.alpha = Mathf.Lerp(0f, 1f, t2);
			yield return null;
		}
		main_Gui.SetCanvas(false);
		leaderBoard_Gui.SetCanvas(true);
		level_Manager.SetMenuTroops(false);
		t2 = 0f;
		while (t2 < 1f)
		{
			t2 += Time.deltaTime / lerpDuration;
			animation_Gui.thisCanvasGroup.alpha = Mathf.Lerp(1f, 0f, t2);
			yield return null;
		}
		animation_Gui.SetCanvas(false);
		SetWhichGuiIsOpen(WhichGuiIsOpen.LeaderBoard_Gui);
	}

	private IEnumerator CloseLeaderBoard_CR()
	{
		animation_Gui.SetCanvas(true);
		animation_Gui.thisCanvasGroup.alpha = 0f;
		float t2 = 0f;
		float lerpDuration = 0.25f;
		while (t2 < 1f)
		{
			t2 += Time.deltaTime / lerpDuration;
			animation_Gui.thisCanvasGroup.alpha = Mathf.Lerp(0f, 1f, t2);
			yield return null;
		}
		main_Gui.SetCanvas(true);
		leaderBoard_Gui.SetCanvas(false);
		level_Manager.SetMenuTroops(true);
		t2 = 0f;
		while (t2 < 1f)
		{
			t2 += Time.deltaTime / lerpDuration;
			animation_Gui.thisCanvasGroup.alpha = Mathf.Lerp(1f, 0f, t2);
			yield return null;
		}
		animation_Gui.SetCanvas(false);
		SetWhichGuiIsOpen(WhichGuiIsOpen.CanQuit);
	}

	private void OpenInAppInfoPanel(string title, string description, bool rewardState, bool infoAdsState)
	{
		SetWhichGuiIsOpen(WhichGuiIsOpen.InAppInfo_Gui);
		inAppInfo_Gui.thisPanel.SetActive(true);
		inAppInfo_Gui.title_Txt.text = title;
		inAppInfo_Gui.description_Txt.text = description;
		SetInAppInfoReward(rewardState);
		SetInfoAdsPanel(infoAdsState);
	}

	private void SetInAppInfoReward(bool state)
	{
		int num = level_Manager.EpicLevelsValues[level_Manager.epicLevelIndex].epic_Reward;
		if (level_Manager.epicLevelIndex == level_Manager.CountCompletedEpicLevels())
		{
			num = level_Manager.EpicLevelsValues[level_Manager.epicLevelIndex].epic_Reward_First_Time;
		}
		inAppInfo_Gui.rewardPanel.SetActive(state);
		inAppInfo_Gui.reward_Txt.text = string.Empty + num;
	}

	private void SetInfoAdsPanel(bool state)
	{
		inAppInfo_Gui.infoAdsPanel.SetActive(state);
	}

	private void CloseInAppInfoPanel()
	{
		if (inApp_Gui.thisCanvas.enabled)
		{
			SetWhichGuiIsOpen(WhichGuiIsOpen.InApp_Gui);
		}
		else if (inAppSpecial_Gui.thisCanvas.enabled)
		{
			SetWhichGuiIsOpen(WhichGuiIsOpen.InAppSpecial_Gui);
		}
		else if (setUpArmy_Gui.thisCanvas.enabled)
		{
			SetWhichGuiIsOpen(WhichGuiIsOpen.SetUpArmy_Gui);
		}
		else if (main_Gui.thisCanvas.enabled)
		{
			SetWhichGuiIsOpen(WhichGuiIsOpen.CanQuit);
		}
		inAppInfo_Gui.thisPanel.SetActive(false);
	}

	public void OpenInAppInfoToast(string message)
	{
		toastCustom_Gui.SetCanvas(true);
		toastCustom_Gui.toastMessage_Txt.text = message;
		toastCustom_Gui.thisCanvasGroup.DOKill();
		toastCustom_Gui.thisCanvasGroup.alpha = 0f;
		toastCustom_Gui.thisCanvasGroup.DOFade(1f, 0.4f).SetEase(Ease.OutCirc).OnComplete(delegate
		{
			CloseToast();
		});
	}

	private void CloseInAppInfoToast()
	{
		toastCustom_Gui.thisCanvasGroup.DOFade(0.5f, 0.4f).SetDelay(1.5f).SetEase(Ease.InCirc)
			.OnComplete(delegate
			{
				toastCustom_Gui.SetCanvas(false);
			});
	}

	public void OpenVipPanel()
	{
		SetWhichGuiIsOpen(WhichGuiIsOpen.Vip_Gui);
		vip_Gui.ResetAnimations();
		vip_Gui.SetCanvas(true);
		level_Manager.SetMenuTroops(false);
	}

	private void CloseVipPanel()
	{
		SetWhichGuiIsOpen(WhichGuiIsOpen.CanQuit);
		vip_Gui.SetCanvas(false);
		level_Manager.SetMenuTroops(true);
	}

	private bool ShouldOpenVIP()
	{
		return /*!InAppManager.HAS_SUBSCRIPTION &&*/ (DateTime.Now.Date - PlayerPrefs_Saves.LoadLastDayOpen().Date).Days > 0;
	}

	public void SetupSubscriptionPrice(string price)
	{
		string empty = string.Empty;
		empty = ((!(packagesManager != null) || packagesManager.whichStore != App_Stores.Google) ? "iTunes" : "Google");
		vip_Gui.subscriptionButton_Text.text = "3 days free trial, then " + price + " per week";
		vip_Gui.subscriptionInfo_Text.text = "VIP Membership offers a weekly subscription, you will have a 3-days FREE trail period, after this period you will be charged for " + price + ".\r\n\r\n        After buying this subscription, you will unlock the following features: Epic Troop Wolf Rider, upgrade three troops (Guard, Giant, Hwacha) to level three, gain 200 gems per day, collect double rewards and remove pop up ads!.\r\n\r\n        Subscription Notice:\r\n\r\n        - Payment will be charged to " + empty + " Account at confirmation of purchase.\r\n        - Subscription automatically renews unless auto-renew is turned off at least 24 - hours before the end of the current period.\r\n        - Account will be charged for renewal within 24 - hours prior to the end of the current period, and identify the cost of the renewal.\r\n        - Subscriptions may be managed by the user and auto - renewal may be turned off by going to the user’s Account Settings after purchase.\r\n        - Any unused portion of a free trial period, if offered, will be forfeited when the user purchases a subscription to that publication, where applicable.";
	}

	public void OpenRateInfoPanel(string title, string descr)
	{
		rateInfo_Gui.thisPanel.SetActive(true);
		rateInfo_Gui.title_Txt.text = title;
		rateInfo_Gui.description_Txt.text = descr;
	}

	public void CloseRateInfoPanel()
	{
		rateInfo_Gui.thisPanel.SetActive(false);
	}

	private void OpenMessage(string message)
	{
		SetWhichGuiIsOpen(WhichGuiIsOpen.Message_Gui);
		message_Gui.SetCanvas(true);
		message_Gui.Message_Txt.text = message;
	}

	private void CloseMessage()
	{
		if (setUpArmy_Gui.thisCanvas.enabled)
		{
			SetWhichGuiIsOpen(WhichGuiIsOpen.SetUpArmy_Gui);
		}
		else if (multiplayerConnect_Gui.thisCanvas.enabled)
		{
			SetWhichGuiIsOpen(WhichGuiIsOpen.MultiplayerConnect_Gui);
		}
		message_Gui.SetCanvas(false);
	}

	public void OpenToast(string message)
	{
		toastCustom_Gui.SetCanvas(true);
		toastCustom_Gui.toastMessage_Txt.text = message;
		toastCustom_Gui.thisCanvasGroup.DOKill();
		toastCustom_Gui.thisCanvasGroup.alpha = 0f;
		toastCustom_Gui.thisCanvasGroup.DOFade(1f, 0.4f).SetEase(Ease.OutCirc).OnComplete(delegate
		{
			CloseToast();
		});
	}

	private void CloseToast()
	{
		toastCustom_Gui.thisCanvasGroup.DOFade(0.5f, 0.4f).SetDelay(1.5f).SetEase(Ease.InCirc)
			.OnComplete(delegate
			{
				toastCustom_Gui.SetCanvas(false);
			});
	}

	public void Loading()
	{
		StopCoroutine("Loading_CR");
		StartCoroutine("Loading_CR");
	}

	private IEnumerator Loading_CR()
	{
		main_Gui.SetCanvas(true);
		loading_Gui.SetCanvas(true);
		settings_Gui.SetCanvas(true);
		inApp_Gui.SetCanvas(true);
		inAppSpecial_Gui.SetCanvas(true);
		setUpArmy_Gui.SetCanvas(true);
		game_Gui.SetCanvas(true);
		shop_Gui.SetCanvas(true);
		upgradeStatsValues_Gui.SetCanvas(true);
		multiplayerConnect_Gui.SetCanvas(true);
		gameOver_Gui.SetCanvas(true);
		pause_Gui.SetCanvas(true);
		leaderBoard_Gui.SetCanvas(true);
		message_Gui.SetCanvas(true);
		toastCustom_Gui.SetCanvas(true);
		animation_Gui.SetCanvas(true);
		loading_Gui.SetCanvas(true);
		vip_Gui.SetCanvas(true);
		settings_Gui.thisCanvasGroup.alpha = 0f;
		inApp_Gui.thisCanvasGroup.alpha = 0f;
		inAppSpecial_Gui.thisCanvasGroup.alpha = 0f;
		setUpArmy_Gui.thisCanvasGroup.alpha = 0f;
		game_Gui.thisCanvasGroup.alpha = 0f;
		shop_Gui.thisCanvasGroup.alpha = 0f;
		upgradeStatsValues_Gui.thisCanvasGroup.alpha = 0f;
		multiplayerConnect_Gui.thisCanvasGroup.alpha = 0f;
		gameOver_Gui.thisCanvasGroup.alpha = 0f;
		pause_Gui.thisCanvasGroup.alpha = 0f;
		leaderBoard_Gui.thisCanvasGroup.alpha = 0f;
		message_Gui.thisCanvasGroup.alpha = 0f;
		toastCustom_Gui.thisCanvasGroup.alpha = 0f;
		animation_Gui.thisCanvasGroup.alpha = 0f;
		vip_Gui.thisCanvasGroup.alpha = 0f;
		loading_Gui.thisCanvasGroup.alpha = 0f;
		level_Manager.ShopGOs.SetActive(true);
		level_Manager.shopCamera.enabled = false;
		yield return new WaitForSeconds(loading_Gui.Loading_Delay);
		settings_Gui.thisCanvasGroup.alpha = 1f;
		inApp_Gui.thisCanvasGroup.alpha = 1f;
		inAppSpecial_Gui.thisCanvasGroup.alpha = 1f;
		setUpArmy_Gui.thisCanvasGroup.alpha = 1f;
		game_Gui.thisCanvasGroup.alpha = 1f;
		shop_Gui.thisCanvasGroup.alpha = 1f;
		upgradeStatsValues_Gui.thisCanvasGroup.alpha = 1f;
		multiplayerConnect_Gui.thisCanvasGroup.alpha = 1f;
		gameOver_Gui.thisCanvasGroup.alpha = 1f;
		pause_Gui.thisCanvasGroup.alpha = 1f;
		leaderBoard_Gui.thisCanvasGroup.alpha = 1f;
		message_Gui.thisCanvasGroup.alpha = 1f;
		toastCustom_Gui.thisCanvasGroup.alpha = 1f;
		animation_Gui.thisCanvasGroup.alpha = 1f;
		vip_Gui.thisCanvasGroup.alpha = 1f;
		loading_Gui.thisCanvasGroup.alpha = 1f;
		settings_Gui.SetCanvas(false);
		inApp_Gui.SetCanvas(false);
		inAppSpecial_Gui.SetCanvas(false);
		setUpArmy_Gui.SetCanvas(false);
		game_Gui.SetCanvas(false);
		shop_Gui.SetCanvas(false);
		upgradeStatsValues_Gui.SetCanvas(false);
		multiplayerConnect_Gui.SetCanvas(false);
		gameOver_Gui.SetCanvas(false);
		pause_Gui.SetCanvas(false);
		leaderBoard_Gui.SetCanvas(false);
		message_Gui.SetCanvas(false);
		toastCustom_Gui.SetCanvas(false);
		animation_Gui.SetCanvas(false);
		loading_Gui.SetCanvas(false);
		if (ShouldOpenVIP())
		{
			OpenVipPanel();
			PlayerPrefs_Saves.SaveLastDayOpen(DateTime.Now);
		}
		else
		{
			vip_Gui.SetCanvas(false);
		}
		level_Manager.ShopGOs.SetActive(false);
		level_Manager.shopCamera.enabled = true;
		level_Manager.shopCamera.gameObject.AddComponent<AudioListener>();
		featuresManager.alManager.Begin();
		Invoke("CacheAds", 1f);
	}

	private void SetWhichGuiIsOpen(WhichGuiIsOpen guiIsOpen)
	{
		whichGuiIsOpen = guiIsOpen;
	}

	private void Android_BackButton()
	{
		if (level_Manager.gameMode == GameMode.Multiplayer && setUpArmy_Gui.thisCanvas.enabled)
		{
			return;
		}
		switch (whichGuiIsOpen)
		{
		case WhichGuiIsOpen.Between:
			return;
		case WhichGuiIsOpen.CanQuit:
			Application.Quit();
			break;
		case WhichGuiIsOpen.Tutorial_Gui:
			CloseTutorialPanel();
			break;
		case WhichGuiIsOpen.Settings_Gui:
			CloseSettings();
			break;
		case WhichGuiIsOpen.InApp_Gui:
			CloseInAppGui();
			break;
		case WhichGuiIsOpen.InAppSpecial_Gui:
			CloseInAppSpecialGui();
			break;
		case WhichGuiIsOpen.SetUpArmy_Gui:
			CloseSetUpArmy();
			break;
		case WhichGuiIsOpen.Game_Gui:
			if (level_Manager.gameMode != GameMode.Multiplayer)
			{
				Pause();
			}
			break;
		case WhichGuiIsOpen.Shop_Gui:
			CloseShop();
			break;
		case WhichGuiIsOpen.UpgradeStatsValues_Gui:
			CloseUpgradeStatsValues();
			break;
		case WhichGuiIsOpen.MultiplayerConnect_Gui:
			CloseMultiplayerConnect();
			break;
		case WhichGuiIsOpen.Pause_Gui:
			Resume();
			break;
		case WhichGuiIsOpen.LeaderBoard_Gui:
			CloseLeaderBoard();
			break;
		case WhichGuiIsOpen.CloudSave_Gui:
			CloseCloudSavePanel();
			break;
		case WhichGuiIsOpen.InfoBoss_Gui:
			CloseInfoBossPanel();
			break;
		case WhichGuiIsOpen.InAppInfo_Gui:
			CloseInAppInfoPanel();
			break;
		case WhichGuiIsOpen.Message_Gui:
			CloseMessage();
			break;
		case WhichGuiIsOpen.Vip_Gui:
			CloseVipPanel();
			break;
		}
		audio_Manager.MenuBack();
	}

	private void SetState_RewardedButtons(bool state)
	{
		if (state)
		{
			gameOver_Gui.consumables_Btn.gameObject.SetActive(true);
		}
		else
		{
			gameOver_Gui.consumables_Btn.gameObject.SetActive(false);
		}
		if (!PlayerPrefs_Saves.LoadCanShowAds())
		{
			setUpArmy_Gui.bonusGold_Btn.interactable = false;
			setUpArmy_Gui.bonusGoldChecked.SetActive(true);
			setUpArmy_Gui.bonusGoldNA.SetActive(false);
			setUpArmy_Gui.bonusArmy_Btn.interactable = false;
			setUpArmy_Gui.bonusArmyChecked.SetActive(true);
			setUpArmy_Gui.bonusArmyNA.SetActive(false);
		}
		else if (state)
		{
			if (!setUpArmy_Gui.bonusGoldChecked.activeSelf)
			{
				setUpArmy_Gui.bonusGold_Btn.interactable = true;
				setUpArmy_Gui.bonusGoldNA.SetActive(false);
			}
			if (!setUpArmy_Gui.bonusArmyChecked.activeSelf)
			{
				setUpArmy_Gui.bonusArmy_Btn.interactable = true;
				setUpArmy_Gui.bonusArmyNA.SetActive(false);
			}
		}
		else
		{
			if (!setUpArmy_Gui.bonusGoldChecked.activeSelf)
			{
				setUpArmy_Gui.bonusGold_Btn.interactable = false;
				setUpArmy_Gui.bonusGoldNA.SetActive(true);
			}
			if (!setUpArmy_Gui.bonusArmyChecked.activeSelf)
			{
				setUpArmy_Gui.bonusArmy_Btn.interactable = false;
				setUpArmy_Gui.bonusArmyNA.SetActive(true);
			}
		}
	}

	public void ResetAdButtons()
	{
		setUpArmy_Gui.bonusGold_Btn.interactable = true;
		setUpArmy_Gui.bonusGoldChecked.SetActive(false);
		setUpArmy_Gui.bonusArmy_Btn.interactable = true;
		setUpArmy_Gui.bonusArmyChecked.SetActive(false);
		gameOver_Gui.consumables_Btn.GetComponent<CanvasGroup>().interactable = true;
		gameOver_Gui.consumables_Btn.GetComponent<CanvasGroup>().alpha = 1f;
	}

	public void CacheAds()
	{
	 
	}

	private void AdLogic()
	{
		if (!PlayerPrefs_Saves.LoadCanShowAds())
		{
			RewardedCompleted();
			return;
		}
		 
	}

	public void IsRewardedVideo_Available()
	{
		if (!PlayerPrefs_Saves.LoadCanShowAds())
		{
			SetState_RewardedButtons(true);
			return;
		}
		//bool flag = unityAds_Manager.HasRewardedVideo();
		//bool flag2 = admob_Manager.HasRewardBasedVideo();
		//if (!flag2 && !flag)
		//{
		//	SetState_RewardedButtons(false);
		//}
		//else if (flag2 || flag)
		//{
		//	SetState_RewardedButtons(true);
		//}
	}

	public void RewardedCompleted()
	{
		if (Last_AdButton == LastPressed_AdButton.BonusGold)
		{
			setUpArmy_Gui.bonusGold_Btn.interactable = false;
			setUpArmy_Gui.bonusGoldChecked.SetActive(true);
			level_Manager.SetBonusGold(true);
			level_Manager.GetAvailableValues();
			level_Manager.CalculatePlayerArmyValues();
			SetAvailableTexts();
		}
		else if (Last_AdButton == LastPressed_AdButton.BonusArmy)
		{
			setUpArmy_Gui.bonusArmy_Btn.interactable = false;
			setUpArmy_Gui.bonusArmyChecked.SetActive(true);
			level_Manager.SetBonusArmy(true);
			level_Manager.GetAvailableValues();
			level_Manager.CalculatePlayerArmyValues();
			SetAvailableTexts();
		}
		else if (Last_AdButton == LastPressed_AdButton.EarnConsumableGameOver)
		{
			gameOver_Gui.consumables_Btn.GetComponent<CanvasGroup>().interactable = false;
			gameOver_Gui.consumables_Btn.GetComponent<CanvasGroup>().alpha = gameOver_Gui.alphaAdButtons;
			PlayerPrefs_Saves.SaveConsumables(gameOver_Gui.finalReward);
			SetConsumablesTexts();
		}
		CacheAds();
		IsRewardedVideo_Available();
		PlayerPrefs.Save();
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary.Add("af_content_type", "RewardedVideo");
		AppsFlyer.trackRichEvent("ad_watched", dictionary);
	}

	private void IntersitialAdLogic()
	{
		if (!PlayerPrefs_Saves.LoadCanShowAds() || PlayerPrefs_Saves.LoadHasBoughtIAP() /*|| InAppManager.HAS_SUBSCRIPTION*/)
		{
			GameOverEnd();
			return;
		}
		roundsCounter++;
		bool flag = false;
		if (roundsCounter >= roundsAds || Time.time - lastTimeInterstitial >= timeBetweenInterstitial)
		{
			//bool flag2 = unityAds_Manager.HasInterstitial();
			//bool flag3 = admob_Manager.HasInterstitial();
			//if (flag2)
			//{
			//	unityAds_Manager.ShowInterstitial();
			//	roundsCounter = 0;
			//	lastTimeInterstitial = Time.time;
			//	flag = true;
			//}
			//else if (flag3)
			//{
			//	admob_Manager.ShowInterstitial();
			//	roundsCounter = 0;
			//	lastTimeInterstitial = Time.time;
			//	flag = true;
			//}
		}
		if (!flag)
		{
			GameOverEnd();
		}
	}
	public void CloudSaveWaitPanelState(bool enabled)
	{
		if (enabled)
		{
			settings_Gui.cloudSaveFirstPanel.SetActive(false);
			settings_Gui.cloudSaveWaitPanel.SetActive(true);
			settings_Gui.cloudSave_Btn.interactable = false;
			settings_Gui.cloudLoad_Btn.interactable = false;
			settings_Gui.cloudBack_Btn.interactable = false;
			SetWhichGuiIsOpen(WhichGuiIsOpen.Between);
		}
		else
		{
			settings_Gui.cloudSaveFirstPanel.SetActive(true);
			settings_Gui.cloudSaveWaitPanel.SetActive(false);
			settings_Gui.cloudSave_Btn.interactable = true;
			settings_Gui.cloudLoad_Btn.interactable = true;
			settings_Gui.cloudBack_Btn.interactable = true;
			SetWhichGuiIsOpen(WhichGuiIsOpen.CloudSave_Gui);
		}
	}

	public void SaveToCloud()
	{
		CloudSaveWaitPanelState(true);
		if (packagesManager.whichStore == App_Stores.Google || packagesManager.whichStore == App_Stores.IOS)
		{
			featuresManager.cloudSaveManager.Save();
		}
	}

	public void LoadFromCloud()
	{
		CloudSaveWaitPanelState(true);
		if (packagesManager.whichStore == App_Stores.Google || packagesManager.whichStore == App_Stores.IOS)
		{
			featuresManager.cloudSaveManager.Load();
		}
	}

	public void SyncLeaderboard(string[] names, int[] trophies, int[] victories, int[] defeats, string user_name, int user_trophies, int user_victories, int user_defeats, long position)
	{
		leaderBoard_Gui.loadingPanel.SetActive(false);
		leaderBoard_Gui.namesPanel.SetActive(true);
		int num = names.Length;
		int i;
		for (i = 0; i < num; i++)
		{
			leaderBoard_Gui.LeaderBoardPlayers[i].number_Txt.text = i + 1 + ".";
			leaderBoard_Gui.LeaderBoardPlayers[i].name_Txt.text = names[i] + string.Empty;
			leaderBoard_Gui.LeaderBoardPlayers[i].trophy_Txt.text = trophies[i] + string.Empty;
			leaderBoard_Gui.LeaderBoardPlayers[i].battlesWon_Txt.text = victories[i] + string.Empty;
			leaderBoard_Gui.LeaderBoardPlayers[i].battlesLost_Txt.text = defeats[i] + string.Empty;
		}
		if (position == -1)
		{
			leaderBoard_Gui.LeaderBoardPlayers[i].number_Txt.text = "...";
			leaderBoard_Gui.LeaderBoardPlayers[i].name_Txt.text = "Unranked";
			leaderBoard_Gui.LeaderBoardPlayers[i].trophy_Txt.text = user_trophies + string.Empty;
			leaderBoard_Gui.LeaderBoardPlayers[i].battlesWon_Txt.text = user_victories + string.Empty;
			leaderBoard_Gui.LeaderBoardPlayers[i].battlesLost_Txt.text = user_defeats + string.Empty;
		}
		else
		{
			leaderBoard_Gui.LeaderBoardPlayers[i].number_Txt.text = position + 1 + ".";
			leaderBoard_Gui.LeaderBoardPlayers[i].name_Txt.text = user_name + string.Empty;
			leaderBoard_Gui.LeaderBoardPlayers[i].trophy_Txt.text = user_trophies + string.Empty;
			leaderBoard_Gui.LeaderBoardPlayers[i].battlesWon_Txt.text = user_victories + string.Empty;
			leaderBoard_Gui.LeaderBoardPlayers[i].battlesLost_Txt.text = user_defeats + string.Empty;
		}
	}
}
