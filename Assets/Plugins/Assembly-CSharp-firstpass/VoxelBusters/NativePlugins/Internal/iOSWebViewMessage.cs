using System.Collections;
using System.Collections.Generic;
using VoxelBusters.Utility;

namespace VoxelBusters.NativePlugins.Internal
{
	public sealed class iOSWebViewMessage : WebViewMessage
	{
		private const string kURLKey = "url";

		private const string kHostKey = "host";

		private const string kArgumentsKey = "arguments";

		private const string kURLSchemeKey = "url-scheme";

		public iOSWebViewMessage(IDictionary _schemeDataJsonDict)
		{
			string ifAvailable = _schemeDataJsonDict.GetIfAvailable<string>("url");
			string ifAvailable2 = _schemeDataJsonDict.GetIfAvailable<string>("url-scheme");
			string ifAvailable3 = _schemeDataJsonDict.GetIfAvailable<string>("host");
			IDictionary ifAvailable4 = _schemeDataJsonDict.GetIfAvailable<IDictionary>("arguments");
			base.URL = ifAvailable;
			base.Scheme = ifAvailable2;
			base.Host = ifAvailable3;
			base.Arguments = new Dictionary<string, string>();
			if (ifAvailable4 == null)
			{
				return;
			}
			foreach (string key2 in ifAvailable4.Keys)
			{
				string key = key2.ToString();
				string value = ifAvailable4[key2].ToString();
				base.Arguments[key] = value;
			}
		}
	}
}
