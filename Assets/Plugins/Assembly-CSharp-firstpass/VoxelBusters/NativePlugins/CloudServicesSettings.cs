using System;
using UnityEngine;

namespace VoxelBusters.NativePlugins
{
	[Serializable]
	public class CloudServicesSettings
	{
		[Serializable]
		internal class AndroidSettings
		{
			[SerializeField]
			[Tooltip("Automatic internal Sync timer to sync with cloud. Tries to connect to cloud, load the data and upload if any changes to cloud.")]
			private float m_syncInterval = 10f;

			internal float SyncInterval
			{
				get
				{
					return m_syncInterval;
				}
			}
		}

		[SerializeField]
		private AndroidSettings m_android;

		internal AndroidSettings Android
		{
			get
			{
				return m_android;
			}
		}
	}
}
