namespace VoxelBusters.NativePlugins.Internal
{
	public interface IRateMyAppKeysCollection
	{
		string IsFirstTimeLaunchKeyName { get; }

		string VersionLastRatedKeyName { get; }

		string ShowPromptAfterKeyName { get; }

		string PromptLastShownKeyName { get; }

		string DontShowKeyName { get; }

		string AppUsageCountKeyName { get; }
	}
}
