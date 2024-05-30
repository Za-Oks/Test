namespace VoxelBusters.NativePlugins.Internal
{
	public interface IRateMyAppEventResponder
	{
		void OnRemindMeLater();

		void OnRate();

		void OnDontShow();
	}
}
