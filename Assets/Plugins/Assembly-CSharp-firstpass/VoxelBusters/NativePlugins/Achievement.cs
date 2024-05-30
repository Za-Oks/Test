using System;
using System.Collections;
using VoxelBusters.NativePlugins.Internal;

namespace VoxelBusters.NativePlugins
{
	public abstract class Achievement : NPObject
	{
		public delegate void LoadAchievementsCompletion(Achievement[] _achievements, string _error);

		public delegate void ReportProgressCompletion(bool _success, string _error);

		protected double m_percentageCompleted;

		public string GlobalIdentifier { get; protected set; }

		public abstract string Identifier { get; protected set; }

		public virtual double PercentageCompleted
		{
			get
			{
				return m_percentageCompleted;
			}
			set
			{
				m_percentageCompleted = Math.Min(100.0, value);
			}
		}

		public abstract bool Completed { get; protected set; }

		public abstract DateTime LastReportedDate { get; protected set; }

		[Obsolete("This property is deprecated. Instead use PercentageCompleted property for reporting progress.")]
		public int PointsScored
		{
			get
			{
				return 0;
			}
			set
			{
				throw new NotSupportedException("Use PercentageCompleted property for reporting progress.");
			}
		}

		private event ReportProgressCompletion ReportProgressFinishedEvent;

		protected Achievement()
			: base(NPObjectManager.eCollectionType.GAME_SERVICES)
		{
		}

		protected Achievement(string _globalIdentifier, string _identifier, double _percentageCompleted, bool _completed, DateTime _reportedDate)
			: base(NPObjectManager.eCollectionType.GAME_SERVICES)
		{
			GlobalIdentifier = _globalIdentifier;
			Identifier = _identifier;
			PercentageCompleted = _percentageCompleted;
			Completed = _completed;
			LastReportedDate = _reportedDate;
		}

		protected Achievement(string _globalIdentifier, string _identifier, double _percentageCompleted = 0.0)
			: this(_globalIdentifier, _identifier, _percentageCompleted, false, DateTime.Now)
		{
		}

		public virtual void ReportProgress(ReportProgressCompletion _onCompletion)
		{
			this.ReportProgressFinishedEvent = _onCompletion;
		}

		public override string ToString()
		{
			return string.Format("[Achievement: Identifier={0}, PercentageCompleted={1}, Completed={2}, LastReportedDate={3}]", Identifier, PercentageCompleted, Completed, LastReportedDate);
		}

		protected virtual void ReportProgressFinished(IDictionary _dataDict)
		{
		}

		protected void ReportProgressFinished(bool _success, string _error)
		{
			if (this.ReportProgressFinishedEvent != null)
			{
				this.ReportProgressFinishedEvent(_success, _error);
			}
		}
	}
}
