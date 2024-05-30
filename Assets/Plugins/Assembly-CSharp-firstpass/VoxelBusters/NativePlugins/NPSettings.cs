using System;
using UnityEngine;
using VoxelBusters.UASUtils;
using VoxelBusters.Utility;

namespace VoxelBusters.NativePlugins
{
	public class NPSettings : SharedScriptableObject<NPSettings>, IAssetStoreProduct
	{
		private const bool kIsFullVersion = true;

		private const string kProductName = "Native Plugins";

		public const string kProductVersion = "1.5.7";

		internal const string kPrefsKeyPropertyModified = "np-property-modified";

		internal const string kMethodPropertyChanged = "OnPropertyModified";

		internal const string kLiteVersionMacro = "NATIVE_PLUGINS_LITE_VERSION";

		private const string kAddressBookMacro = "USES_ADDRESS_BOOK";

		private const string kBillingMacro = "USES_BILLING";

		private const string kCloudServicesMacro = "USES_CLOUD_SERVICES";

		private const string kGameServicesMacro = "USES_GAME_SERVICES";

		private const string kMediaLibraryMacro = "USES_MEDIA_LIBRARY";

		private const string kNetworkConnectivityMacro = "USES_NETWORK_CONNECTIVITY";

		private const string kNotificationServiceMacro = "USES_NOTIFICATION_SERVICE";

		private const string kSharingMacro = "USES_SHARING";

		private const string kTwitterMacro = "USES_TWITTER";

		private const string kWebViewMacro = "USES_WEBVIEW";

		private const string kRateMyAppMacro = "USES_RATE_MY_APP";

		private const string kMacroSoomlaGrowService = "USES_SOOMLA_GROW";

		private const string kMacroOneSignalService = "USES_ONE_SIGNAL";

		[NonSerialized]
		private AssetStoreProduct m_assetStoreProduct;

		[SerializeField]
		[HideInInspector]
		private string m_lastOpenedDateString;

		[SerializeField]
		private ApplicationSettings m_applicationSettings = new ApplicationSettings();

		[SerializeField]
		private NetworkConnectivitySettings m_networkConnectivitySettings = new NetworkConnectivitySettings();

		[SerializeField]
		private UtilitySettings m_utilitySettings = new UtilitySettings();

		[SerializeField]
		private BillingSettings m_billingSettings = new BillingSettings();

		[SerializeField]
		private CloudServicesSettings m_cloudServicesSettings = new CloudServicesSettings();

		[SerializeField]
		private MediaLibrarySettings m_mediaLibrarySettings = new MediaLibrarySettings();

		[SerializeField]
		private NotificationServiceSettings m_notificationSettings = new NotificationServiceSettings();

		[SerializeField]
		private SocialNetworkSettings m_socialNetworkSettings = new SocialNetworkSettings();

		[SerializeField]
		private GameServicesSettings m_gameServicesSettings = new GameServicesSettings();

		[SerializeField]
		private WebViewSettings m_webViewSettings = new WebViewSettings();

		[SerializeField]
		private AddonServicesSettings m_addonServicesSettings = new AddonServicesSettings();

		public static ApplicationSettings Application
		{
			get
			{
				return SharedScriptableObject<NPSettings>.Instance.m_applicationSettings;
			}
		}

		public static NetworkConnectivitySettings NetworkConnectivity
		{
			get
			{
				return SharedScriptableObject<NPSettings>.Instance.m_networkConnectivitySettings;
			}
		}

		public static UtilitySettings Utility
		{
			get
			{
				return SharedScriptableObject<NPSettings>.Instance.m_utilitySettings;
			}
		}

		public static BillingSettings Billing
		{
			get
			{
				return SharedScriptableObject<NPSettings>.Instance.m_billingSettings;
			}
		}

		public static CloudServicesSettings CloudServices
		{
			get
			{
				return SharedScriptableObject<NPSettings>.Instance.m_cloudServicesSettings;
			}
		}

		public static MediaLibrarySettings MediaLibrary
		{
			get
			{
				return SharedScriptableObject<NPSettings>.Instance.m_mediaLibrarySettings;
			}
		}

		public static NotificationServiceSettings Notification
		{
			get
			{
				return SharedScriptableObject<NPSettings>.Instance.m_notificationSettings;
			}
		}

		public static SocialNetworkSettings SocialNetworkSettings
		{
			get
			{
				return SharedScriptableObject<NPSettings>.Instance.m_socialNetworkSettings;
			}
		}

		public static GameServicesSettings GameServicesSettings
		{
			get
			{
				return SharedScriptableObject<NPSettings>.Instance.m_gameServicesSettings;
			}
		}

		public static AddonServicesSettings AddonServicesSettings
		{
			get
			{
				return SharedScriptableObject<NPSettings>.Instance.m_addonServicesSettings;
			}
		}

		public static WebViewSettings WebViewSettings
		{
			get
			{
				return SharedScriptableObject<NPSettings>.Instance.m_webViewSettings;
			}
		}

		public AssetStoreProduct AssetStoreProduct
		{
			get
			{
				return m_assetStoreProduct;
			}
		}

		public ApplicationSettings ApplicationSettings
		{
			get
			{
				return m_applicationSettings;
			}
		}

		protected override void Reset()
		{
			base.Reset();
		}

		protected override void OnEnable()
		{
			base.OnEnable();
		}

		public bool CanShowRateMyAppDialog()
		{
			return false;
		}

		public void OnBeforeShowingRateMyAppDialog()
		{
		}
	}
}
