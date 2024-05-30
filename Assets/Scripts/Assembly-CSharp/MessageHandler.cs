using Sfs2X.Entities.Data;
using UnityEngine;

public class MessageHandler : MonoBehaviour
{
	public MultiplayerController multiplayerController;

	public ConnectionScreen connectionScreen;

	public void Begin()
	{
		multiplayerController.Send_UserJoined(NetworkUtils.localPlayerName);
	}

	public void HandleMessage(string e, SFSObject objIn)
	{
		switch (e)
		{
		case "SERVER_USER_JOINED":
		{
			int int7 = objIn.GetInt("PLAYER_ID");
			if (int7 != NetworkUtils.localPlayerID)
			{
				string utfString = objIn.GetUtfString("ENEMY_NAME");
				string utfString2 = objIn.GetUtfString("NAME");
				int int8 = objIn.GetInt("ENEMY_ELO");
				int int9 = objIn.GetInt("PLAYER_ELO");
				bool @bool = objIn.GetBool("IS_PLAYER");
				int int10 = objIn.GetInt("ENVIRONMENT");
				multiplayerController.Receive_AddEnemie(int7, utfString, utfString2, int8, int9, @bool, int10);
			}
			break;
		}
		case "SERVER_ADD_ARMY":
		{
			int int4 = objIn.GetInt("ARMY_ID");
			int int5 = objIn.GetInt("ID");
			int int6 = objIn.GetInt("LEVEL");
			float @float = objIn.GetFloat("X_POS");
			float float2 = objIn.GetFloat("Z_POS");
			multiplayerController.Receive_AddArmy(int4, int5, int6, @float, float2);
			break;
		}
		case "SERVER_REMOVE_ARMY":
		{
			int int3 = objIn.GetInt("ID");
			multiplayerController.Receive_RemoveArmy(int3);
			break;
		}
		case "SERVER_TIME":
		{
			int int2 = objIn.GetInt("TIME");
			multiplayerController.Receive_Time(int2);
			break;
		}
		case "SERVER_READY":
			multiplayerController.Receive_Ready();
			break;
		case "SERVER_CLEAR_ARMY":
			multiplayerController.Receive_ClearArmy();
			break;
		case "SERVER_ELO_RESULT":
		{
			int @int = objIn.GetInt("TROPHIES");
			multiplayerController.Receive_EloResult(@int);
			connectionScreen.Disconnect(19);
			break;
		}
		}
	}
}
