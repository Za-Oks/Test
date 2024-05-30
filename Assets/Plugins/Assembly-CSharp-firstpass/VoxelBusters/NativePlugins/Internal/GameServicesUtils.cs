namespace VoxelBusters.NativePlugins.Internal
{
	public class GameServicesUtils
	{
		public static LeaderboardMetadata[] leaderboardMetadataCollection;

		public static AchievementMetadata[] achievementMetadataCollection;

		public static string GetLeaderboardID(string _globalID)
		{
			return GetPlatformID(leaderboardMetadataCollection, _globalID);
		}

		public static string GetLeaderboardGID(string _platformID)
		{
			return GetGlobalID(leaderboardMetadataCollection, _platformID);
		}

		public static string GetAchievementID(string _globalID)
		{
			return GetPlatformID(achievementMetadataCollection, _globalID);
		}

		public static string GetAchievementGID(string _platformID)
		{
			return GetGlobalID(achievementMetadataCollection, _platformID);
		}

		public static string GetPlatformID(IIdentifierContainer[] _collection, string _globalID)
		{
			IIdentifierContainer identifierContainer = _collection.FindObjectWithGlobalID(_globalID);
			if (identifierContainer == null)
			{
				return null;
			}
			return identifierContainer.GetCurrentPlatformID();
		}

		public static string GetGlobalID(IIdentifierContainer[] _collection, string _platformID)
		{
			IIdentifierContainer identifierContainer = _collection.FindObjectWithPlatformID(_platformID);
			if (identifierContainer == null)
			{
				return _platformID;
			}
			return identifierContainer.GlobalID;
		}
	}
}
