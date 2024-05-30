using System;
using System.IO;
using ExifLibrary;
using UnityEngine;

namespace VoxelBusters.Utility
{
	public class DownloadTexture : Request
	{
		public delegate void Completion(Texture2D _texture, string _error);

		public bool AutoFixOrientation { get; set; }

		public float ScaleFactor { get; set; }

		public Completion OnCompletion { get; set; }

		public DownloadTexture(URL _URL, bool _isAsynchronous, bool _autoFixOrientation)
			: base(_URL, _isAsynchronous)
		{
			AutoFixOrientation = _autoFixOrientation;
			base.WWWObject = new WWW(_URL.URLString);
			ScaleFactor = 1f;
		}

		protected override void DidFailStartRequestWithError(string _error)
		{
			if (OnCompletion != null)
			{
				OnCompletion(null, _error);
			}
		}

		protected override void OnFetchingResponse()
		{
			Texture2D texture2D = null;
			if (!string.IsNullOrEmpty(base.WWWObject.error))
			{
				Debug.Log("[DownloadTexture] Failed to download texture. Error = " + base.WWWObject.error + ".");
				if (OnCompletion != null)
				{
					OnCompletion(null, base.WWWObject.error);
					return;
				}
			}
			Texture2D texture2D2 = base.WWWObject.texture;
			if (AutoFixOrientation)
			{
				Stream fileStream = new MemoryStream(base.WWWObject.bytes);
				ExifFile exifFile = null;
				try
				{
					exifFile = ExifFile.Read(fileStream);
				}
				catch (Exception ex)
				{
					Debug.LogError("[DownloadTexture] Failed loading Exif data : " + ex);
				}
				if (ScaleFactor != 1f)
				{
					texture2D2 = texture2D2.Scale(ScaleFactor);
				}
				if (exifFile != null && exifFile.Properties.ContainsKey(ExifTag.Orientation))
				{
					Orientation orientation = (Orientation)exifFile.Properties[ExifTag.Orientation].Value;
					Debug.Log("[DownloadTexture] Orientation=" + orientation);
					switch (orientation)
					{
					case Orientation.Normal:
						texture2D = texture2D2;
						break;
					case Orientation.MirroredVertically:
						texture2D = texture2D2.MirrorTexture(true, false);
						break;
					case Orientation.Rotated180:
						texture2D = texture2D2.MirrorTexture(true, true);
						break;
					case Orientation.MirroredHorizontally:
						texture2D = texture2D2.MirrorTexture(false, true);
						break;
					case Orientation.RotatedLeftAndMirroredVertically:
						texture2D = texture2D2.MirrorTexture(true, false).Rotate(-90f);
						break;
					case Orientation.RotatedRight:
						texture2D = texture2D2.Rotate(90f);
						break;
					case Orientation.RotatedLeft:
						texture2D = texture2D2.MirrorTexture(false, true).Rotate(-90f);
						break;
					case Orientation.RotatedRightAndMirroredVertically:
						texture2D = texture2D2.Rotate(-90f);
						break;
					default:
						texture2D = texture2D2;
						Debug.LogWarning("[DownloadTexture] Unknown orientation found : " + orientation);
						break;
					}
				}
				else
				{
					texture2D = texture2D2;
				}
			}
			else
			{
				if (ScaleFactor != 1f)
				{
					texture2D2 = texture2D2.Scale(ScaleFactor);
				}
				texture2D = texture2D2;
			}
			if (OnCompletion != null)
			{
				OnCompletion(texture2D, null);
			}
		}
	}
}
