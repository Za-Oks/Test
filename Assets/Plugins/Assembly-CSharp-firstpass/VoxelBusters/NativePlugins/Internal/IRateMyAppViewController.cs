namespace VoxelBusters.NativePlugins.Internal
{
	public interface IRateMyAppViewController
	{
		void ShowDialog(string _title, string _message, string[] _buttons, ShowDialogResultDelegate _onCompletion);
	}
}
