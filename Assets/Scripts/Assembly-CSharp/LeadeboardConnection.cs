using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using UnityEngine;

public class LeadeboardConnection : MonoBehaviour
{
	private PackagesManager packagesManager;

	[Header("MULTIPLAYER")]
	public ServerRegion region = ServerRegion.EU;

	public string version;

	[Header("SERVER DEBUG")]
	public bool isTestServer;

	public bool isLocal;

	private string zoneName = "Totally2_Leaderboard";

	private SmartFox sfs;

	private Ui_Manager uimanager;

	private bool abort;

	private float lastTime_SyncLeaderboard = -100f;

	private float timeBetween_SyncLeaderboard = 10f;

	private string[] top_ten_usernames;

	private int[] top_ten_trophies;

	private int[] top_ten_victories;

	private int[] top_ten_defeats;

	private long user_position;

	private string user_nickname = string.Empty;

	private int user_trophies;

	private int user_victories;

	private int user_defeats;

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
		Disconnect();
	}

	private void Init()
	{
		sfs = new SmartFox();
		NetworkUtils.connection = sfs;
		sfs.ThreadSafeMode = true;
		sfs.AddEventListener(SFSEvent.CONNECTION, OnConnection);
		sfs.AddEventListener(SFSEvent.LOGIN, OnLogin);
		sfs.AddEventListener(SFSEvent.LOGIN_ERROR, OnLoginError);
		sfs.AddEventListener(SFSEvent.EXTENSION_RESPONSE, OnExtensionResponce);
		sfs.AddEventListener(SFSEvent.CONNECTION_LOST, OnConnectionLost);
	}

	public void Connect()
	{
		if (Time.time - lastTime_SyncLeaderboard < timeBetween_SyncLeaderboard)
		{
			uimanager.SyncLeaderboard(top_ten_usernames, top_ten_trophies, top_ten_victories, top_ten_defeats, user_nickname, user_trophies, user_victories, user_defeats, user_position);
			return;
		}
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

	public void Disconnect()
	{
		NetworkUtils.Disconnect();
	}

	public void Abort()
	{
		abort = true;
		Disconnect();
	}

	private void OnExtensionResponce(BaseEvent e)
	{
		string text = (string)e.Params["cmd"];
		SFSObject sFSObject = (SFSObject)e.Params["params"];
		if (!(text == "SERVER_SHOW_TOP_TEN"))
		{
			return;
		}
		switch (sFSObject.GetInt("ERROR.CODE"))
		{
		case -1:
			lastTime_SyncLeaderboard = Time.time;
			top_ten_usernames = sFSObject.GetUtfStringArray("LEADERBOARD_NICKNAMES");
			top_ten_trophies = sFSObject.GetIntArray("LEADERBOARD_TROPHIES");
			top_ten_victories = sFSObject.GetIntArray("LEADERBOARD_VICTORIES");
			top_ten_defeats = sFSObject.GetIntArray("LEADERBOARD_DEFEATS");
			user_position = sFSObject.GetLong("LEADERBOARD_USER_POSITION");
			user_nickname = string.Empty;
			user_trophies = 0;
			user_victories = 0;
			user_defeats = 0;
			if (user_position != -1)
			{
				user_nickname = sFSObject.GetUtfString("LEADERBOARD_USER_NICKANAME");
				user_trophies = sFSObject.GetInt("LEADERBOARD_USER_TROPHIES");
				user_victories = sFSObject.GetInt("LEADERBOARD_USER_VICTORIES");
				user_defeats = sFSObject.GetInt("LEADERBOARD_USER_DEFEATS");
			}
			uimanager.SyncLeaderboard(top_ten_usernames, top_ten_trophies, top_ten_victories, top_ten_defeats, user_nickname, user_trophies, user_victories, user_defeats, user_position);
			break;
		case 0:
			DisplayServerDownPopUp();
			break;
		case 1:
			DisplayUpdateRequiredPopUp();
			break;
		case 2:
			DisplayUnknownErrorPopUp(6);
			break;
		case -2:
			DisplayUnknownErrorPopUp(7);
			break;
		default:
			DisplayUnknownErrorPopUp(8);
			break;
		}
		Disconnect();
	}

	private void OnConnection(BaseEvent e)
	{
		if (abort)
		{
			Disconnect();
			return;
		}
		if ((bool)e.Params["success"])
		{
			sfs.Send(new LoginRequest(string.Empty, string.Empty, zoneName));
			return;
		}
		DisplayUnknownErrorPopUp(11);
		Disconnect();
	}

	private void OnLogin(BaseEvent e)
	{
		if (abort)
		{
			Disconnect();
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
		string val2 = string.Empty;
		if (PlayerPrefs_Account.LoadAccountCreated())
		{
			val2 = PlayerPrefs_Account.LoadUsername();
		}
		ISFSObject iSFSObject = new SFSObject();
		iSFSObject.PutUtfString("STORE", val);
		iSFSObject.PutUtfString("VERSION", version);
		iSFSObject.PutUtfString("USERNAME", val2);
		NetworkUtils.connection.Send(new ExtensionRequest("SHOW_TOP_TEN", iSFSObject));
	}

	private void OnLoginError(BaseEvent e)
	{
		Disconnect();
		DisplayUnknownErrorPopUp(9);
	}

	private void OnConnectionLost(BaseEvent e)
	{
		Disconnect();
		DisplayUnknownErrorPopUp(10);
	}

	private void DisplayUpdateRequiredPopUp()
	{
		uimanager.UpdateRequiredPopUp();
		uimanager.CloseLeaderBoard();
	}

	private void DisplayServerDownPopUp()
	{
		uimanager.ServerDownPopUp();
		uimanager.CloseLeaderBoard();
	}

	private void DisplayUnknownErrorPopUp(int id)
	{
		uimanager.UnknownErrorPopUp();
		uimanager.CloseLeaderBoard();
	}

	private void DisplayWrongDataPopUp()
	{
		uimanager.WrongDataPopUp();
		uimanager.CloseLeaderBoard();
	}
}
