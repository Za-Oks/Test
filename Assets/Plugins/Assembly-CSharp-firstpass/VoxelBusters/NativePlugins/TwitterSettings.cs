using System;
using UnityEngine;

namespace VoxelBusters.NativePlugins
{
	[Serializable]
	public class TwitterSettings
	{
		[SerializeField]
		[Tooltip("The Twitter application consumer key.")]
		private string m_consumerKey;

		[SerializeField]
		[Tooltip("The Twitter application consumer secret.")]
		private string m_consumerSecret;

		internal string ConsumerKey
		{
			get
			{
				return m_consumerKey;
			}
			private set
			{
				m_consumerKey = value;
			}
		}

		public string ConsumerSecret
		{
			get
			{
				return m_consumerSecret;
			}
			private set
			{
				m_consumerSecret = value;
			}
		}
	}
}
