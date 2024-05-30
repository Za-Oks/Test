using System.Collections;
using VoxelBusters.Utility;

namespace VoxelBusters.NativePlugins
{
	public class ExternalAuthenticationCredentials
	{
		public class Android
		{
			private const string kServerAuthCodeKey = "server-auth-code";

			public string ServerAuthCode { get; set; }

			public void Load(IDictionary _jsonDict)
			{
				string ifAvailable = _jsonDict.GetIfAvailable<string>("server-auth-code");
				ServerAuthCode = ifAvailable.FromBase64();
			}
		}

		public class iOS
		{
			public void Load(IDictionary _jsonDict)
			{
			}
		}

		private iOS m_iOS = new iOS();

		private Android m_android = new Android();

		public iOS iOSCredentials
		{
			get
			{
				return m_iOS;
			}
		}

		public Android AndroidCredentials
		{
			get
			{
				return m_android;
			}
		}

		public ExternalAuthenticationCredentials(IDictionary _payloadDict)
		{
			m_android.Load(_payloadDict);
		}
	}
}
