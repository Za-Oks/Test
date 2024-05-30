using System;
using System.Collections;
using UnityEngine;
using VoxelBusters.NativePlugins.Internal;
using VoxelBusters.UASUtils;
using VoxelBusters.Utility;

namespace VoxelBusters.NativePlugins
{
	public class Utility : MonoBehaviour
	{
		private IUtilityPlatform m_platform;

		public RateMyApp RateMyApp { get; private set; }

		private void Awake()
		{
			m_platform = CreatePlatformSpecificObject();
			RateMyAppSettings rateMyApp = NPSettings.Utility.RateMyApp;
			if (rateMyApp.IsEnabled)
			{
				RateMyApp = m_platform.CreateRateMyApp(rateMyApp);
				RateMyApp.RecordAppLaunch();
			}
		}

		private IEnumerator Start()
		{
			yield return new WaitForEndOfFrame();
			if (RateMyApp != null)
			{
				RateMyApp.AskForReview();
			}
		}

		private void OnApplicationPause(bool _isPaused)
		{
			if (!_isPaused && RateMyApp != null)
			{
				RateMyApp.AskForReview();
			}
		}

		public string GetUUID()
		{
			return Guid.NewGuid().ToString();
		}

		public void OpenStoreLink(params PlatformValue[] _storeIdentifiers)
		{
			PlatformValue currentPlatformValue = PlatformValueHelper.GetCurrentPlatformValue(_storeIdentifiers);
			if (currentPlatformValue == null)
			{
				DebugUtility.Logger.Log("Native Plugins", "[Utility] The operation could not be completed because application identifier is invalid.");
			}
			else
			{
				OpenStoreLink(currentPlatformValue.Value);
			}
		}

		public void OpenStoreLink(string _applicationID)
		{
			m_platform.OpenStoreLink(_applicationID);
		}

		public void SetApplicationIconBadgeNumber(int _badgeNumber)
		{
			m_platform.SetApplicationIconBadgeNumber(_badgeNumber);
		}

		public string GetBundleVersion()
		{
			return PlayerSettings.GetBundleVersion();
		}

		public string GetBundleIdentifier()
		{
			return PlayerSettings.GetBundleIdentifier();
		}

		private IUtilityPlatform CreatePlatformSpecificObject()
		{
			return new UtilityAndroid();
		}
	}
}
