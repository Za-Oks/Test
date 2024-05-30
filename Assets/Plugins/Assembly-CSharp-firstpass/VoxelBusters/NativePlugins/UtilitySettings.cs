using System;
using UnityEngine;
using VoxelBusters.NativePlugins.Internal;

namespace VoxelBusters.NativePlugins
{
	[Serializable]
	public class UtilitySettings
	{
		[Serializable]
		public class AndroidSettings
		{
			[SerializeField]
			[NotifyNPSettingsOnValueChange]
			[Tooltip("Enable this for setting application badge on Android. Disable this if not used as it will skip adding extra libraries")]
			private bool m_modifiesApplicationBadge = true;

			internal bool ModifiesApplicationBadge
			{
				get
				{
					return m_modifiesApplicationBadge;
				}
			}
		}

		[SerializeField]
		[Tooltip("Rate My App dialog settings.")]
		private RateMyAppSettings m_rateMyApp;

		[SerializeField]
		private AndroidSettings m_android;

		public RateMyAppSettings RateMyApp
		{
			get
			{
				return m_rateMyApp;
			}
			private set
			{
				m_rateMyApp = value;
			}
		}

		public AndroidSettings Android
		{
			get
			{
				return m_android;
			}
		}
	}
}
