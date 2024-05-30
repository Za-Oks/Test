using System;
using UnityEngine;

namespace VoxelBusters.NativePlugins
{
	[Serializable]
	public class WebViewSettings
	{
		[Serializable]
		internal class AndroidSettings
		{
			[SerializeField]
			[Tooltip("Set this to make use of full performance if you have a full screen activity. But, the events you pass from webview to Unity will be queued and fired once you close the webview.")]
			private bool m_useNewActivityForFullScreenWebView;

			internal bool UseNewActivityForFullScreenWebView
			{
				get
				{
					return m_useNewActivityForFullScreenWebView;
				}
			}
		}

		[SerializeField]
		private AndroidSettings m_android;

		internal AndroidSettings Android
		{
			get
			{
				return m_android;
			}
		}
	}
}
