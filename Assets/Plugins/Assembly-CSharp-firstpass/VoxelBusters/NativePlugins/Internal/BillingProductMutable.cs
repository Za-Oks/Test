namespace VoxelBusters.NativePlugins.Internal
{
	public class BillingProductMutable : BillingProduct
	{
		protected BillingProductMutable()
		{
		}

		protected BillingProductMutable(BillingProduct _product)
			: base(_product)
		{
		}

		public void SetIsConsumable(bool _isConsumable)
		{
			base.IsConsumable = _isConsumable;
		}

		public void SetLocalizePrice(string _locPrice)
		{
			base.LocalizedPrice = _locPrice;
		}

		public void SetCurrencyCode(string _code)
		{
			base.CurrencyCode = _code;
		}

		public void SetCurrencySymbol(string _symbol)
		{
			base.CurrencySymbol = _symbol;
		}
	}
}
