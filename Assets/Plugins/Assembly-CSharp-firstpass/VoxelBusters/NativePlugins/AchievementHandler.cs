using VoxelBusters.UASUtils;

namespace VoxelBusters.NativePlugins
{
	public class AchievementHandler
	{
		internal static AchievementDescription[] achievementDescriptionList;

		internal static int achievementDescriptionCount;

		internal static void SetAchievementDescriptionList(AchievementDescription[] _descriptionList)
		{
			if (_descriptionList == null)
			{
				achievementDescriptionList = null;
				achievementDescriptionCount = 0;
			}
			else
			{
				achievementDescriptionList = _descriptionList;
				achievementDescriptionCount = _descriptionList.Length;
			}
		}

		public static AchievementDescription GetAchievementDescriptionWithID(string _achievementID)
		{
			if (achievementDescriptionList == null)
			{
				DebugUtility.Logger.LogError("Native Plugins", "[GameServices] Please fetch achievement description list before accessing achievement properties.");
				return null;
			}
			for (int i = 0; i < achievementDescriptionCount; i++)
			{
				AchievementDescription achievementDescription = achievementDescriptionList[i];
				string identifier = achievementDescription.Identifier;
				if (identifier.Equals(_achievementID))
				{
					return achievementDescription;
				}
			}
			DebugUtility.Logger.LogError("Native Plugins", string.Format("[GameServices] Couldnt find achievement description with identifier= {0}.", _achievementID));
			return null;
		}

		public static AchievementDescription GetAchievementDescriptionWithGlobalID(string _achievementGID)
		{
			if (achievementDescriptionList == null)
			{
				DebugUtility.Logger.LogError("Native Plugins", "[GameServices] Please fetch achievement description list before accessing achievement properties.");
				return null;
			}
			for (int i = 0; i < achievementDescriptionCount; i++)
			{
				AchievementDescription achievementDescription = achievementDescriptionList[i];
				string globalIdentifier = achievementDescription.GlobalIdentifier;
				if (globalIdentifier.Equals(_achievementGID))
				{
					return achievementDescription;
				}
			}
			DebugUtility.Logger.LogError("Native Plugins", string.Format("[GameServices] Couldnt find achievement description with global identifier= {0}.", _achievementGID));
			return null;
		}
	}
}
