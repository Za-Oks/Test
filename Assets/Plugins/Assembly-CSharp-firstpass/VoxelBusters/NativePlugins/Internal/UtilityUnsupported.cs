using UnityEngine;
using VoxelBusters.UASUtils;

namespace VoxelBusters.NativePlugins.Internal
{
	public class UtilityUnsupported : IUtilityPlatform
	{
		public void OpenStoreLink(string _applicationID)
		{
			Debug.LogWarning("The operation could not be completed because the requested feature is not supported.");
		}

		public void SetApplicationIconBadgeNumber(int _badgeNumber)
		{
			DebugUtility.Logger.LogWarning("Native Plugins", "The operation could not be completed because the requested feature is supported only on iOS platform.");
		}

		public RateMyApp CreateRateMyApp(RateMyAppSettings _settings)
		{
			RateMyAppGenericController rateMyAppGenericController = new RateMyAppGenericController();
			return RateMyApp.Create(rateMyAppGenericController, rateMyAppGenericController, rateMyAppGenericController, rateMyAppGenericController, _settings);
		}
	}
}
