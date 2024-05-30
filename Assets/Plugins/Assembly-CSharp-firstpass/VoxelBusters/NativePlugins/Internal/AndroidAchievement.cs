using System;
using System.Collections;
using UnityEngine;
using VoxelBusters.Utility;

namespace VoxelBusters.NativePlugins.Internal
{
	internal sealed class AndroidAchievement : Achievement
	{
		private const string kAchievementInfoKey = "achievement-info";

		private const string kIdentifier = "identifier";

		private const string kCurrentSteps = "points-scored";

		private const string kMaxSteps = "maximum-points";

		private const string kCompleted = "is-completed";

		private const string kLastReportDate = "last-report-date";

		private const string kDescription = "description";

		private int m_pointsScored;

		public override string Identifier { get; protected set; }

		public override bool Completed { get; protected set; }

		public override DateTime LastReportedDate { get; protected set; }

		public override double PercentageCompleted
		{
			get
			{
				return m_percentageCompleted;
			}
			set
			{
				m_percentageCompleted = Math.Min(100.0, value);
				m_pointsScored = Mathf.FloorToInt((float)(m_percentageCompleted * (double)MaximumPoints / 100.0));
				m_pointsScored = Mathf.Min(m_pointsScored, MaximumPoints);
			}
		}

		private int MaximumPoints
		{
			get
			{
				return NPBinding.GameServices.GetNoOfStepsForCompletingAchievement(base.GlobalIdentifier);
			}
		}

		private AndroidAchievement()
		{
		}

		public AndroidAchievement(string _globalIdentifier, string _identifier, double _percentageCompleted = 0.0)
			: base(_globalIdentifier, _identifier, _percentageCompleted)
		{
		}

		internal AndroidAchievement(IDictionary _achievementData)
		{
			SetDetails(_achievementData);
		}

		internal void SetDetails(IDictionary _achievementData)
		{
			Identifier = _achievementData.GetIfAvailable<string>("identifier");
			Completed = _achievementData.GetIfAvailable("is-completed", false);
			int ifAvailable = _achievementData.GetIfAvailable("points-scored", 0);
			int ifAvailable2 = _achievementData.GetIfAvailable("maximum-points", 0);
			PercentageCompleted = (double)ifAvailable / (double)ifAvailable2 * 100.0;
			long ifAvailable3 = _achievementData.GetIfAvailable("last-report-date", 0L);
			LastReportedDate = ifAvailable3.ToDateTimeFromJavaTime();
			base.GlobalIdentifier = GameServicesUtils.GetAchievementGID(Identifier);
		}

		internal static AndroidAchievement[] ConvertAchievementList(IList _achievementList)
		{
			if (_achievementList == null)
			{
				return null;
			}
			int count = _achievementList.Count;
			AndroidAchievement[] array = new AndroidAchievement[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = new AndroidAchievement(_achievementList[i] as IDictionary);
			}
			return array;
		}

		public override void ReportProgress(ReportProgressCompletion _onCompletion)
		{
			base.ReportProgress(_onCompletion);
			int maximumPointsFromAchievementDescription = GetMaximumPointsFromAchievementDescription();
			if (MaximumPoints != maximumPointsFromAchievementDescription)
			{
				Debug.LogError("[GameServices] Please make sure number of steps set in NPSettings and Maximum points for incremental achivement configured in Game Play services are same.");
			}
			GameServicesAndroid.Plugin.Call("reportProgress", GetInstanceID(), Identifier, m_pointsScored, _onCompletion != null);
		}

		protected override void ReportProgressFinished(IDictionary _dataDict)
		{
			string ifAvailable = _dataDict.GetIfAvailable<string>("error");
			IDictionary ifAvailable2 = _dataDict.GetIfAvailable<IDictionary>("achievement-info");
			if (ifAvailable2 != null)
			{
				SetDetails(ifAvailable2);
			}
			ReportProgressFinished(ifAvailable == null, ifAvailable);
		}

		private int GetMaximumPointsFromAchievementDescription()
		{
			AchievementDescription achievementDescriptionWithID = AchievementHandler.GetAchievementDescriptionWithID(Identifier);
			if (achievementDescriptionWithID == null)
			{
				return 0;
			}
			return achievementDescriptionWithID.MaximumPoints;
		}
	}
}
