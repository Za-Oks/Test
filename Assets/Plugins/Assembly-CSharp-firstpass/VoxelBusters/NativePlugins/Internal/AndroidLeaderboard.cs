using System.Collections;
using System.Collections.Generic;
using VoxelBusters.Utility;

namespace VoxelBusters.NativePlugins.Internal
{
	internal sealed class AndroidLeaderboard : Leaderboard
	{
		private const string kLeaderboardInfo = "leaderboard-info";

		private const string kLeaderboardScores = "leaderboard-scores";

		private const string kLeaderboardLocalScore = "leaderboard-local-score";

		private const string kIdentifier = "identifier";

		private const string kUserScope = "user-scope";

		private const string kTimeScope = "time-scope";

		private const string kTitle = "title";

		private const string kScores = "scores";

		private const string kLocalUserScore = "local-user-score";

		private const string kUserScopeGlobal = "user-scope-gobal";

		private const string kUserScopeFriendsOnly = "user-scope-friends";

		private const string kTimeScopeToday = "time-scope-today";

		private const string kTimeScopeWeek = "time-scope-week";

		private const string kTimeScopeAllTime = "time-scope-all-time";

		internal static Dictionary<string, eLeaderboardUserScope> kUserScopeMap = new Dictionary<string, eLeaderboardUserScope>
		{
			{
				"user-scope-gobal",
				eLeaderboardUserScope.GLOBAL
			},
			{
				"user-scope-friends",
				eLeaderboardUserScope.FRIENDS_ONLY
			}
		};

		internal static Dictionary<string, eLeaderboardTimeScope> kTimeScopeMap = new Dictionary<string, eLeaderboardTimeScope>
		{
			{
				"time-scope-today",
				eLeaderboardTimeScope.TODAY
			},
			{
				"time-scope-week",
				eLeaderboardTimeScope.WEEK
			},
			{
				"time-scope-all-time",
				eLeaderboardTimeScope.ALL_TIME
			}
		};

		public override string Identifier { get; protected set; }

		public override eLeaderboardUserScope UserScope { get; set; }

		public override eLeaderboardTimeScope TimeScope { get; set; }

		public override string Title { get; protected set; }

		public override Score[] Scores { get; protected set; }

		public override Score LocalUserScore { get; protected set; }

		private AndroidLeaderboard()
		{
		}

		internal AndroidLeaderboard(string _globalIdentifier, string _identifier)
			: base(_globalIdentifier, _identifier)
		{
		}

		internal AndroidLeaderboard(IDictionary _leaderboardData)
		{
			Identifier = _leaderboardData.GetIfAvailable<string>("identifier");
			string ifAvailable = _leaderboardData.GetIfAvailable<string>("user-scope");
			UserScope = kUserScopeMap[ifAvailable];
			string ifAvailable2 = _leaderboardData.GetIfAvailable<string>("time-scope");
			TimeScope = kTimeScopeMap[ifAvailable2];
			Title = _leaderboardData.GetIfAvailable<string>("title");
			IList ifAvailable3 = _leaderboardData.GetIfAvailable<List<object>>("scores");
			Scores = AndroidScore.ConvertScoreList(ifAvailable3);
			IDictionary ifAvailable4 = _leaderboardData.GetIfAvailable<Dictionary<string, object>>("local-user-score");
			LocalUserScore = AndroidScore.ConvertScore(ifAvailable4);
			base.GlobalIdentifier = GameServicesUtils.GetLeaderboardGID(Identifier);
		}

		public override void LoadTopScores(LoadScoreCompletion _onCompletion)
		{
			base.LoadTopScores(_onCompletion);
			GameServicesAndroid.Plugin.Call("loadTopScores", GetInstanceID(), Identifier, GetTimeScopeString(), GetUserScopeString(), base.MaxResults);
		}

		public override void LoadPlayerCenteredScores(LoadScoreCompletion _onCompletion)
		{
			base.LoadPlayerCenteredScores(_onCompletion);
			GameServicesAndroid.Plugin.Call("loadPlayerCenteredScores", GetInstanceID(), Identifier, GetTimeScopeString(), GetUserScopeString(), base.MaxResults);
		}

		public override void LoadMoreScores(eLeaderboardPageDirection _pageDirection, LoadScoreCompletion _onCompletion)
		{
			base.LoadMoreScores(_pageDirection, _onCompletion);
			GameServicesAndroid.Plugin.Call("loadMoreScores", GetInstanceID(), Identifier, (int)_pageDirection, base.MaxResults);
		}

		protected override void LoadScoresFinished(IDictionary _dataDict)
		{
			string ifAvailable = _dataDict.GetIfAvailable<string>("error");
			IDictionary ifAvailable2 = _dataDict.GetIfAvailable<IDictionary>("leaderboard-info");
			if (ifAvailable2 != null)
			{
				IDictionary ifAvailable3 = ifAvailable2.GetIfAvailable<IDictionary>("leaderboard-local-score");
				IList ifAvailable4 = ifAvailable2.GetIfAvailable<IList>("leaderboard-scores");
				LocalUserScore = AndroidScore.ConvertScore(ifAvailable3);
				Scores = AndroidScore.ConvertScoreList(ifAvailable4);
			}
			LoadScoresFinished(Scores, LocalUserScore, ifAvailable);
		}

		private string GetTimeScopeString()
		{
			return kTimeScopeMap.GetKey(TimeScope);
		}

		private string GetUserScopeString()
		{
			return kUserScopeMap.GetKey(UserScope);
		}
	}
}
