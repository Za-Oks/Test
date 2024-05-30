using System.Collections.Generic;
using VoxelBusters.Utility;

namespace VoxelBusters.NativePlugins
{
	public class WebViewMessage
	{
		public string URL { get; protected set; }

		public string Scheme { get; protected set; }

		public string Host { get; protected set; }

		public Dictionary<string, string> Arguments { get; protected set; }

		protected WebViewMessage()
		{
			Scheme = null;
			Host = null;
			Arguments = null;
		}

		public override string ToString()
		{
			return string.Format("[WebViewMessage Scheme={0}, Host={1}, Arguments={2}]", Scheme, Host, Arguments.ToJSON());
		}
	}
}
