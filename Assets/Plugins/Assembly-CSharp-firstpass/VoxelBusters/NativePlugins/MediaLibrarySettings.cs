using System;
using UnityEngine;
using VoxelBusters.NativePlugins.Internal;

namespace VoxelBusters.NativePlugins
{
	[Serializable]
	public class MediaLibrarySettings
	{
		[Serializable]
		public class AndroidSettings
		{
			[SerializeField]
			[NotifyNPSettingsOnValueChange]
			[Tooltip("Youtube API key assigned to your application.")]
			private string m_youtubeAPIKey;

			[SerializeField]
			[Tooltip("If you set this to false, the images will be saved to default gallery. Else to app specific album.")]
			private bool m_saveToGallerySavesToAppFolder = true;

			internal string YoutubeAPIKey
			{
				get
				{
					return m_youtubeAPIKey;
				}
			}

			internal bool SaveGalleryImagesToAppSpecificFolder
			{
				get
				{
					return m_saveToGallerySavesToAppFolder;
				}
			}
		}

		[SerializeField]
		private AndroidSettings m_android = new AndroidSettings();

		internal AndroidSettings Android
		{
			get
			{
				return m_android;
			}
		}
	}
}
