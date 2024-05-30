using System;
using System.Collections;
using System.Collections.Generic;
using VoxelBusters.Utility;

namespace VoxelBusters.NativePlugins.Internal
{
	internal sealed class AndroidScore : Score
	{
		private const string kScoreInfoKey = "score-info";

		private const string kIdentifier = "identifier";

		private const string kUser = "user";

		private const string kValue = "value";

		private const string kDate = "date";

		private const string kFormattedValue = "formatted-value";

		private const string kRank = "rank";

		public override string LeaderboardID { get; protected set; }

		public override User User { get; protected set; }

		public override long Value { get; set; }

		public override DateTime Date { get; protected set; }

		public override int Rank { get; protected set; }

		private AndroidScore()
		{
		}

		public AndroidScore(string _leaderboardGlobalID, string _leaderboardID, User _user, long _scoreValue = 0L)
			: base(_leaderboardGlobalID, _leaderboardID, _user, _scoreValue)
		{
		}

		internal AndroidScore(IDictionary _scoreData)
		{
			LeaderboardID = _scoreData.GetIfAvailable<string>("identifier");
			base.LeaderboardGlobalID = GameServicesUtils.GetLeaderboardGID(LeaderboardID);
			User = new AndroidUser(_scoreData.GetIfAvailable<Dictionary<string, object>>("user"));
			Value = _scoreData.GetIfAvailable("value", 0L);
			long ifAvailable = _scoreData.GetIfAvailable("date", 0L);
			Date = ifAvailable.ToDateTimeFromJavaTime();
			Rank = _scoreData.GetIfAvailable("rank", 0);
		}

		internal static AndroidScore ConvertScore(IDictionary _score)
		{
			if (_score == null)
			{
				return null;
			}
			return new AndroidScore(_score);
		}

		internal static AndroidScore[] ConvertScoreList(IList _scoreList)
		{
			if (_scoreList == null)
			{
				return null;
			}
			int count = _scoreList.Count;
			AndroidScore[] array = new AndroidScore[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = new AndroidScore(_scoreList[i] as IDictionary);
			}
			return array;
		}

		public override void ReportScore(ReportScoreCompletion _onCompletion)
		{
			base.ReportScore(_onCompletion);
			GameServicesAndroid.Plugin.Call("reportScore", GetInstanceID(), LeaderboardID, Value, _onCompletion != null);
		}

		protected override void ReportScoreFinished(IDictionary _dataDict)
		{
			string ifAvailable = _dataDict.GetIfAvailable<string>("error");
			IDictionary ifAvailable2 = _dataDict.GetIfAvailable<IDictionary>("score-info");
			if (ifAvailable2 != null)
			{
				Value = ifAvailable2.GetIfAvailable("value", 0L);
				long ifAvailable3 = ifAvailable2.GetIfAvailable("date", 0L);
				Date = ifAvailable3.ToDateTimeFromJavaTime();
			}
			ReportScoreFinished(string.IsNullOrEmpty(ifAvailable), ifAvailable);
		}
	}
}
