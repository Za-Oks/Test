using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.UASUtils;

public class AndroidPluginUtility
{
	private static Dictionary<string, AndroidJavaObject> sSingletonInstances = new Dictionary<string, AndroidJavaObject>();

	public static AndroidJavaObject GetSingletonInstance(string _className, string _methodName = "getInstance")
	{
		AndroidJavaObject value;
		sSingletonInstances.TryGetValue(_className, out value);
		if (value == null)
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass(_className);
			if (androidJavaClass == null)
			{
				DebugUtility.Logger.LogError("Native Plugins", "Class=" + _className + " not found!");
				return null;
			}
			value = androidJavaClass.CallStatic<AndroidJavaObject>(_methodName, new object[0]);
			sSingletonInstances.Add(_className, value);
		}
		return value;
	}

	public static AndroidJavaClass CreateClassObject(string _className)
	{
		AndroidJavaClass androidJavaClass = new AndroidJavaClass(_className);
		if (androidJavaClass == null)
		{
			DebugUtility.Logger.LogError("Native Plugins", "Class=" + _className + " not found!");
		}
		return androidJavaClass;
	}
}
