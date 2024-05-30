using System.Collections;
using System.Collections.Generic;
using VoxelBusters.Utility;

namespace VoxelBusters.NativePlugins.Internal
{
	public sealed class BillingProductAndroid : BillingProductMutable
	{
		private const string kProductIdentifier = "product-identifier";

		private const string kLocalisedPrice = "localised-price";

		private const string kPriceAmount = "price-amount-micros";

		private const string kPriceCurrencyCode = "currency-code";

		private const string kPriceCurrencySymbol = "currency-symbol";

		private const string kName = "name";

		private const string kDescription = "description";

		public BillingProductAndroid(IDictionary _productJsonDict)
		{
			m_productIdentifiers = new PlatformValue[1] { PlatformValue.Android(_productJsonDict["product-identifier"] as string) };
			base.Name = _productJsonDict["name"] as string;
			base.Description = _productJsonDict["description"] as string;
			base.Price = (float)_productJsonDict.GetIfAvailable("price-amount-micros", 0L) / 1000000f;
			base.LocalizedPrice = _productJsonDict.GetIfAvailable<string>("localised-price");
			base.CurrencyCode = _productJsonDict.GetIfAvailable<string>("currency-code");
			base.CurrencySymbol = _productJsonDict.GetIfAvailable<string>("currency-symbol");
		}

		public static IDictionary CreateJSONObject(BillingProduct _product)
		{
			IDictionary dictionary = new Dictionary<string, string>();
			dictionary["product-identifier"] = _product.ProductIdentifier;
			dictionary["name"] = _product.Name;
			dictionary["description"] = _product.Description;
			dictionary["price-amount-micros"] = (_product.Price * 1000000f).ToString();
			dictionary["localised-price"] = _product.LocalizedPrice;
			dictionary["currency-code"] = _product.CurrencyCode;
			dictionary["currency-symbol"] = _product.CurrencySymbol;
			return dictionary;
		}
	}
}
