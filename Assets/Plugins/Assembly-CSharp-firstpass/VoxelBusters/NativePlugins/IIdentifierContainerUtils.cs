using UnityEngine;

namespace VoxelBusters.NativePlugins
{
	public static class IIdentifierContainerUtils
	{
		public static IIdentifierContainer FindObjectWithGlobalID(this IIdentifierContainer[] _collection, string _globalID)
		{
			foreach (IIdentifierContainer identifierContainer in _collection)
			{
				if (string.Equals(identifierContainer.GlobalID, _globalID))
				{
					return identifierContainer;
				}
			}
			Debug.Log(string.Format("[IDContainer] Couldn't find id container with global ID= {0}.", _globalID));
			return null;
		}

		public static IIdentifierContainer FindObjectWithPlatformID(this IIdentifierContainer[] _collection, string _platformID)
		{
			foreach (IIdentifierContainer identifierContainer in _collection)
			{
				string currentPlatformID = identifierContainer.GetCurrentPlatformID();
				if (string.Equals(currentPlatformID, _platformID))
				{
					return identifierContainer;
				}
			}
			Debug.Log(string.Format("[IDContainer] Couldn't find id container with platform ID= {0}.", _platformID));
			return null;
		}

		public static string GetCurrentPlatformID(this IIdentifierContainer _object)
		{
			PlatformValue currentPlatformValue = PlatformValueHelper.GetCurrentPlatformValue(_object.PlatformIDs);
			if (currentPlatformValue == null)
			{
				return null;
			}
			return currentPlatformValue.Value;
		}
	}
}
