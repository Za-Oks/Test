using System.Collections;

namespace VoxelBusters.NativePlugins.Internal
{
	public class RateMyAppGenericStoreBase : IRateMyAppKeysCollection, IRateMyAppOperationHandler
	{
		private const string kIsFirstTimeLaunch = "np-is-first-time-launch";

		private const string kVersionLastRated = "np-version-last-rated";

		private const string kShowPromptAfter = "np-show-prompt-after";

		private const string kPromptLastShown = "np-prompt-last-shown";

		private const string kDontShow = "np-dont-show";

		private const string kAppUsageCount = "np-app-usage-count";

		public string IsFirstTimeLaunchKeyName
		{
			get
			{
				return "np-is-first-time-launch";
			}
		}

		public string VersionLastRatedKeyName
		{
			get
			{
				return "np-version-last-rated";
			}
		}

		public string ShowPromptAfterKeyName
		{
			get
			{
				return "np-show-prompt-after";
			}
		}

		public string PromptLastShownKeyName
		{
			get
			{
				return "np-prompt-last-shown";
			}
		}

		public string DontShowKeyName
		{
			get
			{
				return "np-dont-show";
			}
		}

		public string AppUsageCountKeyName
		{
			get
			{
				return "np-app-usage-count";
			}
		}

		public void Execute(IEnumerator _routine)
		{
			NPBinding.Utility.StartCoroutine(_routine);
		}
	}
}
