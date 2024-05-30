using System.IO;
using UnityEngine;

namespace VoxelBusters.NativePlugins.Demo
{
	public class Utility : MonoBehaviour
	{
		private void Start()
		{
			ScreenCapture.CaptureScreenshot("Screenshot.png");
		}

		public static string GetScreenshotPath()
		{
			return Path.Combine(Application.persistentDataPath, "Screenshot.png");
		}
	}
}
