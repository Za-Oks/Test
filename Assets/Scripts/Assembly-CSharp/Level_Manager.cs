using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level_Manager : MonoBehaviour
{
	private struct RemoveValues
	{
		public Army armyValue;

		public int price;

		public BotInfo botInfo;
	}

	[Serializable]
	public class ArmyBalance
	{
		public Army army;

		public int statsLvL;
	}

	[Header("References")]
	public Ui_Manager ui_Manager;

	public PlayerPrefs_Players playerPrefs_Players;

	public TopPanel_Manager topPanel_Manager;

	public InfoPanel_Manager infoPanel_Manager;

	public GameController gc;

	public Audio_Manager audio_Manager;

	public MultiplayerController multiplayerController;

	public TroopMaterialManager troopMaterialManager;

	[Header("GOs")]
	public GameObject MenuGOs;

	public GameObject MenuTroops;

	public GameObject ShopGOs;

	public GameObject PlayerQuad;

	public GameObject EnemyQuad;

	public Camera shopCamera;

	[Header("Level Design Values")]
	public LevelDesignValues[] LevelsDesignValues;

	public LevelDesign customDesign;

	public LevelDesign multiplayerDesign;

	[Header("Values")]
	public int troopsNumber;

	public int bossesNumber;

	public int customArmyNumber;

	public int multiArmyNumber;

	public int unlockEpicLevelsIndex;

	public LayerMask groundArmyMask;

	public LayerMask blockMask;

	public LayerMask botMask;

	public Toggle AutoAction_Toggle;

	public Sprite[] SpritesLvL;

	[Header("Tutorial Values")]
	public int tutorialArmyNumber;

	[Header("Multiplayer Values")]
	public int userAvailableGold;

	public int enemyAvailableGold;

	[Header("Army")]
	public ArmyInspector Archer;

	public ArmyInspector CannonMan;

	public ArmyInspector Gladiator;

	public ArmyInspector Knight;

	public ArmyInspector Man;

	public ArmyInspector CatapultMan;

	public ArmyInspector Musketeer;

	public ArmyInspector AxeMan;

	public ArmyInspector ShieldMan;

	public ArmyInspector SpearMan;

	public ArmyInspector Giant;

	public ArmyInspector Guard;

	public ArmyInspector Ballista;

	public ArmyInspector Crossbow;

	public ArmyInspector WarElephant;

	public ArmyInspector Kamikaze;

	public ArmyInspector Ninja;

	public ArmyInspector AK47;

	public ArmyInspector M16;

	public ArmyInspector MageTower;

	public ArmyInspector Chariot;

	public ArmyInspector Camel;

	public ArmyInspector Pirate;

	public ArmyInspector Hwacha;

	public ArmyInspector Mage;

	public ArmyInspector BearRider;

	public ArmyInspector OrganGun;

	public ArmyInspector TrebuchetMan;

	public ArmyInspector Berseker;

	[Header("Bosses")]
	public ArmyInspector Spartan;

	public ArmyInspector Sentinel;

	public ArmyInspector Samurai;

	public ArmyInspector FireDragon;

	public ArmyInspector Minigun;

	public ArmyInspector Rhino;

	public ArmyInspector Guardian;

	public ArmyInspector Wolf;

	[Header("Levels")]
	public LevelValues[] LevelsValues;

	[Header("Epic Levels")]
	public LevelValues[] EpicLevelsValues;

	[Header("Set up Values")]
	public bool GetConsumables;

	public bool CreateLevels;

	public bool UnlockGame;

	public LevelDesign CreateDesign;

	public Slider StatsLvLSlider;

	[Header("Go To Values")]
	public bool goToLevel;

	public int targetLevel;

	public int epicTargetLevel;

	[Header("Profile Values")]
	public bool IsProfile;

	[Range(0f, 100f)]
	public int armyCount;

	[Range(0f, 2f)]
	public int profileStatsLvL;

	public Army[] Troops;

	[Header("Balance Values")]
	public bool IsBalance;

	[Range(0f, 100f)]
	public float xPosBalance;

	public ArmyBalance[] PlayerBalance;

	public ArmyBalance[] EnemyBalance;

	private List<BotInfo> PlayerBotValues;

	private List<BotInfo> EnemyBotValues;

	private List<BotInfo> PlaceLevelArmyValues;

	private List<ArmyUsed> ArmyUsedValues;

	[HideInInspector]
	public Army armyValue;

	[HideInInspector]
	public int priceValue;

	[HideInInspector]
	public int availableGold;

	[HideInInspector]
	public int availableArmy;

	[HideInInspector]
	public int bonusGoldValue;

	[HideInInspector]
	public int bonusArmyValue;

	[HideInInspector]
	public int maxArmy;

	[HideInInspector]
	public int levelIndex;

	[HideInInspector]
	public int lastPlayedLevelIndex;

	[HideInInspector]
	public int epicLevelIndex;

	[HideInInspector]
	public int lastPlayedEpicLevelIndex;

	private GameObject Archer_Parent;

	private GameObject CannonMan_Parent;

	private GameObject Gladiator_Parent;

	private GameObject Knight_Parent;

	private GameObject Man_Parent;

	private GameObject CatapultMan_Parent;

	private GameObject Musketeer_Parent;

	private GameObject AxeMan_Parent;

	private GameObject ShieldMan_Parent;

	private GameObject SpearMan_Parent;

	private GameObject Giant_Parent;

	private GameObject Guard_Parent;

	private GameObject Ballista_Parent;

	private GameObject Crossbow_Parent;

	private GameObject WarElephant_Parent;

	private GameObject Kamikaze_Parent;

	private GameObject Ninja_Parent;

	private GameObject AK47_Parent;

	private GameObject M16_Parent;

	private GameObject MageTower_Parent;

	private GameObject Chariot_Parent;

	private GameObject Camel_Parent;

	private GameObject Pirate_Parent;

	private GameObject Hwacha_Parent;

	private GameObject Mage_Parent;

	private GameObject BearRider_Parent;

	private GameObject OrganGun_Parent;

	private GameObject TrebuchetMan_Parent;

	private GameObject Berseker_Parent;

	private GameObject Spartan_Parent;

	private GameObject Sentinel_Parent;

	private GameObject Samurai_Parent;

	private GameObject FireDragon_Parent;

	private GameObject Minigun_Parent;

	private GameObject Rhino_Parent;

	private GameObject Guardian_Parent;

	private GameObject Wolf_Parent;

	private Dictionary<string, BotInfo> bots = new Dictionary<string, BotInfo>();

	private List<BotInfo> Archer_Values = new List<BotInfo>();

	private List<BotInfo> CannonMan_Values = new List<BotInfo>();

	private List<BotInfo> Gladiator_Values = new List<BotInfo>();

	private List<BotInfo> Knight_Values = new List<BotInfo>();

	private List<BotInfo> Man_Values = new List<BotInfo>();

	private List<BotInfo> CatapultMan_Values = new List<BotInfo>();

	private List<BotInfo> Musketeer_Values = new List<BotInfo>();

	private List<BotInfo> AxeMan_Values = new List<BotInfo>();

	private List<BotInfo> ShieldMan_Values = new List<BotInfo>();

	private List<BotInfo> SpearMan_Values = new List<BotInfo>();

	private List<BotInfo> Giant_Values = new List<BotInfo>();

	private List<BotInfo> Guard_Values = new List<BotInfo>();

	private List<BotInfo> Ballista_Values = new List<BotInfo>();

	private List<BotInfo> Crossbow_Values = new List<BotInfo>();

	private List<BotInfo> WarElephant_Values = new List<BotInfo>();

	private List<BotInfo> Kamikaze_Values = new List<BotInfo>();

	private List<BotInfo> Ninja_Values = new List<BotInfo>();

	private List<BotInfo> AK47_Values = new List<BotInfo>();

	private List<BotInfo> M16_Values = new List<BotInfo>();

	private List<BotInfo> MageTower_Values = new List<BotInfo>();

	private List<BotInfo> Chariot_Values = new List<BotInfo>();

	private List<BotInfo> Camel_Values = new List<BotInfo>();

	private List<BotInfo> Pirate_Values = new List<BotInfo>();

	private List<BotInfo> Hwacha_Values = new List<BotInfo>();

	private List<BotInfo> Mage_Values = new List<BotInfo>();

	private List<BotInfo> BearRider_Values = new List<BotInfo>();

	private List<BotInfo> OrganGun_Values = new List<BotInfo>();

	private List<BotInfo> TrebuchetMan_Values = new List<BotInfo>();

	private List<BotInfo> Berseker_Values = new List<BotInfo>();

	private List<BotInfo> Spartan_Values = new List<BotInfo>();

	private List<BotInfo> Sentinel_Values = new List<BotInfo>();

	private List<BotInfo> Samurai_Values = new List<BotInfo>();

	private List<BotInfo> FireDragon_Values = new List<BotInfo>();

	private List<BotInfo> Minigun_Values = new List<BotInfo>();

	private List<BotInfo> Rhino_Values = new List<BotInfo>();

	private List<BotInfo> Guardian_Values = new List<BotInfo>();

	private List<BotInfo> Wolf_Values = new List<BotInfo>();

	private GameObject Troops_Parent;

	private GameObject Bosses_Parent;

	private PlaceAction action;

	private float placeTimer = 0.05f;

	private float tempTime = 100f;

	[HideInInspector]
	public GameMode gameMode;

	[HideInInspector]
	public BattleResult battleResult;

	[HideInInspector]
	public bool isMainScene;

	[HideInInspector]
	public bool isPlayer = true;

	[HideInInspector]
	public bool isPlayerReady;

	[HideInInspector]
	public bool isEnemyReady;

	private int loadedEnvironments;

	[HideInInspector]
	public int sendIDs;

	private List<LevelsArmyValues> LevelArmyValues;

	private float zTopBalance = 15f;

	private float zPosBalance;

	private float xOffsetBalance = 5f;

	private float zOffsetBalance = 5f;

	[HideInInspector]
	public List<ArmyInspector> ArmyInspectorTroops = new List<ArmyInspector>();

	private bool bonusGold { get; set; }

	private bool bonusArmy { get; set; }

	public int tempUserAvailableGold { get; set; }

	public int tempEnemyAvailableGold { get; set; }

	public void SetBonusGold(bool state)
	{
		if (!PlayerPrefs_Saves.LoadCanShowAds())
		{
			bonusGold = true;
		}
		else
		{
			bonusGold = state;
		}
	}

	public void SetBonusArmy(bool state)
	{
		if (!PlayerPrefs_Saves.LoadCanShowAds())
		{
			bonusArmy = true;
		}
		else
		{
			bonusArmy = state;
		}
	}

	public bool GetBonusGold()
	{
		return bonusGold;
	}

	public bool GetBonusArmy()
	{
		return bonusArmy;
	}

	private void Awake()
	{
		placeTimer = 0.05f;
		gameMode = GameMode.None;
		isMainScene = SceneManager.GetActiveScene().name.Equals("MainScene");
		Troops_Parent = new GameObject("Troops Parent");
		Bosses_Parent = new GameObject("Bosses Parent");
		PlayerBotValues = new List<BotInfo>();
		EnemyBotValues = new List<BotInfo>();
		PlaceLevelArmyValues = new List<BotInfo>();
		LevelArmyValues = new List<LevelsArmyValues>();
		ArmyUsedValues = new List<ArmyUsed>();
		lastPlayedLevelIndex = LevelsValues.Length - 1;
		lastPlayedEpicLevelIndex = EpicLevelsValues.Length - 1;
		InitTroopsSaveStats();
		InitTogglesValues();
		InitUpgradePanelsValues();
		InitValues();
		InstantiateArmyTroops();
		InstantiateArmyBosses();
		UnlockEveything();
		if (isMainScene)
		{
			if (goToLevel)
			{
				UnlockTargetLevel(targetLevel);
				UnlockTargetEpicLevel(epicTargetLevel);
			}
			SetStateLevelDesigns(true);
			LoadEnvironments();
		}
		else
		{
			gameMode = GameMode.Tutorial;
			PlaceTutorialArmy();
		}
	}

	public bool AddRemoveArmy(Vector3 touchPos, bool isFirst)
	{
		tempTime += Time.deltaTime;
		if (isFirst && AutoAction_Toggle.isOn)
		{
			tempTime = 100f;
			action = GetPlaceAction(touchPos);
			if (isMainScene)
			{
				ui_Manager.SetPlaceRemoveImg(action);
			}
		}
		bool result = false;
		if (CreateLevels)
		{
			if (action == PlaceAction.Place && tempTime >= placeTimer)
			{
				PlaceLevelArmy(touchPos);
				tempTime = 0f;
			}
			else if (action == PlaceAction.Remove)
			{
				RemoveLevelArmy(touchPos);
			}
		}
		else if (action == PlaceAction.Place && tempTime >= placeTimer)
		{
			result = PlaceArmy(touchPos);
			tempTime = 0f;
		}
		else if (action == PlaceAction.Remove)
		{
			RemoveArmy(touchPos);
		}
		return result;
	}

	public void UnlockEveything()
	{
	}

	public void UnlockSetUI(bool isGameplay)
	{
	}

	private void LoadEnvironments()
	{
		StartCoroutine(LoadIcyAdditive());
		StartCoroutine(LoadIslandAdditive());
		StartCoroutine(LoadVolcanoAdditive());
		StartCoroutine(LoadAncientGreeceAdditive());
		StartCoroutine(LoadEpicAdditive());
		StartCoroutine(LoadForestAdditive());
	}

	private IEnumerator LoadForestAdditive()
	{
		AsyncOperation async = SceneManager.LoadSceneAsync("Forest", LoadSceneMode.Additive);
		while (!async.isDone)
		{
			yield return null;
		}
		GameObject.Find("Forest Environment").transform.SetParent(LevelsDesignValues[1].levelDesignGO.transform);
		CheckIfAllEnvironmentsLoaded();
	}

	private IEnumerator LoadIcyAdditive()
	{
		AsyncOperation async = SceneManager.LoadSceneAsync("Icy", LoadSceneMode.Additive);
		while (!async.isDone)
		{
			yield return null;
		}
		GameObject.Find("Icy Environment").transform.SetParent(LevelsDesignValues[2].levelDesignGO.transform);
		CheckIfAllEnvironmentsLoaded();
	}

	private IEnumerator LoadIslandAdditive()
	{
		AsyncOperation async = SceneManager.LoadSceneAsync("Island", LoadSceneMode.Additive);
		while (!async.isDone)
		{
			yield return null;
		}
		GameObject.Find("Island Environment").transform.SetParent(LevelsDesignValues[3].levelDesignGO.transform);
		CheckIfAllEnvironmentsLoaded();
	}

	private IEnumerator LoadVolcanoAdditive()
	{
		AsyncOperation async = SceneManager.LoadSceneAsync("Volcano", LoadSceneMode.Additive);
		while (!async.isDone)
		{
			yield return null;
		}
		GameObject.Find("Volcano Environment").transform.SetParent(LevelsDesignValues[4].levelDesignGO.transform);
		CheckIfAllEnvironmentsLoaded();
	}

	private IEnumerator LoadAncientGreeceAdditive()
	{
		AsyncOperation async = SceneManager.LoadSceneAsync("AncientGreece", LoadSceneMode.Additive);
		while (!async.isDone)
		{
			yield return null;
		}
		GameObject.Find("AncientGreece Environment").transform.SetParent(LevelsDesignValues[5].levelDesignGO.transform);
		CheckIfAllEnvironmentsLoaded();
	}

	private IEnumerator LoadEpicAdditive()
	{
		AsyncOperation async = SceneManager.LoadSceneAsync("Epic", LoadSceneMode.Additive);
		while (!async.isDone)
		{
			yield return null;
		}
		GameObject.Find("Epic Environment").transform.SetParent(LevelsDesignValues[6].levelDesignGO.transform);
		CheckIfAllEnvironmentsLoaded();
	}

	private void CheckIfAllEnvironmentsLoaded()
	{
		loadedEnvironments++;
		if (loadedEnvironments == 6)
		{
			ui_Manager.SetCanvasLoading(false);
		}
	}

	private int SetBotSendID()
	{
		sendIDs++;
		return sendIDs;
	}

	private void InitValues()
	{
		armyValue = Army.Man;
		priceValue = Man.price;
		SetTroopValues(Man);
		SetBonusGold(false);
		SetBonusArmy(false);
	}

	private void InstantiateArmyTroops()
	{
		Archer_Parent = new GameObject("Archer_Parent");
		CannonMan_Parent = new GameObject("CannonMan_Parent");
		Gladiator_Parent = new GameObject("Gladiator_Parent");
		Knight_Parent = new GameObject("Knight_Parent");
		Man_Parent = new GameObject("Man_Parent");
		CatapultMan_Parent = new GameObject("CatapultMan_Parent");
		Musketeer_Parent = new GameObject("Musketeer_Parent");
		AxeMan_Parent = new GameObject("AxeMan_Parent");
		ShieldMan_Parent = new GameObject("ShieldMan_Parent");
		SpearMan_Parent = new GameObject("SpearMan_Parent");
		Giant_Parent = new GameObject("Giant_Parent");
		Guard_Parent = new GameObject("Guard_Parent");
		Ballista_Parent = new GameObject("Ballista_Parent");
		Crossbow_Parent = new GameObject("Crossbow_Parent");
		WarElephant_Parent = new GameObject("WarElephant_Parent");
		Kamikaze_Parent = new GameObject("Kamikaze_Parent");
		Ninja_Parent = new GameObject("Ninja_Parent");
		AK47_Parent = new GameObject("AK47_Parent");
		M16_Parent = new GameObject("M16_Parent");
		MageTower_Parent = new GameObject("MageTower_Parent");
		Chariot_Parent = new GameObject("Chariot_Parent");
		Camel_Parent = new GameObject("Camel_Parent");
		Pirate_Parent = new GameObject("Pirate_Parent");
		Hwacha_Parent = new GameObject("Hwacha_Parent");
		Mage_Parent = new GameObject("Mage_Parent");
		BearRider_Parent = new GameObject("BearRider_Parent");
		OrganGun_Parent = new GameObject("OrganGun_Parent");
		TrebuchetMan_Parent = new GameObject("TrebuchetMan_Parent");
		Berseker_Parent = new GameObject("Berseker_Parent");
		Archer_Parent.transform.SetParent(Troops_Parent.transform);
		CannonMan_Parent.transform.SetParent(Troops_Parent.transform);
		Gladiator_Parent.transform.SetParent(Troops_Parent.transform);
		Knight_Parent.transform.SetParent(Troops_Parent.transform);
		Man_Parent.transform.SetParent(Troops_Parent.transform);
		CatapultMan_Parent.transform.SetParent(Troops_Parent.transform);
		Musketeer_Parent.transform.SetParent(Troops_Parent.transform);
		AxeMan_Parent.transform.SetParent(Troops_Parent.transform);
		ShieldMan_Parent.transform.SetParent(Troops_Parent.transform);
		SpearMan_Parent.transform.SetParent(Troops_Parent.transform);
		Giant_Parent.transform.SetParent(Troops_Parent.transform);
		Guard_Parent.transform.SetParent(Troops_Parent.transform);
		Ballista_Parent.transform.SetParent(Troops_Parent.transform);
		Crossbow_Parent.transform.SetParent(Troops_Parent.transform);
		WarElephant_Parent.transform.SetParent(Troops_Parent.transform);
		Kamikaze_Parent.transform.SetParent(Troops_Parent.transform);
		Ninja_Parent.transform.SetParent(Troops_Parent.transform);
		AK47_Parent.transform.SetParent(Troops_Parent.transform);
		M16_Parent.transform.SetParent(Troops_Parent.transform);
		MageTower_Parent.transform.SetParent(Troops_Parent.transform);
		Chariot_Parent.transform.SetParent(Troops_Parent.transform);
		Camel_Parent.transform.SetParent(Troops_Parent.transform);
		Pirate_Parent.transform.SetParent(Troops_Parent.transform);
		Hwacha_Parent.transform.SetParent(Troops_Parent.transform);
		Mage_Parent.transform.SetParent(Troops_Parent.transform);
		BearRider_Parent.transform.SetParent(Troops_Parent.transform);
		OrganGun_Parent.transform.SetParent(Troops_Parent.transform);
		TrebuchetMan_Parent.transform.SetParent(Troops_Parent.transform);
		Berseker_Parent.transform.SetParent(Troops_Parent.transform);
		for (int i = 0; i < troopsNumber; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(Archer.GO, Archer_Parent.transform);
			GameObject gameObject2 = UnityEngine.Object.Instantiate(CannonMan.GO, CannonMan_Parent.transform);
			GameObject gameObject3 = UnityEngine.Object.Instantiate(Gladiator.GO, Gladiator_Parent.transform);
			GameObject gameObject4 = UnityEngine.Object.Instantiate(Knight.GO, Knight_Parent.transform);
			GameObject gameObject5 = UnityEngine.Object.Instantiate(Man.GO, Man_Parent.transform);
			GameObject gameObject6 = UnityEngine.Object.Instantiate(CatapultMan.GO, CatapultMan_Parent.transform);
			GameObject gameObject7 = UnityEngine.Object.Instantiate(Musketeer.GO, Musketeer_Parent.transform);
			GameObject gameObject8 = UnityEngine.Object.Instantiate(AxeMan.GO, AxeMan_Parent.transform);
			GameObject gameObject9 = UnityEngine.Object.Instantiate(ShieldMan.GO, ShieldMan_Parent.transform);
			GameObject gameObject10 = UnityEngine.Object.Instantiate(SpearMan.GO, SpearMan_Parent.transform);
			GameObject gameObject11 = UnityEngine.Object.Instantiate(Giant.GO, Giant_Parent.transform);
			GameObject gameObject12 = UnityEngine.Object.Instantiate(Guard.GO, Guard_Parent.transform);
			GameObject gameObject13 = UnityEngine.Object.Instantiate(Ballista.GO, Ballista_Parent.transform);
			GameObject gameObject14 = UnityEngine.Object.Instantiate(Crossbow.GO, Crossbow_Parent.transform);
			GameObject gameObject15 = UnityEngine.Object.Instantiate(WarElephant.GO, WarElephant_Parent.transform);
			GameObject gameObject16 = UnityEngine.Object.Instantiate(Kamikaze.GO, Kamikaze_Parent.transform);
			GameObject gameObject17 = UnityEngine.Object.Instantiate(Ninja.GO, Ninja_Parent.transform);
			GameObject gameObject18 = UnityEngine.Object.Instantiate(AK47.GO, AK47_Parent.transform);
			GameObject gameObject19 = UnityEngine.Object.Instantiate(M16.GO, M16_Parent.transform);
			GameObject gameObject20 = UnityEngine.Object.Instantiate(MageTower.GO, MageTower_Parent.transform);
			GameObject gameObject21 = UnityEngine.Object.Instantiate(Chariot.GO, Chariot_Parent.transform);
			GameObject gameObject22 = UnityEngine.Object.Instantiate(Camel.GO, Camel_Parent.transform);
			GameObject gameObject23 = UnityEngine.Object.Instantiate(Pirate.GO, Pirate_Parent.transform);
			GameObject gameObject24 = UnityEngine.Object.Instantiate(Hwacha.GO, Hwacha_Parent.transform);
			GameObject gameObject25 = UnityEngine.Object.Instantiate(Mage.GO, Mage_Parent.transform);
			GameObject gameObject26 = UnityEngine.Object.Instantiate(BearRider.GO, BearRider_Parent.transform);
			GameObject gameObject27 = UnityEngine.Object.Instantiate(OrganGun.GO, OrganGun_Parent.transform);
			GameObject gameObject28 = UnityEngine.Object.Instantiate(TrebuchetMan.GO, TrebuchetMan_Parent.transform);
			GameObject gameObject29 = UnityEngine.Object.Instantiate(Berseker.GO, Berseker_Parent.transform);
			gameObject.name = Archer.GO.name + " " + i;
			gameObject2.name = CannonMan.GO.name + " " + i;
			gameObject3.name = Gladiator.GO.name + " " + i;
			gameObject4.name = Knight.GO.name + " " + i;
			gameObject5.name = Man.GO.name + " " + i;
			gameObject6.name = CatapultMan.GO.name + " " + i;
			gameObject7.name = Musketeer.GO.name + " " + i;
			gameObject8.name = AxeMan.GO.name + " " + i;
			gameObject9.name = ShieldMan.GO.name + " " + i;
			gameObject10.name = SpearMan.GO.name + " " + i;
			gameObject11.name = Giant.GO.name + " " + i;
			gameObject12.name = Guard.GO.name + " " + i;
			gameObject13.name = Ballista.GO.name + " " + i;
			gameObject14.name = Crossbow.GO.name + " " + i;
			gameObject15.name = WarElephant.GO.name + " " + i;
			gameObject16.name = Kamikaze.GO.name + " " + i;
			gameObject17.name = Ninja.GO.name + " " + i;
			gameObject18.name = AK47.GO.name + " " + i;
			gameObject19.name = M16.GO.name + " " + i;
			gameObject20.name = MageTower.GO.name + " " + i;
			gameObject21.name = Chariot.GO.name + " " + i;
			gameObject22.name = Camel.GO.name + " " + i;
			gameObject23.name = Pirate.GO.name + " " + i;
			gameObject24.name = Hwacha.GO.name + " " + i;
			gameObject25.name = Mage.GO.name + " " + i;
			gameObject26.name = BearRider.GO.name + " " + i;
			gameObject27.name = OrganGun.GO.name + " " + i;
			gameObject28.name = TrebuchetMan.GO.name + " " + i;
			gameObject29.name = Berseker.GO.name + " " + i;
			gameObject.SetActive(false);
			gameObject2.SetActive(false);
			gameObject3.SetActive(false);
			gameObject4.SetActive(false);
			gameObject5.SetActive(false);
			gameObject6.SetActive(false);
			gameObject7.SetActive(false);
			gameObject8.SetActive(false);
			gameObject9.SetActive(false);
			gameObject10.SetActive(false);
			gameObject11.SetActive(false);
			gameObject12.SetActive(false);
			gameObject13.SetActive(false);
			gameObject14.SetActive(false);
			gameObject15.SetActive(false);
			gameObject16.SetActive(false);
			gameObject17.SetActive(false);
			gameObject18.SetActive(false);
			gameObject19.SetActive(false);
			gameObject20.SetActive(false);
			gameObject21.SetActive(false);
			gameObject22.SetActive(false);
			gameObject23.SetActive(false);
			gameObject24.SetActive(false);
			gameObject25.SetActive(false);
			gameObject26.SetActive(false);
			gameObject27.SetActive(false);
			gameObject28.SetActive(false);
			gameObject29.SetActive(false);
			BotInfo item = new BotInfo(gameObject, Archer, SetBotSendID());
			BotInfo item2 = new BotInfo(gameObject2, CannonMan, SetBotSendID());
			BotInfo item3 = new BotInfo(gameObject3, Gladiator, SetBotSendID());
			BotInfo item4 = new BotInfo(gameObject4, Knight, SetBotSendID());
			BotInfo item5 = new BotInfo(gameObject5, Man, SetBotSendID());
			BotInfo item6 = new BotInfo(gameObject6, CatapultMan, SetBotSendID());
			BotInfo item7 = new BotInfo(gameObject7, Musketeer, SetBotSendID());
			BotInfo item8 = new BotInfo(gameObject8, AxeMan, SetBotSendID());
			BotInfo item9 = new BotInfo(gameObject9, ShieldMan, SetBotSendID());
			BotInfo item10 = new BotInfo(gameObject10, SpearMan, SetBotSendID());
			BotInfo item11 = new BotInfo(gameObject11, Giant, SetBotSendID());
			BotInfo item12 = new BotInfo(gameObject12, Guard, SetBotSendID());
			BotInfo item13 = new BotInfo(gameObject13, Ballista, SetBotSendID());
			BotInfo item14 = new BotInfo(gameObject14, Crossbow, SetBotSendID());
			BotInfo item15 = new BotInfo(gameObject15, WarElephant, SetBotSendID());
			BotInfo item16 = new BotInfo(gameObject16, Kamikaze, SetBotSendID());
			BotInfo item17 = new BotInfo(gameObject17, Ninja, SetBotSendID());
			BotInfo item18 = new BotInfo(gameObject18, AK47, SetBotSendID());
			BotInfo item19 = new BotInfo(gameObject19, M16, SetBotSendID());
			BotInfo item20 = new BotInfo(gameObject20, MageTower, SetBotSendID());
			BotInfo item21 = new BotInfo(gameObject21, Chariot, SetBotSendID());
			BotInfo item22 = new BotInfo(gameObject22, Camel, SetBotSendID());
			BotInfo item23 = new BotInfo(gameObject23, Pirate, SetBotSendID());
			BotInfo item24 = new BotInfo(gameObject24, Hwacha, SetBotSendID());
			BotInfo item25 = new BotInfo(gameObject25, Mage, SetBotSendID());
			BotInfo item26 = new BotInfo(gameObject26, BearRider, SetBotSendID());
			BotInfo item27 = new BotInfo(gameObject27, OrganGun, SetBotSendID());
			BotInfo item28 = new BotInfo(gameObject28, TrebuchetMan, SetBotSendID());
			BotInfo item29 = new BotInfo(gameObject29, Berseker, SetBotSendID());
			Archer_Values.Add(item);
			CannonMan_Values.Add(item2);
			Gladiator_Values.Add(item3);
			Knight_Values.Add(item4);
			Man_Values.Add(item5);
			CatapultMan_Values.Add(item6);
			Musketeer_Values.Add(item7);
			AxeMan_Values.Add(item8);
			ShieldMan_Values.Add(item9);
			SpearMan_Values.Add(item10);
			Giant_Values.Add(item11);
			Guard_Values.Add(item12);
			Ballista_Values.Add(item13);
			Crossbow_Values.Add(item14);
			WarElephant_Values.Add(item15);
			Kamikaze_Values.Add(item16);
			Ninja_Values.Add(item17);
			AK47_Values.Add(item18);
			M16_Values.Add(item19);
			MageTower_Values.Add(item20);
			Chariot_Values.Add(item21);
			Camel_Values.Add(item22);
			Pirate_Values.Add(item23);
			Hwacha_Values.Add(item24);
			Mage_Values.Add(item25);
			BearRider_Values.Add(item26);
			OrganGun_Values.Add(item27);
			TrebuchetMan_Values.Add(item28);
			Berseker_Values.Add(item29);
		}
	}

	private void InstantiateArmyBosses()
	{
		if (isMainScene)
		{
			Spartan_Parent = new GameObject("Spartan_Parent");
			Sentinel_Parent = new GameObject("Sentinel_Parent");
			Samurai_Parent = new GameObject("Samurai_Parent");
			FireDragon_Parent = new GameObject("FireDragon_Parent");
			Minigun_Parent = new GameObject("Minigun_Parent");
			Rhino_Parent = new GameObject("Rhino_Parent");
			Guardian_Parent = new GameObject("Guardian_Parent");
			Wolf_Parent = new GameObject("Wolf_Parent");
			Spartan_Parent.transform.SetParent(Bosses_Parent.transform);
			Sentinel_Parent.transform.SetParent(Bosses_Parent.transform);
			Samurai_Parent.transform.SetParent(Bosses_Parent.transform);
			FireDragon_Parent.transform.SetParent(Bosses_Parent.transform);
			Minigun_Parent.transform.SetParent(Bosses_Parent.transform);
			Rhino_Parent.transform.SetParent(Bosses_Parent.transform);
			Guardian_Parent.transform.SetParent(Bosses_Parent.transform);
			Wolf_Parent.transform.SetParent(Bosses_Parent.transform);
			for (int i = 0; i < bossesNumber; i++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(Spartan.GO, Spartan_Parent.transform);
				GameObject gameObject2 = UnityEngine.Object.Instantiate(Sentinel.GO, Sentinel_Parent.transform);
				GameObject gameObject3 = UnityEngine.Object.Instantiate(Samurai.GO, Samurai_Parent.transform);
				GameObject gameObject4 = UnityEngine.Object.Instantiate(FireDragon.GO, FireDragon_Parent.transform);
				GameObject gameObject5 = UnityEngine.Object.Instantiate(Minigun.GO, Minigun_Parent.transform);
				GameObject gameObject6 = UnityEngine.Object.Instantiate(Rhino.GO, Rhino_Parent.transform);
				GameObject gameObject7 = UnityEngine.Object.Instantiate(Guardian.GO, Guardian_Parent.transform);
				GameObject gameObject8 = UnityEngine.Object.Instantiate(Wolf.GO, Wolf_Parent.transform);
				gameObject.name = Spartan.GO.name + " " + i;
				gameObject2.name = Sentinel.GO.name + " " + i;
				gameObject3.name = Samurai.GO.name + " " + i;
				gameObject4.name = FireDragon.GO.name + " " + i;
				gameObject5.name = Minigun.GO.name + " " + i;
				gameObject6.name = Rhino.GO.name + " " + i;
				gameObject7.name = Guardian.GO.name + " " + i;
				gameObject8.name = Wolf.GO.name + " " + i;
				gameObject.SetActive(false);
				gameObject2.SetActive(false);
				gameObject3.SetActive(false);
				gameObject4.SetActive(false);
				gameObject5.SetActive(false);
				gameObject6.SetActive(false);
				gameObject7.SetActive(false);
				gameObject8.SetActive(false);
				BotInfo item = new BotInfo(gameObject, Spartan, SetBotSendID());
				BotInfo item2 = new BotInfo(gameObject2, Sentinel, SetBotSendID());
				BotInfo item3 = new BotInfo(gameObject3, Samurai, SetBotSendID());
				BotInfo item4 = new BotInfo(gameObject4, FireDragon, SetBotSendID());
				BotInfo item5 = new BotInfo(gameObject5, Minigun, SetBotSendID());
				BotInfo item6 = new BotInfo(gameObject6, Rhino, SetBotSendID());
				BotInfo item7 = new BotInfo(gameObject7, Guardian, SetBotSendID());
				BotInfo item8 = new BotInfo(gameObject8, Wolf, SetBotSendID());
				Spartan_Values.Add(item);
				Sentinel_Values.Add(item2);
				Samurai_Values.Add(item3);
				FireDragon_Values.Add(item4);
				Minigun_Values.Add(item5);
				Rhino_Values.Add(item6);
				Guardian_Values.Add(item7);
				Wolf_Values.Add(item8);
			}
		}
	}

	public void ResetBotList()
	{
		bots = new Dictionary<string, BotInfo>();
	}

	public void FinalizeBotList()
	{
		gc.SetupLevel(bots);
	}

	public void ClosePlayerSpritesLvL()
	{
		int count = PlayerBotValues.Count;
		for (int i = 0; i < count; i++)
		{
			PlayerBotValues[i].botInfoUpgrades.CloseSpriteLvL();
		}
	}

	public void CloseEnemySpritesLvL()
	{
		int count = EnemyBotValues.Count;
		for (int i = 0; i < count; i++)
		{
			EnemyBotValues[i].botInfoUpgrades.CloseSpriteLvL();
		}
	}

	public void CloseLevelSpritesLvL()
	{
		int count = PlaceLevelArmyValues.Count;
		for (int i = 0; i < count; i++)
		{
			PlaceLevelArmyValues[i].botInfoUpgrades.CloseSpriteLvL();
		}
	}

	public void SpawnPlayerArmy()
	{
		int count = PlayerBotValues.Count;
		for (int i = 0; i < count; i++)
		{
			BotInfo botInfo = PlayerBotValues[i];
			bots.Add(botInfo.gameObject.name, botInfo);
			if (gameMode == GameMode.Levels || gameMode == GameMode.EpicLevels || gameMode == GameMode.Custom)
			{
				AddArmyUsedValues(botInfo);
			}
		}
		PlayerBotValues.Clear();
	}

	public void SpawnEnemyArmy()
	{
		int count = EnemyBotValues.Count;
		for (int i = 0; i < count; i++)
		{
			BotInfo botInfo = EnemyBotValues[i];
			bots.Add(botInfo.gameObject.name, botInfo);
			if (gameMode == GameMode.Custom)
			{
				AddArmyUsedValues(botInfo);
			}
		}
		EnemyBotValues.Clear();
	}

	public void SpawnLevelsArmy()
	{
		int count = LevelsValues[levelIndex].levelSetUp.ArmySetUp.Count;
		for (int i = 0; i < count; i++)
		{
			LevelsArmyValues levelsArmyValues = LevelsValues[levelIndex].levelSetUp.ArmySetUp[i];
			Army tempArmy = levelsArmyValues.armyValue;
			Vector3 position = levelsArmyValues.position;
			Quaternion rotation = Quaternion.Euler(0f, 270f, 0f);
			int statsLvL = levelsArmyValues.statsLvL;
			BotInfo botInfo = ReturnBotInfo(tempArmy);
			botInfo.SetUpStatsLvL(statsLvL);
			botInfo.SetUp(false);
			botInfo.referencesManager.thisTR.position = position;
			botInfo.referencesManager.thisTR.rotation = rotation;
			botInfo.gameObject.SetActive(true);
			bots.Add(botInfo.gameObject.name, botInfo);
		}
	}

	public void SpawnEpicLevelsArmy()
	{
		int count = EpicLevelsValues[epicLevelIndex].levelSetUp.ArmySetUp.Count;
		for (int i = 0; i < count; i++)
		{
			LevelsArmyValues levelsArmyValues = EpicLevelsValues[epicLevelIndex].levelSetUp.ArmySetUp[i];
			Army tempArmy = levelsArmyValues.armyValue;
			Vector3 position = levelsArmyValues.position;
			Quaternion rotation = Quaternion.Euler(0f, 270f, 0f);
			int statsLvL = levelsArmyValues.statsLvL;
			BotInfo botInfo = ReturnBotInfo(tempArmy);
			botInfo.SetUpStatsLvL(statsLvL);
			botInfo.SetUp(false);
			botInfo.referencesManager.thisTR.position = position;
			botInfo.referencesManager.thisTR.rotation = rotation;
			botInfo.gameObject.SetActive(true);
			bots.Add(botInfo.gameObject.name, botInfo);
		}
	}

	public void SpawnTutorialArmy()
	{
		int count = LevelsValues[0].levelSetUp.ArmySetUp.Count;
		for (int i = 0; i < count; i++)
		{
			LevelsArmyValues levelsArmyValues = LevelsValues[0].levelSetUp.ArmySetUp[i];
			Army tempArmy = levelsArmyValues.armyValue;
			Vector3 position = levelsArmyValues.position;
			Quaternion rotation = Quaternion.Euler(0f, 270f, 0f);
			int statsLvL = levelsArmyValues.statsLvL;
			BotInfo botInfo = ReturnBotInfo(tempArmy);
			botInfo.SetUpStatsLvL(statsLvL);
			botInfo.SetUp(false);
			botInfo.referencesManager.thisTR.position = position;
			botInfo.referencesManager.thisTR.rotation = rotation;
			botInfo.gameObject.SetActive(true);
			bots.Add(botInfo.gameObject.name, botInfo);
		}
	}

	public void PlaceLevelsArmy()
	{
		int count = LevelsValues[levelIndex].levelSetUp.ArmySetUp.Count;
		for (int i = 0; i < count; i++)
		{
			LevelsArmyValues levelsArmyValues = LevelsValues[levelIndex].levelSetUp.ArmySetUp[i];
			Army tempArmy = levelsArmyValues.armyValue;
			Vector3 position = levelsArmyValues.position;
			Quaternion rotation = Quaternion.Euler(0f, 270f, 0f);
			int statsLvL = levelsArmyValues.statsLvL;
			BotInfo botInfo = ReturnBotInfo(tempArmy);
			botInfo.SetUpStatsLvL(statsLvL);
			botInfo.SetUp(false);
			botInfo.botInfoUpgrades.SetSpriteLvL(ReturnSpriteLvL(statsLvL), false);
			botInfo.referencesManager.thisTR.position = position;
			botInfo.referencesManager.thisTR.rotation = rotation;
			botInfo.gameObject.SetActive(true);
			PlaceLevelArmyValues.Add(botInfo);
		}
	}

	public void PlaceEpicLevelsArmy()
	{
		int count = EpicLevelsValues[epicLevelIndex].levelSetUp.ArmySetUp.Count;
		for (int i = 0; i < count; i++)
		{
			LevelsArmyValues levelsArmyValues = EpicLevelsValues[epicLevelIndex].levelSetUp.ArmySetUp[i];
			Army tempArmy = levelsArmyValues.armyValue;
			Vector3 position = levelsArmyValues.position;
			Quaternion rotation = Quaternion.Euler(0f, 270f, 0f);
			int statsLvL = levelsArmyValues.statsLvL;
			BotInfo botInfo = ReturnBotInfo(tempArmy);
			botInfo.SetUpStatsLvL(statsLvL);
			botInfo.SetUp(false);
			botInfo.botInfoUpgrades.SetSpriteLvL(ReturnSpriteLvL(statsLvL), false);
			botInfo.referencesManager.thisTR.position = position;
			botInfo.referencesManager.thisTR.rotation = rotation;
			botInfo.gameObject.SetActive(true);
			PlaceLevelArmyValues.Add(botInfo);
		}
	}

	public void PlaceTutorialArmy()
	{
		int count = LevelsValues[0].levelSetUp.ArmySetUp.Count;
		for (int i = 0; i < count; i++)
		{
			LevelsArmyValues levelsArmyValues = LevelsValues[0].levelSetUp.ArmySetUp[i];
			Army tempArmy = levelsArmyValues.armyValue;
			Vector3 position = levelsArmyValues.position;
			Quaternion rotation = Quaternion.Euler(0f, 270f, 0f);
			int statsLvL = levelsArmyValues.statsLvL;
			BotInfo botInfo = ReturnBotInfo(tempArmy);
			botInfo.SetUpStatsLvL(statsLvL);
			botInfo.SetUp(false);
			botInfo.botInfoUpgrades.SetSpriteLvL(ReturnSpriteLvL(statsLvL), false);
			botInfo.referencesManager.thisTR.position = position;
			botInfo.referencesManager.thisTR.rotation = rotation;
			botInfo.gameObject.SetActive(true);
			PlaceLevelArmyValues.Add(botInfo);
		}
	}

	public void ResetLevelsArmy()
	{
		int count = PlaceLevelArmyValues.Count;
		for (int i = 0; i < count; i++)
		{
			PlaceLevelArmyValues[i].gameObject.SetActive(false);
		}
		PlaceLevelArmyValues.Clear();
	}

	public void ResetPlayerArmy()
	{
		int count = PlayerBotValues.Count;
		for (int i = 0; i < count; i++)
		{
			PlayerBotValues[i].gameObject.SetActive(false);
		}
		PlayerBotValues.Clear();
		ResetCountersValues();
	}

	public void ResetEnemyArmy()
	{
		int count = EnemyBotValues.Count;
		for (int i = 0; i < count; i++)
		{
			EnemyBotValues[i].gameObject.SetActive(false);
		}
		EnemyBotValues.Clear();
		ResetCountersValues();
	}

	public void SetPlayerQuad(bool state)
	{
		PlayerQuad.SetActive(state);
	}

	public void SetEnemyQuad(bool state)
	{
		EnemyQuad.SetActive(state);
	}

	public void SetValuesArmyInspector(ArmyInspector tempInspector)
	{
		armyValue = tempInspector.army;
		priceValue = tempInspector.price;
		SetTroopValues(tempInspector);
		infoPanel_Manager.InfoPanelOpen();
	}

	public void SetTroopValues(ArmyInspector tempTroop)
	{
		string text = tempTroop.ReturnName();
		string text2 = tempTroop.ReturnLevelInfo();
		int index = tempTroop.LoadStatsLvL();
		StatsFinal statsFinal = tempTroop.ReturnFinalStats(index);
		int digits = 2;
		infoPanel_Manager.troopInfo_Txt.text = text + " - " + text2;
		infoPanel_Manager.troopHealth_Txt.text = ((float)Math.Round(statsFinal.healthFinal, digits)).ToString();
		infoPanel_Manager.troopDamage_Txt.text = ((float)Math.Round(statsFinal.damageFinal, digits)).ToString();
		infoPanel_Manager.troopAttackSpeed_Txt.text = ((float)Math.Round(statsFinal.attackSpeedFinal, digits)).ToString();
		infoPanel_Manager.troopMovementSpeed_Txt.text = ((float)Math.Round(statsFinal.movementSpeedFinal, digits)).ToString();
		infoPanel_Manager.troopHitDistance_Txt.text = statsFinal.hitDistance.ToString();
		infoPanel_Manager.troopPenetration_Txt.text = statsFinal.penetration.ToString();
	}

	public void SetTroopBossValues(string tempName)
	{
		ArmyInspector armyInspector = (tempName.Equals("Spartan") ? Spartan : (tempName.Equals("Sentinel") ? Sentinel : (tempName.Equals("SamuraiNEW") ? Samurai : (tempName.Equals("FireDragonNEW") ? FireDragon : (tempName.Equals("Minigun") ? Minigun : (tempName.Equals("Rhino") ? Rhino : (tempName.Equals("Guardian") ? Guardian : ((!tempName.Equals("Wolf")) ? null : Wolf))))))));
		string text = armyInspector.ReturnTroopBossInfo();
		int index = armyInspector.LoadStatsLvL();
		StatsFinal statsFinal = armyInspector.ReturnFinalStats(index);
		int digits = 2;
		ui_Manager.shop_Gui.troopBossInfo_Txt.text = text;
		ui_Manager.shop_Gui.troopBossHealth_Txt.text = ((float)Math.Round(statsFinal.healthFinal, digits)).ToString();
		ui_Manager.shop_Gui.troopBossDamage_Txt.text = ((float)Math.Round(statsFinal.damageFinal, digits)).ToString();
		ui_Manager.shop_Gui.troopBossAttackSpeed_Txt.text = ((float)Math.Round(statsFinal.attackSpeedFinal, digits)).ToString();
		ui_Manager.shop_Gui.troopBossMovementSpeed_Txt.text = ((float)Math.Round(statsFinal.movementSpeedFinal, digits)).ToString();
		ui_Manager.shop_Gui.troopBossHitDistance_Txt.text = statsFinal.hitDistance.ToString();
		ui_Manager.shop_Gui.troopBossPenetration_Txt.text = statsFinal.penetration.ToString();
	}

	private BotInfo ReturnBotInfo(Army tempArmy)
	{
		BotInfo result = null;
		switch (tempArmy)
		{
		case Army.Archer:
			result = FirstAvailableArmyValue(Archer_Values, tempArmy);
			break;
		case Army.CannonMan:
			result = FirstAvailableArmyValue(CannonMan_Values, tempArmy);
			break;
		case Army.Gladiator:
			result = FirstAvailableArmyValue(Gladiator_Values, tempArmy);
			break;
		case Army.Knight:
			result = FirstAvailableArmyValue(Knight_Values, tempArmy);
			break;
		case Army.Man:
			result = FirstAvailableArmyValue(Man_Values, tempArmy);
			break;
		case Army.CatapultMan:
			result = FirstAvailableArmyValue(CatapultMan_Values, tempArmy);
			break;
		case Army.Musketeer:
			result = FirstAvailableArmyValue(Musketeer_Values, tempArmy);
			break;
		case Army.AxeMan:
			result = FirstAvailableArmyValue(AxeMan_Values, tempArmy);
			break;
		case Army.ShieldMan:
			result = FirstAvailableArmyValue(ShieldMan_Values, tempArmy);
			break;
		case Army.SpearMan:
			result = FirstAvailableArmyValue(SpearMan_Values, tempArmy);
			break;
		case Army.Giant:
			result = FirstAvailableArmyValue(Giant_Values, tempArmy);
			break;
		case Army.Guard:
			result = FirstAvailableArmyValue(Guard_Values, tempArmy);
			break;
		case Army.Ballista:
			result = FirstAvailableArmyValue(Ballista_Values, tempArmy);
			break;
		case Army.Crossbow:
			result = FirstAvailableArmyValue(Crossbow_Values, tempArmy);
			break;
		case Army.WarElephant:
			result = FirstAvailableArmyValue(WarElephant_Values, tempArmy);
			break;
		case Army.Kamikaze:
			result = FirstAvailableArmyValue(Kamikaze_Values, tempArmy);
			break;
		case Army.Ninja:
			result = FirstAvailableArmyValue(Ninja_Values, tempArmy);
			break;
		case Army.AK47:
			result = FirstAvailableArmyValue(AK47_Values, tempArmy);
			break;
		case Army.M16:
			result = FirstAvailableArmyValue(M16_Values, tempArmy);
			break;
		case Army.MageTower:
			result = FirstAvailableArmyValue(MageTower_Values, tempArmy);
			break;
		case Army.Chariot:
			result = FirstAvailableArmyValue(Chariot_Values, tempArmy);
			break;
		case Army.Camel:
			result = FirstAvailableArmyValue(Camel_Values, tempArmy);
			break;
		case Army.Pirate:
			result = FirstAvailableArmyValue(Pirate_Values, tempArmy);
			break;
		case Army.Hwacha:
			result = FirstAvailableArmyValue(Hwacha_Values, tempArmy);
			break;
		case Army.Mage:
			result = FirstAvailableArmyValue(Mage_Values, tempArmy);
			break;
		case Army.BearRider:
			result = FirstAvailableArmyValue(BearRider_Values, tempArmy);
			break;
		case Army.OrganGun:
			result = FirstAvailableArmyValue(OrganGun_Values, tempArmy);
			break;
		case Army.TrebuchetMan:
			result = FirstAvailableArmyValue(TrebuchetMan_Values, tempArmy);
			break;
		case Army.Berseker:
			result = FirstAvailableArmyValue(Berseker_Values, tempArmy);
			break;
		case Army.Spartan:
			result = FirstAvailableArmyValue(Spartan_Values, tempArmy);
			break;
		case Army.Sentinel:
			result = FirstAvailableArmyValue(Sentinel_Values, tempArmy);
			break;
		case Army.SamuraiNEW:
			result = FirstAvailableArmyValue(Samurai_Values, tempArmy);
			break;
		case Army.FireDragonNEW:
			result = FirstAvailableArmyValue(FireDragon_Values, tempArmy);
			break;
		case Army.Minigun:
			result = FirstAvailableArmyValue(Minigun_Values, tempArmy);
			break;
		case Army.Rhino:
			result = FirstAvailableArmyValue(Rhino_Values, tempArmy);
			break;
		case Army.Guardian:
			result = FirstAvailableArmyValue(Guardian_Values, tempArmy);
			break;
		case Army.Wolf:
			result = FirstAvailableArmyValue(Wolf_Values, tempArmy);
			break;
		}
		return result;
	}

	private BotInfo FirstAvailableArmyValue(List<BotInfo> Values, Army tempArmy)
	{
		int count = Values.Count;
		for (int i = 0; i < count; i++)
		{
			if (!Values[i].gameObject.activeInHierarchy)
			{
				return Values[i];
			}
		}
		GameObject gameObject;
		ArmyInspector tempArmyInspector;
		switch (tempArmy)
		{
		case Army.Archer:
			gameObject = UnityEngine.Object.Instantiate(Archer.GO);
			gameObject.name = Archer.GO.name + " " + count;
			gameObject.transform.SetParent(Archer_Parent.transform);
			gameObject.SetActive(false);
			tempArmyInspector = Archer;
			break;
		case Army.CannonMan:
			gameObject = UnityEngine.Object.Instantiate(CannonMan.GO);
			gameObject.name = CannonMan.GO.name + " " + count;
			gameObject.transform.SetParent(CannonMan_Parent.transform);
			gameObject.SetActive(false);
			tempArmyInspector = CannonMan;
			break;
		case Army.Gladiator:
			gameObject = UnityEngine.Object.Instantiate(Gladiator.GO);
			gameObject.name = Gladiator.GO.name + " " + count;
			gameObject.transform.SetParent(Gladiator_Parent.transform);
			gameObject.SetActive(false);
			tempArmyInspector = Gladiator;
			break;
		case Army.Knight:
			gameObject = UnityEngine.Object.Instantiate(Knight.GO);
			gameObject.name = Knight.GO.name + " " + count;
			gameObject.transform.SetParent(Knight_Parent.transform);
			gameObject.SetActive(false);
			tempArmyInspector = Knight;
			break;
		case Army.Man:
			gameObject = UnityEngine.Object.Instantiate(Man.GO);
			gameObject.name = Man.GO.name + " " + count;
			gameObject.transform.SetParent(Man_Parent.transform);
			gameObject.SetActive(false);
			tempArmyInspector = Man;
			break;
		case Army.CatapultMan:
			gameObject = UnityEngine.Object.Instantiate(CatapultMan.GO);
			gameObject.name = CatapultMan.GO.name + " " + count;
			gameObject.transform.SetParent(CatapultMan_Parent.transform);
			gameObject.SetActive(false);
			tempArmyInspector = CatapultMan;
			break;
		case Army.Musketeer:
			gameObject = UnityEngine.Object.Instantiate(Musketeer.GO);
			gameObject.name = Musketeer.GO.name + " " + count;
			gameObject.transform.SetParent(Musketeer_Parent.transform);
			gameObject.SetActive(false);
			tempArmyInspector = Musketeer;
			break;
		case Army.AxeMan:
			gameObject = UnityEngine.Object.Instantiate(AxeMan.GO);
			gameObject.name = AxeMan.GO.name + " " + count;
			gameObject.transform.SetParent(AxeMan_Parent.transform);
			gameObject.SetActive(false);
			tempArmyInspector = AxeMan;
			break;
		case Army.ShieldMan:
			gameObject = UnityEngine.Object.Instantiate(ShieldMan.GO);
			gameObject.name = ShieldMan.GO.name + " " + count;
			gameObject.transform.SetParent(ShieldMan_Parent.transform);
			gameObject.SetActive(false);
			tempArmyInspector = ShieldMan;
			break;
		case Army.SpearMan:
			gameObject = UnityEngine.Object.Instantiate(SpearMan.GO);
			gameObject.name = SpearMan.GO.name + " " + count;
			gameObject.transform.SetParent(SpearMan_Parent.transform);
			gameObject.SetActive(false);
			tempArmyInspector = SpearMan;
			break;
		case Army.Giant:
			gameObject = UnityEngine.Object.Instantiate(Giant.GO);
			gameObject.name = Giant.GO.name + " " + count;
			gameObject.transform.SetParent(Giant_Parent.transform);
			gameObject.SetActive(false);
			tempArmyInspector = Giant;
			break;
		case Army.Guard:
			gameObject = UnityEngine.Object.Instantiate(Guard.GO);
			gameObject.name = Guard.GO.name + " " + count;
			gameObject.transform.SetParent(Guard_Parent.transform);
			gameObject.SetActive(false);
			tempArmyInspector = Guard;
			break;
		case Army.Ballista:
			gameObject = UnityEngine.Object.Instantiate(Ballista.GO);
			gameObject.name = Ballista.GO.name + " " + count;
			gameObject.transform.SetParent(Ballista_Parent.transform);
			gameObject.SetActive(false);
			tempArmyInspector = Ballista;
			break;
		case Army.Crossbow:
			gameObject = UnityEngine.Object.Instantiate(Crossbow.GO);
			gameObject.name = Crossbow.GO.name + " " + count;
			gameObject.transform.SetParent(Crossbow_Parent.transform);
			gameObject.SetActive(false);
			tempArmyInspector = Crossbow;
			break;
		case Army.WarElephant:
			gameObject = UnityEngine.Object.Instantiate(WarElephant.GO);
			gameObject.name = WarElephant.GO.name + " " + count;
			gameObject.transform.SetParent(WarElephant_Parent.transform);
			gameObject.SetActive(false);
			tempArmyInspector = WarElephant;
			break;
		case Army.Kamikaze:
			gameObject = UnityEngine.Object.Instantiate(Kamikaze.GO);
			gameObject.name = Kamikaze.GO.name + " " + count;
			gameObject.transform.SetParent(Kamikaze_Parent.transform);
			gameObject.SetActive(false);
			tempArmyInspector = Kamikaze;
			break;
		case Army.Ninja:
			gameObject = UnityEngine.Object.Instantiate(Ninja.GO);
			gameObject.name = Ninja.GO.name + " " + count;
			gameObject.transform.SetParent(Ninja_Parent.transform);
			gameObject.SetActive(false);
			tempArmyInspector = Ninja;
			break;
		case Army.AK47:
			gameObject = UnityEngine.Object.Instantiate(AK47.GO);
			gameObject.name = AK47.GO.name + " " + count;
			gameObject.transform.SetParent(AK47_Parent.transform);
			gameObject.SetActive(false);
			tempArmyInspector = AK47;
			break;
		case Army.M16:
			gameObject = UnityEngine.Object.Instantiate(M16.GO);
			gameObject.name = M16.GO.name + " " + count;
			gameObject.transform.SetParent(M16_Parent.transform);
			gameObject.SetActive(false);
			tempArmyInspector = M16;
			break;
		case Army.MageTower:
			gameObject = UnityEngine.Object.Instantiate(MageTower.GO);
			gameObject.name = MageTower.GO.name + " " + count;
			gameObject.transform.SetParent(MageTower_Parent.transform);
			gameObject.SetActive(false);
			tempArmyInspector = MageTower;
			break;
		case Army.Chariot:
			gameObject = UnityEngine.Object.Instantiate(Chariot.GO);
			gameObject.name = Chariot.GO.name + " " + count;
			gameObject.transform.SetParent(Chariot_Parent.transform);
			gameObject.SetActive(false);
			tempArmyInspector = Chariot;
			break;
		case Army.Camel:
			gameObject = UnityEngine.Object.Instantiate(Camel.GO);
			gameObject.name = Camel.GO.name + " " + count;
			gameObject.transform.SetParent(Camel_Parent.transform);
			gameObject.SetActive(false);
			tempArmyInspector = Camel;
			break;
		case Army.Pirate:
			gameObject = UnityEngine.Object.Instantiate(Pirate.GO);
			gameObject.name = Pirate.GO.name + " " + count;
			gameObject.transform.SetParent(Pirate_Parent.transform);
			gameObject.SetActive(false);
			tempArmyInspector = Pirate;
			break;
		case Army.Hwacha:
			gameObject = UnityEngine.Object.Instantiate(Hwacha.GO);
			gameObject.name = Hwacha.GO.name + " " + count;
			gameObject.transform.SetParent(Hwacha_Parent.transform);
			gameObject.SetActive(false);
			tempArmyInspector = Hwacha;
			break;
		case Army.Mage:
			gameObject = UnityEngine.Object.Instantiate(Mage.GO);
			gameObject.name = Mage.GO.name + " " + count;
			gameObject.transform.SetParent(Mage_Parent.transform);
			gameObject.SetActive(false);
			tempArmyInspector = Mage;
			break;
		case Army.BearRider:
			gameObject = UnityEngine.Object.Instantiate(BearRider.GO);
			gameObject.name = BearRider.GO.name + " " + count;
			gameObject.transform.SetParent(BearRider_Parent.transform);
			gameObject.SetActive(false);
			tempArmyInspector = BearRider;
			break;
		case Army.OrganGun:
			gameObject = UnityEngine.Object.Instantiate(OrganGun.GO);
			gameObject.name = OrganGun.GO.name + " " + count;
			gameObject.transform.SetParent(OrganGun_Parent.transform);
			gameObject.SetActive(false);
			tempArmyInspector = OrganGun;
			break;
		case Army.TrebuchetMan:
			gameObject = UnityEngine.Object.Instantiate(TrebuchetMan.GO);
			gameObject.name = TrebuchetMan.GO.name + " " + count;
			gameObject.transform.SetParent(TrebuchetMan_Parent.transform);
			gameObject.SetActive(false);
			tempArmyInspector = TrebuchetMan;
			break;
		case Army.Berseker:
			gameObject = UnityEngine.Object.Instantiate(Berseker.GO);
			gameObject.name = Berseker.GO.name + " " + count;
			gameObject.transform.SetParent(Berseker_Parent.transform);
			gameObject.SetActive(false);
			tempArmyInspector = Berseker;
			break;
		case Army.Spartan:
			gameObject = UnityEngine.Object.Instantiate(Spartan.GO);
			gameObject.name = Spartan.GO.name + " " + count;
			gameObject.transform.SetParent(Spartan_Parent.transform);
			gameObject.SetActive(false);
			tempArmyInspector = Spartan;
			break;
		case Army.Sentinel:
			gameObject = UnityEngine.Object.Instantiate(Sentinel.GO);
			gameObject.name = Sentinel.GO.name + " " + count;
			gameObject.transform.SetParent(Sentinel_Parent.transform);
			gameObject.SetActive(false);
			tempArmyInspector = Sentinel;
			break;
		case Army.SamuraiNEW:
			gameObject = UnityEngine.Object.Instantiate(Samurai.GO);
			gameObject.name = Samurai.GO.name + " " + count;
			gameObject.transform.SetParent(Samurai_Parent.transform);
			gameObject.SetActive(false);
			tempArmyInspector = Samurai;
			break;
		case Army.FireDragonNEW:
			gameObject = UnityEngine.Object.Instantiate(FireDragon.GO);
			gameObject.name = FireDragon.GO.name + " " + count;
			gameObject.transform.SetParent(FireDragon_Parent.transform);
			gameObject.SetActive(false);
			tempArmyInspector = FireDragon;
			break;
		case Army.Minigun:
			gameObject = UnityEngine.Object.Instantiate(Minigun.GO);
			gameObject.name = Minigun.GO.name + " " + count;
			gameObject.transform.SetParent(Minigun_Parent.transform);
			gameObject.SetActive(false);
			tempArmyInspector = Minigun;
			break;
		case Army.Rhino:
			gameObject = UnityEngine.Object.Instantiate(Rhino.GO);
			gameObject.name = Rhino.GO.name + " " + count;
			gameObject.transform.SetParent(Rhino_Parent.transform);
			gameObject.SetActive(false);
			tempArmyInspector = Rhino;
			break;
		case Army.Guardian:
			gameObject = UnityEngine.Object.Instantiate(Guardian.GO);
			gameObject.name = Guardian.GO.name + " " + count;
			gameObject.transform.SetParent(Guardian_Parent.transform);
			gameObject.SetActive(false);
			tempArmyInspector = Guardian;
			break;
		case Army.Wolf:
			gameObject = UnityEngine.Object.Instantiate(Wolf.GO);
			gameObject.name = Wolf.GO.name + " " + count;
			gameObject.transform.SetParent(Wolf_Parent.transform);
			gameObject.SetActive(false);
			tempArmyInspector = Wolf;
			break;
		default:
			gameObject = new GameObject();
			tempArmyInspector = null;
			break;
		}
		BotInfo botInfo = new BotInfo(gameObject, tempArmyInspector, SetBotSendID());
		Values.Add(botInfo);
		return botInfo;
	}

	private void AddArmyUsedValues(BotInfo tempBotInfo)
	{
		ArmyUsed item = default(ArmyUsed);
		item.army = tempBotInfo.armyInspector.army;
		item.position = tempBotInfo.referencesManager.thisTR.position;
		item.rotation = tempBotInfo.referencesManager.thisTR.rotation;
		ArmyUsedValues.Add(item);
	}

	public void SpawnArmyUsedValues()
	{
		int count = ArmyUsedValues.Count;
		for (int i = 0; i < count; i++)
		{
			Army army = ArmyUsedValues[i].army;
			Vector3 position = ArmyUsedValues[i].position;
			Quaternion rotation = ArmyUsedValues[i].rotation;
			BotInfo botInfo = ReturnBotInfo(army);
			botInfo.referencesManager.thisTR.position = position;
			botInfo.referencesManager.thisTR.rotation = rotation;
			int num = botInfo.armyInspector.LoadStatsLvL();
			bool flag = ((botInfo.referencesManager.thisTR.position.x < 0f) ? true : false);
			botInfo.SetUpStatsLvL(num);
			botInfo.SetUp(flag);
			botInfo.botInfoUpgrades.SetSpriteLvL(ReturnSpriteLvL(num), flag);
			botInfo.gameObject.SetActive(true);
			if (gameMode == GameMode.Levels || gameMode == GameMode.EpicLevels)
			{
				availableGold -= botInfo.armyInspector.price;
				availableArmy--;
			}
			if (flag)
			{
				PlayerBotValues.Add(botInfo);
			}
			else
			{
				EnemyBotValues.Add(botInfo);
			}
		}
		ArmyUsedValues.Clear();
	}

	public void ResetArmyUsedValues()
	{
		ArmyUsedValues.Clear();
	}

	public Sprite ReturnSpriteLvL(int tempLvL)
	{
		return SpritesLvL[tempLvL];
	}

	public void SetPlaceAction(PlaceAction tempAction)
	{
		action = tempAction;
	}

	public PlaceAction ReturnPlaceAction()
	{
		return action;
	}

	private bool PlaceArmy(Vector3 pos)
	{
		if (isMainScene)
		{
			topPanel_Manager.TopPanelClose();
			infoPanel_Manager.InfoPanelClose();
		}
		Ray ray = Camera.main.ScreenPointToRay(pos);
		RaycastHit hitInfo;
		if (Physics.Raycast(ray, out hitInfo, 500f, botMask))
		{
			return false;
		}
		BotInfo botInfo = null;
		bool flag = false;
		float num = 0f;
		float zPos = 0f;
		Ray ray2 = Camera.main.ScreenPointToRay(pos);
		RaycastHit hitInfo2;
		if (Physics.Raycast(ray2, out hitInfo2, 500f, groundArmyMask))
		{
			num = hitInfo2.point.x;
			zPos = hitInfo2.point.z;
			botInfo = CanPlaceArmy(armyValue, num, zPos);
			flag = CanAddArmy(num);
		}
		if (botInfo != null && flag)
		{
			bool setPercent = true;
			int num2 = priceValue;
			if (gameMode == GameMode.Custom)
			{
				ui_Manager.SetArmiesGoldCustom(num2, PlaceAction.Place, num);
				setPercent = false;
				num2 = 0;
			}
			SetAvailableGold(-num2);
			SetAvailableArmy(-1);
			SetCountersValues(1, setPercent, armyValue);
			int num3 = ((gameMode != GameMode.Tutorial) ? botInfo.armyInspector.LoadStatsLvL() : 2);
			bool flag2 = ((botInfo.referencesManager.thisTR.position.x < 0f) ? true : false);
			botInfo.SetUpStatsLvL(num3);
			botInfo.SetUp(flag2);
			botInfo.botInfoUpgrades.SetSpriteLvL(ReturnSpriteLvL(num3), flag2);
			botInfo.gameObject.SetActive(true);
			if (flag2)
			{
				PlayerBotValues.Add(botInfo);
			}
			else
			{
				EnemyBotValues.Add(botInfo);
			}
			if (gameMode == GameMode.Multiplayer)
			{
				multiplayerController.Send_AddArmy((int)botInfo.armyInspector.army, botInfo.sendID, num3, num, zPos);
			}
			if (isMainScene)
			{
				ui_Manager.SetAvailableTexts();
				ui_Manager.SetAvailableGoldTextsMultiplayer();
				ui_Manager.SetArmiesTextsMultiplayer();
			}
			audio_Manager.AddTroop();
			return true;
		}
		return false;
	}

	private BotInfo CanPlaceArmy(Army tempArmy, float xPos, float zPos)
	{
		BotInfo botInfo = ReturnBotInfo(tempArmy);
		Vector3 position = new Vector3(xPos, 0f, zPos);
		Quaternion rotation = ((!(xPos < 0f)) ? Quaternion.Euler(0f, 270f, 0f) : Quaternion.Euler(0f, 90f, 0f));
		botInfo.referencesManager.thisTR.position = position;
		botInfo.referencesManager.thisTR.rotation = rotation;
		Vector3 position2 = botInfo.boxCenter.position;
		Vector3 boxSize = botInfo.boxSize;
		Collider[] array = Physics.OverlapBox(position2, boxSize, Quaternion.identity, blockMask);
		int num = array.Length;
		if (num > 0)
		{
			return null;
		}
		return botInfo;
	}

	public bool CanAddArmy(float xPos)
	{
		int num = priceValue;
		int num2 = availableGold - num;
		int num3 = availableArmy;
		bool flag = CanAddMoreTroops();
		if (gameMode == GameMode.Custom)
		{
			num2 = 0;
			flag = true;
			num3 = ((!(xPos < 0f)) ? (customArmyNumber - CountActiveArmyEnemy()) : (customArmyNumber - CountActiveArmyPlayer()));
		}
		else if (gameMode == GameMode.Multiplayer)
		{
			num2 = tempUserAvailableGold - num;
			num3 = ((!isPlayer) ? (multiArmyNumber - CountActiveArmyEnemy()) : (multiArmyNumber - CountActiveArmyPlayer()));
		}
		else if (gameMode == GameMode.Tutorial)
		{
			num2 = 0;
			flag = true;
			num3 = tutorialArmyNumber - CountActiveArmyPlayer();
		}
		if (CreateLevels)
		{
			num2 = 0;
			num3 = 1;
			flag = true;
		}
		if (num2 >= 0 && num3 > 0 && flag)
		{
			return true;
		}
		return false;
	}

	private void RemoveArmy(Vector3 pos)
	{
		bool setPercent = true;
		Ray ray = Camera.main.ScreenPointToRay(pos);
		RaycastHit hitInfo;
		if (!Physics.Raycast(ray, out hitInfo, 500f, botMask))
		{
			return;
		}
		RemoveValues removeValues = default(RemoveValues);
		float x = hitInfo.transform.position.x;
		if (x > 0f && gameMode == GameMode.Levels)
		{
			audio_Manager.FailAddTroop();
			return;
		}
		if (gameMode == GameMode.Multiplayer)
		{
			if (isPlayer && x > 0f)
			{
				audio_Manager.FailAddTroop();
				return;
			}
			if (!isPlayer && x <= 0f)
			{
				audio_Manager.FailAddTroop();
				return;
			}
			if (isPlayerReady)
			{
				audio_Manager.FailAddTroop();
				return;
			}
		}
		if (x < 0f)
		{
			removeValues = RemovePlayerArmyList(hitInfo.transform.name);
		}
		else if (x > 0f)
		{
			removeValues = RemoveEnemyArmyList(hitInfo.transform.name);
		}
		if (gameMode == GameMode.Custom)
		{
			ui_Manager.SetArmiesGoldCustom(removeValues.price, PlaceAction.Remove, x);
			setPercent = false;
		}
		SetAvailableGold(removeValues.price);
		SetAvailableArmy(1);
		SetCountersValues(-1, setPercent, removeValues.armyValue);
		if (gameMode == GameMode.Multiplayer)
		{
			multiplayerController.Send_RemoveArmy((int)removeValues.armyValue, removeValues.botInfo.sendID);
		}
		if (isMainScene)
		{
			ui_Manager.SetAvailableTexts();
			ui_Manager.SetAvailableGoldTextsMultiplayer();
			ui_Manager.SetArmiesTextsMultiplayer();
		}
		audio_Manager.RemoveTroop();
	}

	private RemoveValues RemovePlayerArmyList(string tempName)
	{
		RemoveValues result = default(RemoveValues);
		int count = PlayerBotValues.Count;
		for (int i = 0; i < count; i++)
		{
			int index = i;
			if (PlayerBotValues[i].gameObject.name == tempName)
			{
				result.armyValue = PlayerBotValues[i].armyInspector.army;
				result.price = PlayerBotValues[i].armyInspector.price;
				result.botInfo = PlayerBotValues[i];
				PlayerBotValues[i].gameObject.SetActive(false);
				PlayerBotValues.RemoveAt(index);
				break;
			}
		}
		return result;
	}

	private RemoveValues RemoveEnemyArmyList(string tempName)
	{
		RemoveValues result = default(RemoveValues);
		int count = EnemyBotValues.Count;
		for (int i = 0; i < count; i++)
		{
			int index = i;
			if (EnemyBotValues[i].gameObject.name == tempName)
			{
				result.armyValue = EnemyBotValues[i].armyInspector.army;
				result.price = EnemyBotValues[i].armyInspector.price;
				result.botInfo = EnemyBotValues[i];
				EnemyBotValues[i].gameObject.SetActive(false);
				EnemyBotValues.RemoveAt(index);
				break;
			}
		}
		return result;
	}

	private bool GameIsCompleted()
	{
		int num = LevelsValues.Length;
		return num == PlayerPrefs_Saves.LoadLevelsCompleted();
	}

	public int CountCompletedLevels()
	{
		return PlayerPrefs_Saves.LoadLevelsCompleted();
	}

	public void UnlockTargetLevel(int targetIndex)
	{
	}

	public void SetTargetLevel()
	{
		bool flag = GameIsCompleted();
		int num = 0;
		if (flag)
		{
			num = lastPlayedLevelIndex;
		}
		else
		{
			int num2 = 0;
			int i = 0;
			int num3 = LevelsValues.Length;
			int num4 = PlayerPrefs_Saves.LoadLevelsCompleted();
			for (; i < num3; i++)
			{
				if (i == num4)
				{
					num2 = i;
					break;
				}
			}
			num = num2;
		}
		levelIndex = num;
	}

	public void NextLevel()
	{
		if (levelIndex != LevelsValues.Length - 1)
		{
			levelIndex++;
		}
	}

	public void PreviousLevel()
	{
		levelIndex--;
	}

	public void GetAvailableValues()
	{
		availableGold = LevelsValues[levelIndex].AvailableGold(bonusGold, gameMode);
		availableArmy = LevelsValues[levelIndex].AvailableArmy(bonusArmy, gameMode);
		bonusGoldValue = LevelsValues[levelIndex].BonusGoldValue();
		bonusArmyValue = LevelsValues[levelIndex].BonusArmyValue();
		maxArmy = LevelsValues[levelIndex].AvailableArmy(bonusArmy, gameMode);
		ResetLevelsArmy();
		PlaceLevelsArmy();
		ui_Manager.CheckCanOpenLockPanel();
	}

	private bool GameIsCompletedEpic()
	{
		int num = EpicLevelsValues.Length;
		return num == PlayerPrefs_Saves.LoadEpicLevelsCompleted();
	}

	public int CountCompletedEpicLevels()
	{
		return PlayerPrefs_Saves.LoadEpicLevelsCompleted();
	}

	public void UnlockTargetEpicLevel(int targetIndex)
	{
	}

	public void SetTargetEpicLevel()
	{
		bool flag = GameIsCompletedEpic();
		int num = 0;
		if (flag)
		{
			num = lastPlayedEpicLevelIndex;
		}
		else
		{
			int num2 = 0;
			int i = 0;
			int num3 = EpicLevelsValues.Length;
			int num4 = PlayerPrefs_Saves.LoadEpicLevelsCompleted();
			for (; i < num3; i++)
			{
				if (i == num4)
				{
					num2 = i;
					break;
				}
			}
			num = num2;
		}
		epicLevelIndex = num;
	}

	public void NextEpicLevel()
	{
		if (epicLevelIndex != EpicLevelsValues.Length - 1)
		{
			epicLevelIndex++;
		}
	}

	public void PreviousEpicLevel()
	{
		epicLevelIndex--;
	}

	public void GetAvailableValuesEpic()
	{
		availableGold = EpicLevelsValues[epicLevelIndex].AvailableGold(bonusGold, gameMode);
		availableArmy = EpicLevelsValues[epicLevelIndex].AvailableArmy(bonusArmy, gameMode);
		bonusGoldValue = EpicLevelsValues[epicLevelIndex].BonusGoldValue();
		bonusArmyValue = EpicLevelsValues[epicLevelIndex].BonusArmyValue();
		maxArmy = EpicLevelsValues[epicLevelIndex].AvailableArmy(bonusArmy, gameMode);
		ResetLevelsArmy();
		PlaceEpicLevelsArmy();
		ui_Manager.CheckCanOpenLockPanel();
	}

	public void SetAvailableGold(int value)
	{
		availableGold += value;
		tempUserAvailableGold += value;
	}

	public void SetAvailableArmy(int value)
	{
		availableArmy += value;
	}

	public void CalculatePlayerArmyValues()
	{
		int count = PlayerBotValues.Count;
		for (int i = 0; i < count; i++)
		{
			SetAvailableGold(-PlayerBotValues[i].armyInspector.price);
			SetAvailableArmy(-1);
		}
	}

	private void SetStateLevelDesigns(bool state)
	{
		for (int i = 0; i < LevelsDesignValues.Length; i++)
		{
			LevelsDesignValues[i].levelDesignGO.SetActive(state);
		}
	}

	public void SetLevelDesign(LevelDesign tempDesign)
	{
		SetStateLevelDesigns(false);
		for (int i = 0; i < LevelsDesignValues.Length; i++)
		{
			if (LevelsDesignValues[i].design == tempDesign)
			{
				LevelsDesignValues[i].levelDesignGO.SetActive(true);
				ui_Manager.setUpArmy_Gui.levelDesign_Img.sprite = LevelsDesignValues[i].levelDesignSR;
				RenderSettings.fogColor = LevelsDesignValues[i].FogColor;
				RenderSettings.fogDensity = LevelsDesignValues[i].FogDensity;
				RenderSettings.skybox = LevelsDesignValues[i].skyBox;
				troopMaterialManager.InitMaterials(i);
				break;
			}
		}
	}

	public void SetLevelDesignCustom()
	{
		SetLevelDesign(customDesign);
	}

	public void SetLevelDesignMultiplayer()
	{
		SetLevelDesign(multiplayerDesign);
	}

	public void SetLevelDesignCreate()
	{
		SetLevelDesign(CreateDesign);
	}

	public void ChangeLevelDesignCustom()
	{
		if (customDesign == LevelDesign.Forest)
		{
			customDesign = LevelDesign.Icy;
		}
		else if (customDesign == LevelDesign.Icy)
		{
			customDesign = LevelDesign.Island;
		}
		else if (customDesign == LevelDesign.Island)
		{
			customDesign = LevelDesign.Volcano;
		}
		else if (customDesign == LevelDesign.Volcano)
		{
			customDesign = LevelDesign.AncientGreece;
		}
		else if (customDesign == LevelDesign.AncientGreece)
		{
			customDesign = LevelDesign.Epic;
		}
		else if (customDesign == LevelDesign.Epic)
		{
			customDesign = LevelDesign.Forest;
		}
	}

	public void ChangeLevelDesignMultiplayer(LevelDesign tempDesign)
	{
		multiplayerDesign = tempDesign;
	}

	public int CountActiveArmyPlayer()
	{
		return PlayerBotValues.Count;
	}

	public int CountActiveArmyEnemy()
	{
		return EnemyBotValues.Count;
	}

	private void InitTroopsSaveStats()
	{
		if (isMainScene)
		{
			Archer.InitTroopSaveStats();
			CannonMan.InitTroopSaveStats();
			Gladiator.InitTroopSaveStats();
			Knight.InitTroopSaveStats();
			Man.InitTroopSaveStats();
			CatapultMan.InitTroopSaveStats();
			Musketeer.InitTroopSaveStats();
			AxeMan.InitTroopSaveStats();
			ShieldMan.InitTroopSaveStats();
			SpearMan.InitTroopSaveStats();
			Giant.InitTroopSaveStats();
			Guard.InitTroopSaveStats();
			Ballista.InitTroopSaveStats();
			Crossbow.InitTroopSaveStats();
			WarElephant.InitTroopSaveStats();
			Kamikaze.InitTroopSaveStats();
			Ninja.InitTroopSaveStats();
			AK47.InitTroopSaveStats();
			M16.InitTroopSaveStats();
			MageTower.InitTroopSaveStats();
			Chariot.InitTroopSaveStats();
			Camel.InitTroopSaveStats();
			Pirate.InitTroopSaveStats();
			Hwacha.InitTroopSaveStats();
			Mage.InitTroopSaveStats();
			BearRider.InitTroopSaveStats();
			OrganGun.InitTroopSaveStats();
			TrebuchetMan.InitTroopSaveStats();
			Berseker.InitTroopSaveStats();
			Spartan.InitTroopSaveStatsBoss();
			Sentinel.InitTroopSaveStatsBoss();
			Samurai.InitTroopSaveStatsBoss();
			FireDragon.InitTroopSaveStatsBoss();
			Minigun.InitTroopSaveStatsBoss();
			Rhino.InitTroopSaveStatsBoss();
			Guardian.InitTroopSaveStatsBoss();
			Wolf.InitTroopSaveStatsBoss();
		}
	}

	public void InitTogglesValues()
	{
		Archer.InitToggleValues();
		CannonMan.InitToggleValues();
		Gladiator.InitToggleValues();
		Knight.InitToggleValues();
		Man.InitToggleValues();
		CatapultMan.InitToggleValues();
		Musketeer.InitToggleValues();
		AxeMan.InitToggleValues();
		ShieldMan.InitToggleValues();
		SpearMan.InitToggleValues();
		Giant.InitToggleValues();
		Guard.InitToggleValues();
		Ballista.InitToggleValues();
		Crossbow.InitToggleValues();
		WarElephant.InitToggleValues();
		Kamikaze.InitToggleValues();
		Ninja.InitToggleValues();
		AK47.InitToggleValues();
		M16.InitToggleValues();
		MageTower.InitToggleValues();
		Chariot.InitToggleValues();
		Camel.InitToggleValues();
		Pirate.InitToggleValues();
		Hwacha.InitToggleValues();
		Mage.InitToggleValues();
		BearRider.InitToggleValues();
		OrganGun.InitToggleValues();
		TrebuchetMan.InitToggleValues();
		Berseker.InitToggleValues();
		Spartan.InitToggleValues();
		Sentinel.InitToggleValues();
		Samurai.InitToggleValues();
		FireDragon.InitToggleValues();
		Minigun.InitToggleValues();
		Rhino.InitToggleValues();
		Guardian.InitToggleValues();
		Wolf.InitToggleValues();
	}

	public void InitUpgradePanelsValues()
	{
		if (isMainScene)
		{
			Archer.InitUpgradePanelValues();
			CannonMan.InitUpgradePanelValues();
			Gladiator.InitUpgradePanelValues();
			Knight.InitUpgradePanelValues();
			Man.InitUpgradePanelValues();
			CatapultMan.InitUpgradePanelValues();
			Musketeer.InitUpgradePanelValues();
			AxeMan.InitUpgradePanelValues();
			ShieldMan.InitUpgradePanelValues();
			SpearMan.InitUpgradePanelValues();
			Giant.InitUpgradePanelValues();
			Guard.InitUpgradePanelValues();
			Ballista.InitUpgradePanelValues();
			Crossbow.InitUpgradePanelValues();
			WarElephant.InitUpgradePanelValues();
			Kamikaze.InitUpgradePanelValues();
			Ninja.InitUpgradePanelValues();
			AK47.InitUpgradePanelValues();
			M16.InitUpgradePanelValues();
			MageTower.InitUpgradePanelValues();
			Chariot.InitUpgradePanelValues();
			Camel.InitUpgradePanelValues();
			Pirate.InitUpgradePanelValues();
			Hwacha.InitUpgradePanelValues();
			Mage.InitUpgradePanelValues();
			BearRider.InitUpgradePanelValues();
			OrganGun.InitUpgradePanelValues();
			TrebuchetMan.InitUpgradePanelValues();
			Berseker.InitUpgradePanelValues();
		}
	}

	public void SetCountersValues(int tempValue, bool setPercent, Army targetArmyValue)
	{
	}

	public void ResetCountersValues()
	{
	}

	public bool CanAddMoreTroops()
	{
		return true;
	}

	public void SetMenuGOs(bool state)
	{
		MenuGOs.SetActive(state);
	}

	public void SetMenuTroops(bool state)
	{
		MenuTroops.SetActive(state);
	}

	public void SetShopGOs(bool state)
	{
		ShopGOs.SetActive(state);
	}

	public void ResetMultiplayer()
	{
		tempUserAvailableGold = userAvailableGold;
		tempEnemyAvailableGold = enemyAvailableGold;
		ui_Manager.SetAvailableGoldTextsMultiplayer();
		ui_Manager.SetArmiesTextsMultiplayer();
	}

	public void ResetPlayerMultiplayer()
	{
		tempUserAvailableGold = userAvailableGold;
		ui_Manager.SetAvailableGoldTextsMultiplayer();
		ui_Manager.SetArmiesTextsMultiplayer();
	}

	public void ResetEnemyMultiplayer()
	{
		tempEnemyAvailableGold = enemyAvailableGold;
		ui_Manager.SetAvailableGoldTextsMultiplayer();
		ui_Manager.SetArmiesTextsMultiplayer();
	}

	public void AddEnemyValuesMultiPlayer(int army, int id, int level, float xPos, float zPos)
	{
		BotInfo botInfo = ReturnBotInfo((Army)army);
		Vector3 position = new Vector3(xPos, 0f, zPos);
		Quaternion rotation = ((!(xPos < 0f)) ? Quaternion.Euler(0f, 270f, 0f) : Quaternion.Euler(0f, 90f, 0f));
		bool flag = !isPlayer;
		botInfo.SetUpStatsLvL(level);
		botInfo.SetUp(flag);
		botInfo.botInfoUpgrades.SetSpriteLvL(ReturnSpriteLvL(level), flag);
		botInfo.receiveID = id;
		botInfo.referencesManager.thisTR.position = position;
		botInfo.referencesManager.thisTR.rotation = rotation;
		botInfo.gameObject.SetActive(true);
		if (flag)
		{
			PlayerBotValues.Add(botInfo);
		}
		else
		{
			EnemyBotValues.Add(botInfo);
		}
		tempEnemyAvailableGold -= botInfo.armyInspector.price;
		ui_Manager.SetAvailableGoldTextsMultiplayer();
		ui_Manager.SetArmiesTextsMultiplayer();
		audio_Manager.AddTroop();
	}

	public void RemoveEnemyValuesMultiPlayer(int id)
	{
		if (!isPlayer)
		{
			int count = PlayerBotValues.Count;
			for (int i = 0; i < count; i++)
			{
				int index = i;
				if (PlayerBotValues[i].receiveID == id)
				{
					tempEnemyAvailableGold += PlayerBotValues[i].armyInspector.price;
					PlayerBotValues[i].receiveID = -1;
					PlayerBotValues[i].gameObject.SetActive(false);
					PlayerBotValues.RemoveAt(index);
					break;
				}
			}
		}
		else
		{
			int count2 = EnemyBotValues.Count;
			for (int j = 0; j < count2; j++)
			{
				int index2 = j;
				if (EnemyBotValues[j].receiveID == id)
				{
					tempEnemyAvailableGold += EnemyBotValues[j].armyInspector.price;
					EnemyBotValues[j].receiveID = -1;
					EnemyBotValues[j].gameObject.SetActive(false);
					EnemyBotValues.RemoveAt(index2);
					break;
				}
			}
		}
		ui_Manager.SetAvailableGoldTextsMultiplayer();
		ui_Manager.SetArmiesTextsMultiplayer();
	}

	public void CreateLevel()
	{
		GameObject gameObject = new GameObject("Enemy Army");
		gameObject.AddComponent<LevelSetUp>();
		int count = LevelArmyValues.Count;
		for (int i = 0; i < count; i++)
		{
			LevelsArmyValues item = LevelArmyValues[i];
			gameObject.GetComponent<LevelSetUp>().ArmySetUp.Add(item);
		}
	}

	private PlaceAction GetPlaceAction(Vector3 pos)
	{
		Ray ray = Camera.main.ScreenPointToRay(pos);
		RaycastHit hitInfo;
		if (Physics.Raycast(ray, out hitInfo, 500f, botMask))
		{
			Collider[] array = Physics.OverlapBox(hitInfo.point, ReturnBotInfo(armyValue).boxSize, Quaternion.identity, botMask);
			if (array.Length == 0)
			{
				return PlaceAction.Place;
			}
			return PlaceAction.Remove;
		}
		return PlaceAction.Place;
	}

	private void PlaceLevelArmy(Vector3 pos)
	{
		Ray ray = Camera.main.ScreenPointToRay(pos);
		RaycastHit hitInfo;
		if (Physics.Raycast(ray, out hitInfo, 500f, groundArmyMask))
		{
			float x = hitInfo.point.x;
			float z = hitInfo.point.z;
			CanPlaceLevelArmy(armyValue, x, z, (int)StatsLvLSlider.value);
		}
	}

	private void CanPlaceLevelArmy(Army tempArmy, float xPos, float zPos, int tempStatsLvL)
	{
		if (!(xPos < 0f))
		{
			int num = (int)Mathf.Round(xPos);
			int num2 = (int)Mathf.Round(zPos);
			BotInfo botInfo = ReturnBotInfo(tempArmy);
			Vector3 position = new Vector3(num, 0f, num2);
			Quaternion rotation = Quaternion.Euler(0f, 270f, 0f);
			botInfo.referencesManager.thisTR.position = position;
			botInfo.referencesManager.thisTR.rotation = rotation;
			Vector3 position2 = botInfo.boxCenter.position;
			Vector3 boxSize = botInfo.boxSize;
			Collider[] array = Physics.OverlapBox(position2, boxSize, Quaternion.identity, blockMask);
			int num3 = array.Length;
			if (num3 <= 0)
			{
				botInfo.SetUpStatsLvL(tempStatsLvL);
				botInfo.SetUp(false);
				botInfo.botInfoUpgrades.SetSpriteLvL(ReturnSpriteLvL(tempStatsLvL), false);
				botInfo.gameObject.SetActive(true);
				LevelsArmyValues levelsArmyValues = new LevelsArmyValues();
				levelsArmyValues.armyGO = botInfo.gameObject;
				levelsArmyValues.armyValue = tempArmy;
				levelsArmyValues.position = position;
				levelsArmyValues.statsLvL = tempStatsLvL;
				LevelArmyValues.Add(levelsArmyValues);
				audio_Manager.AddTroop();
			}
		}
	}

	private void RemoveLevelArmy(Vector3 pos)
	{
		Ray ray = Camera.main.ScreenPointToRay(pos);
		RaycastHit hitInfo;
		if (Physics.Raycast(ray, out hitInfo, 500f, blockMask))
		{
			RemoveLevelArmyList(hitInfo.transform.name);
			audio_Manager.RemoveTroop();
		}
	}

	private void RemoveLevelArmyList(string tempName)
	{
		int count = LevelArmyValues.Count;
		for (int i = 0; i < count; i++)
		{
			int index = i;
			if (LevelArmyValues[i].armyGO.name == tempName)
			{
				LevelArmyValues[i].armyGO.SetActive(false);
				LevelArmyValues.RemoveAt(index);
				break;
			}
		}
	}

	public void ProfileArmy()
	{
		ResetLevelsArmy();
		ResetPlayerArmy();
		ResetEnemyArmy();
		float num = 30f;
		float num2 = 3f;
		float num3 = num;
		float num4 = 5f;
		float num5 = 5f;
		for (int i = 0; i < armyCount; i++)
		{
			BotInfo botInfo = ReturnBotInfo(SetRandomArmy());
			botInfo.SetUpStatsLvL(profileStatsLvL);
			botInfo.SetUp(true);
			botInfo.referencesManager.thisTR.position = new Vector3(0f - num2, 0f, num3);
			botInfo.referencesManager.thisTR.rotation = Quaternion.Euler(0f, 90f, 0f);
			botInfo.gameObject.SetActive(true);
			PlayerBotValues.Add(botInfo);
			BotInfo botInfo2 = ReturnBotInfo(SetRandomArmy());
			botInfo2.SetUpStatsLvL(profileStatsLvL);
			botInfo2.SetUp(false);
			botInfo2.botInfoUpgrades.SetSpriteLvL(ReturnSpriteLvL(profileStatsLvL), false);
			botInfo2.referencesManager.thisTR.position = new Vector3(num2, 0f, num3);
			botInfo2.referencesManager.thisTR.rotation = Quaternion.Euler(0f, 270f, 0f);
			botInfo2.gameObject.SetActive(true);
			EnemyBotValues.Add(botInfo2);
			num3 -= num5;
			if (num3 < 0f - num)
			{
				num2 += num4;
				num3 = num;
			}
		}
		ResetBotList();
		SpawnPlayerArmy();
		SpawnEnemyArmy();
		FinalizeBotList();
		gc.Begin();
	}

	private Army SetRandomArmy()
	{
		int num = UnityEngine.Random.Range(0, Troops.Length);
		return Troops[num];
	}

	public void SpawnBalance()
	{
		ResetLevelsArmy();
		ResetPlayerArmy();
		ResetEnemyArmy();
		SpawnPlayerArmyBalance();
		SpawnEnemyArmyBalance();
		ResetBotList();
		SpawnPlayerArmy();
		SpawnEnemyArmy();
		FinalizeBotList();
		gc.Begin();
	}

	private void SpawnPlayerArmyBalance()
	{
		zPosBalance = zTopBalance;
		for (int i = 0; i < PlayerBalance.Length; i++)
		{
			BotInfo botInfo = ReturnBotInfo(PlayerBalance[i].army);
			botInfo.SetUpStatsLvL(PlayerBalance[i].statsLvL);
			botInfo.SetUp(true);
			botInfo.botInfoUpgrades.SetSpriteLvL(ReturnSpriteLvL(PlayerBalance[i].statsLvL), true);
			botInfo.referencesManager.thisTR.position = new Vector3(0f - xPosBalance, 0f, zPosBalance);
			botInfo.referencesManager.thisTR.rotation = Quaternion.Euler(0f, 90f, 0f);
			botInfo.gameObject.SetActive(true);
			PlayerBotValues.Add(botInfo);
			zPosBalance -= zOffsetBalance;
			if (zPosBalance < 0f - zTopBalance)
			{
				xPosBalance += xOffsetBalance;
				zPosBalance = zTopBalance;
			}
		}
	}

	private void SpawnEnemyArmyBalance()
	{
		zPosBalance = zTopBalance;
		for (int i = 0; i < EnemyBalance.Length; i++)
		{
			BotInfo botInfo = ReturnBotInfo(EnemyBalance[i].army);
			botInfo.SetUpStatsLvL(EnemyBalance[i].statsLvL);
			botInfo.SetUp(false);
			botInfo.botInfoUpgrades.SetSpriteLvL(ReturnSpriteLvL(EnemyBalance[i].statsLvL), false);
			botInfo.referencesManager.thisTR.position = new Vector3(xPosBalance, 0f, zPosBalance);
			botInfo.referencesManager.thisTR.rotation = Quaternion.Euler(0f, 270f, 0f);
			botInfo.gameObject.SetActive(true);
			EnemyBotValues.Add(botInfo);
			zPosBalance -= zOffsetBalance;
			if (zPosBalance < 0f - zTopBalance)
			{
				xPosBalance += xOffsetBalance;
				zPosBalance = 30f;
			}
		}
	}

	public void SetLevelsNames()
	{
		int num = LevelsValues.Length;
		for (int i = 0; i < num; i++)
		{
			bool isBossLevel = LevelsValues[i].isBossLevel;
			string text = "Level " + (i + 1);
			if (isBossLevel)
			{
				text += " - Boss";
			}
			LevelsValues[i].levelSetUp.transform.name = text;
			LevelsValues[i].levelSetUp.transform.SetSiblingIndex(i);
		}
	}

	public void SetEpicLevelsNames()
	{
		int num = EpicLevelsValues.Length;
		for (int i = 0; i < num; i++)
		{
			bool isBossLevel = EpicLevelsValues[i].isBossLevel;
			string text = "Epic Level " + (i + 1);
			if (isBossLevel)
			{
				text += string.Empty;
			}
			EpicLevelsValues[i].levelSetUp.transform.name = text;
			EpicLevelsValues[i].levelSetUp.transform.SetSiblingIndex(i);
		}
	}

	private void InitArmyInspectorTroopsList()
	{
		if (isMainScene)
		{
			ArmyInspectorTroops.Clear();
			ArmyInspectorTroops.Add(Archer);
			ArmyInspectorTroops.Add(CannonMan);
			ArmyInspectorTroops.Add(Gladiator);
			ArmyInspectorTroops.Add(Knight);
			ArmyInspectorTroops.Add(Man);
			ArmyInspectorTroops.Add(CatapultMan);
			ArmyInspectorTroops.Add(Musketeer);
			ArmyInspectorTroops.Add(AxeMan);
			ArmyInspectorTroops.Add(ShieldMan);
			ArmyInspectorTroops.Add(SpearMan);
			ArmyInspectorTroops.Add(Giant);
			ArmyInspectorTroops.Add(Guard);
			ArmyInspectorTroops.Add(Ballista);
			ArmyInspectorTroops.Add(Crossbow);
			ArmyInspectorTroops.Add(WarElephant);
			ArmyInspectorTroops.Add(Kamikaze);
			ArmyInspectorTroops.Add(Ninja);
			ArmyInspectorTroops.Add(AK47);
			ArmyInspectorTroops.Add(M16);
			ArmyInspectorTroops.Add(MageTower);
			ArmyInspectorTroops.Add(Chariot);
			ArmyInspectorTroops.Add(Camel);
			ArmyInspectorTroops.Add(Pirate);
			ArmyInspectorTroops.Add(Hwacha);
			ArmyInspectorTroops.Add(Mage);
			ArmyInspectorTroops.Add(BearRider);
			ArmyInspectorTroops.Add(OrganGun);
			ArmyInspectorTroops.Add(TrebuchetMan);
			ArmyInspectorTroops.Add(Berseker);
			ArmyInspectorTroops.Sort((ArmyInspector x, ArmyInspector y) => x.price.CompareTo(y.price));
		}
	}

	public void InitUpgradeStatsValues()
	{
		isMainScene = true;
		ui_Manager.upgradeStatsValues_Gui.InitializeButtonsValues();
		ui_Manager.upgradeStatsValues_Gui.CloseDefault_Items();
		InitArmyInspectorTroopsList();
	}

	public void DestroyUpgradeStatsValuesPanels()
	{
		int count = ArmyInspectorTroops.Count;
		for (int i = 0; i < count; i++)
		{
			if (ArmyInspectorTroops[i].upgrade_Panel != null)
			{
				UnityEngine.Object.DestroyImmediate(ArmyInspectorTroops[i].upgrade_Panel);
			}
		}
	}

	public void SetUpgradeStatsValues()
	{
		int count = ArmyInspectorTroops.Count;
		Vector2 size = ui_Manager.upgradeStatsValues_Gui.scrollView.rect.size;
		int num = 3;
		float num2 = 2f * Mathf.Abs(ui_Manager.upgradeStatsValues_Gui.panelY);
		int num3 = count;
		num3 /= num;
		if (count % num == 0)
		{
			num3--;
		}
		for (int i = 0; i < num3; i++)
		{
			num2 += ui_Manager.upgradeStatsValues_Gui.DiffY;
		}
		ui_Manager.upgradeStatsValues_Gui.scrollView.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
		ui_Manager.upgradeStatsValues_Gui.scrollView.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, num2);
		int num4 = 0;
		float x = 0f;
		float num5 = ui_Manager.upgradeStatsValues_Gui.panelY;
		for (int j = 0; j < count; j++)
		{
			if (j % num == 0)
			{
				x = ui_Manager.upgradeStatsValues_Gui.panel1PosX;
			}
			if (j % num == 1)
			{
				x = ui_Manager.upgradeStatsValues_Gui.panel2PosX;
			}
			if (j % num == 2)
			{
				x = ui_Manager.upgradeStatsValues_Gui.panel3PosX;
			}
			if (num4 == num)
			{
				num5 -= ui_Manager.upgradeStatsValues_Gui.DiffY;
				num4 = 0;
			}
			ArmyInspector armyInspector = ArmyInspectorTroops[j];
			GameObject gameObject = UnityEngine.Object.Instantiate(ui_Manager.upgradeStatsValues_Gui.panel1_Row1.gameObject);
			gameObject.transform.SetParent(ui_Manager.upgradeStatsValues_Gui.scrollView.transform, true);
			gameObject.name = "Panel " + armyInspector.army;
			gameObject.SetActive(true);
			armyInspector.upgrade_Panel = gameObject;
			armyInspector.ui_Manager = ui_Manager;
			RectTransform component = gameObject.GetComponent<RectTransform>();
			component.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, ui_Manager.upgradeStatsValues_Gui.Size.x);
			component.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, ui_Manager.upgradeStatsValues_Gui.Size.y);
			component.localPosition = new Vector3(x, num5, 0f);
			num4++;
		}
	}

	public void RefreshBosses()
	{
		int num = playerPrefs_Players.Players_SetUp.Length;
		for (int i = 0; i < num; i++)
		{
			string columnName = playerPrefs_Players.Players_SetUp[i].columnName;
			if (PlayerPrefs.GetString(columnName).Equals("unlocked"))
			{
				playerPrefs_Players.Players_SetUp[i].status = "unlocked";
				playerPrefs_Players.Players_SetUp[i].bossToggle.interactable = true;
				playerPrefs_Players.Players_SetUp[i].lock_Panel.SetActive(false);
				playerPrefs_Players.Players_SetUp[i].bossInAppShop.gameObject.SetActive(false);
			}
		}
	}

	public void RefreshUpgrades()
	{
		Archer.SetUpgradeStatsTexts();
		CannonMan.SetUpgradeStatsTexts();
		Gladiator.SetUpgradeStatsTexts();
		Knight.SetUpgradeStatsTexts();
		Man.SetUpgradeStatsTexts();
		CatapultMan.SetUpgradeStatsTexts();
		Musketeer.SetUpgradeStatsTexts();
		AxeMan.SetUpgradeStatsTexts();
		ShieldMan.SetUpgradeStatsTexts();
		SpearMan.SetUpgradeStatsTexts();
		Giant.SetUpgradeStatsTexts();
		Guard.SetUpgradeStatsTexts();
		Ballista.SetUpgradeStatsTexts();
		Crossbow.SetUpgradeStatsTexts();
		WarElephant.SetUpgradeStatsTexts();
		Kamikaze.SetUpgradeStatsTexts();
		Ninja.SetUpgradeStatsTexts();
		AK47.SetUpgradeStatsTexts();
		M16.SetUpgradeStatsTexts();
		MageTower.SetUpgradeStatsTexts();
		Chariot.SetUpgradeStatsTexts();
		Camel.SetUpgradeStatsTexts();
		Pirate.SetUpgradeStatsTexts();
		Hwacha.SetUpgradeStatsTexts();
		Mage.SetUpgradeStatsTexts();
		BearRider.SetUpgradeStatsTexts();
		OrganGun.SetUpgradeStatsTexts();
		TrebuchetMan.SetUpgradeStatsTexts();
		Berseker.SetUpgradeStatsTexts();
	}

	public void RefreshLevelsCompleted(int tempTargetIndex)
	{
		PlayerPrefs_Saves.SaveLevelsCompleted(tempTargetIndex);
		SetTargetLevel();
		GetAvailableValues();
	}

	public void RefreshEpicLevelsCompleted(int tempTargetIndex)
	{
		PlayerPrefs_Saves.SaveEpicLevelsCompleted(tempTargetIndex);
		SetTargetEpicLevel();
		GetAvailableValuesEpic();
	}

	public void CloudSaveArmy(string columnName, int level)
	{
		if (PlayerPrefs.GetInt(columnName) < level)
		{
			PlayerPrefs.SetInt(columnName, level);
			PlayerPrefs.Save();
		}
	}

	public void CloudSaveUnlockEpic(string name)
	{
		PlayerPrefs_Players.UnLockPlayer(name);
		PlayerPrefs.Save();
	}
}
