using System;
using UnityEngine;

namespace VoxelBusters.NativePlugins
{
	[Serializable]
	public class SocialNetworkSettings
	{
		[SerializeField]
		private TwitterSettings m_twitterSettings;

		public TwitterSettings TwitterSettings
		{
			get
			{
				return m_twitterSettings;
			}
		}

		public SocialNetworkSettings()
		{
			m_twitterSettings = new TwitterSettings();
		}
	}
}
