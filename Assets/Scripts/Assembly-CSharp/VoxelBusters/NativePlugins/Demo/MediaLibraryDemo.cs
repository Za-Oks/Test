using UnityEngine;

namespace VoxelBusters.NativePlugins.Demo
{
	public class MediaLibraryDemo : NPDisabledFeatureDemo
	{
		[SerializeField]
		[Header("Video Section")]
		[Tooltip("This needs to be direct link to the video file. ex: http://www.google.com/video.mp4")]
		private string m_videoURL;

		[SerializeField]
		private string m_youtubeVideoID;

		[SerializeField]
		private TextAsset m_vimeoPlayerHTML;

		[SerializeField]
		private string m_vimeoVideoID;

		private bool m_canEditImages = true;

		private Texture m_texture;
	}
}
