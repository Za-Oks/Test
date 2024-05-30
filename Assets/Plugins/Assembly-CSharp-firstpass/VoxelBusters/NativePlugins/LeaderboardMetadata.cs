using System;
using UnityEngine;

namespace VoxelBusters.NativePlugins
{
	[Serializable]
	public class LeaderboardMetadata : IIdentifierContainer
	{
		[SerializeField]
		[Tooltip("String used to uniquely identify achievement across all the platforms.")]
		private string m_globalID;

		[SerializeField]
		[Tooltip("Collection of identifiers, where each identifier is used to identify achievement in a specific platform game server.")]
		private PlatformValue[] m_platformIDs;

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

		internal static LeaderboardMetadata Create(IDContainer _container)
		{
			LeaderboardMetadata leaderboardMetadata = new LeaderboardMetadata();
			leaderboardMetadata.m_globalID = _container.GlobalID;
			leaderboardMetadata.m_platformIDs = _container.PlatformIDs;
			return leaderboardMetadata;
		}
	}
}
