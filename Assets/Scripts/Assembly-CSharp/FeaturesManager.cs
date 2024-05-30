using UnityEngine;

public class FeaturesManager : MonoBehaviour
{
	public class ALManager
	{
		private class CustomAchievement
		{
			public string appleID;

			public string googleID;

			public int score;

			public CustomAchievement(string appleID, string googleID, int score)
			{
				this.appleID = appleID;
				this.googleID = googleID;
				this.score = score;
			}
		}

		private Level_Manager levelManager;

		private CustomAchievement[] ACHIEVEMENTS_LEVELS = new CustomAchievement[11]
		{
			new CustomAchievement("totally2_achievement_level_1", "CgkI4KTK7JUNEAIQAQ", 1),
			new CustomAchievement("totally2_achievement_level_10", "CgkI4KTK7JUNEAIQAg", 10),
			new CustomAchievement("totally2_achievement_level_20", "CgkI4KTK7JUNEAIQAw", 20),
			new CustomAchievement("totally2_achievement_level_30", "CgkI4KTK7JUNEAIQBA", 30),
			new CustomAchievement("totally2_achievement_level_40", "CgkI4KTK7JUNEAIQBQ", 40),
			new CustomAchievement("totally2_achievement_level_50", "CgkI4KTK7JUNEAIQBg", 50),
			new CustomAchievement("totally2_achievement_level_60", "CgkI4KTK7JUNEAIQBw", 60),
			new CustomAchievement("totally2_achievement_level_70", "CgkI4KTK7JUNEAIQCA", 70),
			new CustomAchievement("totally2_achievement_level_80", "CgkI4KTK7JUNEAIQCQ", 80),
			new CustomAchievement("totally2_achievement_level_90", "CgkI4KTK7JUNEAIQCg", 90),
			new CustomAchievement("totally2_achievement_level_100", "CgkI4KTK7JUNEAIQCw", 100)
		};

		public ALManager(Level_Manager levelManager)
		{
			this.levelManager = levelManager;
			Initialize();
		}

		private void Initialize()
		{
		}

		public void Begin()
		{
			SignIn();
			if (NPBinding.GameServices.LocalUser.IsAuthenticated)
			{
				Synchronize();
			}
		}

		public void SignIn()
		{
			if (NPBinding.GameServices.LocalUser.IsAuthenticated)
			{
				return;
			}
			NPBinding.GameServices.LocalUser.Authenticate(delegate(bool _success, string _error)
			{
				if (_success)
				{
					Synchronize();
				}
			});
		}

		public void SignOut()
		{
			if (NPBinding.GameServices.LocalUser.IsAuthenticated)
			{
				NPBinding.GameServices.LocalUser.SignOut(delegate
				{
				});
			}
		}

		public void ShowAchievements()
		{
		}

		public void ShowLeaderboards()
		{
		}

		public void Synchronize()
		{
		}

		private void SynchAchievements()
		{
		}

		private void SynchAchievementsEpicLevels()
		{
		}

		private void SynchLeaderboards()
		{
		}
	}

	public class CloudSaveManager
	{
		private CloudSaveDecoder cloudSaveDecoder;

		private Ui_Manager uimanager;

		private ALManager almanager;

		private const string SAVE_KEY = "abs_save_new";

		private bool showDebug;

		public CloudSaveManager(CloudSaveDecoder cloudSaveDecoder, Ui_Manager uimanager, ALManager almanager)
		{
			this.cloudSaveDecoder = cloudSaveDecoder;
			this.uimanager = uimanager;
			this.almanager = almanager;
		}

		public void OnEnable()
		{
		}

		public void OnDisable()
		{
		}

		private void OnKeyValueStoreDidInitialiseEvent(bool _success)
		{
			DebugMe("KeyValueStoreDidInitialiseEvent " + _success);
		}

		private void OnKeyValueStoreDidSynchronise(bool _success)
		{
			DebugMe("OnKeyValueStoreDidSynchronise " + _success);
		}

		public void Save()
		{
		}

		public void Load()
		{
		}

		public void DebugMe(string str)
		{
			if (showDebug)
			{
				Debug.Log("RAPPSTD " + str);
			}
		}
	}

	private Level_Manager levelManager;

	private Ui_Manager uimanager;

	private CloudSaveDecoder cloudSaveDecoder;

	public ALManager alManager;

	public CloudSaveManager cloudSaveManager;

	private void Awake()
	{
		levelManager = GameObject.FindGameObjectWithTag("Level_Manager").GetComponent<Level_Manager>();
		uimanager = GameObject.FindGameObjectWithTag("Ui_Manager").GetComponent<Ui_Manager>();
		cloudSaveDecoder = GetComponent<CloudSaveDecoder>();
		alManager = new ALManager(levelManager);
		cloudSaveManager = new CloudSaveManager(cloudSaveDecoder, uimanager, alManager);
	}

	private void OnEnable()
	{
		cloudSaveManager.OnEnable();
	}

	private void OnDisable()
	{
		cloudSaveManager.OnDisable();
	}
}
