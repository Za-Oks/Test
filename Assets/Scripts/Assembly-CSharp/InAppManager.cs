//using System;
//using System.Collections.Generic;
//using UnityEngine;
 
 

//public class InAppManager : MonoBehaviour, IStoreListener
//{
//	public static bool HAS_SUBSCRIPTION;

//	private Ui_Manager uimanager;

//	private PackagesManager packagesManager;

//	private IStoreController m_StoreController;

//	private IExtensionProvider m_StoreExtensionProvider;

//	private readonly string google_public_key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAgUHfD3pHplT0HtgJw4Ae3JlZrlAZ4gHnPmDRnpCQ4dw7rFwrlJ3tcYnhSK+nq0NJucxJgedhaw+3DeNvShQHUwyziKVaet+W3XfU7uMyrB9qRdzklyB6D77xHqq3kEEY2Ur7Wcw8kcFXYsvdH4X8dnZ54h/8RbIEUjGOfCNEmxtfpV8dFjAgFYIOI+Y721Kz1afu6HNy8bZlWroj2PmUZSQMeB/PcDtEk0eNDGwTpiUGVmEHQ9iKnpP4y4Rzh2EymQstL7U0ZzjXXrhxzZaOCW3EucKQ11EGzWhlTo8BTELRJUG89DIByzxUgGTeqbVp3sukb65N7lo5o32mwqY7wQIDAQAB";

//	private List<string> clickedIAP = new List<string>();

//	private static string PID_NOADS = "noads";

//	private static string PID_GEM_2500 = "gempack_2500";

//	private static string PID_GEM_7000 = "gempack_7000";

//	private static string PID_GEM_15000 = "gempack_15000";

//	private static string PID_GEM_50000 = "gempack_50000";

//	private static string PID_FIREDRAGON = "unlockFireDragon";

//	private static string PID_SAMURAI = "unlockSamurai";

//	private static string PID_SPARTAN = "unlockSpartan";

//	private static string PID_SENTINEL = "unlockSentinel";

//	private static string PID_MINIGUN = "unlockMinigun";

//	private static string PID_RHINO = "unlockRhino";

//	private static string PID_GUARDIAN = "unlockGuardian";

//	private static string PID_WOLF = "unlockWolf";

//	private static string PID_MEMBERSHIP = "membership";

//	private static string PID_COMBO_SPARTAN_SENTINEL = "unlockCombo_SpartanSentinel";

//	private static string PID_COMBO_SPARTAN_SENTINEL_SAMURAI = "unlockCombo_SpartanSentinelSamurai";

//	private static string PID_COMBO_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON = "unlockCombo_SpartanSentinelSamuraiFireDragon";

//	private static string PID_COMBO_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON_MINIGUN = "unlockCombo_SpartanSentinelSamuraiFireDragonMinigun";

//	private static string PID_COMBO_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON_MINIGUN_RHINO = "unlockCombo_SpartanSentinelSamuraiFireDragonMinigunRhino";

//	private static string PID_COMBO_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON_MINIGUN_RHINO_GUARDIAN = "unlockCombo_SpartanSentinelSamuraiFireDragonMinigunRhinoGuardian";

//	private static string PID_COMBO_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON_MINIGUN_RHINO_GUARDIAN_WOLF = "com.rappidstudios.simulatorbattlephysics.unlockcombo_eight";

//	private static string PID_SF_FIRE_DRAGON_MINIGUN_RHINO = "specialOffer_fir_min_rhi";

//	private static string PID_SF_FIRE_DRAGON_MINIGUN = "specialOffer_fir_min";

//	private static string PID_SF_FIRE_DRAGON_RHINO = "specialOffer_fir_rhi";

//	private static string PID_SF_GEM_5000 = "specialOffer_gem_5000";

//	private static string PID_SF_GEM_40000 = "specialOffer_gem_40000";

//	private static string SKU_NO_ADS = "com.rappidstudios.simulatorbattlephysics.noads";

//	private static string SKU_GEM_2500 = "com.rappidstudios.simulatorbattlephysics.gempack_2500";

//	private static string SKU_GEM_7000 = "com.rappidstudios.simulatorbattlephysics.gempack_7000";

//	private static string SKU_GEM_15000 = "com.rappidstudios.simulatorbattlephysics.gempack_15000";

//	private static string SKU_GEM_50000 = "com.rappidstudios.simulatorbattlephysics.gempack_50000";

//	private static string SKU_SPARTAN = "com.rappidstudios.simulatorbattlephysics.unlockspartan";

//	private static string SKU_SENTINEL = "com.rappidstudios.simulatorbattlephysics.unlocksentinel";

//	private static string SKU_SAMURAI = "com.rappidstudios.simulatorbattlephysics.unlocksamurai";

//	private static string SKU_FIRE_DRAGON = "com.rappidstudios.simulatorbattlephysics.unlockfiredragon";

//	private static string SKU_MINIGUN = "com.rappidstudios.simulatorbattlephysics.unlockminigun";

//	private static string SKU_RHINO = "com.rappidstudios.simulatorbattlephysics.unlockrhino";

//	private static string SKU_GUARDIAN = "com.rappidstudios.simulatorbattlephysics.unlockguardian";

//	private static string SKU_WOLF = "com.rappidstudios.simulatorbattlephysics.unlockwolf";

//	private static string SKU_COMBO_SPARTAN_SENTINEL = "com.rappidstudios.simulatorbattlephysics.unlockcombo_spartansentinel";

//	private static string SKU_COMBO_SPARTAN_SENTINEL_SAMURAI = "com.rappidstudios.simulatorbattlephysics.unlockcombo_spartansentinelsamurai";

//	private static string SKU_COMBO_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON = "com.rappidstudios.simulatorbattlephysics.unlockcombo_spartansentinelsamuraifiredragon";

//	private static string SKU_COMBO_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON_MINIGUN = "com.rappidstudios.simulatorbattlephysics.unlockcombo_spartansentinelsamuraifiredragonminigun";

//	private static string SKU_COMBO_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON_MINIGUN_RHINO = "com.rappidstudios.simulatorbattlephysics.unlockcombo_spartansentinelsamuraifiredragonminigunrhino";

//	private static string SKU_COMBO_GOOGLE_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON_MINIGUN_RHINO_GUARDIAN = "com.rappidstudios.simulatorbattlephysics.unlockcombo_spartansentinelsamuraifiredragonminigunrhinoguardian";

//	private static string SKU_COMBO_APPLE_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON_MINIGUN_RHINO_GUARDIAN = "com.rappidstudios.simulatorbattlephysics.unlockcombo_7x";

//	private static string SKU_COMBO_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON_MINIGUN_RHINO_GUARDIAN_WOLF = "com.rappidstudios.simulatorbattlephysics.unlockcombo_eight";

//	private static string SKU_SF_FIRE_DRAGON_MINIGUN_RHINO = "com.rappidstudios.simulatorbattlephysics.sf_fir_min_rhi";

//	private static string SKU_SF_FIRE_DRAGON_MINIGUN = "com.rappidstudios.simulatorbattlephysics.sf_fir_min";

//	private static string SKU_SF_FIRE_DRAGON_RHINO = "com.rappidstudios.simulatorbattlephysics.sf_fir_rhi";

//	private static string SKU_SF_GEM_5000 = "com.rappidstudios.simulatorbattlephysics.sf_gem_5000";

//	private static string SKU_SF_GEM_40000 = "com.rappidstudios.simulatorbattlephysics.sf_gem_40000";

//	private static string SKU_MEMBERSHIP = "com.rappidstudios.simulatorbattlephysics.membership";

//	private static bool showDebug;

//	private void Awake()
//	{
//		uimanager = GameObject.FindGameObjectWithTag("Ui_Manager").GetComponent<Ui_Manager>();
//		if (GameObject.FindWithTag("PackagesManager") != null)
//		{
//			packagesManager = GameObject.FindWithTag("PackagesManager").GetComponent<PackagesManager>();
//		}
//		HAS_SUBSCRIPTION = HasSubscription();
//	}

//	private void Start()
//	{
//		if (m_StoreController == null)
//		{
//			InitializePurchasing();
//		}
//	}

//	private void InitializePurchasing()
//	{
//		if (!IsInitialized())
//		{
//			ConfigurationBuilder configurationBuilder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
//			configurationBuilder.AddProduct(PID_NOADS, ProductType.NonConsumable, new IDs
//			{
//				{ SKU_NO_ADS, "GooglePlay" },
//				{ SKU_NO_ADS, "AppleAppStore" },
//				{ SKU_NO_ADS, "AmazonApps" }
//			});
//			configurationBuilder.AddProduct(PID_SPARTAN, ProductType.NonConsumable, new IDs
//			{
//				{ SKU_SPARTAN, "GooglePlay" },
//				{ SKU_SPARTAN, "AppleAppStore" },
//				{ SKU_SPARTAN, "AmazonApps" }
//			});
//			configurationBuilder.AddProduct(PID_SENTINEL, ProductType.NonConsumable, new IDs
//			{
//				{ SKU_SENTINEL, "GooglePlay" },
//				{ SKU_SENTINEL, "AppleAppStore" },
//				{ SKU_SENTINEL, "AmazonApps" }
//			});
//			configurationBuilder.AddProduct(PID_SAMURAI, ProductType.NonConsumable, new IDs
//			{
//				{ SKU_SAMURAI, "GooglePlay" },
//				{ SKU_SAMURAI, "AppleAppStore" },
//				{ SKU_SAMURAI, "AmazonApps" }
//			});
//			configurationBuilder.AddProduct(PID_FIREDRAGON, ProductType.NonConsumable, new IDs
//			{
//				{ SKU_FIRE_DRAGON, "GooglePlay" },
//				{ SKU_FIRE_DRAGON, "AppleAppStore" },
//				{ SKU_FIRE_DRAGON, "AmazonApps" }
//			});
//			configurationBuilder.AddProduct(PID_MINIGUN, ProductType.NonConsumable, new IDs
//			{
//				{ SKU_MINIGUN, "GooglePlay" },
//				{ SKU_MINIGUN, "AppleAppStore" },
//				{ SKU_MINIGUN, "AmazonApps" }
//			});
//			configurationBuilder.AddProduct(PID_RHINO, ProductType.NonConsumable, new IDs
//			{
//				{ SKU_RHINO, "GooglePlay" },
//				{ SKU_RHINO, "AppleAppStore" },
//				{ SKU_RHINO, "AmazonApps" }
//			});
//			configurationBuilder.AddProduct(PID_GUARDIAN, ProductType.NonConsumable, new IDs
//			{
//				{ SKU_GUARDIAN, "GooglePlay" },
//				{ SKU_GUARDIAN, "AppleAppStore" },
//				{ SKU_GUARDIAN, "AmazonApps" }
//			});
//			configurationBuilder.AddProduct(PID_WOLF, ProductType.NonConsumable, new IDs
//			{
//				{ SKU_WOLF, "GooglePlay" },
//				{ SKU_WOLF, "AppleAppStore" },
//				{ SKU_WOLF, "AmazonApps" }
//			});
//			configurationBuilder.AddProduct(PID_COMBO_SPARTAN_SENTINEL, ProductType.NonConsumable, new IDs
//			{
//				{ SKU_COMBO_SPARTAN_SENTINEL, "GooglePlay" },
//				{ SKU_COMBO_SPARTAN_SENTINEL, "AmazonApps" },
//				{ SKU_COMBO_SPARTAN_SENTINEL, "AppleAppStore" }
//			});
//			configurationBuilder.AddProduct(PID_COMBO_SPARTAN_SENTINEL_SAMURAI, ProductType.NonConsumable, new IDs
//			{
//				{ SKU_COMBO_SPARTAN_SENTINEL_SAMURAI, "GooglePlay" },
//				{ SKU_COMBO_SPARTAN_SENTINEL_SAMURAI, "AmazonApps" },
//				{ SKU_COMBO_SPARTAN_SENTINEL_SAMURAI, "AppleAppStore" }
//			});
//			configurationBuilder.AddProduct(PID_COMBO_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON, ProductType.NonConsumable, new IDs
//			{
//				{ SKU_COMBO_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON, "GooglePlay" },
//				{ SKU_COMBO_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON, "AmazonApps" },
//				{ SKU_COMBO_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON, "AppleAppStore" }
//			});
//			configurationBuilder.AddProduct(PID_COMBO_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON_MINIGUN, ProductType.NonConsumable, new IDs
//			{
//				{ SKU_COMBO_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON_MINIGUN, "GooglePlay" },
//				{ SKU_COMBO_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON_MINIGUN, "AmazonApps" },
//				{ SKU_COMBO_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON_MINIGUN, "AppleAppStore" }
//			});
//			configurationBuilder.AddProduct(PID_COMBO_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON_MINIGUN_RHINO, ProductType.NonConsumable, new IDs
//			{
//				{ SKU_COMBO_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON_MINIGUN_RHINO, "GooglePlay" },
//				{ SKU_COMBO_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON_MINIGUN_RHINO, "AmazonApps" },
//				{ SKU_COMBO_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON_MINIGUN_RHINO, "AppleAppStore" }
//			});
//			configurationBuilder.AddProduct(PID_COMBO_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON_MINIGUN_RHINO_GUARDIAN, ProductType.NonConsumable, new IDs
//			{
//				{ SKU_COMBO_GOOGLE_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON_MINIGUN_RHINO_GUARDIAN, "GooglePlay" },
//				{ SKU_COMBO_GOOGLE_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON_MINIGUN_RHINO_GUARDIAN, "AmazonApps" },
//				{ SKU_COMBO_APPLE_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON_MINIGUN_RHINO_GUARDIAN, "AppleAppStore" }
//			});
//			configurationBuilder.AddProduct(PID_COMBO_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON_MINIGUN_RHINO_GUARDIAN_WOLF, ProductType.NonConsumable, new IDs
//			{
//				{ SKU_COMBO_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON_MINIGUN_RHINO_GUARDIAN_WOLF, "GooglePlay" },
//				{ SKU_COMBO_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON_MINIGUN_RHINO_GUARDIAN_WOLF, "AmazonApps" },
//				{ SKU_COMBO_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON_MINIGUN_RHINO_GUARDIAN_WOLF, "AppleAppStore" }
//			});
//			configurationBuilder.AddProduct(PID_GEM_2500, ProductType.Consumable, new IDs
//			{
//				{ SKU_GEM_2500, "GooglePlay" },
//				{ SKU_GEM_2500, "AppleAppStore" },
//				{ SKU_GEM_2500, "AmazonApps" }
//			});
//			configurationBuilder.AddProduct(PID_GEM_7000, ProductType.Consumable, new IDs
//			{
//				{ SKU_GEM_7000, "GooglePlay" },
//				{ SKU_GEM_7000, "AppleAppStore" },
//				{ SKU_GEM_7000, "AmazonApps" }
//			});
//			configurationBuilder.AddProduct(PID_GEM_15000, ProductType.Consumable, new IDs
//			{
//				{ SKU_GEM_15000, "GooglePlay" },
//				{ SKU_GEM_15000, "AppleAppStore" },
//				{ SKU_GEM_15000, "AmazonApps" }
//			});
//			configurationBuilder.AddProduct(PID_GEM_50000, ProductType.Consumable, new IDs
//			{
//				{ SKU_GEM_50000, "GooglePlay" },
//				{ SKU_GEM_50000, "AppleAppStore" },
//				{ SKU_GEM_50000, "AmazonApps" }
//			});
//			configurationBuilder.AddProduct(PID_SF_FIRE_DRAGON_MINIGUN_RHINO, ProductType.Consumable, new IDs
//			{
//				{ SKU_SF_FIRE_DRAGON_MINIGUN_RHINO, "GooglePlay" },
//				{ SKU_SF_FIRE_DRAGON_MINIGUN_RHINO, "AppleAppStore" },
//				{ SKU_SF_FIRE_DRAGON_MINIGUN_RHINO, "AmazonApps" }
//			});
//			configurationBuilder.AddProduct(PID_SF_FIRE_DRAGON_MINIGUN, ProductType.Consumable, new IDs
//			{
//				{ SKU_SF_FIRE_DRAGON_MINIGUN, "GooglePlay" },
//				{ SKU_SF_FIRE_DRAGON_MINIGUN, "AppleAppStore" },
//				{ SKU_SF_FIRE_DRAGON_MINIGUN, "AmazonApps" }
//			});
//			configurationBuilder.AddProduct(PID_SF_FIRE_DRAGON_RHINO, ProductType.Consumable, new IDs
//			{
//				{ SKU_SF_FIRE_DRAGON_RHINO, "GooglePlay" },
//				{ SKU_SF_FIRE_DRAGON_RHINO, "AppleAppStore" },
//				{ SKU_SF_FIRE_DRAGON_RHINO, "AmazonApps" }
//			});
//			configurationBuilder.AddProduct(PID_SF_GEM_5000, ProductType.Consumable, new IDs
//			{
//				{ SKU_SF_GEM_5000, "GooglePlay" },
//				{ SKU_SF_GEM_5000, "AppleAppStore" },
//				{ SKU_SF_GEM_5000, "AmazonApps" }
//			});
//			configurationBuilder.AddProduct(PID_SF_GEM_40000, ProductType.Consumable, new IDs
//			{
//				{ SKU_SF_GEM_40000, "GooglePlay" },
//				{ SKU_SF_GEM_40000, "AppleAppStore" },
//				{ SKU_SF_GEM_40000, "AmazonApps" }
//			});
//			configurationBuilder.AddProduct(PID_MEMBERSHIP, ProductType.Subscription, new IDs
//			{
//				{ SKU_MEMBERSHIP, "GooglePlay" },
//				{ SKU_MEMBERSHIP, "AppleAppStore" },
//				{ SKU_MEMBERSHIP, "AmazonApps" }
//			});
//			UnityPurchasing.Initialize(this, configurationBuilder);
//		}
//	}

//	private bool IsInitialized()
//	{
//		return m_StoreController != null && m_StoreExtensionProvider != null;
//	}

//	private void BuyProductID(string productId)
//	{
//		try
//		{
//			if (IsInitialized())
//			{
//				Product product = m_StoreController.products.WithID(productId);
//				if (product != null && product.availableToPurchase)
//				{
//					DebugIt(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
//					if (!clickedIAP.Contains(productId))
//					{
//						clickedIAP.Add(productId);
//					}
//					m_StoreController.InitiatePurchase(product);
//				}
//				else
//				{
//					DebugIt("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
//				}
//			}
//			else
//			{
//				DebugIt("BuyProductID FAIL. Not initialized.");
//			}
//		}
//		catch (Exception ex)
//		{
//			DebugIt("BuyProductID: FAIL. Exception during purchase. " + ex);
//		}
//	}

//	private void RestorePurchasesLogic()
//	{
//		if (!IsInitialized())
//		{
//			DebugIt("RestorePurchases FAIL. Not initialized.");
//		}
//		else if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
//		{
//			DebugIt("RestorePurchases started ...");
//			IAppleExtensions extension = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
//			extension.RestoreTransactions(delegate(bool result)
//			{
//				DebugIt("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
//			});
//		}
//		else
//		{
//			DebugIt("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
//		}
//	}

//	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
//	{
//		DebugIt("OnInitialized: PASS");
//		m_StoreController = controller;
//		m_StoreExtensionProvider = extensions;
//		ProductMetadata metadata = m_StoreController.products.WithID(PID_NOADS).metadata;
//		uimanager.SetupInAppProduct(InAppProducts.NoAds, metadata.localizedPriceString, metadata.localizedTitle, metadata.localizedDescription);
//		ProductMetadata metadata2 = m_StoreController.products.WithID(PID_GEM_2500).metadata;
//		ProductMetadata metadata3 = m_StoreController.products.WithID(PID_GEM_7000).metadata;
//		ProductMetadata metadata4 = m_StoreController.products.WithID(PID_GEM_15000).metadata;
//		ProductMetadata metadata5 = m_StoreController.products.WithID(PID_GEM_50000).metadata;
//		uimanager.SetupInAppProduct(InAppProducts.Gempack_2500, metadata2.localizedPriceString, metadata2.localizedTitle, metadata2.localizedDescription);
//		uimanager.SetupInAppProduct(InAppProducts.Gempack_7000, metadata3.localizedPriceString, metadata3.localizedTitle, metadata3.localizedDescription);
//		uimanager.SetupInAppProduct(InAppProducts.Gempack_15000, metadata4.localizedPriceString, metadata4.localizedTitle, metadata4.localizedDescription);
//		uimanager.SetupInAppProduct(InAppProducts.Gempack_50000, metadata5.localizedPriceString, metadata5.localizedTitle, metadata5.localizedDescription);
//		ProductMetadata metadata6 = m_StoreController.products.WithID(PID_SPARTAN).metadata;
//		ProductMetadata metadata7 = m_StoreController.products.WithID(PID_SENTINEL).metadata;
//		ProductMetadata metadata8 = m_StoreController.products.WithID(PID_SAMURAI).metadata;
//		ProductMetadata metadata9 = m_StoreController.products.WithID(PID_FIREDRAGON).metadata;
//		ProductMetadata metadata10 = m_StoreController.products.WithID(PID_MINIGUN).metadata;
//		ProductMetadata metadata11 = m_StoreController.products.WithID(PID_RHINO).metadata;
//		ProductMetadata metadata12 = m_StoreController.products.WithID(PID_GUARDIAN).metadata;
//		ProductMetadata metadata13 = m_StoreController.products.WithID(PID_WOLF).metadata;
//		uimanager.SetupInAppProduct(InAppProducts.Spartan, metadata6.localizedPriceString, metadata6.localizedTitle, metadata6.localizedDescription);
//		uimanager.SetupInAppProduct(InAppProducts.Sentinel, metadata7.localizedPriceString, metadata7.localizedTitle, metadata7.localizedDescription);
//		uimanager.SetupInAppProduct(InAppProducts.Samurai, metadata8.localizedPriceString, metadata8.localizedTitle, metadata8.localizedDescription);
//		uimanager.SetupInAppProduct(InAppProducts.FireDragon, metadata9.localizedPriceString, metadata9.localizedTitle, metadata9.localizedDescription);
//		uimanager.SetupInAppProduct(InAppProducts.Minigun, metadata10.localizedPriceString, metadata10.localizedTitle, metadata10.localizedDescription);
//		uimanager.SetupInAppProduct(InAppProducts.Rhino, metadata11.localizedPriceString, metadata11.localizedTitle, metadata11.localizedDescription);
//		uimanager.SetupInAppProduct(InAppProducts.Guardian, metadata12.localizedPriceString, metadata12.localizedTitle, metadata12.localizedDescription);
//		uimanager.SetupInAppProduct(InAppProducts.Wolf, metadata13.localizedPriceString, metadata13.localizedTitle, metadata13.localizedDescription);
//		ProductMetadata metadata14 = m_StoreController.products.WithID(PID_COMBO_SPARTAN_SENTINEL).metadata;
//		ProductMetadata metadata15 = m_StoreController.products.WithID(PID_COMBO_SPARTAN_SENTINEL_SAMURAI).metadata;
//		ProductMetadata metadata16 = m_StoreController.products.WithID(PID_COMBO_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON).metadata;
//		ProductMetadata metadata17 = m_StoreController.products.WithID(PID_COMBO_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON_MINIGUN).metadata;
//		ProductMetadata metadata18 = m_StoreController.products.WithID(PID_COMBO_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON_MINIGUN_RHINO).metadata;
//		ProductMetadata metadata19 = m_StoreController.products.WithID(PID_COMBO_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON_MINIGUN_RHINO_GUARDIAN).metadata;
//		ProductMetadata metadata20 = m_StoreController.products.WithID(PID_COMBO_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON_MINIGUN_RHINO_GUARDIAN_WOLF).metadata;
//		uimanager.SetupInAppProduct(InAppProducts.Combo_Spartan_Sentinel, metadata14.localizedPriceString, metadata14.localizedTitle, metadata14.localizedDescription);
//		uimanager.SetupInAppProduct(InAppProducts.Combo_Spartan_Sentinel_Samurai, metadata15.localizedPriceString, metadata15.localizedTitle, metadata15.localizedDescription);
//		uimanager.SetupInAppProduct(InAppProducts.Combo_Spartan_Sentinel_Samurai_Fire_Dragon, metadata16.localizedPriceString, metadata16.localizedTitle, metadata16.localizedDescription);
//		uimanager.SetupInAppProduct(InAppProducts.Combo_Spartan_Sentinel_Samurai_Fire_Dragon_Minigun, metadata17.localizedPriceString, metadata17.localizedTitle, metadata17.localizedDescription);
//		uimanager.SetupInAppProduct(InAppProducts.Combo_Spartan_Sentinel_Samurai_Fire_Dragon_Minigun_Rhino, metadata18.localizedPriceString, metadata18.localizedTitle, metadata18.localizedDescription);
//		uimanager.SetupInAppProduct(InAppProducts.Combo_Spartan_Sentinel_Samurai_Fire_Dragon_Minigun_Rhino_Guardian, metadata19.localizedPriceString, metadata19.localizedTitle, metadata19.localizedDescription);
//		uimanager.SetupInAppProduct(InAppProducts.Combo_Spartan_Sentinel_Samurai_Fire_Dragon_Minigun_Rhino_Guardian_Wolf, metadata20.localizedPriceString, metadata20.localizedTitle, metadata20.localizedDescription);
//		ProductMetadata metadata21 = m_StoreController.products.WithID(PID_SF_FIRE_DRAGON_MINIGUN_RHINO).metadata;
//		ProductMetadata metadata22 = m_StoreController.products.WithID(PID_SF_FIRE_DRAGON_MINIGUN).metadata;
//		ProductMetadata metadata23 = m_StoreController.products.WithID(PID_SF_FIRE_DRAGON_RHINO).metadata;
//		ProductMetadata metadata24 = m_StoreController.products.WithID(PID_SF_GEM_5000).metadata;
//		ProductMetadata metadata25 = m_StoreController.products.WithID(PID_SF_GEM_40000).metadata;
//		uimanager.SetupInAppProduct(InAppProducts.SF_Fire_Dragon_Minigun_Rhino, metadata21.localizedPriceString, metadata21.localizedTitle, metadata21.localizedDescription);
//		uimanager.SetupInAppProduct(InAppProducts.SF_Fire_Dragon_Minigun, metadata22.localizedPriceString, metadata22.localizedTitle, metadata22.localizedDescription);
//		uimanager.SetupInAppProduct(InAppProducts.SF_Fire_Dragon_Rhino, metadata23.localizedPriceString, metadata23.localizedTitle, metadata23.localizedDescription);
//		uimanager.SetupInAppProduct(InAppProducts.SF_Gem_5000, metadata24.localizedPriceString, metadata24.localizedTitle, metadata24.localizedDescription);
//		uimanager.SetupInAppProduct(InAppProducts.SF_Gem_40000, metadata25.localizedPriceString, metadata25.localizedTitle, metadata25.localizedDescription);
//		ProductMetadata metadata26 = m_StoreController.products.WithID(PID_MEMBERSHIP).metadata;
//		uimanager.SetupSubscriptionPrice(metadata26.localizedPriceString);
//		if (packagesManager != null && packagesManager.whichStore == App_Stores.IOS)
//		{
//			try
//			{
//				ConfigurationBuilder configurationBuilder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
//				IAppleConfiguration appleConfiguration = configurationBuilder.Configure<IAppleConfiguration>();
//				byte[] receiptData = Convert.FromBase64String(appleConfiguration.appReceipt);
//				AppleReceipt appleReceipt = new AppleValidator(AppleTangle.Data()).Validate(receiptData);
//				int num = appleReceipt.inAppPurchaseReceipts.Length;
//				if (num > 0)
//				{
//					AppleInAppPurchaseReceipt appleInAppPurchaseReceipt = null;
//					AppleInAppPurchaseReceipt appleInAppPurchaseReceipt2 = null;
//					for (int num2 = num - 1; num2 >= 0; num2--)
//					{
//						appleInAppPurchaseReceipt2 = appleReceipt.inAppPurchaseReceipts[num2];
//						if (appleInAppPurchaseReceipt2 != null && (appleInAppPurchaseReceipt == null || appleInAppPurchaseReceipt2.purchaseDate.CompareTo(appleInAppPurchaseReceipt.purchaseDate) > 0))
//						{
//							appleInAppPurchaseReceipt = appleInAppPurchaseReceipt2;
//						}
//					}
//					if (IsActiveSubscription_Apple(appleInAppPurchaseReceipt))
//					{
//						DebugIt("-----> Sub is Active");
//						SaveSubscriptionDate_Apple(appleInAppPurchaseReceipt.subscriptionExpirationDate, appleInAppPurchaseReceipt.purchaseDate);
//					}
//					else
//					{
//						DebugIt("-----> Sub is NOT Active");
//						if (HasSubscription())
//						{
//							PlayerPrefs_Saves.SaveSubscriptionAppleExpirationDate(DateTime.Now.ToUniversalTime());
//							PlayerPrefs.Save();
//							EnableSubscription();
//						}
//					}
//				}
//				return;
//			}
//			catch (Exception)
//			{
//				return;
//			}
//		}
//		if (!(packagesManager != null) || packagesManager.whichStore != App_Stores.Google)
//		{
//			return;
//		}
//		Product product = controller.products.WithID(PID_MEMBERSHIP);
//		GooglePurchaseData googlePurchaseData = new GooglePurchaseData(product.receipt);
//		if (product.hasReceipt)
//		{
//			DebugIt("-----> Has receipt");
//			SaveSubscription_Google();
//			return;
//		}
//		DebugIt("-----> Does NOT have receipt");
//		if (HasSubscription())
//		{
//			PlayerPrefs_Saves.SaveHasGoogleSubscription(false);
//			PlayerPrefs.Save();
//			EnableSubscription();
//		}
//	}

//	public void OnInitializeFailed(InitializationFailureReason error)
//	{
//		DebugIt("OnInitializeFailed InitializationFailureReason:" + error);
//	}

//	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
//	{
//		if (string.Equals(args.purchasedProduct.definition.id, PID_NOADS, StringComparison.Ordinal))
//		{
//			DebugIt(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//			UnlockNoAds();
//			BoughtIAP();
//		}
//		else if (string.Equals(args.purchasedProduct.definition.id, PID_GEM_2500, StringComparison.Ordinal))
//		{
//			DebugIt(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//			UnlockGems(2500);
//			BoughtIAP();
//		}
//		else if (string.Equals(args.purchasedProduct.definition.id, PID_GEM_7000, StringComparison.Ordinal))
//		{
//			DebugIt(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//			UnlockGems(7000);
//			BoughtIAP();
//		}
//		else if (string.Equals(args.purchasedProduct.definition.id, PID_GEM_15000, StringComparison.Ordinal))
//		{
//			DebugIt(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//			UnlockGems(15000);
//			BoughtIAP();
//		}
//		else if (string.Equals(args.purchasedProduct.definition.id, PID_SPARTAN, StringComparison.Ordinal))
//		{
//			DebugIt(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//			Unlock_Spartan();
//			BoughtIAP();
//		}
//		else if (string.Equals(args.purchasedProduct.definition.id, PID_SENTINEL, StringComparison.Ordinal))
//		{
//			DebugIt(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//			Unlock_Sentinel();
//			BoughtIAP();
//		}
//		else if (string.Equals(args.purchasedProduct.definition.id, PID_COMBO_SPARTAN_SENTINEL, StringComparison.Ordinal))
//		{
//			DebugIt(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//			Unlock_Combo_Spartan_Sentinel();
//			BoughtIAP();
//		}
//		else if (string.Equals(args.purchasedProduct.definition.id, PID_COMBO_SPARTAN_SENTINEL_SAMURAI, StringComparison.Ordinal))
//		{
//			DebugIt(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//			Unlock_Combo_Spartan_Sentinel_Samurai();
//			BoughtIAP();
//		}
//		else if (string.Equals(args.purchasedProduct.definition.id, PID_SAMURAI, StringComparison.Ordinal))
//		{
//			DebugIt(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//			Unlock_Samurai();
//			BoughtIAP();
//		}
//		else if (string.Equals(args.purchasedProduct.definition.id, PID_FIREDRAGON, StringComparison.Ordinal))
//		{
//			DebugIt(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//			Unlock_FireDragon();
//			BoughtIAP();
//		}
//		else if (string.Equals(args.purchasedProduct.definition.id, PID_COMBO_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON, StringComparison.Ordinal))
//		{
//			DebugIt(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//			Unlock_Combo_Spartan_Sentinel_Samurai_FireDragon();
//			BoughtIAP();
//		}
//		else if (string.Equals(args.purchasedProduct.definition.id, PID_GEM_50000, StringComparison.Ordinal))
//		{
//			DebugIt(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//			UnlockGems(50000);
//			BoughtIAP();
//		}
//		else if (string.Equals(args.purchasedProduct.definition.id, PID_MINIGUN, StringComparison.Ordinal))
//		{
//			DebugIt(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//			Unlock_Minigun();
//			BoughtIAP();
//		}
//		else if (string.Equals(args.purchasedProduct.definition.id, PID_COMBO_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON_MINIGUN, StringComparison.Ordinal))
//		{
//			DebugIt(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//			Unlock_Combo_Spartan_Sentinel_Samurai_FireDragon_Minigun();
//			BoughtIAP();
//		}
//		else if (string.Equals(args.purchasedProduct.definition.id, PID_RHINO, StringComparison.Ordinal))
//		{
//			DebugIt(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//			Unlock_Rhino();
//			BoughtIAP();
//		}
//		else if (string.Equals(args.purchasedProduct.definition.id, PID_COMBO_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON_MINIGUN_RHINO, StringComparison.Ordinal))
//		{
//			DebugIt(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//			Unlock_Combo_Spartan_Sentinel_Samurai_FireDragon_Minigun_Rhino();
//			BoughtIAP();
//		}
//		else if (string.Equals(args.purchasedProduct.definition.id, PID_SF_FIRE_DRAGON_MINIGUN_RHINO, StringComparison.Ordinal))
//		{
//			DebugIt(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//			Unlock_SF_FireDragon_Minigun_Rhino();
//			BoughtIAP();
//		}
//		else if (string.Equals(args.purchasedProduct.definition.id, PID_SF_FIRE_DRAGON_MINIGUN, StringComparison.Ordinal))
//		{
//			DebugIt(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//			Unlock_SF_FireDragon_Minigun();
//			BoughtIAP();
//		}
//		else if (string.Equals(args.purchasedProduct.definition.id, PID_SF_FIRE_DRAGON_RHINO, StringComparison.Ordinal))
//		{
//			DebugIt(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//			Unlock_SF_FireDragon_Rhino();
//			BoughtIAP();
//		}
//		else if (string.Equals(args.purchasedProduct.definition.id, PID_SF_GEM_5000, StringComparison.Ordinal))
//		{
//			DebugIt(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//			UnlockGems(5000);
//			BoughtIAP();
//		}
//		else if (string.Equals(args.purchasedProduct.definition.id, PID_SF_GEM_40000, StringComparison.Ordinal))
//		{
//			DebugIt(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//			UnlockGems(40000);
//			BoughtIAP();
//		}
//		else if (string.Equals(args.purchasedProduct.definition.id, PID_GUARDIAN, StringComparison.Ordinal))
//		{
//			DebugIt(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//			Unlock_Guardian();
//			BoughtIAP();
//		}
//		else if (string.Equals(args.purchasedProduct.definition.id, PID_COMBO_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON_MINIGUN_RHINO_GUARDIAN, StringComparison.Ordinal))
//		{
//			DebugIt(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//			Unlock_Combo_Spartan_Sentinel_Samurai_FireDragon_Minigun_Rhino_Guardian();
//			BoughtIAP();
//		}
//		else if (string.Equals(args.purchasedProduct.definition.id, PID_WOLF, StringComparison.Ordinal))
//		{
//			DebugIt(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//			Unlock_Wolf();
//			BoughtIAP();
//		}
//		else if (string.Equals(args.purchasedProduct.definition.id, PID_COMBO_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON_MINIGUN_RHINO_GUARDIAN_WOLF, StringComparison.Ordinal))
//		{
//			DebugIt(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//			Unlock_Combo_Spartan_Sentinel_Samurai_FireDragon_Minigun_Rhino_Guardian_Wolf();
//			BoughtIAP();
//		}
//		else if (string.Equals(args.purchasedProduct.definition.id, PID_MEMBERSHIP, StringComparison.Ordinal))
//		{
//			DebugIt(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//			try
//			{
//				CrossPlatformValidator crossPlatformValidator = new CrossPlatformValidator(GooglePlayTangle.Data(), AppleTangle.Data(), Application.identifier);
//				IPurchaseReceipt[] array = crossPlatformValidator.Validate(args.purchasedProduct.receipt);
//				int num = array.Length;
//				if (num > 0)
//				{
//					AppleInAppPurchaseReceipt appleInAppPurchaseReceipt = null;
//					AppleInAppPurchaseReceipt appleInAppPurchaseReceipt2 = null;
//					GooglePlayReceipt googlePlayReceipt = null;
//					for (int num2 = num - 1; num2 >= 0; num2--)
//					{
//						if (packagesManager.whichStore == App_Stores.Google)
//						{
//							googlePlayReceipt = array[num2] as GooglePlayReceipt;
//							if (googlePlayReceipt != null)
//							{
//								break;
//							}
//						}
//						else if (packagesManager.whichStore == App_Stores.IOS)
//						{
//							appleInAppPurchaseReceipt2 = array[num2] as AppleInAppPurchaseReceipt;
//							if (appleInAppPurchaseReceipt2 != null && (appleInAppPurchaseReceipt == null || appleInAppPurchaseReceipt2.purchaseDate.CompareTo(appleInAppPurchaseReceipt.purchaseDate) > 0))
//							{
//								appleInAppPurchaseReceipt = appleInAppPurchaseReceipt2;
//							}
//						}
//					}
//					if (packagesManager.whichStore == App_Stores.Google)
//					{
//						if (IsActiveSubscription_Google(googlePlayReceipt))
//						{
//							DebugIt("-----> Sub is Active");
//							SaveSubscriptionDate_Google(googlePlayReceipt.purchaseDate);
//							SaveSubscription_Google();
//						}
//						else
//						{
//							DebugIt("-----> Sub is NOT Active");
//							if (HasSubscription())
//							{
//								PlayerPrefs_Saves.SaveHasGoogleSubscription(false);
//								PlayerPrefs.Save();
//								EnableSubscription();
//							}
//						}
//					}
//					else if (packagesManager.whichStore == App_Stores.IOS)
//					{
//						if (IsActiveSubscription_Apple(appleInAppPurchaseReceipt))
//						{
//							DebugIt("-----> Sub is Active");
//							SaveSubscriptionDate_Apple(appleInAppPurchaseReceipt.subscriptionExpirationDate, appleInAppPurchaseReceipt.purchaseDate);
//						}
//						else
//						{
//							DebugIt("-----> Sub is NOT Active");
//							if (HasSubscription())
//							{
//								PlayerPrefs_Saves.SaveSubscriptionAppleExpirationDate(DateTime.Now.ToUniversalTime());
//								PlayerPrefs.Save();
//								EnableSubscription();
//							}
//						}
//					}
//				}
//			}
//			catch (Exception)
//			{
//			}
//		}
//		DebugIt("CURRENT IAP: " + args.purchasedProduct.definition.id);
//		DebugIt("ON THE LIST: ");
//		foreach (string item in clickedIAP)
//		{
//			DebugIt(item);
//		}
//		if (clickedIAP.Contains(args.purchasedProduct.definition.id))
//		{
//			DebugIt("SENDING TO APPSFLYER");
//			try
//			{
//				string price = args.purchasedProduct.metadata.localizedPrice.ToString();
//				string isoCurrencyCode = args.purchasedProduct.metadata.isoCurrencyCode;
//				Dictionary<string, string> extraParams = new Dictionary<string, string>();
//				string receipt = args.purchasedProduct.receipt;
//				Dictionary<string, object> dictionary = (Dictionary<string, object>)MiniJson.JsonDecode(receipt);
//				string json = (string)dictionary["Payload"];
//				Dictionary<string, object> dictionary2 = (Dictionary<string, object>)MiniJson.JsonDecode(json);
//				string purchaseData = (string)dictionary2["json"];
//				string signature = (string)dictionary2["signature"];
//				AppsFlyer.validateReceipt(google_public_key, purchaseData, signature, price, isoCurrencyCode, extraParams);
//			}
//			catch (Exception ex2)
//			{
//				DebugIt("RSTD ERROR APPSFLYER " + ex2.Message);
//			}
//			clickedIAP.Remove(args.purchasedProduct.definition.id);
//		}
//		return PurchaseProcessingResult.Complete;
//	}

//	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
//	{
//		DebugIt(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
//	}

//	public void RestorePurchases()
//	{
//		DebugIt("RestorePurchases");
//		RestorePurchasesLogic();
//	}

//	public void PurchaseNoAds()
//	{
//		DebugIt("PurchaseNoAds");
//		BuyProductID(PID_NOADS);
//	}

//	private void UnlockNoAds()
//	{
//		DebugIt("UnlockNoAds");
//		uimanager.UnlockNoAds(true);
//	}

//	private void BoughtIAP()
//	{
//		if (!PlayerPrefs_Saves.LoadHasBoughtIAP())
//		{
//			PlayerPrefs_Saves.SaveHasBoughtIAP(true);
//			PlayerPrefs.Save();
//		}
//	}

//	public void PurchaseGempack_2500()
//	{
//		DebugIt("PurchaseGempack_2500");
//		BuyProductID(PID_GEM_2500);
//	}

//	public void PurchaseGempack_7000()
//	{
//		DebugIt("PurchaseGempack_7000");
//		BuyProductID(PID_GEM_7000);
//	}

//	public void PurchaseGempack_15000()
//	{
//		DebugIt("PurchaseGempack_15000");
//		BuyProductID(PID_GEM_15000);
//	}

//	public void PurchaseGempack_50000()
//	{
//		DebugIt("PurchaseGempack_50000");
//		BuyProductID(PID_GEM_50000);
//	}

//	private void UnlockGems(int amount)
//	{
//		uimanager.UnlockGems(amount, true);
//	}

//	public void Purchase_Spartan()
//	{
//		DebugIt("Purchase_Spartan");
//		BuyProductID(PID_SPARTAN);
//	}

//	private void Unlock_Spartan()
//	{
//		DebugIt("Unlock_Spartan");
//		uimanager.UnlockSpartan(true);
//	}

//	public void Purchase_Sentinel()
//	{
//		DebugIt("Purchase_UnlockSentinel");
//		BuyProductID(PID_SENTINEL);
//	}

//	private void Unlock_Sentinel()
//	{
//		DebugIt("Unlock_UnlockSentinel");
//		uimanager.UnlockSentinel(true);
//	}

//	public void Purchase_Samurai()
//	{
//		DebugIt("Purchase_UnlockSamurai");
//		BuyProductID(PID_SAMURAI);
//	}

//	private void Unlock_Samurai()
//	{
//		DebugIt("Unlock_UnlockSamurai");
//		uimanager.UnlockSamurai(true);
//	}

//	public void Purchase_FireDragon()
//	{
//		DebugIt("Purchase_FireDragon");
//		BuyProductID(PID_FIREDRAGON);
//	}

//	private void Unlock_FireDragon()
//	{
//		DebugIt("Unlock_FireDragon");
//		uimanager.UnlockFireDragon(true);
//	}

//	public void Purchase_Minigun()
//	{
//		DebugIt("Purchase_Minigun");
//		BuyProductID(PID_MINIGUN);
//	}

//	private void Unlock_Minigun()
//	{
//		DebugIt("Unlock_Minigun");
//		uimanager.UnlockMinigun(true);
//	}

//	public void Purchase_Rhino()
//	{
//		DebugIt("Purchase_Rhino");
//		BuyProductID(PID_RHINO);
//	}

//	private void Unlock_Rhino()
//	{
//		DebugIt("Unlock_Rhino");
//		uimanager.UnlockRhino(true);
//	}

//	public void Purchase_Guardian()
//	{
//		DebugIt("Purchase_Guardian");
//		BuyProductID(PID_GUARDIAN);
//	}

//	private void Unlock_Guardian()
//	{
//		DebugIt("Unlock_Guardian");
//		uimanager.UnlockGuardian(true);
//	}

//	public void Purchase_Wolf()
//	{
//		DebugIt("Purchase_Wolf");
//		BuyProductID(PID_WOLF);
//	}

//	private void Unlock_Wolf()
//	{
//		DebugIt("Unlock_Wolf");
//		uimanager.UnlockWolf(true);
//	}

//	public void Purchase_Combo_Spartan_Sentinel()
//	{
//		DebugIt("Purchase_Combo_Spartan_Sentinel");
//		BuyProductID(PID_COMBO_SPARTAN_SENTINEL);
//	}

//	private void Unlock_Combo_Spartan_Sentinel()
//	{
//		DebugIt("Unlock_Combo_Spartan_Sentinel");
//		uimanager.Unlock_Spartan_Sentinel(true);
//	}

//	public void Purchase_Combo_Spartan_Sentinel_Samurai()
//	{
//		DebugIt("Purchase_Combo_Spartan_Sentinel_Samurai");
//		BuyProductID(PID_COMBO_SPARTAN_SENTINEL_SAMURAI);
//	}

//	private void Unlock_Combo_Spartan_Sentinel_Samurai()
//	{
//		DebugIt("Unlock_Combo_Spartan_Sentinel_Samurai");
//		uimanager.Unlock_Spartan_Sentinel_Samurai(true);
//	}

//	public void Purchase_Combo_Spartan_Sentinel_Samurai_FireDragon()
//	{
//		DebugIt("Purchase_Combo_Spartan_Sentinel_Samurai_FireDragon");
//		BuyProductID(PID_COMBO_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON);
//	}

//	private void Unlock_Combo_Spartan_Sentinel_Samurai_FireDragon()
//	{
//		DebugIt("Unlock_Combo_Spartan_Sentinel_Samurai_FireDragon");
//		uimanager.Unlock_Spartan_Sentinel_Samurai_FireDragon(true);
//	}

//	public void Purchase_Combo_Spartan_Sentinel_Samurai_FireDragon_Minigun()
//	{
//		DebugIt("Purchase_Combo_Spartan_Sentinel_Samurai_FireDragon_Minigun");
//		BuyProductID(PID_COMBO_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON_MINIGUN);
//	}

//	private void Unlock_Combo_Spartan_Sentinel_Samurai_FireDragon_Minigun()
//	{
//		DebugIt("Unlock_Combo_Spartan_Sentinel_Samurai_FireDragon_Minigun");
//		uimanager.Unlock_Spartan_Sentinel_Samurai_FireDragon_Minigun(true);
//	}

//	public void Purchase_Combo_Spartan_Sentinel_Samurai_FireDragon_Minigun_Rhino()
//	{
//		DebugIt("Purchase_Combo_Spartan_Sentinel_Samurai_FireDragon_Minigun_Rhino");
//		BuyProductID(PID_COMBO_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON_MINIGUN_RHINO);
//	}

//	private void Unlock_Combo_Spartan_Sentinel_Samurai_FireDragon_Minigun_Rhino()
//	{
//		DebugIt("Unlock_Combo_Spartan_Sentinel_Samurai_FireDragon_Minigun_Rhino");
//		uimanager.Unlock_Spartan_Sentinel_Samurai_FireDragon_Minigun_Rhino(true);
//	}

//	public void Purchase_Combo_Spartan_Sentinel_Samurai_FireDragon_Minigun_Rhino_Guardian()
//	{
//		DebugIt("Purchase_Combo_Spartan_Sentinel_Samurai_FireDragon_Minigun_Rhino_Guardian");
//		BuyProductID(PID_COMBO_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON_MINIGUN_RHINO_GUARDIAN);
//	}

//	private void Unlock_Combo_Spartan_Sentinel_Samurai_FireDragon_Minigun_Rhino_Guardian()
//	{
//		DebugIt("Unlock_Combo_Spartan_Sentinel_Samurai_FireDragon_Minigun_Rhino_Guardian");
//		uimanager.Unlock_Spartan_Sentinel_Samurai_FireDragon_Minigun_Rhino_Guardian(true);
//	}

//	public void Purchase_Combo_Spartan_Sentinel_Samurai_FireDragon_Minigun_Rhino_Guardian_Wolf()
//	{
//		DebugIt("Purchase_Combo_Spartan_Sentinel_Samurai_FireDragon_Minigun_Rhino_Guardian_Wolf");
//		BuyProductID(PID_COMBO_SPARTAN_SENTINEL_SAMURAI_FIRE_DRAGON_MINIGUN_RHINO_GUARDIAN_WOLF);
//	}

//	private void Unlock_Combo_Spartan_Sentinel_Samurai_FireDragon_Minigun_Rhino_Guardian_Wolf()
//	{
//		DebugIt("Unlock_Combo_Spartan_Sentinel_Samurai_FireDragon_Minigun_Rhino_Guardian_Wolf");
//		uimanager.Unlock_Spartan_Sentinel_Samurai_FireDragon_Minigun_Rhino_Guardian_Wolf(true);
//	}

//	public void Purchase_SF_FireDragon_Minigun_Rhino()
//	{
//		DebugIt("Purchase_SF_FireDragon_Minigun_Rhino");
//		BuyProductID(PID_SF_FIRE_DRAGON_MINIGUN_RHINO);
//	}

//	private void Unlock_SF_FireDragon_Minigun_Rhino()
//	{
//		DebugIt("Unlock_SF_FireDragon_Minigun_Rhino");
//		uimanager.Unlock_SF_FireDragon_Minigun_Rhino(true);
//	}

//	public void Purchase_SF_FireDragon_Minigun()
//	{
//		DebugIt("Purchase_SF_FireDragon_Minigun");
//		BuyProductID(PID_SF_FIRE_DRAGON_MINIGUN);
//	}

//	private void Unlock_SF_FireDragon_Minigun()
//	{
//		DebugIt("Unlock_SF_FireDragon_Minigun");
//		uimanager.Unlock_SF_FireDragon_Minigun(true);
//	}

//	public void Purchase_SF_FireDragon_Rhino()
//	{
//		DebugIt("Purchase_SF_FireDragon_Rhino");
//		BuyProductID(PID_SF_FIRE_DRAGON_RHINO);
//	}

//	private void Unlock_SF_FireDragon_Rhino()
//	{
//		DebugIt("Unlock_SF_FireDragon_Rhino");
//		uimanager.Unlock_SF_FireDragon_Rhino(true);
//	}

//	public void Purchase_SF_Gempack_5000()
//	{
//		DebugIt("Purchase_SF_Gempack_5000");
//		BuyProductID(PID_SF_GEM_5000);
//	}

//	public void Purchase_SF_Gempack_40000()
//	{
//		DebugIt("Purchase_SF_Gempack_40000");
//		BuyProductID(PID_SF_GEM_40000);
//	}

//	public void PurchaseMembership()
//	{
//		DebugIt("PurchaseMembership");
//		BuyProductID(PID_MEMBERSHIP);
//	}

//	private void PrintSubscription_Google(GooglePlayReceipt productReceipt)
//	{
//		DebugIt("1)" + productReceipt.packageName);
//		DebugIt("2)" + productReceipt.productID);
//		DebugIt("3)" + productReceipt.purchaseDate);
//		DebugIt("4)" + productReceipt.purchaseState);
//		DebugIt("5)" + productReceipt.purchaseToken);
//		DebugIt("6)" + productReceipt.transactionID);
//	}

//	private void PrintSubscription_Apple(AppleInAppPurchaseReceipt productReceipt)
//	{
//		DebugIt("1)" + productReceipt.originalTransactionIdentifier);
//		DebugIt("2)" + productReceipt.productID);
//		DebugIt("3)" + productReceipt.transactionID);
//		DebugIt(string.Concat("4)", productReceipt.cancellationDate, string.Empty));
//		DebugIt("5)" + productReceipt.isFreeTrial + string.Empty);
//		DebugIt("6)" + productReceipt.isIntroductoryPricePeriod + string.Empty);
//		DebugIt(string.Concat("7)", productReceipt.originalPurchaseDate, string.Empty));
//		DebugIt("8)" + productReceipt.productType + string.Empty);
//		DebugIt(string.Concat("9)", productReceipt.purchaseDate, string.Empty));
//		DebugIt("10)" + productReceipt.quantity + string.Empty);
//		DebugIt(string.Concat("11)", productReceipt.subscriptionExpirationDate, string.Empty));
//	}

//	private bool IsActiveSubscription_Google(GooglePlayReceipt receipt)
//	{
//		return receipt != null;
//	}

//	private bool IsActiveSubscription_Apple(AppleInAppPurchaseReceipt receipt)
//	{
//		if (receipt == null)
//		{
//			return false;
//		}
//		return receipt.subscriptionExpirationDate.CompareTo(DateTime.Now.ToUniversalTime()) > 0 && receipt.purchaseDate.CompareTo(receipt.cancellationDate) > 0;
//	}

//	private void SaveSubscription_Google()
//	{
//		if (!PlayerPrefs_Saves.LoadHasGoogleSubscription())
//		{
//			PlayerPrefs_Saves.SaveHasGoogleSubscription(true);
//			PlayerPrefs.Save();
//			EnableSubscription();
//		}
//	}

//	private void SaveSubscriptionDate_Google(DateTime purchaseDate)
//	{
//		PlayerPrefs_Saves.SaveSubscriptionPurchaseDate(purchaseDate);
//		PlayerPrefs_Saves.SaveSubscriptionCollectedDays(0);
//		PlayerPrefs.Save();
//	}

//	private void SaveSubscriptionDate_Apple(DateTime expirationDate, DateTime purchaseDate)
//	{
//		if (expirationDate.CompareTo(PlayerPrefs_Saves.LoadSubscriptionAppleExpirationDate()) > 0)
//		{
//			PlayerPrefs_Saves.SaveSubscriptionAppleExpirationDate(expirationDate);
//			PlayerPrefs_Saves.SaveSubscriptionPurchaseDate(purchaseDate);
//			PlayerPrefs_Saves.SaveSubscriptionCollectedDays(0);
//			PlayerPrefs.Save();
//			EnableSubscription();
//		}
//	}

//	public bool HasSubscription()
//	{
//		if (packagesManager != null)
//		{
//			if (packagesManager.whichStore == App_Stores.IOS)
//			{
//				return PlayerPrefs_Saves.LoadSubscriptionAppleExpirationDate().CompareTo(DateTime.Now.ToUniversalTime()) > 0;
//			}
//			if (packagesManager.whichStore == App_Stores.Google)
//			{
//				return PlayerPrefs_Saves.LoadHasGoogleSubscription();
//			}
//		}
//		return false;
//	}

//	private void EnableSubscription()
//	{
//		HAS_SUBSCRIPTION = HasSubscription();
//		if (HAS_SUBSCRIPTION)
//		{
//			uimanager.EnableVIP();
//		}
//		else
//		{
//			uimanager.DisableVIP();
//		}
//	}

//	public static void DebugIt(string msg)
//	{
//		if (showDebug)
//		{
//			Debug.Log("UIAP " + msg);
//		}
//	}
//}
