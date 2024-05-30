using UnityEngine;
using VoxelBusters.Utility;

namespace VoxelBusters.NativePlugins.Demo
{
	public class RemoteNotificationTest : NPDisabledFeatureDemo
	{
		[SerializeField]
		[EnumMaskField(typeof(NotificationType))]
		private NotificationType m_notificationType;
	}
}
