using System;
using System.Collections;
using UnityEngine;
using VoxelBusters.Utility;

namespace VoxelBusters.NativePlugins.Internal
{
	internal sealed class AndroidAchievementDescription : AchievementDescription
	{
		private const string kIdentifier = "identifier";

		private const string kTitle = "title";

		private const string kAcheivedDescription = "achieved-description";

		private const string kUnAcheivedDescription = "un-achieved-description";

		private const string kMaximumPoints = "maximum-points";

		private const string kImagePath = "image-path";

		private const string kState = "state";

		private const string kStateHidden = "state-hidden";

		private const string kStateUnlocked = "state-unlocked";

		private const string kStateRevealed = "state-revealed";

		private const string kType = "type";

		private const string kTypeStandard = "type-standard";

		private const string kTypeIncremental = "type-incremental";

		private string m_identifier;

		private string m_title;

		private string m_achievedDescription;

		private string m_unachievedDescription;

		private int m_maximumPoints;

		private bool m_isHidden;

		private string m_imagePath;

		public override string Identifier
		{
			get
			{
				return m_identifier;
			}
			protected set
			{
				throw new Exception("[GameServices] Only getter is supported.");
			}
		}

		public override string Title
		{
			get
			{
				return m_title;
			}
			protected set
			{
				throw new Exception("[GameServices] Only getter is supported.");
			}
		}

		public override string AchievedDescription
		{
			get
			{
				return m_achievedDescription;
			}
			protected set
			{
				throw new Exception("[GameServices] Only getter is supported.");
			}
		}

		public override string UnachievedDescription
		{
			get
			{
				return m_unachievedDescription;
			}
			protected set
			{
				throw new Exception("[GameServices] Only getter is supported.");
			}
		}

		[Obsolete("This property is deprecated. Instead use NPBinding.GameServices.GetNoOfStepsForCompletingAchievement method.")]
		public override int MaximumPoints
		{
			get
			{
				return m_maximumPoints;
			}
			protected set
			{
				throw new Exception("[GameServices] Only getter is supported.");
			}
		}

		public override bool IsHidden
		{
			get
			{
				return m_isHidden;
			}
			protected set
			{
				throw new Exception("[GameServices] Only getter is supported.");
			}
		}

		private AndroidAchievementDescription()
		{
		}

		internal AndroidAchievementDescription(IDictionary _descriptionData)
		{
			m_identifier = _descriptionData.GetIfAvailable<string>("identifier");
			m_title = _descriptionData.GetIfAvailable<string>("title");
			m_unachievedDescription = _descriptionData.GetIfAvailable<string>("un-achieved-description");
			m_achievedDescription = GetAchievedDescription(m_title);
			m_maximumPoints = _descriptionData.GetIfAvailable("maximum-points", 0);
			string ifAvailable = _descriptionData.GetIfAvailable<string>("state");
			if (ifAvailable.Equals("state-hidden"))
			{
				m_isHidden = true;
			}
			m_imagePath = _descriptionData.GetIfAvailable<string>("image-path");
		}

		internal static AchievementDescription[] ConvertAchievementDescriptionList(IList _achievementDescriptionList)
		{
			if (_achievementDescriptionList == null)
			{
				return null;
			}
			int count = _achievementDescriptionList.Count;
			AchievementDescription[] array = new AndroidAchievementDescription[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = new AndroidAchievementDescription(_achievementDescriptionList[i] as IDictionary);
			}
			return array;
		}

		protected override void RequestForImage()
		{
			string error = null;
			if (string.IsNullOrEmpty(m_imagePath))
			{
				error = "Image not available!";
			}
			RequestForImageFinished(URL.URLWithString(m_imagePath), error);
		}

		private string GetAchievedDescription(string _achievementTitle)
		{
			string[] achievedDescriptionFormats = NPSettings.GameServicesSettings.Android.AchievedDescriptionFormats;
			if (achievedDescriptionFormats != null && achievedDescriptionFormats.Length > 0)
			{
				int num = UnityEngine.Random.Range(0, achievedDescriptionFormats.Length);
				string text = achievedDescriptionFormats[num];
				return text.Replace("#", _achievementTitle);
			}
			return _achievementTitle;
		}
	}
}
