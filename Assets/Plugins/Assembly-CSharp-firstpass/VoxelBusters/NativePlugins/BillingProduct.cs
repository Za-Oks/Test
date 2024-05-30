using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace VoxelBusters.NativePlugins
{
	[Serializable]
	public class BillingProduct
	{
		[SerializeField]
		[Tooltip("The name of the product. This information is used for simulating feature behaviour in Editor.")]
		private string m_name;

		[SerializeField]
		[Tooltip("The description of the product. This information is used for simulating feature behaviour in Editor.")]
		private string m_description;

		[SerializeField]
		[Tooltip("The flag determines the product type. If enabled, product is identified as consumable.")]
		private bool m_isConsumable;

		[SerializeField]
		[HideInInspector]
		private float m_price;

		[Tooltip("Array of string values, where each value represents a product in a specific platform.")]
		[SerializeField]
		[FormerlySerializedAs("m_productIDs")]
		protected PlatformValue[] m_productIdentifiers;

		[SerializeField]
		[Tooltip("Optional extra data to let store to send back in purchase response. Used for security purposes and supported by Google's Android Platform only currently.")]
		private string m_developerPayload;

		[SerializeField]
		[HideInInspector]
		private string m_iosProductId;

		[SerializeField]
		[HideInInspector]
		private string m_androidProductId;

		public string Name
		{
			get
			{
				return m_name;
			}
			protected set
			{
				m_name = value;
			}
		}

		public string Description
		{
			get
			{
				return m_description;
			}
			protected set
			{
				m_description = value;
			}
		}

		public bool IsConsumable
		{
			get
			{
				return m_isConsumable;
			}
			protected set
			{
				m_isConsumable = value;
			}
		}

		public float Price
		{
			get
			{
				return m_price;
			}
			protected set
			{
				m_price = value;
			}
		}

		public string LocalizedPrice { get; protected set; }

		public string CurrencyCode { get; protected set; }

		public string CurrencySymbol { get; protected set; }

		public string ProductIdentifier
		{
			get
			{
				PlatformValue currentPlatformValue = PlatformValueHelper.GetCurrentPlatformValue(m_productIdentifiers);
				if (currentPlatformValue == null)
				{
					return null;
				}
				return currentPlatformValue.Value;
			}
		}

		public string DeveloperPayload
		{
			get
			{
				return m_developerPayload;
			}
			set
			{
				m_developerPayload = value;
			}
		}

		protected BillingProduct()
		{
		}

		protected BillingProduct(BillingProduct _product)
		{
			Name = _product.Name;
			Description = _product.Description;
			IsConsumable = _product.IsConsumable;
			Price = _product.Price;
			m_productIdentifiers = _product.m_productIdentifiers;
			LocalizedPrice = _product.LocalizedPrice;
			CurrencyCode = _product.CurrencyCode;
			CurrencySymbol = _product.CurrencySymbol;
		}

		public static BillingProduct Create(string _name, bool _isConsumable, params PlatformValue[] _productIDs)
		{
			BillingProduct billingProduct = new BillingProduct();
			billingProduct.Name = _name;
			billingProduct.IsConsumable = _isConsumable;
			billingProduct.m_productIdentifiers = _productIDs;
			return billingProduct;
		}

		public override string ToString()
		{
			return string.Format("[BillingProduct: Name={0}, ProductIdentifier={1}, IsConsumable={2}, LocalizedPrice={3}, CurrencyCode={4}, CurrencySymbol={5}]", Name, ProductIdentifier, IsConsumable, LocalizedPrice, CurrencyCode, CurrencySymbol);
		}

		internal void RebuildObject()
		{
			if (!string.IsNullOrEmpty(m_iosProductId) || !string.IsNullOrEmpty(m_androidProductId))
			{
				m_productIdentifiers = new PlatformValue[2]
				{
					PlatformValue.IOS(m_iosProductId),
					PlatformValue.Android(m_androidProductId)
				};
				m_iosProductId = null;
				m_androidProductId = null;
			}
		}
	}
}
