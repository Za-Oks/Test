using System;
using UnityEngine;

namespace VoxelBusters.UASUtils
{
	public class NullLogger : ILogger, ILogHandler
	{
		public ILogHandler logHandler
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public bool logEnabled
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		public LogType filterLogType
		{
			get
			{
				return LogType.Log;
			}
			set
			{
			}
		}

		public bool IsLogTypeAllowed(LogType logType)
		{
			return false;
		}

		public void Log(LogType logType, object message)
		{
		}

		public void Log(LogType logType, object message, UnityEngine.Object context)
		{
		}

		public void Log(LogType logType, string tag, object message)
		{
		}

		public void Log(LogType logType, string tag, object message, UnityEngine.Object context)
		{
		}

		public void Log(object message)
		{
		}

		public void Log(string tag, object message)
		{
		}

		public void Log(string tag, object message, UnityEngine.Object context)
		{
		}

		public void LogWarning(string tag, object message)
		{
		}

		public void LogWarning(string tag, object message, UnityEngine.Object context)
		{
		}

		public void LogError(string tag, object message)
		{
		}

		public void LogError(string tag, object message, UnityEngine.Object context)
		{
		}

		public void LogFormat(LogType logType, string format, params object[] args)
		{
		}

		public void LogException(Exception exception)
		{
		}

		public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
		{
		}

		public void LogException(Exception exception, UnityEngine.Object context)
		{
		}
	}
}
