using System;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.NativePlugins
{
	[Serializable]
	public class BillingSettings
	{
		[Serializable]
		public class AndroidSettings
		{
			[SerializeField]
			[Tooltip("The public key provided by Google Play service for In-app Billing.")]
			private string m_publicKey;

			public string PublicKey
			{
				get
				{
					return m_publicKey;
				}
			}
		}

		[Serializable]
		public class iOSSettings
		{
			[SerializeField]
			[Tooltip("If enabled, payment receipts are validated before sending events. It's an optional measure used to avoid unauthorized puchases.")]
			private bool m_supportsReceiptValidation = true;

			[SerializeField]
			[Tooltip("Custom server URL used for receipt validation. By default, Apple server is used.")]
			private string m_validateUsingServerURL;

			internal bool SupportsReceiptValidation
			{
				get
				{
					return m_supportsReceiptValidation;
				}
			}

			internal bool MakeCopyOfBuildInfo
			{
				get
				{
					return false;
				}
			}

			internal string ValidateUsingServerURL
			{
				get
				{
					return m_validateUsingServerURL;
				}
			}
		}

		[SerializeField]
		[Tooltip("An array of billing products registered in Store.")]
		private List<BillingProduct> m_products;

		[SerializeField]
		private iOSSettings m_iOS;

		[SerializeField]
		private AndroidSettings m_android;

		public BillingProduct[] Products
		{
			get
			{
				if (m_products.Count == 0)
				{
					return null;
				}
				return m_products.ToArray();
			}
			private set
			{
				m_products = new List<BillingProduct>(value);
			}
		}

		public iOSSettings iOS
		{
			get
			{
				return m_iOS;
			}
			private set
			{
				m_iOS = value;
			}
		}

		public AndroidSettings Android
		{
			get
			{
				return m_android;
			}
			private set
			{
				m_android = value;
			}
		}

		public BillingSettings()
		{
			Products = new BillingProduct[0];
			iOS = new iOSSettings();
			Android = new AndroidSettings();
		}
	}
}
