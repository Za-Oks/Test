using System;
using System.Collections;
using System.Globalization;
using VoxelBusters.NativePlugins.Internal;

namespace VoxelBusters.NativePlugins
{
	public abstract class Score : NPObject
	{
		public delegate void ReportScoreCompletion(bool _success, string _error);

		public string LeaderboardGlobalID { get; protected set; }

		public abstract string LeaderboardID { get; protected set; }

		public abstract User User { get; protected set; }

		public abstract long Value { get; set; }

		public abstract DateTime Date { get; protected set; }

		public virtual string FormattedValue
		{
			get
			{
				return Value.ToString("0,0", CultureInfo.InvariantCulture);
			}
		}

		public abstract int Rank { get; protected set; }

		protected event ReportScoreCompletion ReportScoreFinishedEvent;

		protected Score()
			: base(NPObjectManager.eCollectionType.GAME_SERVICES)
		{
		}

		protected Score(string _leaderboardGlobalID, string _leaderboardID, User _user, long _scoreValue)
			: base(NPObjectManager.eCollectionType.GAME_SERVICES)
		{
			LeaderboardGlobalID = _leaderboardGlobalID;
			LeaderboardID = _leaderboardID;
			User = _user;
			Value = _scoreValue;
			Date = DateTime.Now;
			Rank = 0;
		}

		public virtual void ReportScore(ReportScoreCompletion _onCompletion)
		{
			this.ReportScoreFinishedEvent = _onCompletion;
		}

		public override string ToString()
		{
			return string.Format("[Score: Rank={0}, UserName={1}, Value={2}]", Rank, User.Name, Value);
		}

		protected virtual void ReportScoreFinished(IDictionary _dataDict)
		{
		}

		protected void ReportScoreFinished(bool _success, string _error)
		{
			if (this.ReportScoreFinishedEvent != null)
			{
				this.ReportScoreFinishedEvent(_success, _error);
			}
		}
	}
}
