using UnityEngine;

namespace VoxelBusters.UASUtils
{
	public class DebugUtility
	{
		private static ILogger nullLogger;

		public static ILogger Logger
		{
			get
			{
				return GetLoggerInstance();
			}
		}

		private static ILogger GetLoggerInstance()
		{
			if (nullLogger == null)
			{
				nullLogger = new NullLogger();
			}
			return nullLogger;
		}
	}
}
