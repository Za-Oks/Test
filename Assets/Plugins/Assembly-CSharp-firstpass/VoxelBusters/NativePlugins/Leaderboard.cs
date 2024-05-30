using System.Collections;
using UnityEngine;
using VoxelBusters.NativePlugins.Internal;

namespace VoxelBusters.NativePlugins
{
	public abstract class Leaderboard : NPObject
	{
		public delegate void LoadScoreCompletion(Score[] _scores, Score _localUserScore, string _error);

		protected const int kLoadScoresMinResults = 1;

		protected const int kLoadScoresMaxResults = 25;

		private int m_maxResults;

		public string GlobalIdentifier { get; protected set; }

		public abstract string Identifier { get; protected set; }

		public abstract string Title { get; protected set; }

		public abstract eLeaderboardUserScope UserScope { get; set; }

		public abstract eLeaderboardTimeScope TimeScope { get; set; }

		public int MaxResults
		{
			get
			{
				return m_maxResults;
			}
			set
			{
				m_maxResults = Mathf.Clamp(value, 1, 25);
			}
		}

		public abstract Score[] Scores { get; protected set; }

		public abstract Score LocalUserScore { get; protected set; }

		private event LoadScoreCompletion LoadScoreFinishedEvent;

		protected Leaderboard()
			: base(NPObjectManager.eCollectionType.GAME_SERVICES)
		{
		}

		protected Leaderboard(string _globalIdentifer, string _identifier, string _title = null, eLeaderboardUserScope _userScope = eLeaderboardUserScope.GLOBAL, eLeaderboardTimeScope _timeScope = eLeaderboardTimeScope.ALL_TIME, int _maxResults = 25, Score[] _scores = null, Score _localUserScore = null)
			: base(NPObjectManager.eCollectionType.GAME_SERVICES)
		{
			GlobalIdentifier = _globalIdentifer;
			Identifier = _identifier;
			Title = _title;
			UserScope = _userScope;
			TimeScope = _timeScope;
			MaxResults = _maxResults;
			Scores = _scores;
			LocalUserScore = _localUserScore;
		}

		public virtual void LoadTopScores(LoadScoreCompletion _onCompletion)
		{
			this.LoadScoreFinishedEvent = _onCompletion;
		}

		public virtual void LoadPlayerCenteredScores(LoadScoreCompletion _onCompletion)
		{
			this.LoadScoreFinishedEvent = _onCompletion;
		}

		public virtual void LoadMoreScores(eLeaderboardPageDirection _pageDirection, LoadScoreCompletion _onCompletion)
		{
			this.LoadScoreFinishedEvent = _onCompletion;
		}

		protected void CacheLoadScoreCompletionCallback(LoadScoreCompletion _onCompletion)
		{
			this.LoadScoreFinishedEvent = _onCompletion;
		}

		public override string ToString()
		{
			return string.Format("[Leaderboard: Identifier={0}, UserScope={1}, TimeScope={2}]", Identifier, UserScope, TimeScope);
		}

		protected virtual void LoadScoresFinished(IDictionary _dataDict)
		{
		}

		protected void LoadScoresFinished(Score[] _scores, Score _localUserScore, string _error)
		{
			Scores = _scores;
			LocalUserScore = _localUserScore;
			if (this.LoadScoreFinishedEvent != null)
			{
				this.LoadScoreFinishedEvent(_scores, _localUserScore, _error);
			}
		}
	}
}
