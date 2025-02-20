using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Spine.Unity
{
	[CreateAssetMenu(fileName = "New SkeletonDataAsset", menuName = "Spine/SkeletonData Asset")]
	public class SkeletonDataAsset : ScriptableObject
	{
		public AtlasAssetBase[] atlasAssets = new AtlasAssetBase[0];

		public float scale = 0.01f;

		public TextAsset skeletonJSON;

		[Tooltip("Use SkeletonDataModifierAssets to apply changes to the SkeletonData after being loaded, such as apply blend mode Materials to Attachments under slots with special blend modes.")]
		public List<SkeletonDataModifierAsset> skeletonDataModifiers = new List<SkeletonDataModifierAsset>();

		[SpineAnimation("", "", false, false)]
		public string[] fromAnimation = new string[0];

		[SpineAnimation("", "", false, false)]
		public string[] toAnimation = new string[0];

		public float[] duration = new float[0];

		public float defaultMix;

		public RuntimeAnimatorController controller;

		private SkeletonData skeletonData;

		private AnimationStateData stateData;

		public bool IsLoaded
		{
			get
			{
				return skeletonData != null;
			}
		}

		private void Reset()
		{
			Clear();
		}

		public static SkeletonDataAsset CreateRuntimeInstance(TextAsset skeletonDataFile, AtlasAssetBase atlasAsset, bool initialize, float scale = 0.01f)
		{
			return CreateRuntimeInstance(skeletonDataFile, new AtlasAssetBase[1] { atlasAsset }, initialize, scale);
		}

		public static SkeletonDataAsset CreateRuntimeInstance(TextAsset skeletonDataFile, AtlasAssetBase[] atlasAssets, bool initialize, float scale = 0.01f)
		{
			SkeletonDataAsset skeletonDataAsset = ScriptableObject.CreateInstance<SkeletonDataAsset>();
			skeletonDataAsset.Clear();
			skeletonDataAsset.skeletonJSON = skeletonDataFile;
			skeletonDataAsset.atlasAssets = atlasAssets;
			skeletonDataAsset.scale = scale;
			if (initialize)
			{
				skeletonDataAsset.GetSkeletonData(true);
			}
			return skeletonDataAsset;
		}

		public void Clear()
		{
			skeletonData = null;
			stateData = null;
		}

		public AnimationStateData GetAnimationStateData()
		{
			if (stateData != null)
			{
				return stateData;
			}
			GetSkeletonData(false);
			return stateData;
		}

		public SkeletonData GetSkeletonData(bool quiet)
		{
			if (skeletonJSON == null)
			{
				if (!quiet)
				{
					Debug.LogError("Skeleton JSON file not set for SkeletonData asset: " + base.name, this);
				}
				Clear();
				return null;
			}
			if (skeletonData != null)
			{
				return skeletonData;
			}
			Atlas[] atlasArray = GetAtlasArray();
			object obj;
			if (atlasArray.Length == 0)
			{
				AttachmentLoader attachmentLoader = new RegionlessAttachmentLoader();
				obj = attachmentLoader;
			}
			else
			{
				obj = new AtlasAttachmentLoader(atlasArray);
			}
			AttachmentLoader attachmentLoader2 = (AttachmentLoader)obj;
			float num = scale;
			bool flag = skeletonJSON.name.ToLower().Contains(".skel");
			SkeletonData sd;
			try
			{
				sd = ((!flag) ? ReadSkeletonData(skeletonJSON.text, attachmentLoader2, num) : ReadSkeletonData(skeletonJSON.bytes, attachmentLoader2, num));
			}
			catch (Exception ex)
			{
				if (!quiet)
				{
					Debug.LogError("Error reading skeleton JSON file for SkeletonData asset: " + base.name + "\n" + ex.Message + "\n" + ex.StackTrace, this);
				}
				return null;
			}
			if (skeletonDataModifiers != null)
			{
				foreach (SkeletonDataModifierAsset skeletonDataModifier in skeletonDataModifiers)
				{
					if (skeletonDataModifier != null)
					{
						skeletonDataModifier.Apply(sd);
					}
				}
			}
			InitializeWithData(sd);
			return skeletonData;
		}

		internal void InitializeWithData(SkeletonData sd)
		{
			skeletonData = sd;
			stateData = new AnimationStateData(skeletonData);
			FillStateData();
		}

		public void FillStateData()
		{
			if (stateData == null)
			{
				return;
			}
			stateData.defaultMix = defaultMix;
			int i = 0;
			for (int num = fromAnimation.Length; i < num; i++)
			{
				if (fromAnimation[i].Length != 0 && toAnimation[i].Length != 0)
				{
					stateData.SetMix(fromAnimation[i], toAnimation[i], duration[i]);
				}
			}
		}

		internal Atlas[] GetAtlasArray()
		{
			List<Atlas> list = new List<Atlas>(atlasAssets.Length);
			for (int i = 0; i < atlasAssets.Length; i++)
			{
				AtlasAssetBase atlasAssetBase = atlasAssets[i];
				if (!(atlasAssetBase == null))
				{
					Atlas atlas = atlasAssetBase.GetAtlas();
					if (atlas != null)
					{
						list.Add(atlas);
					}
				}
			}
			return list.ToArray();
		}

		internal static SkeletonData ReadSkeletonData(byte[] bytes, AttachmentLoader attachmentLoader, float scale)
		{
			using (MemoryStream input = new MemoryStream(bytes))
			{
				SkeletonBinary skeletonBinary = new SkeletonBinary(attachmentLoader);
				skeletonBinary.Scale = scale;
				SkeletonBinary skeletonBinary2 = skeletonBinary;
				return skeletonBinary2.ReadSkeletonData(input);
			}
		}

		internal static SkeletonData ReadSkeletonData(string text, AttachmentLoader attachmentLoader, float scale)
		{
			StringReader reader = new StringReader(text);
			SkeletonJson skeletonJson = new SkeletonJson(attachmentLoader);
			skeletonJson.Scale = scale;
			SkeletonJson skeletonJson2 = skeletonJson;
			return skeletonJson2.ReadSkeletonData(reader);
		}
	}
}
