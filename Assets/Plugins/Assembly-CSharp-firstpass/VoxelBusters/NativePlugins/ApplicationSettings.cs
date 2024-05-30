using System;
using UnityEngine;
using VoxelBusters.NativePlugins.Internal;

namespace VoxelBusters.NativePlugins
{
	[Serializable]
	public class ApplicationSettings
	{
		[Serializable]
		public class AddonServices
		{
			[SerializeField]
			[NotifyNPSettingsOnValueChange]
			[Tooltip("If enabled, Soomla Grow service will be active within your application.")]
			private bool m_usesSoomlaGrow;

			[SerializeField]
			[NotifyNPSettingsOnValueChange]
			[Tooltip("If enabled, One Signal service will be active within your application.")]
			private bool m_usesOneSignal;

			public bool UsesSoomlaGrow
			{
				get
				{
					return m_usesSoomlaGrow;
				}
			}

			public bool UsesOneSignal
			{
				get
				{
					return m_usesOneSignal;
				}
			}
		}

		[Serializable]
		public class AndroidSettings
		{
			[SerializeField]
			[Tooltip("The string that identifies your app in Google Play Store.")]
			private string m_storeIdentifier;

			public string StoreIdentifier
			{
				get
				{
					return m_storeIdentifier;
				}
				private set
				{
					m_storeIdentifier = value;
				}
			}
		}

		[Serializable]
		public class Features
		{
			[Serializable]
			public class MultiComponentFeature
			{
				public bool value = true;
			}

			[Serializable]
			public class MediaLibraryFeature : MultiComponentFeature
			{
				public bool usesCamera = true;

				public bool usesPhotoAlbum = true;
			}

			[Serializable]
			public class NotificationServiceFeature : MultiComponentFeature
			{
				public bool usesLocalNotification = true;

				public bool usesRemoteNotification = true;
			}

			[SerializeField]
			[NotifyNPSettingsOnValueChange]
			[Tooltip("If enabled, Address Book feature will be active within your application.")]
			private bool m_usesAddressBook = true;

			[SerializeField]
			[NotifyNPSettingsOnValueChange]
			[Tooltip("If enabled, Billing feature will be active within your application.")]
			private bool m_usesBilling = true;

			[NotifyNPSettingsOnValueChange]
			[SerializeField]
			[Tooltip("If enabled, Cloud Services feature will be active within your application.")]
			private bool m_usesCloudServices = true;

			[SerializeField]
			[NotifyNPSettingsOnValueChange]
			[Tooltip("If enabled, Game Services feature will be active within your application.")]
			private bool m_usesGameServices = true;

			[SerializeField]
			[Tooltip("If enabled, Media Library feature will be active within your application.")]
			private MediaLibraryFeature m_mediaLibrary = new MediaLibraryFeature
			{
				value = true
			};

			[SerializeField]
			[NotifyNPSettingsOnValueChange]
			[Tooltip("If enabled, Network Connectivity feature will be active within your application.")]
			private bool m_usesNetworkConnectivity = true;

			[SerializeField]
			[Tooltip("If enabled, Notification Service feature will be active within your application.")]
			private NotificationServiceFeature m_notificationService = new NotificationServiceFeature
			{
				value = true
			};

			[SerializeField]
			[NotifyNPSettingsOnValueChange]
			[Tooltip("If enabled, Sharing feature will be active within your application.")]
			private bool m_usesSharing = true;

			[SerializeField]
			[NotifyNPSettingsOnValueChange]
			[Tooltip("If enabled, Twitter feature will be active within your application.")]
			private bool m_usesTwitter = true;

			[SerializeField]
			[NotifyNPSettingsOnValueChange]
			[Tooltip("If enabled, WebView feature will be active within your application.")]
			private bool m_usesWebView = true;

			public bool UsesAddressBook
			{
				get
				{
					return m_usesAddressBook;
				}
			}

			public bool UsesBilling
			{
				get
				{
					return m_usesBilling;
				}
			}

			public bool UsesCloudServices
			{
				get
				{
					return m_usesCloudServices;
				}
			}

			public bool UsesGameServices
			{
				get
				{
					return m_usesGameServices;
				}
			}

			public bool UsesMediaLibrary
			{
				get
				{
					return m_mediaLibrary.value;
				}
			}

			public MediaLibraryFeature MediaLibrary
			{
				get
				{
					return m_mediaLibrary;
				}
			}

			public bool UsesNetworkConnectivity
			{
				get
				{
					return m_usesNetworkConnectivity;
				}
			}

			public bool UsesNotificationService
			{
				get
				{
					return m_notificationService.value;
				}
			}

			public NotificationServiceFeature NotificationService
			{
				get
				{
					return m_notificationService;
				}
			}

			public bool UsesSharing
			{
				get
				{
					return m_usesSharing;
				}
			}

			public bool UsesTwitter
			{
				get
				{
					return m_usesTwitter;
				}
			}

			public bool UsesWebView
			{
				get
				{
					return m_usesWebView;
				}
			}
		}

		[Serializable]
		public class iOSSettings
		{
			[SerializeField]
			[Tooltip("The string that identifies your app in iOS App Store.")]
			private string m_storeIdentifier;

			[SerializeField]
			private string m_addressBookUsagePermissionDescription = "$(PRODUCT_NAME) uses contacts";

			[SerializeField]
			private string m_cameraUsagePermissionDescription = "$(PRODUCT_NAME) uses camera";

			[SerializeField]
			private string m_photoAlbumUsagePermissionDescription = "$(PRODUCT_NAME) uses photo library";

			[SerializeField]
			private string m_photoAlbumModifyUsagePermissionDescription = "$(PRODUCT_NAME) saves images to photo library";

			public string StoreIdentifier
			{
				get
				{
					return m_storeIdentifier;
				}
				private set
				{
					m_storeIdentifier = value;
				}
			}

			public string AddressBookUsagePermissionDescription
			{
				get
				{
					return m_addressBookUsagePermissionDescription;
				}
			}

			public string CameraUsagePermissionDescription
			{
				get
				{
					return m_cameraUsagePermissionDescription;
				}
			}

			public string PhotoAlbumUsagePermissionDescription
			{
				get
				{
					return m_photoAlbumUsagePermissionDescription;
				}
			}

			public string PhotoAlbumModifyUsagePermissionDescription
			{
				get
				{
					return m_photoAlbumModifyUsagePermissionDescription;
				}
			}
		}

		[SerializeField]
		[Tooltip("Select the features that you would like to use.")]
		private Features m_supportedFeatures = new Features();

		[SerializeField]
		[Tooltip("Select the Addon services that you would like to use.")]
		private AddonServices m_supportedAddonServices = new AddonServices();

		[SerializeField]
		private iOSSettings m_iOS = new iOSSettings();

		[SerializeField]
		private AndroidSettings m_android = new AndroidSettings();

		internal bool IsDebugMode
		{
			get
			{
				return Debug.isDebugBuild;
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

		internal Features SupportedFeatures
		{
			get
			{
				return m_supportedFeatures;
			}
		}

		internal AddonServices SupportedAddonServices
		{
			get
			{
				return m_supportedAddonServices;
			}
		}

		public string StoreIdentifier
		{
			get
			{
				return m_android.StoreIdentifier;
			}
		}
	}
}
