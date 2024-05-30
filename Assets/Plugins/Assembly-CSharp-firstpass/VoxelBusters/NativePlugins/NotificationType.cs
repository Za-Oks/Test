using System;

namespace VoxelBusters.NativePlugins
{
	[Flags]
	public enum NotificationType
	{
		Badge = 1,
		Sound = 2,
		Alert = 4
	}
}
