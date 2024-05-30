public static class ServerInfo
{
	public class Server
	{
		public string Host_TCP;

		public int Port_TCP;

		public string Host_UDP;

		public int Port_UDP;

		public Server(string Host_TCP, int Port_TCP, string Host_UDP, int Port_UDP)
		{
			this.Host_TCP = Host_TCP;
			this.Port_TCP = Port_TCP;
			this.Host_UDP = Host_UDP;
			this.Port_UDP = Port_UDP;
		}
	}

	private const string Host_TCP_USA = "node29587-totally2.mircloud.host";

	private const int Port_TCP_USA = 11064;

	private const string Host_UDP_USA = "node29587-totally2.mircloud.host";

	private const int Port_UDP_USA = 11074;

	private const string Host_TCP_EU = "node29587-totally2.mircloud.host";

	private const int Port_TCP_EU = 11064;

	private const string Host_UDP_EU = "node29587-totally2.mircloud.host";

	private const int Port_UDP_EU = 11074;

	private const string Host_TCP_TEST = "node23882-rappidstudios-test3.mircloud.host";

	private const int Port_TCP_TEST = 11093;

	private const string Host_UDP_TEST = "node23882-rappidstudios-test3.mircloud.host";

	private const int Port_UDP_TEST = 11094;

	private const string Host_TCP_Local = "127.0.0.1";

	private const int Port_TCP_Local = 9933;

	private const string Host_UDP_Local = "127.0.0.1";

	private const int Port_UDP_Local = 9933;

	public static Server SERVER_USA = new Server("node29587-totally2.mircloud.host", 11064, "node29587-totally2.mircloud.host", 11074);

	public static Server SERVER_EU = new Server("node29587-totally2.mircloud.host", 11064, "node29587-totally2.mircloud.host", 11074);

	public static Server SERVER_LOCAL = new Server("127.0.0.1", 9933, "127.0.0.1", 9933);

	public static Server SERVER_TEST = new Server("node23882-rappidstudios-test3.mircloud.host", 11093, "node23882-rappidstudios-test3.mircloud.host", 11094);
}
