using System.Collections;
using UnityEngine;
using VoxelBusters.Utility;

namespace VoxelBusters.NativePlugins.Demo
{
	public class NotificationDemo : NPDisabledFeatureDemo
	{
		[SerializeField]
		[EnumMaskField(typeof(NotificationType))]
		private NotificationType m_notificationType;

		private ArrayList m_scheduledNotificationIDList = new ArrayList();
	}
}
