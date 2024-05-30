using Sfs2X;
using Sfs2X.Entities;

public static class NetworkUtils
{
	public static SmartFox connection;

	public static Room connectedRoom;

	public static int localPlayerID;

	public static string localPlayerName;

	public static bool IsConnected()
	{
		return connection != null && connection.IsConnected;
	}

	public static void Disconnect()
	{
		if (IsConnected())
		{
			connection.RemoveAllEventListeners();
			connection.Disconnect();
		}
		connection = null;
		connectedRoom = null;
		localPlayerID = -1;
		localPlayerName = "Player";
	}
}
