using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using UnityEngine;

public class MultiplayerController : MonoBehaviour
{
	private Ui_Manager uimanager;

	private Level_Manager levelManager;

	private void Awake()
	{
		uimanager = GameObject.FindGameObjectWithTag("Ui_Manager").GetComponent<Ui_Manager>();
		levelManager = GameObject.FindGameObjectWithTag("Level_Manager").GetComponent<Level_Manager>();
	}

	public void Receive_AddEnemie(int enemyID, string enemyName, string playerName, int enemyElo, int playerElo, bool isPlayer, int environment)
	{
		NetworkUtils.localPlayerName = playerName;
		uimanager.OpenMultiplayerSetupArmy(playerName + " (" + playerElo + ")", enemyName + " (" + enemyElo + ")", isPlayer, environment);
	}

	public void Receive_AddArmy(int armyID, int id, int level, float xPos, float zPos)
	{
		if (!uimanager.multiplayerStarted)
		{
			levelManager.AddEnemyValuesMultiPlayer(armyID, id, level, xPos, zPos);
		}
	}

	public void Receive_RemoveArmy(int id)
	{
		if (!uimanager.multiplayerStarted)
		{
			levelManager.RemoveEnemyValuesMultiPlayer(id);
		}
	}

	public void Receive_Time(int time)
	{
		uimanager.MultiplayerSetTime(time);
	}

	public void Receive_Ready()
	{
		uimanager.MultiplayerEnemyReady();
	}

	public void Receive_ClearArmy()
	{
		if (!uimanager.multiplayerStarted)
		{
			uimanager.MultiplayerEnemyClearAmy();
		}
	}

	public void Receive_EloResult(int trophies)
	{
		uimanager.OpenTrophies(trophies);
	}

	public void Send_UserJoined(string name)
	{
		if (NetworkUtils.IsConnected())
		{
			ISFSObject iSFSObject = new SFSObject();
			iSFSObject.PutUtfString("NAME", name);
			NetworkUtils.connection.Send(new ExtensionRequest("CLIENT.JOINED", iSFSObject, NetworkUtils.connectedRoom));
		}
	}

	public void Send_AddArmy(int armyID, int id, int level, float xPos, float zPos)
	{
		if (NetworkUtils.IsConnected())
		{
			ISFSObject iSFSObject = new SFSObject();
			iSFSObject.PutInt("ARMY_ID", armyID);
			iSFSObject.PutInt("ID", id);
			iSFSObject.PutInt("LEVEL", level);
			iSFSObject.PutFloat("X_POS", xPos);
			iSFSObject.PutFloat("Z_POS", zPos);
			NetworkUtils.connection.Send(new ExtensionRequest("CLIENT.ADD_ARMY", iSFSObject, NetworkUtils.connectedRoom));
		}
	}

	public void Send_RemoveArmy(int armyID, int id)
	{
		if (NetworkUtils.IsConnected())
		{
			ISFSObject iSFSObject = new SFSObject();
			iSFSObject.PutInt("ARMY_ID", armyID);
			iSFSObject.PutInt("ID", id);
			NetworkUtils.connection.Send(new ExtensionRequest("CLIENT.REMOVE_ARMY", iSFSObject, NetworkUtils.connectedRoom));
		}
	}

	public void Send_Ready()
	{
		if (NetworkUtils.IsConnected())
		{
			ISFSObject parameters = new SFSObject();
			NetworkUtils.connection.Send(new ExtensionRequest("CLIENT.READY", parameters, NetworkUtils.connectedRoom));
		}
	}

	public void Send_ClearArmy()
	{
		if (NetworkUtils.IsConnected())
		{
			ISFSObject parameters = new SFSObject();
			NetworkUtils.connection.Send(new ExtensionRequest("CLIENT.CLEAR_ARMY", parameters, NetworkUtils.connectedRoom));
		}
	}

	public void Send_Won()
	{
		if (NetworkUtils.IsConnected())
		{
			ISFSObject parameters = new SFSObject();
			NetworkUtils.connection.Send(new ExtensionRequest("CLIENT.WON", parameters, NetworkUtils.connectedRoom));
		}
	}

	public void Send_Lost()
	{
		if (NetworkUtils.IsConnected())
		{
			ISFSObject parameters = new SFSObject();
			NetworkUtils.connection.Send(new ExtensionRequest("CLIENT.LOST", parameters, NetworkUtils.connectedRoom));
		}
	}

	public void Send_Draw()
	{
		if (NetworkUtils.IsConnected())
		{
			ISFSObject parameters = new SFSObject();
			NetworkUtils.connection.Send(new ExtensionRequest("CLIENT.DRAW", parameters, NetworkUtils.connectedRoom));
		}
	}
}
