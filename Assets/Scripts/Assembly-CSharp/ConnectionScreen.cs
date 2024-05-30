using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using Sfs2X.Util;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionScreen : MonoBehaviour
{
	private PackagesManager packagesManager;

	[Header("MULTIPLAYER")]
	public ServerRegion region = ServerRegion.EU;

	public string version;

	[Header("REFERENCES")]
	public MessageHandler messageHandler;

	public InputField nickname_input;

	public InputField username_input;

	public InputField password_input;

	[Header("SERVER DEBUG")]
	public bool isTestServer;

	public bool isTestRoom;

	public bool isLocal;

	private string zoneName = "Totally2";

	private SmartFox sfs;

	private BaseEvent baseEvent;

	private Ui_Manager uimanager;

	private bool abort;

	private bool udpEnabled;

	private bool udpInit;

	private bool waitingForUDP;

	private void Awake()
	{
		uimanager = GameObject.FindWithTag("Ui_Manager").GetComponent<Ui_Manager>();
		if (GameObject.FindWithTag("PackagesManager") != null)
		{
			packagesManager = GameObject.FindWithTag("PackagesManager").GetComponent<PackagesManager>();
		}
	}

	private void Update()
	{
		if (sfs != null)
		{
			sfs.ProcessEvents();
		}
	}

	private void OnApplicationQuit()
	{
		Disconnect(1);
	}

	private void Init()
	{
		udpInit = false;
		waitingForUDP = false;
		NetworkUtils.localPlayerName = username_input.text;
		sfs = new SmartFox();
		NetworkUtils.connection = sfs;
		sfs.ThreadSafeMode = true;
		sfs.AddEventListener(SFSEvent.CONNECTION, OnConnection);
		sfs.AddEventListener(SFSEvent.LOGIN, OnLogin);
		sfs.AddEventListener(SFSEvent.LOGIN_ERROR, OnLoginError);
		sfs.AddEventListener(SFSEvent.ROOM_JOIN, OnJoinRoom);
		sfs.AddEventListener(SFSEvent.ROOM_JOIN_ERROR, OnJoinRoomError);
		sfs.AddEventListener(SFSEvent.EXTENSION_RESPONSE, OnExtensionResponce);
		sfs.AddEventListener(SFSEvent.CONNECTION_LOST, OnConnectionLost);
		if (udpEnabled)
		{
			sfs.AddEventListener(SFSEvent.UDP_INIT, OnUDPInit);
		}
	}

	public void Connect()
	{
		abort = false;
		Init();
		if (isLocal)
		{
			sfs.Connect(ServerInfo.SERVER_LOCAL.Host_TCP, ServerInfo.SERVER_LOCAL.Port_TCP);
		}
		else if (isTestServer)
		{
			sfs.Connect(ServerInfo.SERVER_TEST.Host_TCP, ServerInfo.SERVER_TEST.Port_TCP);
		}
		else if (region == ServerRegion.EU)
		{
			sfs.Connect(ServerInfo.SERVER_EU.Host_TCP, ServerInfo.SERVER_EU.Port_TCP);
		}
		else if (region == ServerRegion.USA)
		{
			sfs.Connect(ServerInfo.SERVER_USA.Host_TCP, ServerInfo.SERVER_USA.Port_TCP);
		}
	}

	public void Disconnect(int id)
	{
		NetworkUtils.Disconnect();
		udpInit = false;
		waitingForUDP = false;
	}

	public void Abort()
	{
		abort = true;
		Disconnect(2);
	}

	private void OnExtensionResponce(BaseEvent e)
	{
		string text = (string)e.Params["cmd"];
		SFSObject sFSObject = (SFSObject)e.Params["params"];
		if (text == "SERVER_CLIENT_VERIFIED")
		{
			int @int = sFSObject.GetInt("TROPHIES");
			PlayerPrefs_Account.SaveRanking(@int);
			PlayerPrefs.Save();
			uimanager.AccountCreated();
		}
		else if (text == "SERVER_ALLOW_CONNECTION")
		{
			switch (sFSObject.GetInt("ERROR.CODE"))
			{
			case -1:
			{
				string utfString = sFSObject.GetUtfString("ROOM");
				OnValidated(utfString);
				break;
			}
			case 0:
				DisplayServerDownPopUp();
				Disconnect(3);
				break;
			case 1:
				DisplayUpdateRequiredPopUp();
				Disconnect(4);
				break;
			case 2:
				DisplayUnknownErrorPopUp(6);
				Disconnect(5);
				break;
			case 3:
				DisplayWrongDataPopUp();
				Disconnect(6);
				break;
			case -2:
				DisplayUnknownErrorPopUp(7);
				Disconnect(7);
				break;
			default:
				DisplayUnknownErrorPopUp(8);
				Disconnect(8);
				break;
			}
		}
		else
		{
			messageHandler.HandleMessage(text, sFSObject);
		}
	}

	private void OnValidated(string roomName)
	{
		if (abort)
		{
			Disconnect(9);
			return;
		}
		if (isLocal)
		{
			sfs.InitUDP(ServerInfo.SERVER_LOCAL.Host_UDP, ServerInfo.SERVER_LOCAL.Port_UDP);
		}
		else if (isTestServer)
		{
			sfs.InitUDP(ServerInfo.SERVER_TEST.Host_UDP, ServerInfo.SERVER_TEST.Port_UDP);
		}
		else if (region == ServerRegion.EU)
		{
			sfs.InitUDP(ServerInfo.SERVER_EU.Host_UDP, ServerInfo.SERVER_EU.Port_UDP);
		}
		else if (region == ServerRegion.USA)
		{
			sfs.InitUDP(ServerInfo.SERVER_USA.Host_UDP, ServerInfo.SERVER_USA.Port_UDP);
		}
		NetworkUtils.localPlayerID = ((User)baseEvent.Params["user"]).Id;
		if (!isTestRoom)
		{
			sfs.Send(new JoinRoomRequest(roomName));
		}
		else
		{
			sfs.Send(new JoinRoomRequest("TestRoom"));
		}
	}

	private void OnConnection(BaseEvent e)
	{
		if (abort)
		{
			Disconnect(10);
			return;
		}
		if ((bool)e.Params["success"])
		{
			sfs.Send(new LoginRequest(string.Empty, string.Empty, zoneName));
			return;
		}
		DisplayUnknownErrorPopUp(1);
		Disconnect(11);
		DebugCanvas.Log("OnConnection FAIL");
	}

	private void OnLogin(BaseEvent e)
	{
		if (abort)
		{
			Disconnect(12);
			return;
		}
		string val = "google";
		if (packagesManager.whichStore == App_Stores.Amazon)
		{
			val = "amazon";
		}
		else if (packagesManager.whichStore == App_Stores.Google)
		{
			val = "google";
		}
		else if (packagesManager.whichStore == App_Stores.IOS)
		{
			val = "apple";
		}
		else if (packagesManager.whichStore == App_Stores.Steam)
		{
			val = "steam";
		}
		baseEvent = e;
		ISFSObject iSFSObject = new SFSObject();
		iSFSObject.PutUtfString("STORE", val);
		iSFSObject.PutUtfString("VERSION", version);
		iSFSObject.PutUtfString("USERNAME", NetworkUtils.localPlayerName);
		iSFSObject.PutUtfString("PASSWORD", password_input.text);
		iSFSObject.PutUtfString("NICKNAME", nickname_input.text);
		NetworkUtils.connection.Send(new ExtensionRequest("CLIENT_VALIDATE", iSFSObject));
	}

	private void OnLoginError(BaseEvent e)
	{
		Disconnect(13);
		DisplayUnknownErrorPopUp(3);
	}

	private void OnJoinRoom(BaseEvent e)
	{
		if (abort)
		{
			Disconnect(14);
			return;
		}
		baseEvent = e;
		if (udpInit || !udpEnabled)
		{
			FinalizeConnection();
		}
		else
		{
			waitingForUDP = true;
		}
	}

	private void OnJoinRoomError(BaseEvent e)
	{
		Disconnect(15);
		DisplayUnknownErrorPopUp(4);
	}

	private void OnUDPInit(BaseEvent evt)
	{
		if (abort)
		{
			Disconnect(16);
		}
		else if ((bool)evt.Params["success"])
		{
			udpInit = true;
			if (waitingForUDP)
			{
				FinalizeConnection();
			}
		}
		else
		{
			udpInit = false;
			Disconnect(17);
			DisplayUnknownErrorPopUp(5);
		}
	}

	private void FinalizeConnection()
	{
		NetworkUtils.connectedRoom = (Room)baseEvent.Params["room"];
		messageHandler.Begin();
	}

	private void OnConnectionLost(BaseEvent e)
	{
		if (NetworkUtils.connection == null)
		{
			uimanager.Disconnected(10);
			uimanager.OpenToast("Connection lost");
			return;
		}
		string text = (string)e.Params["reason"];
		if (!uimanager.multiplayerStarted)
		{
			uimanager.Disconnected(11);
			if (text == ClientDisconnectionReason.KICK)
			{
				uimanager.OpenToast("Opponent disconnected");
			}
			else
			{
				uimanager.OpenToast("Connection lost");
			}
		}
	}

	private void DisplayUpdateRequiredPopUp()
	{
		uimanager.UpdateRequiredPopUp();
		uimanager.CloseConnectScreen();
	}

	private void DisplayServerDownPopUp()
	{
		uimanager.ServerDownPopUp();
		uimanager.CloseConnectScreen();
	}

	private void DisplayUnknownErrorPopUp(int id)
	{
		uimanager.UnknownErrorPopUp();
		uimanager.CloseConnectScreen();
	}

	private void DisplayWrongDataPopUp()
	{
		uimanager.WrongDataPopUp();
		uimanager.CloseConnectScreen();
	}
}
