using System;
using UnityEngine;

[Serializable]
public class BotInfo
{
	public GameObject gameObject;

	public ArmyInspector armyInspector;

	public Transform boxCenter;

	public Vector3 boxSize;

	public bool isPlayer;

	public int statsLvL;

	public Level_Manager level_Manager;

	public AllReferencesManager referencesManager;

	public BotInfoSetUp botInfoUpgrades;

	public BotDistance closestEnemy;

	public int sendID { get; set; }

	public int receiveID { get; set; }

	public BotInfo(GameObject tempGO, ArmyInspector tempArmyInspector, int tempSendID)
	{
		gameObject = tempGO;
		armyInspector = tempArmyInspector;
		sendID = tempSendID;
		receiveID = -1;
		boxCenter = gameObject.transform.Find("BoxCenter");
		boxCenter.gameObject.SetActive(false);
		boxSize = new Vector3(boxCenter.localScale.z, boxCenter.localScale.y, boxCenter.localScale.x) / 2f;
		isPlayer = false;
		level_Manager = GameObject.FindWithTag("Level_Manager").GetComponent<Level_Manager>();
		referencesManager = gameObject.GetComponent<AllReferencesManager>();
		botInfoUpgrades = gameObject.GetComponent<BotInfoSetUp>();
		referencesManager.thisRB.constraints = (RigidbodyConstraints)84;
	}

	public void SetUpStatsLvL(int tempStatsLvL)
	{
		statsLvL = tempStatsLvL;
	}

	public void SetUp(bool tempIsPlayer)
	{
		referencesManager.thisRB.isKinematic = true;
		closestEnemy = null;
		isPlayer = tempIsPlayer;
		referencesManager.botMovement.isPlayer = tempIsPlayer;
		referencesManager.botMovement.thisBotInfo = this;
		botInfoUpgrades.ResetGOs();
		botInfoUpgrades.SetGOs(statsLvL);
		botInfoUpgrades.SetTeam(isPlayer);
		if (isPlayer)
		{
			gameObject.layer = LayerMask.NameToLayer("Bot");
		}
		else
		{
			gameObject.layer = LayerMask.NameToLayer("Bot2");
		}
	}

	public void InitStats()
	{
		referencesManager.InitializeStats(armyInspector.ReturnFinalStats(statsLvL), statsLvL);
	}
}
