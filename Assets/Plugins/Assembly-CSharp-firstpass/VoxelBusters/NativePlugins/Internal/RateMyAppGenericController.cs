namespace VoxelBusters.NativePlugins.Internal
{
	public class RateMyAppGenericController : RateMyAppGenericStoreBase, IRateMyAppViewController, IRateMyAppEventResponder
	{
		public void ShowDialog(string _title, string _message, string[] _buttons, ShowDialogResultDelegate _onCompletion)
		{
			NPBinding.UI.ShowAlertDialogWithMultipleButtons(_title, _message, _buttons, delegate(string _buttonPressed)
			{
				if (_onCompletion != null)
				{
					_onCompletion(_buttonPressed);
				}
			});
		}

		public void OnRemindMeLater()
		{
		}

		public void OnRate()
		{
			NPBinding.Utility.OpenStoreLink(NPSettings.Application.StoreIdentifier);
		}

		public void OnDontShow()
		{
		}
	}
}
