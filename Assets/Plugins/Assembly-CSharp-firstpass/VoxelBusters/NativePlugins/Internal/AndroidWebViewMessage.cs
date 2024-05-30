using System.Collections;
using System.Collections.Generic;
using VoxelBusters.Utility;

namespace VoxelBusters.NativePlugins.Internal
{
	public sealed class AndroidWebViewMessage : WebViewMessage
	{
		private const string kURL = "url";

		private const string kHost = "host";

		private const string kArguments = "arguments";

		private const string kURLScheme = "url-scheme";

		public AndroidWebViewMessage(IDictionary _schemeDataJsonDict)
		{
			string ifAvailable = _schemeDataJsonDict.GetIfAvailable<string>("url");
			string ifAvailable2 = _schemeDataJsonDict.GetIfAvailable<string>("url-scheme");
			string ifAvailable3 = _schemeDataJsonDict.GetIfAvailable<string>("host");
			IDictionary dictionary = _schemeDataJsonDict["arguments"] as IDictionary;
			base.URL = ifAvailable;
			base.Scheme = ifAvailable2;
			base.Host = ifAvailable3;
			base.Arguments = new Dictionary<string, string>();
			foreach (object key2 in dictionary.Keys)
			{
				string key = key2.ToString();
				string value = dictionary[key2].ToString();
				base.Arguments[key] = value;
			}
		}
	}
}
