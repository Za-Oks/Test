using System;
using System.Collections;
using UnityEngine;
using VoxelBusters.NativePlugins.Internal;
using VoxelBusters.Utility;

namespace VoxelBusters.NativePlugins
{
	public abstract class AchievementDescription : NPObject
	{
		public delegate void LoadAchievementDescriptionsCompletion(AchievementDescription[] _descriptions, string _error);

		private Texture2D m_image;

		public string GlobalIdentifier { get; protected set; }

		public abstract string Identifier { get; protected set; }

		public abstract string Title { get; protected set; }

		public abstract string AchievedDescription { get; protected set; }

		public abstract string UnachievedDescription { get; protected set; }

		public abstract bool IsHidden { get; protected set; }

		public string InstanceID { get; private set; }

		[Obsolete("This property is deprecated. Instead use NPBinding.GameServices.GetNoOfStepsForCompletingAchievement method.")]
		public abstract int MaximumPoints { get; protected set; }

		private event DownloadTexture.Completion DownloadImageFinishedEvent;

		protected AchievementDescription()
			: base(NPObjectManager.eCollectionType.GAME_SERVICES)
		{
		}

		public void GetImageAsync(DownloadTexture.Completion _onCompletion)
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
			return string.Format("[AchievementDescription: Identifier={0}, Title={1}, IsHidden={2}]", Identifier, Title, IsHidden);
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
