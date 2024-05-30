using System;
using System.Collections;
using UnityEngine;
using VoxelBusters.NativePlugins.Internal;
using VoxelBusters.UASUtils;
using VoxelBusters.Utility;

namespace VoxelBusters.NativePlugins
{
	public class GameServices : MonoBehaviour
	{
		public delegate void GameServiceViewClosed(string _error);

		public delegate void LoadExternalAuthenticationCredentialsCompletion(ExternalAuthenticationCredentials _credentials, string _error);

		private const string kLocalUserAuthFinishedEvent = "AuthenticationFinished";

		private const string kLocalUserLoadFriendsFinishedEvent = "LoadFriendsFinished";

		private const string kLocalUserSignOutFinishedEvent = "SignOutFinished";

		private const string kLeaderboardLoadScoresFinishedEvent = "LoadScoresFinished";

		private const string kAchievementReportProgressFinishedEvent = "ReportProgressFinished";

		private const string kDescriptionRequestForImageFinishedEvent = "RequestForImageFinished";

		private const string kScoreReportScoreFinished = "ReportScoreFinished";

		private const string kUserRequestForImageFinished = "RequestForImageFinished";

		public virtual LocalUser LocalUser
		{
			get
			{
				return null;
			}
			protected set
			{
				throw new Exception("[GameServices] Only getter is supported.");
			}
		}

		private event Achievement.LoadAchievementsCompletion LoadAchievementsFinishedEvent;

		private event AchievementDescription.LoadAchievementDescriptionsCompletion LoadAchievementDescriptionsFinishedEvent;

		private event User.LoadUsersCompletion LoadUsersFinishedEvent;

		private event GameServiceViewClosed ShowLeaderboardViewFinishedEvent;

		private event GameServiceViewClosed ShowAchievementViewFinishedEvent;

		private event LoadExternalAuthenticationCredentialsCompletion LoadExternalAuthenticationCredentialsFinishedEvent;

		protected virtual void Awake()
		{
			SetLeaderboardMetadataCollection(NPSettings.GameServicesSettings.LeaderboardMetadataCollection);
			SetAchievementMetadataCollection(NPSettings.GameServicesSettings.AchievementMetadataCollection);
		}

		public virtual bool IsAvailable()
		{
			return false;
		}

		public void SetLeaderboardMetadataCollection(params LeaderboardMetadata[] _collection)
		{
			GameServicesUtils.leaderboardMetadataCollection = _collection;
		}

		public Leaderboard CreateLeaderboardWithID(string _leaderboardID)
		{
			string leaderboardGID = GameServicesUtils.GetLeaderboardGID(_leaderboardID);
			return CreateLeaderboard(leaderboardGID, _leaderboardID);
		}

		public Leaderboard CreateLeaderboardWithGlobalID(string _leaderboardGID)
		{
			string leaderboardID = GameServicesUtils.GetLeaderboardID(_leaderboardGID);
			return CreateLeaderboard(_leaderboardGID, leaderboardID);
		}

		protected virtual Leaderboard CreateLeaderboard(string _leaderboardGID, string _leaderboardID)
		{
			return null;
		}

		public void LoadAchievementDescriptions(AchievementDescription.LoadAchievementDescriptionsCompletion _onCompletion)
		{
			LoadAchievementDescriptions(true, _onCompletion);
		}

		protected virtual void LoadAchievementDescriptions(bool _needsVerification, AchievementDescription.LoadAchievementDescriptionsCompletion _onCompletion)
		{
			this.LoadAchievementDescriptionsFinishedEvent = _onCompletion;
			if (_needsVerification && !VerifyUser())
			{
				LoadAchievementDescriptionsFinished(null, "The requested operation could not be completed because local player has not been authenticated.");
			}
		}

		public void SetAchievementMetadataCollection(params AchievementMetadata[] _collection)
		{
			GameServicesUtils.achievementMetadataCollection = _collection;
		}

		public Achievement CreateAchievementWithID(string _achievementID)
		{
			string achievementGID = GameServicesUtils.GetAchievementGID(_achievementID);
			return CreateAchievement(achievementGID, _achievementID);
		}

		public Achievement CreateAchievementWithGlobalID(string _achievementGID)
		{
			string achievementID = GameServicesUtils.GetAchievementID(_achievementGID);
			return CreateAchievement(_achievementGID, achievementID);
		}

		protected virtual Achievement CreateAchievement(string _achievementGID, string _achievementID)
		{
			return null;
		}

		public virtual void LoadAchievements(Achievement.LoadAchievementsCompletion _onCompletion)
		{
			this.LoadAchievementsFinishedEvent = _onCompletion;
			if (!VerifyUser())
			{
				LoadAchievementsFinished(null, "The requested operation could not be completed because local player has not been authenticated.");
			}
		}

		public void ReportProgressWithID(string _achievementID, double _percentageCompleted, Achievement.ReportProgressCompletion _onCompletion)
		{
			string achievementGID = GameServicesUtils.GetAchievementGID(_achievementID);
			ReportProgress(achievementGID, _achievementID, _percentageCompleted, _onCompletion);
		}

		public void ReportProgressWithGlobalID(string _achievementGID, double _percentageCompleted, Achievement.ReportProgressCompletion _onCompletion)
		{
			string achievementID = GameServicesUtils.GetAchievementID(_achievementGID);
			ReportProgress(_achievementGID, achievementID, _percentageCompleted, _onCompletion);
		}

		private void ReportProgress(string _achievementGID, string _achievementID, double _percentageCompleted, Achievement.ReportProgressCompletion _onCompletion)
		{
			Achievement achievement = CreateAchievement(_achievementGID, _achievementID);
			if (achievement == null)
			{
				DebugUtility.Logger.LogError("Native Plugins", "[GameServices] Failed to report progress.");
				if (_onCompletion != null)
				{
					_onCompletion(false, "The requested operation could not be completed because Game Service failed to create Achievement object.");
				}
			}
			else
			{
				achievement.PercentageCompleted = _percentageCompleted;
				achievement.ReportProgress(_onCompletion);
			}
		}

		public int GetNoOfStepsForCompletingAchievement(string _achievementGID)
		{
			AchievementMetadata achievementMetadata = (AchievementMetadata)GameServicesUtils.achievementMetadataCollection.FindObjectWithGlobalID(_achievementGID);
			if (achievementMetadata == null)
			{
				return -1;
			}
			return achievementMetadata.NoOfSteps;
		}

		public virtual void LoadUsers(string[] _userIDs, User.LoadUsersCompletion _onCompletion)
		{
			this.LoadUsersFinishedEvent = _onCompletion;
			if (!VerifyUser())
			{
				LoadUsersFinished(null, "The requested operation could not be completed because local player has not been authenticated.");
			}
			else if (_userIDs == null)
			{
				DebugUtility.Logger.LogError("Native Plugins", "[GameServices] UserID list is null.");
				LoadUsersFinished(null, "The requested operation could not be completed because user id list is null.");
			}
		}

		protected virtual Score CreateScoreForLocalUser(string _leaderboardGID, string _leaderboardID)
		{
			return null;
		}

		public void ReportScoreWithID(string _leaderboardID, long _score, Score.ReportScoreCompletion _onCompletion)
		{
			string leaderboardGID = GameServicesUtils.GetLeaderboardGID(_leaderboardID);
			ReportScore(leaderboardGID, _leaderboardID, _score, _onCompletion);
		}

		public void ReportScoreWithGlobalID(string _leaderboardGID, long _score, Score.ReportScoreCompletion _onCompletion)
		{
			string leaderboardID = GameServicesUtils.GetLeaderboardID(_leaderboardGID);
			ReportScore(_leaderboardGID, leaderboardID, _score, _onCompletion);
		}

		private void ReportScore(string _leaderboardGID, string _leaderboardID, long _score, Score.ReportScoreCompletion _onCompletion)
		{
			Score score = CreateScoreForLocalUser(_leaderboardGID, _leaderboardID);
			if (score == null)
			{
				DebugUtility.Logger.LogError("Native Plugins", "[GameServices] Failed to report score.");
				if (_onCompletion != null)
				{
					_onCompletion(false, "The requested operation could not be completed because Game Service failed to create Score object.");
				}
			}
			else
			{
				score.Value = _score;
				score.ReportScore(_onCompletion);
			}
		}

		public virtual void ShowAchievementsUI(GameServiceViewClosed _onCompletion)
		{
			this.ShowAchievementViewFinishedEvent = _onCompletion;
			this.PauseUnity();
			if (!VerifyUser())
			{
				ShowAchievementViewFinished("The requested operation could not be completed because local player has not been authenticated.");
			}
		}

		public virtual void ShowLeaderboardUIWithID(string _leaderboardID, eLeaderboardTimeScope _timeScope, GameServiceViewClosed _onCompletion)
		{
			this.ShowLeaderboardViewFinishedEvent = _onCompletion;
			this.PauseUnity();
			if (!VerifyUser())
			{
				ShowLeaderboardViewFinished("The requested operation could not be completed because local player has not been authenticated.");
			}
		}

		public void ShowLeaderboardUIWithGlobalID(string _leaderboardGID, eLeaderboardTimeScope _timeScope, GameServiceViewClosed _onCompletion)
		{
			string leaderboardID = GameServicesUtils.GetLeaderboardID(_leaderboardGID);
			ShowLeaderboardUIWithID(leaderboardID, _timeScope, _onCompletion);
		}

		protected bool VerifyUser()
		{
			if (LocalUser.IsAuthenticated)
			{
				return true;
			}
			DebugUtility.Logger.LogError("Native Plugins", "[GameServices] User not authenticated.");
			return false;
		}

		public virtual void LoadExternalAuthenticationCredentials(LoadExternalAuthenticationCredentialsCompletion _onCompletion)
		{
			this.LoadExternalAuthenticationCredentialsFinishedEvent = _onCompletion;
			if (!VerifyUser())
			{
				LoadExternalAuthenticationCredentialsFinished(null, "The requested operation could not be completed because local player has not been authenticated.");
			}
		}

		[Obsolete("This method is deprecated. Instead use SetLeaderboardMetadataCollection.")]
		public void SetLeaderboardIDCollection(params IDContainer[] _idCollection)
		{
			int num = _idCollection.Length;
			LeaderboardMetadata[] array = new LeaderboardMetadata[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = LeaderboardMetadata.Create(_idCollection[i]);
			}
			SetLeaderboardMetadataCollection(array);
		}

		[Obsolete("This method is deprecated. Instead use CreateLeaderboardWithID.")]
		public Leaderboard CreateLeaderboard(string _leaderboardID)
		{
			return CreateLeaderboardWithID(_leaderboardID);
		}

		[Obsolete("This method is deprecated. Instead use SetAchievementMetadataCollection.")]
		public void SetAchievementIDCollection(params IDContainer[] _idCollection)
		{
			int num = _idCollection.Length;
			AchievementMetadata[] array = new AchievementMetadata[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = AchievementMetadata.Create(_idCollection[i]);
			}
			SetAchievementMetadataCollection(array);
		}

		[Obsolete("This method is deprecated. Instead use CreateAchievementWithID.")]
		public Achievement CreateAchievement(string _achievementID)
		{
			return CreateAchievementWithID(_achievementID);
		}

		[Obsolete("This method is deprecated.  Instead use ReportProgressWithID.")]
		public void ReportProgress(string _achievementID, int _pointsScored, Achievement.ReportProgressCompletion _onCompletion)
		{
			ReportProgressWithID(_achievementID, _pointsScored, _onCompletion);
		}

		[Obsolete("This method is deprecated. Use ReportProgressWithID which takes percentage value as progress.")]
		public void ReportProgressWithID(string _achievementID, int _pointsScored, Achievement.ReportProgressCompletion _onCompletion)
		{
			string achievementGID = GameServicesUtils.GetAchievementGID(_achievementID);
			ReportProgress(achievementGID, _achievementID, _pointsScored, _onCompletion);
		}

		[Obsolete("This method is deprecated. Use ReportProgressWithGlobalID which takes percentage value as progress.")]
		public void ReportProgressWithGlobalID(string _achievementGID, int _pointsScored, Achievement.ReportProgressCompletion _onCompletion)
		{
			string achievementID = GameServicesUtils.GetAchievementID(_achievementGID);
			ReportProgress(_achievementGID, achievementID, _pointsScored, _onCompletion);
		}

		private void ReportProgress(string _achievementGID, string _achievementID, int _pointsScored, Achievement.ReportProgressCompletion _onCompletion)
		{
			AchievementDescription achievementDescriptionWithGlobalID = AchievementHandler.GetAchievementDescriptionWithGlobalID(_achievementGID);
			if (achievementDescriptionWithGlobalID == null)
			{
				DebugUtility.Logger.LogError("Native Plugins", "[GameServices] Failed to report progress.");
				if (_onCompletion != null)
				{
					_onCompletion(false, "The requested operation could not be completed because Game Services couldn't find description for given Achievement identifier.");
				}
				return;
			}
			Achievement achievement = CreateAchievement(_achievementGID, _achievementID);
			if (achievement == null)
			{
				DebugUtility.Logger.LogError("Native Plugins", "[GameServices] Failed to report progress.");
				if (_onCompletion != null)
				{
					_onCompletion(false, "The requested operation could not be completed because Game Service failed to create Achievement object.");
				}
			}
			else
			{
				achievement.PercentageCompleted = (double)_pointsScored / (double)achievementDescriptionWithGlobalID.MaximumPoints * 100.0;
				achievement.ReportProgress(_onCompletion);
			}
		}

		[Obsolete("This method is deprecated. Instead use ReportScoreWithID.")]
		public void ReportScore(string _leaderboardID, long _score, Score.ReportScoreCompletion _onCompletion)
		{
			ReportScoreWithID(_leaderboardID, _score, _onCompletion);
		}

		[Obsolete("This method is deprecated. Instead use ShowLeaderboardUIWithID.")]
		public void ShowLeaderboardUI(string _leaderboardID, eLeaderboardTimeScope _timeScope, GameServiceViewClosed _onCompletion)
		{
			ShowLeaderboardUIWithID(_leaderboardID, _timeScope, _onCompletion);
		}

		private void AuthenticationFinished(string _dataStr)
		{
			IDictionary dataDict = (IDictionary)JSONUtility.FromJSON(_dataStr);
			AuthenticationFinished(dataDict);
		}

		private void AuthenticationFinished(IDictionary _dataDict)
		{
			LocalUser localUser = LocalUser;
			localUser.InvokeMethod("AuthenticationFinished", new object[1] { _dataDict }, new Type[1] { typeof(IDictionary) });
		}

		private void LoadFriendsFinished(string _dataStr)
		{
			IDictionary dataDict = (IDictionary)JSONUtility.FromJSON(_dataStr);
			LoadFriendsFinished(dataDict);
		}

		private void LoadFriendsFinished(IDictionary _dataDict)
		{
			LocalUser localUser = LocalUser;
			localUser.InvokeMethod("LoadFriendsFinished", new object[1] { _dataDict }, new Type[1] { typeof(IDictionary) });
		}

		private void SignOutFinished(string _dataStr)
		{
			IDictionary dataDict = (IDictionary)JSONUtility.FromJSON(_dataStr);
			SignOutFinished(dataDict);
		}

		private void SignOutFinished(IDictionary _dataDict)
		{
			LocalUser localUser = LocalUser;
			localUser.InvokeMethod("SignOutFinished", new object[1] { _dataDict }, new Type[1] { typeof(IDictionary) });
		}

		protected void LoadScoresFinished(string _dataStr)
		{
			IDictionary dataDict = (IDictionary)JSONUtility.FromJSON(_dataStr);
			LoadScoresFinished(dataDict);
		}

		protected virtual void LoadScoresFinished(IDictionary _dataDict)
		{
		}

		protected void LoadScoresFinished(string _instanceID, IDictionary _dataDict)
		{
			Leaderboard objectWithInstanceID = NPObjectManager.GetObjectWithInstanceID<Leaderboard>(_instanceID, NPObjectManager.eCollectionType.GAME_SERVICES);
			if (objectWithInstanceID != null)
			{
				objectWithInstanceID.InvokeMethod("LoadScoresFinished", new object[1] { _dataDict }, new Type[1] { typeof(IDictionary) });
			}
		}

		protected void LoadAchievementDescriptionsFinished(string _dataStr)
		{
			IDictionary dataDict = (IDictionary)JSONUtility.FromJSON(_dataStr);
			LoadAchievementDescriptionsFinished(dataDict);
		}

		protected virtual void LoadAchievementDescriptionsFinished(IDictionary _dataDict)
		{
		}

		protected void LoadAchievementDescriptionsFinished(AchievementDescription[] _descriptions, string _error)
		{
			AchievementHandler.SetAchievementDescriptionList(_descriptions);
			if (this.LoadAchievementDescriptionsFinishedEvent != null)
			{
				this.LoadAchievementDescriptionsFinishedEvent(_descriptions, _error);
			}
		}

		protected void RequestForAchievementImageFinished(string _dataStr)
		{
			IDictionary dataDict = (IDictionary)JSONUtility.FromJSON(_dataStr);
			RequestForAchievementImageFinished(dataDict);
		}

		protected virtual void RequestForAchievementImageFinished(IDictionary _dataDict)
		{
		}

		protected void RequestForAchievementImageFinished(string _instanceID, IDictionary _dataDict)
		{
			AchievementDescription objectWithInstanceID = NPObjectManager.GetObjectWithInstanceID<AchievementDescription>(_instanceID, NPObjectManager.eCollectionType.GAME_SERVICES);
			if (objectWithInstanceID != null)
			{
				objectWithInstanceID.InvokeMethod("RequestForImageFinished", new object[1] { _dataDict }, new Type[1] { typeof(IDictionary) });
			}
		}

		protected void LoadAchievementsFinished(string _dataStr)
		{
			IDictionary dataDict = (IDictionary)JSONUtility.FromJSON(_dataStr);
			LoadAchievementsFinished(dataDict);
		}

		protected virtual void LoadAchievementsFinished(IDictionary _dataDict)
		{
		}

		protected void LoadAchievementsFinished(Achievement[] _achievements, string _error)
		{
			if (this.LoadAchievementsFinishedEvent != null)
			{
				this.LoadAchievementsFinishedEvent(_achievements, _error);
			}
		}

		protected void ReportProgressFinished(string _dataStr)
		{
			IDictionary dataDict = (IDictionary)JSONUtility.FromJSON(_dataStr);
			ReportProgressFinished(dataDict);
		}

		protected virtual void ReportProgressFinished(IDictionary _dataDict)
		{
		}

		protected void ReportProgressFinished(string _instanceID, IDictionary _dataDict)
		{
			Achievement objectWithInstanceID = NPObjectManager.GetObjectWithInstanceID<Achievement>(_instanceID, NPObjectManager.eCollectionType.GAME_SERVICES);
			if (objectWithInstanceID != null)
			{
				objectWithInstanceID.InvokeMethod("ReportProgressFinished", new object[1] { _dataDict }, new Type[1] { typeof(IDictionary) });
			}
		}

		protected void LoadUsersFinished(string _dataStr)
		{
			IDictionary dataDict = (IDictionary)JSONUtility.FromJSON(_dataStr);
			LoadUsersFinished(dataDict);
		}

		protected virtual void LoadUsersFinished(IDictionary _dataDict)
		{
		}

		protected void LoadUsersFinished(User[] _users, string _error)
		{
			if (this.LoadUsersFinishedEvent != null)
			{
				this.LoadUsersFinishedEvent(_users, _error);
			}
		}

		protected void RequestForUserImageFinished(string _dataStr)
		{
			IDictionary dataDict = (IDictionary)JSONUtility.FromJSON(_dataStr);
			RequestForUserImageFinished(dataDict);
		}

		protected virtual void RequestForUserImageFinished(IDictionary _dataDict)
		{
		}

		protected void RequestForUserImageFinished(string _instanceID, IDictionary _dataDict)
		{
			User objectWithInstanceID = NPObjectManager.GetObjectWithInstanceID<User>(_instanceID, NPObjectManager.eCollectionType.GAME_SERVICES);
			if (objectWithInstanceID != null)
			{
				objectWithInstanceID.InvokeMethod("RequestForImageFinished", new object[1] { _dataDict }, new Type[1] { typeof(IDictionary) });
			}
		}

		protected void ReportScoreFinished(string _dataStr)
		{
			IDictionary dataDict = (IDictionary)JSONUtility.FromJSON(_dataStr);
			ReportScoreFinished(dataDict);
		}

		protected virtual void ReportScoreFinished(IDictionary _dataDict)
		{
		}

		protected void ReportScoreFinished(string _instanceID, IDictionary _dataDict)
		{
			Score objectWithInstanceID = NPObjectManager.GetObjectWithInstanceID<Score>(_instanceID, NPObjectManager.eCollectionType.GAME_SERVICES);
			if (objectWithInstanceID != null)
			{
				objectWithInstanceID.InvokeMethod("ReportScoreFinished", new object[1] { _dataDict }, new Type[1] { typeof(IDictionary) });
			}
		}

		protected virtual void ShowLeaderboardViewFinished(string _error)
		{
			this.ResumeUnity();
			if (this.ShowLeaderboardViewFinishedEvent != null)
			{
				this.ShowLeaderboardViewFinishedEvent((!string.IsNullOrEmpty(_error)) ? _error : null);
			}
		}

		protected virtual void ShowAchievementViewFinished(string _error)
		{
			this.ResumeUnity();
			if (this.ShowAchievementViewFinishedEvent != null)
			{
				this.ShowAchievementViewFinishedEvent((!string.IsNullOrEmpty(_error)) ? _error : null);
			}
		}

		protected void LoadExternalAuthenticationCredentialsFinished(string _dataStr)
		{
			IDictionary dataDict = (IDictionary)JSONUtility.FromJSON(_dataStr);
			Debug.Log("LoadExternalAuthenticationCredentialsFinished" + _dataStr);
			LoadExternalAuthenticationCredentialsFinished(dataDict);
		}

		protected virtual void LoadExternalAuthenticationCredentialsFinished(IDictionary _dataDict)
		{
		}

		protected void LoadExternalAuthenticationCredentialsFinished(IDictionary _credentialsData, string _error)
		{
			ExternalAuthenticationCredentials credentials = null;
			if (string.IsNullOrEmpty(_error))
			{
				credentials = new ExternalAuthenticationCredentials(_credentialsData);
			}
			if (this.LoadExternalAuthenticationCredentialsFinishedEvent != null)
			{
				this.LoadExternalAuthenticationCredentialsFinishedEvent(credentials, _error);
			}
		}
	}
}
