using System;
using UnityEngine;

namespace VoxelBusters.NativePlugins
{
	[Serializable]
	public class AchievementMetadata : IIdentifierContainer
	{
		[SerializeField]
		[Tooltip("String used to uniquely identify achievement across all the platforms.")]
		private string m_globalID;

		[SerializeField]
		[Tooltip("Collection of identifiers, where each identifier is used to identify achievement in a specific platform game server.")]
		private PlatformValue[] m_platformIDs;

		[SerializeField]
		[Tooltip("The number of steps required to complete an achievement. Must be greater than 0.")]
		private int m_noOfSteps = 1;

		public int NoOfSteps
		{
			get
			{
				return m_noOfSteps;
			}
			set
			{
				m_noOfSteps = value;
			}
		}

		public string GlobalID
		{
			get
			{
				return m_globalID;
			}
			set
			{
				m_globalID = value;
			}
		}

		public PlatformValue[] PlatformIDs
		{
			get
			{
				return m_platformIDs;
			}
			set
			{
				m_platformIDs = value;
			}
		}

		public AchievementMetadata()
		{
			m_noOfSteps = 1;
		}

		internal static AchievementMetadata Create(IDContainer _container)
		{
			AchievementMetadata achievementMetadata = new AchievementMetadata();
			achievementMetadata.m_globalID = _container.GlobalID;
			achievementMetadata.m_platformIDs = _container.PlatformIDs;
			return achievementMetadata;
		}
	}
}
