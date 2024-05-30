using UnityEngine;

namespace VoxelBusters.NativePlugins.Demo
{
	public class TwitterDemo : NPDisabledFeatureDemo
	{
		[SerializeField]
		private string m_shareMessage = "this is what I wanted to share";

		[SerializeField]
		private string m_shareURL = "http://www.voxelbusters.com";

		[SerializeField]
		private Texture2D m_shareImage;
	}
}
