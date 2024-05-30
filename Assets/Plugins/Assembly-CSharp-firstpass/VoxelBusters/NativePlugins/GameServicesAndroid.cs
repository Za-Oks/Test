using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.NativePlugins.Internal;
using VoxelBusters.Utility;

namespace VoxelBusters.NativePlugins
{
	public sealed class GameServicesAndroid : GameServices
	{
		internal class Native
		{
			internal class Class
			{
				internal const string NAME = "com.voxelbusters.nativeplugins.features.gameservices.GameServicesHandler";
			}

			internal class Methods
			{
				internal const string IS_SERVICE_AVAILABLE = "isServiceAvailable";

				internal const string REGISTER_SERVICE = "register";

				internal const string REPORT_PROGRESS = "reportProgress";

				internal const string LOAD_ACHIEVEMENT_DESCRIPTIONS = "loadAchievementDescriptions";

				internal const string LOAD_ACHIEVEMENTS = "loadAchievements";

				internal const string SHOW_ACHIEVEMENTS_UI = "showAchievementsUi";

				internal const string GET_ACHIEVEMENT_DATA = "getAchievement";

				internal const string LOAD_TOP_SCORES = "loadTopScores";

				internal const string LOAD_PLAYER_CENTERED_SCORES = "loadPlayerCenteredScores";

				internal const string LOAD_MORE_SCORES = "loadMoreScores";

				internal const string REPORT_SCORE = "reportScore";

				internal const string SHOW_LEADERBOARD_UI = "showLeaderboardsUi";

				internal const string LOAD_USERS = "loadUsers";

				internal const string LOAD_LOCAL_USER_FRIENDS = "loadLocalUserFriends";

				internal const string AUTHENTICATE_LOCAL_USER = "authenticateLocalUser";

				internal const string IS_LOCAL_USER_AUTHENTICATED = "isSignedIn";

				internal const string SIGN_OUT_LOCAL_USER = "signOut";

				internal const string LOAD_PROFILE_PICTURE = "loadProfilePicture";

				internal const string SET_SHOW_DEFAULT_ERROR_DIALOGS = "setShowDefaultErrorDialogs";

				internal const string LOAD_EXTERNAL_AUTHENTICATION_DETAILS = "loadExternalAuthenticationDetails";
			}
		}

		private AndroidLocalUser m_localUser;

		public const string kNativeMessageError = "error";

		public const string kObjectInstanceID = "instance-id";

		public const string kImagePath = "image-path";

		private const string kAchievementsList = "achievements-list";

		private const string kUsersList = "users-list";

		private const string kCredentialsData = "credentials-data";

		public override LocalUser LocalUser
		{
			get
			{
				return m_localUser;
			}
			protected set
			{
				m_localUser = value as AndroidLocalUser;
			}
		}

		internal static AndroidJavaObject Plugin { get; set; }

		private GameServicesAndroid()
		{
			Plugin = AndroidPluginUtility.GetSingletonInstance("com.voxelbusters.nativeplugins.features.gameservices.GameServicesHandler");
		}

		protected override void Awake()
		{
			base.Awake();
			LocalUser = new AndroidLocalUser();
			Plugin.Call("register", NPSettings.Application.SupportedFeatures.UsesCloudServices, NPSettings.GameServicesSettings.Android.AllowAutoLogin);
			Plugin.Call("setShowDefaultErrorDialogs", NPSettings.GameServicesSettings.Android.ShowDefaultErrorDialogs);
		}

		public override bool IsAvailable()
		{
			return Plugin != null && Plugin.Call<bool>("isServiceAvailable", new object[0]);
		}

		protected override Leaderboard CreateLeaderboard(string _leaderboarGID, string _leaderboardID)
		{
			if (!VerifyUser())
			{
				return null;
			}
			if (string.IsNullOrEmpty(_leaderboardID))
			{
				return null;
			}
			return new AndroidLeaderboard(_leaderboarGID, _leaderboardID);
		}

		protected override Achievement CreateAchievement(string _achievementGID, string _achievementID)
		{
			if (!VerifyUser())
			{
				return null;
			}
			if (string.IsNullOrEmpty(_achievementID))
			{
				return null;
			}
			return new AndroidAchievement(_achievementGID, _achievementID);
		}

		protected override void LoadAchievementDescriptions(bool _needsVerification, AchievementDescription.LoadAchievementDescriptionsCompletion _onCompletion)
		{
			base.LoadAchievementDescriptions(_needsVerification, _onCompletion);
			if (!_needsVerification || VerifyUser())
			{
				Plugin.Call("loadAchievementDescriptions");
			}
		}

		public override void LoadAchievements(Achievement.LoadAchievementsCompletion _onCompletion)
		{
			base.LoadAchievements(_onCompletion);
			if (VerifyUser())
			{
				Plugin.Call("loadAchievements");
			}
		}

		public override void LoadUsers(string[] _userIDs, User.LoadUsersCompletion _onCompletion)
		{
			base.LoadUsers(_userIDs, _onCompletion);
			if (_userIDs != null && VerifyUser())
			{
				string text = _userIDs.ToJSON();
				Plugin.Call("loadUsers", GetInstanceID().ToString(), text);
			}
		}

		protected override Score CreateScoreForLocalUser(string _leaderboardGID, string _leaderboardID)
		{
			if (!VerifyUser())
			{
				return null;
			}
			if (string.IsNullOrEmpty(_leaderboardID))
			{
				return null;
			}
			return new AndroidScore(_leaderboardGID, _leaderboardID, LocalUser, 0L);
		}

		public override void ShowAchievementsUI(GameServiceViewClosed _onCompletion)
		{
			base.ShowAchievementsUI(_onCompletion);
			if (VerifyUser())
			{
				Plugin.Call("showAchievementsUi");
			}
		}

		public override void ShowLeaderboardUIWithID(string _leaderboardID, eLeaderboardTimeScope _timeScope, GameServiceViewClosed _onCompletion)
		{
			base.ShowLeaderboardUIWithID(_leaderboardID, _timeScope, _onCompletion);
			if (VerifyUser())
			{
				string key = AndroidLeaderboard.kTimeScopeMap.GetKey(_timeScope);
				Plugin.Call("showLeaderboardsUi", _leaderboardID, key);
			}
		}

		public override void LoadExternalAuthenticationCredentials(LoadExternalAuthenticationCredentialsCompletion _onCompletion)
		{
			base.LoadExternalAuthenticationCredentials(_onCompletion);
			if (VerifyUser())
			{
				Plugin.Call("loadExternalAuthenticationDetails", NPSettings.GameServicesSettings.Android.ServerClientID);
			}
		}

		protected override void LoadScoresFinished(IDictionary _dataDict)
		{
			string ifAvailable = _dataDict.GetIfAvailable<string>("instance-id");
			LoadScoresFinished(ifAvailable, _dataDict);
		}

		protected override void LoadAchievementDescriptionsFinished(IDictionary _dataDict)
		{
			IList ifAvailable = _dataDict.GetIfAvailable<List<object>>("achievements-list");
			string ifAvailable2 = _dataDict.GetIfAvailable<string>("error");
			AchievementDescription[] descriptions = null;
			Debug.Log("LoadAchievementDescriptionsFinished [IDictionary] " + ifAvailable.ToJSON());
			if (ifAvailable != null)
			{
				descriptions = AndroidAchievementDescription.ConvertAchievementDescriptionList(ifAvailable);
			}
			LoadAchievementDescriptionsFinished(descriptions, ifAvailable2);
		}

		protected override void RequestForAchievementImageFinished(IDictionary _dataDict)
		{
			string ifAvailable = _dataDict.GetIfAvailable<string>("instance-id");
			RequestForAchievementImageFinished(ifAvailable, _dataDict);
		}

		protected override void LoadAchievementsFinished(IDictionary _dataDict)
		{
			IList ifAvailable = _dataDict.GetIfAvailable<IList>("achievements-list");
			string ifAvailable2 = _dataDict.GetIfAvailable<string>("error");
			Achievement[] achievements = null;
			if (ifAvailable != null)
			{
				achievements = AndroidAchievement.ConvertAchievementList(ifAvailable);
			}
			LoadAchievementsFinished(achievements, ifAvailable2);
		}

		protected override void ReportProgressFinished(IDictionary _dataDict)
		{
			string ifAvailable = _dataDict.GetIfAvailable<string>("instance-id");
			ReportProgressFinished(ifAvailable, _dataDict);
		}

		protected override void LoadUsersFinished(IDictionary _dataDict)
		{
			IList ifAvailable = _dataDict.GetIfAvailable<IList>("users-list");
			string ifAvailable2 = _dataDict.GetIfAvailable<string>("error");
			User[] users = null;
			if (ifAvailable != null)
			{
				users = AndroidUser.ConvertToUserList(ifAvailable);
			}
			LoadUsersFinished(users, ifAvailable2);
		}

		protected override void RequestForUserImageFinished(IDictionary _dataDict)
		{
			string ifAvailable = _dataDict.GetIfAvailable<string>("instance-id");
			RequestForUserImageFinished(ifAvailable, _dataDict);
		}

		protected override void ReportScoreFinished(IDictionary _dataDict)
		{
			string ifAvailable = _dataDict.GetIfAvailable<string>("instance-id");
			ReportScoreFinished(ifAvailable, _dataDict);
		}

		private void UserDisconnected(string _reason)
		{
			Debug.Log("User disconnected! " + _reason);
		}

		protected override void LoadExternalAuthenticationCredentialsFinished(IDictionary _dataDict)
		{
			IDictionary ifAvailable = _dataDict.GetIfAvailable<IDictionary>("credentials-data");
			string ifAvailable2 = _dataDict.GetIfAvailable<string>("error");
			LoadExternalAuthenticationCredentialsFinished(ifAvailable, ifAvailable2);
		}
	}
}
