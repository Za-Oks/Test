using System.Collections;
using System.Collections.Generic;
using VoxelBusters.Utility;

namespace VoxelBusters.NativePlugins.Internal
{
	public sealed class BillingProductIOS : BillingProductMutable
	{
		private const string kTitle = "localized-title";

		private const string kProductID = "product-identifier";

		private const string kDescription = "localized-description";

		private const string kPrice = "price";

		private const string kLocalizedPrice = "localized-price";

		private const string kCurrencyCode = "currency-code";

		private const string kCurrencySymbol = "currency-symbol";

		public BillingProductIOS(IDictionary _productJsonDict)
		{
			base.Name = _productJsonDict.GetIfAvailable<string>("localized-title");
			m_productIdentifiers = new PlatformValue[1] { PlatformValue.IOS(_productJsonDict.GetIfAvailable<string>("product-identifier")) };
			base.Description = _productJsonDict.GetIfAvailable<string>("localized-description");
			base.Price = _productJsonDict.GetIfAvailable("price", 0f);
			base.LocalizedPrice = _productJsonDict.GetIfAvailable<string>("localized-price");
			base.CurrencyCode = _productJsonDict.GetIfAvailable<string>("currency-code");
			base.CurrencySymbol = _productJsonDict.GetIfAvailable<string>("currency-symbol");
		}

		public static IDictionary CreateJSONObject(BillingProduct _product)
		{
			IDictionary dictionary = new Dictionary<string, string>();
			dictionary["localized-title"] = _product.Name;
			dictionary["product-identifier"] = _product.ProductIdentifier;
			dictionary["localized-description"] = _product.Description;
			dictionary["price"] = _product.Price.ToString();
			dictionary["localized-price"] = _product.LocalizedPrice;
			dictionary["currency-code"] = _product.CurrencyCode;
			dictionary["currency-symbol"] = _product.CurrencySymbol;
			return dictionary;
		}
	}
}
