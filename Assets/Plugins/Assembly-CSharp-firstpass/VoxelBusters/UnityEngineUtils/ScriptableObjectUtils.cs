using System.IO;
using UnityEngine;
using VoxelBusters.Utility;

namespace VoxelBusters.UnityEngineUtils
{
	public static class ScriptableObjectUtils
	{
		public static T LoadAssetAtPath<T>(string path) where T : ScriptableObject
		{
			string path2 = "Assets/Resources/";
			string path3 = Path.GetFullPath(path2).MakeRelativePath(Path.GetFullPath(path));
			path3 = Path.ChangeExtension(path3, null);
			return Resources.Load<T>(path3);
		}
	}
}
