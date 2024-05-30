using UnityEngine;

namespace VoxelBusters.NativePlugins.Demo
{
	public class WebViewDemo : NPDisabledFeatureDemo
	{
		[SerializeField]
		private string m_url;

		[SerializeField]
		[Multiline(6)]
		private string m_HTMLString;

		[SerializeField]
		private string m_javaScript;

		[SerializeField]
		private string m_evalString;

		[SerializeField]
		private string[] m_schemeList = new string[3] { "unity", "mailto", "tel" };
	}
}
