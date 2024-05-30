using System.Collections;
using UnityEngine;
using VoxelBusters.NativePlugins.Internal;
using VoxelBusters.Utility;

namespace VoxelBusters.NativePlugins
{
	public abstract class User : NPObject
	{
		public delegate void LoadUsersCompletion(User[] _users, string _error);

		private Texture2D m_image;

		public abstract string Identifier { get; protected set; }

		public abstract string Name { get; protected set; }

		public abstract string Alias { get; protected set; }

		protected event DownloadTexture.Completion DownloadImageFinishedEvent;

		protected User()
			: base(NPObjectManager.eCollectionType.GAME_SERVICES)
		{
		}

		public virtual void GetImageAsync(DownloadTexture.Completion _onCompletion)
		{
			this.DownloadImageFinishedEvent = _onCompletion;
			if (m_image != null)
			{
				this.DownloadImageFinishedEvent(m_image, null);
			}
			else
			{
				RequestForImage();
			}
		}

		protected virtual void RequestForImage()
		{
		}

		public override string ToString()
		{
			return string.Format("[User: Name={0}]", Name);
		}

		protected virtual void RequestForImageFinished(IDictionary _dataDict)
		{
		}

		protected void RequestForImageFinished(URL _imagePathURL, string _error)
		{
			if (_error != null)
			{
				DownloadImageFinished(null, _error);
				return;
			}
			DownloadTexture downloadTexture = new DownloadTexture(_imagePathURL, true, true);
			downloadTexture.OnCompletion = delegate(Texture2D _image, string _downloadError)
			{
				DownloadImageFinished(_image, _downloadError);
			};
			downloadTexture.StartRequest();
		}

		protected void DownloadImageFinished(Texture2D _image, string _error)
		{
			m_image = _image;
			if (this.DownloadImageFinishedEvent != null)
			{
				this.DownloadImageFinishedEvent(_image, _error);
			}
		}
	}
}
