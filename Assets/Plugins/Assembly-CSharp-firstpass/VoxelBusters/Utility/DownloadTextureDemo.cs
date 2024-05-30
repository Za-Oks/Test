using UnityEngine;

namespace VoxelBusters.Utility
{
	public class DownloadTextureDemo : MonoBehaviour
	{
		[SerializeField]
		private string m_URLString;

		[SerializeField]
		private MeshRenderer m_renderer;

		public void StartDownload()
		{
			URL uRL = ((!m_URLString.StartsWith("http")) ? URL.FileURLWithPath(m_URLString) : URL.URLWithString(m_URLString));
			DownloadTexture downloadTexture = new DownloadTexture(uRL, true, true);
			downloadTexture.OnCompletion = delegate(Texture2D _texture, string _error)
			{
				Debug.Log(string.Format("[DownloadTextureDemo] Texture download completed. Error= {0}.", _error.GetPrintableString()));
				if (_texture != null)
				{
					m_renderer.sharedMaterial.mainTexture = _texture;
				}
			};
			downloadTexture.StartRequest();
		}
	}
}
