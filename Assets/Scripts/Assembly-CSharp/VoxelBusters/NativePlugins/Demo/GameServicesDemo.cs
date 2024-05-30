using UnityEngine;
using VoxelBusters.Utility;

namespace VoxelBusters.NativePlugins.Demo
{
	public class GameServicesDemo : NPDemoBase
	{
		[SerializeField]
		[Header("Leaderboard Properties")]
		private eLeaderboardTimeScope m_timeScope;

		[SerializeField]
		private int m_maxScoreResults = 20;

		private Leaderboard m_curLeaderboard;

		private string m_curLeaderboardGID;

		private int m_curLeaderboardGIDIndex = -1;

		private string[] m_leaderboardGIDList = new string[0];

		private string m_curAchievementGID;

		private int m_curAchievementGIDIndex = -1;

		private string[] m_achievementGIDList = new string[0];

		protected override void Start()
		{
			base.Start();
			ExtractGID();
			AddExtraInfoTexts("You can configure this feature in NPSettings->Game Services Settings.", "Using platform specific identifier to access Achievements/Leaderboard object is very troublesome. \nInstead, make use of global identifier for unified access of Achievements/Leaderboard irrespective of platform. \nThis can be done by either adding identifier info in Game Services Settings or else manually set it at runtime using SetLeaderboardIDCollection & SetAchievementIDCollection API.", "For testing iOS build, set Game Center to Sandox mode in your device settings and then log in to Game Center application using sandbox test accounts. Once you are done with it, you can try testing this feature.", "In Unity Editor, we are simulating this feature to help developers test funtionality in Editor itself. You can manually fill info by opening Editor Game Center from Menu (Window->Voxel Busters->Native Plugins) or use Game Services Settings to auto fill values.");
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			AddNewResult("[NOTE] Google Play Service is being used.");
			if (m_leaderboardGIDList.Length == 0)
			{
				AppendResult("Could not find leaderboard id information. Please configure it.");
			}
			else if (m_curLeaderboardGIDIndex == -1)
			{
				ChangeLeaderboardGID(true);
			}
			if (m_achievementGIDList.Length == 0)
			{
				AppendResult("Could not find achievement id information. Please configure it.");
			}
			else if (m_curAchievementGIDIndex == -1)
			{
				ChangeAchievementGID(true);
			}
		}

		protected override void DisplayFeatureFunctionalities()
		{
			base.DisplayFeatureFunctionalities();
			if (GUILayout.Button("Is Available"))
			{
				if (IsAvailable())
				{
					AddNewResult("Game Services feature is supported.");
				}
				else
				{
					AddNewResult("Game Services feature is not supported.");
				}
			}
			if (!IsAvailable())
			{
				GUILayout.Box("Sorry, Game Services feature is not supported on this device.");
				return;
			}
			if (GUILayout.Button("Is Authenticated"))
			{
				if (IsAuthenticated())
				{
					AddNewResult("Local user is authenticated.");
				}
				else
				{
					AddNewResult("Local user is not yet authenticated!");
				}
			}
			if (!IsAuthenticated())
			{
				if (GUILayout.Button("Authenticate User"))
				{
					AuthenticateUser();
				}
				GUILayout.Box("Sorry, user is currently not signed in to Game Services. Please authenticate the user before accessing Game Services features.");
			}
			else
			{
				DrawUserSection();
				DrawLeaderboardSection();
				DrawAchievementSection();
				DrawUISection();
				DrawMiscSection();
			}
		}

		private void DrawUserSection()
		{
			GUILayout.Label("Local User", "sub-title");
			if (GUILayout.Button("Sign Out"))
			{
				SignOut();
			}
			if (GUILayout.Button("Load Friends"))
			{
				LoadFriends();
			}
			if (GUILayout.Button("Load Users"))
			{
				string[] userIDList = new string[1] { NPBinding.GameServices.LocalUser.Identifier };
				LoadUsers(userIDList);
			}
		}

		private void DrawLeaderboardSection()
		{
			GUILayout.Label("Leaderboard", "sub-title");
			if (m_leaderboardGIDList.Length == 0)
			{
				GUILayout.Box("Could not find Leaderboard configuration in GameServices. If you want to access Leaderboard feature, then please configure it.");
				return;
			}
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("Previous Leaderboard"))
			{
				ChangeLeaderboardGID(false);
			}
			if (GUILayout.Button("Next Leaderboard"))
			{
				ChangeLeaderboardGID(true);
			}
			GUILayout.EndHorizontal();
			GUILayout.Box(string.Format("Current Leaderboard GID= {0}.", m_curLeaderboardGID));
			if (GUILayout.Button("Create Leaderboard"))
			{
				Leaderboard leaderboard = CreateLeaderboardWithGlobalID(m_curLeaderboardGID);
				AddNewResult(string.Format("Leaderboard with global identifier {0} is created.", leaderboard.GlobalIdentifier));
			}
			if (GUILayout.Button("Report Score"))
			{
				ReportScoreWithGlobalID(m_curLeaderboardGID);
			}
			if (GUILayout.Button("Load Top Scores"))
			{
				LoadTopScores();
			}
			if (GUILayout.Button("Load Player Centered Scores"))
			{
				LoadPlayerCenteredScores();
			}
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("Load Previous Scores"))
			{
				LoadMoreScores(eLeaderboardPageDirection.PREVIOUS);
			}
			if (GUILayout.Button("Load Next Scores"))
			{
				LoadMoreScores(eLeaderboardPageDirection.NEXT);
			}
			GUILayout.EndHorizontal();
		}

		private void DrawAchievementSection()
		{
			GUILayout.Label("Achievement", "sub-title");
			if (m_achievementGIDList.Length == 0)
			{
				GUILayout.Box("Could not find Achievement configuration in GameServices. If you want to access Achievement feature, then please configure it.");
				return;
			}
			if (GUILayout.Button("Load Achievement Descriptions"))
			{
				LoadAchievementDescriptions();
			}
			if (GUILayout.Button("Load Achievements"))
			{
				LoadAchievements();
			}
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("Previous Achievement"))
			{
				ChangeAchievementGID(false);
			}
			if (GUILayout.Button("Next Achievement"))
			{
				ChangeAchievementGID(true);
			}
			GUILayout.EndHorizontal();
			GUILayout.Box(string.Format("Current achievement GID= {0}.", m_curAchievementGID));
			if (GUILayout.Button("Create Achievement"))
			{
				Achievement achievement = CreateAchievementWithGlobalID(m_curAchievementGID);
				AddNewResult(string.Format("Achievement with global identifier {0} is created.", achievement.GlobalIdentifier));
			}
			if (GUILayout.Button("Get No Of Steps For Completing Achievement"))
			{
				int noOfStepsForCompletingAchievement = GetNoOfStepsForCompletingAchievement(m_curAchievementGID);
				AddNewResult(string.Format("No of steps required for completing achievement is {0}.", noOfStepsForCompletingAchievement));
			}
			if (GUILayout.Button("Report Progress"))
			{
				ReportProgressWithGlobalID(m_curAchievementGID);
			}
		}

		private void DrawUISection()
		{
			bool flag = m_leaderboardGIDList.Length != 0;
			bool flag2 = m_achievementGIDList.Length != 0;
			if (flag || flag2)
			{
				GUILayout.Label("UI", "sub-title");
			}
			if (flag && GUILayout.Button("Show Leaderboard UI"))
			{
				ShowLeaderboardUIWithGlobalID(m_curLeaderboardGID);
			}
			if (flag2 && GUILayout.Button("Show Achievements UI"))
			{
				ShowAchievementsUI();
			}
		}

		private void DrawMiscSection()
		{
			GUILayout.Label("Misc", "sub-title");
			if (GUILayout.Button("Load External Authentication Credentials"))
			{
				LoadExternalAuthenticationCredentials();
			}
		}

		private bool IsAvailable()
		{
			return NPBinding.GameServices.IsAvailable();
		}

		private bool IsAuthenticated()
		{
			return NPBinding.GameServices.LocalUser.IsAuthenticated;
		}

		private void AuthenticateUser()
		{
			NPBinding.GameServices.LocalUser.Authenticate(delegate(bool _success, string _error)
			{
				AddNewResult("Local user authentication finished.");
				AppendResult(string.Format("Error= {0}.", _error.GetPrintableString()));
				if (_success)
				{
					AppendResult(string.Format("Local user details= {0}.", NPBinding.GameServices.LocalUser));
				}
			});
		}

		private void SignOut()
		{
			NPBinding.GameServices.LocalUser.SignOut(delegate(bool _success, string _error)
			{
				if (_success)
				{
					AddNewResult("Local user is signed out successfully!");
				}
				else
				{
					AddNewResult("Request to signout local user failed.");
					AppendResult(string.Format("Error= {0}.", _error.GetPrintableString()));
				}
			});
		}

		private void LoadFriends()
		{
			NPBinding.GameServices.LocalUser.LoadFriends(delegate(User[] _friends, string _error)
			{
				AddNewResult("Load friends info request finished.");
				AppendResult(string.Format("Error= {0}.", _error.GetPrintableString()));
				if (_friends != null)
				{
					foreach (User user in _friends)
					{
						AppendResult(user.ToString());
					}
				}
			});
		}

		private void LoadUsers(string[] _userIDList)
		{
			NPBinding.GameServices.LoadUsers(_userIDList, delegate(User[] _users, string _error)
			{
				AddNewResult("Load users info request finished.");
				AppendResult(string.Format("Error= {0}.", _error.GetPrintableString()));
				if (_users != null)
				{
					foreach (User user in _users)
					{
						AppendResult(user.ToString());
					}
				}
			});
		}

		private Leaderboard CreateLeaderboardWithGlobalID(string _leaderboardGID)
		{
			m_curLeaderboard = NPBinding.GameServices.CreateLeaderboardWithGlobalID(_leaderboardGID);
			m_curLeaderboard.MaxResults = m_maxScoreResults;
			return m_curLeaderboard;
		}

		private void LoadPlayerCenteredScores()
		{
			if (m_curLeaderboard == null)
			{
				AddNewResult("The requested operation could not be completed because leaderboard instance is null. Please create new leaderboard instance.");
			}
			else
			{
				m_curLeaderboard.LoadPlayerCenteredScores(OnLoadScoresFinished);
			}
		}

		private void LoadTopScores()
		{
			if (m_curLeaderboard == null)
			{
				AddNewResult("The requested operation could not be completed because leaderboard instance is null. Please create new leaderboard instance.");
			}
			else
			{
				m_curLeaderboard.LoadTopScores(OnLoadScoresFinished);
			}
		}

		private void LoadMoreScores(eLeaderboardPageDirection _direction)
		{
			if (m_curLeaderboard == null)
			{
				AddNewResult("The requested operation could not be completed because leaderboard instance is null. Please create new leaderboard instance.");
			}
			else
			{
				m_curLeaderboard.LoadMoreScores(_direction, OnLoadScoresFinished);
			}
		}

		private void ReportScoreWithGlobalID(string _leaderboardGID)
		{
			int _randomScore = Random.Range(0, 100);
			NPBinding.GameServices.ReportScoreWithGlobalID(_leaderboardGID, _randomScore, delegate(bool _success, string _error)
			{
				if (_success)
				{
					AddNewResult(string.Format("Request to report score to leaderboard with GID= {0} finished successfully.", _leaderboardGID));
					AppendResult(string.Format("New score= {0}.", _randomScore));
				}
				else
				{
					AddNewResult(string.Format("Request to report score to leaderboard with GID= {0} failed.", _leaderboardGID));
					AppendResult(string.Format("Error= {0}.", _error.GetPrintableString()));
				}
			});
		}

		private Achievement CreateAchievementWithGlobalID(string _achievementGID)
		{
			return NPBinding.GameServices.CreateAchievementWithGlobalID(_achievementGID);
		}

		private int GetNoOfStepsForCompletingAchievement(string _achievementGID)
		{
			return NPBinding.GameServices.GetNoOfStepsForCompletingAchievement(_achievementGID);
		}

		private void LoadAchievementDescriptions()
		{
			NPBinding.GameServices.LoadAchievementDescriptions(delegate(AchievementDescription[] _descriptions, string _error)
			{
				AddNewResult("Request to load achievement descriptions finished.");
				AppendResult(string.Format("Error= {0}.", _error.GetPrintableString()));
				if (_descriptions != null)
				{
					int num = _descriptions.Length;
					AppendResult(string.Format("Total loaded descriptions= {0}.", num));
					for (int i = 0; i < num; i++)
					{
						AppendResult(string.Format("[Index {0}]: {1}", i, _descriptions[i]));
					}
				}
			});
		}

		private void LoadAchievements()
		{
			NPBinding.GameServices.LoadAchievements(delegate(Achievement[] _achievements, string _error)
			{
				AddNewResult("Request to load achievements finished.");
				AppendResult(string.Format("Error= {0}.", _error.GetPrintableString()));
				if (_achievements != null)
				{
					int num = _achievements.Length;
					AppendResult(string.Format("Total loaded achievements= {0}.", num));
					for (int i = 0; i < num; i++)
					{
						AppendResult(string.Format("[Index {0}]: {1}", i, _achievements[i]));
					}
				}
			});
		}

		private void ReportProgressWithGlobalID(string _achievementGID)
		{
			int noOfStepsForCompletingAchievement = NPBinding.GameServices.GetNoOfStepsForCompletingAchievement(_achievementGID);
			int num = Random.Range(0, noOfStepsForCompletingAchievement + 1);
			double _progress = (double)num / (double)noOfStepsForCompletingAchievement * 100.0;
			NPBinding.GameServices.ReportProgressWithGlobalID(_achievementGID, _progress, delegate(bool _status, string _error)
			{
				if (_status)
				{
					AddNewResult(string.Format("Request to report progress of achievement with GID= {0} finished successfully.", _achievementGID));
					AppendResult(string.Format("Percentage completed= {0}.", _progress));
				}
				else
				{
					AddNewResult(string.Format("Request to report progress of achievement with GID= {0} failed.", _achievementGID));
					AppendResult(string.Format("Error= {0}.", _error.GetPrintableString()));
				}
			});
		}

		private void ShowAchievementsUI()
		{
			AddNewResult("Sending request to show achievements view.");
			NPBinding.GameServices.ShowAchievementsUI(delegate(string _error)
			{
				AddNewResult("Achievements view dismissed.");
				AppendResult(string.Format("Error= {0}.", _error.GetPrintableString()));
			});
		}

		private void ShowLeaderboardUIWithGlobalID(string _leaderboadGID)
		{
			AddNewResult("Sending request to show leaderboard view.");
			NPBinding.GameServices.ShowLeaderboardUIWithGlobalID(_leaderboadGID, m_timeScope, delegate(string _error)
			{
				AddNewResult("Leaderboard view dismissed.");
				AppendResult(string.Format("Error= {0}.", _error.GetPrintableString()));
			});
		}

		private void LoadExternalAuthenticationCredentials()
		{
			AddNewResult("Sending request to Load External Auth Credentials.");
			NPBinding.GameServices.LoadExternalAuthenticationCredentials(delegate(ExternalAuthenticationCredentials _credentials, string _error)
			{
				AddNewResult("LoadExternalAuthenticationCredentials Finished");
				AppendResult(string.Format("Error= {0}.", _error.GetPrintableString()));
				if (_credentials != null)
				{
					AppendResult(_credentials.AndroidCredentials.ServerAuthCode);
				}
			});
		}

		private void OnLoadScoresFinished(Score[] _scores, Score _localUserScore, string _error)
		{
			AddNewResult("Load leaderboard scores request finished.");
			AppendResult(string.Format("Error= {0}.", _error.GetPrintableString()));
			if (_scores != null)
			{
				int num = _scores.Length;
				AppendResult(string.Format("Totally {0} score entries were loaded.", num));
				AppendResult(string.Format("Local user score= {0}.", (_localUserScore != null) ? _localUserScore.ToString() : "NULL"));
				for (int i = 0; i < num; i++)
				{
					AppendResult(string.Format("[Index {0}]: {1}.", i, _scores[i]));
				}
			}
		}

		private void ExtractGID()
		{
			//LeaderboardMetadata[] leaderboardMetadataCollection = NPSettings.GameServicesSettings.LeaderboardMetadataCollection;
			//int num = leaderboardMetadataCollection.Length;
			//m_leaderboardGIDList = new string[num];
			//for (int i = 0; i < num; i++)
			//{
			//	m_leaderboardGIDList[i] = leaderboardMetadataCollection[i].GlobalID;
			//}
			//AchievementMetadata[] achievementMetadataCollection = NPSettings.GameServicesSettings.AchievementMetadataCollection;
			//int num2 = achievementMetadataCollection.Length;
			//m_achievementGIDList = new string[num2];
			//for (int j = 0; j < num2; j++)
			//{
			//	m_achievementGIDList[j] = achievementMetadataCollection[j].GlobalID;
			//}
		}

		private void ChangeLeaderboardGID(bool _gotoNext)
		{
			int num = m_leaderboardGIDList.Length;
			if (_gotoNext)
			{
				m_curLeaderboardGIDIndex++;
				if (m_curLeaderboardGIDIndex >= num)
				{
					m_curLeaderboardGIDIndex = 0;
				}
			}
			else
			{
				m_curLeaderboardGIDIndex--;
				if (m_curLeaderboardGIDIndex < 0)
				{
					m_curLeaderboardGIDIndex = num - 1;
				}
			}
			m_curLeaderboardGID = m_leaderboardGIDList[m_curLeaderboardGIDIndex];
		}

		private void ChangeAchievementGID(bool _gotoNext)
		{
			int num = m_achievementGIDList.Length;
			if (_gotoNext)
			{
				m_curAchievementGIDIndex++;
				if (m_curAchievementGIDIndex >= num)
				{
					m_curAchievementGIDIndex = 0;
				}
			}
			else
			{
				m_curAchievementGIDIndex--;
				if (m_curAchievementGIDIndex < 0)
				{
					m_curAchievementGIDIndex = num - 1;
				}
			}
			m_curAchievementGID = m_achievementGIDList[m_curAchievementGIDIndex];
		}
	}
}
