using System;
using UnityEngine;
using VoxelBusters.NativePlugins.Internal;

namespace VoxelBusters.NativePlugins
{
	[Serializable]
	public class GameServicesSettings
	{
		[Serializable]
		public class AndroidSettings
		{
			[SerializeField]
			[NotifyNPSettingsOnValueChange]
			[Tooltip("Your application id in Google Play services.")]
			private string m_playServicesApplicationID;

			[SerializeField]
			[NotifyNPSettingsOnValueChange]
			[Tooltip("Your Server Client ID for getting external authentication credentials")]
			private string m_serverClientID;

			[Tooltip("String formats used to derive completed achievement description. Note: Achievement title will be inserted in place of token '#'.")]
			private string[] m_achievedDescriptionFormats = new string[1] { "Awesome! Achievement # completed." };

			[SerializeField]
			[Tooltip("Allow to show default error dialogs.")]
			private bool m_showDefaultErrorDialogs = true;

			[SerializeField]
			[Tooltip("Allow auto sign in if user logged in previously.")]
			private bool m_allowAutoLogin = true;

			internal string PlayServicesApplicationID
			{
				get
				{
					return m_playServicesApplicationID;
				}
			}

			internal string ServerClientID
			{
				get
				{
					return m_serverClientID;
				}
			}

			internal string[] AchievedDescriptionFormats
			{
				get
				{
					return m_achievedDescriptionFormats;
				}
			}

			internal bool ShowDefaultErrorDialogs
			{
				get
				{
					return m_showDefaultErrorDialogs;
				}
			}

			internal bool AllowAutoLogin
			{
				get
				{
					return m_allowAutoLogin;
				}
			}
		}

		[Serializable]
		public class iOSSettings
		{
			[SerializeField]
			[Tooltip("If checked, a banner is displayed when an achievement is completed.")]
			private bool m_showDefaultAchievementCompletionBanner = true;

			internal bool ShowDefaultAchievementCompletionBanner
			{
				get
				{
					return m_showDefaultAchievementCompletionBanner;
				}
			}
		}

		[SerializeField]
		[Tooltip("Store additional information about all the leaderboards that are used.")]
		private LeaderboardMetadata[] m_leaderboardMetadataCollection;

		[SerializeField]
		[Tooltip("Store additional information about all the achievements that are used.")]
		private AchievementMetadata[] m_achievementMetadataCollection;

		[SerializeField]
		private iOSSettings m_iOS = new iOSSettings();

		[SerializeField]
		private AndroidSettings m_android = new AndroidSettings();

		[SerializeField]
		[HideInInspector]
		private IDContainer[] m_achievementIDCollection = new IDContainer[0];

		[SerializeField]
		[HideInInspector]
		private IDContainer[] m_leaderboardIDCollection = new IDContainer[0];

		internal LeaderboardMetadata[] LeaderboardMetadataCollection
		{
			get
			{
				return m_leaderboardMetadataCollection;
			}
			set
			{
				m_leaderboardMetadataCollection = value;
			}
		}

		internal AchievementMetadata[] AchievementMetadataCollection
		{
			get
			{
				return m_achievementMetadataCollection;
			}
			set
			{
				m_achievementMetadataCollection = value;
			}
		}

		internal iOSSettings IOS
		{
			get
			{
				return m_iOS;
			}
		}

		internal AndroidSettings Android
		{
			get
			{
				return m_android;
			}
		}

		internal IDContainer[] AchievementIDCollection
		{
			get
			{
				return m_achievementIDCollection;
			}
		}

		internal IDContainer[] LeaderboardIDCollection
		{
			get
			{
				return m_leaderboardIDCollection;
			}
		}
	}
}
