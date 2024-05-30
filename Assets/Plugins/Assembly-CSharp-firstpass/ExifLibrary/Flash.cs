using System;

namespace ExifLibrary
{
	[Flags]
	public enum Flash : ushort
	{
		FlashDidNotFire = 0,
		StrobeReturnLightNotDetected = 4,
		StrobeReturnLightDetected = 2,
		FlashFired = 1,
		CompulsoryFlashMode = 8,
		AutoMode = 0x10,
		NoFlashFunction = 0x20,
		RedEyeReductionMode = 0x40
	}
}
