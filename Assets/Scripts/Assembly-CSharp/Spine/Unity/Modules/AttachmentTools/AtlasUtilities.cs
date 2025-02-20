using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Spine.Unity.Modules.AttachmentTools
{
	public static class AtlasUtilities
	{
		internal const TextureFormat SpineTextureFormat = TextureFormat.RGBA32;

		internal const float DefaultMipmapBias = -0.5f;

		internal const bool UseMipMaps = false;

		internal const float DefaultScale = 0.01f;

		private const int NonrenderingRegion = -1;

		private static Dictionary<AtlasRegion, Texture2D> CachedRegionTextures = new Dictionary<AtlasRegion, Texture2D>();

		private static List<Texture2D> CachedRegionTexturesList = new List<Texture2D>();

		public static AtlasRegion ToAtlasRegion(this Texture2D t, Material materialPropertySource, float scale = 0.01f)
		{
			return t.ToAtlasRegion(materialPropertySource.shader, scale, materialPropertySource);
		}

		public static AtlasRegion ToAtlasRegion(this Texture2D t, Shader shader, float scale = 0.01f, Material materialPropertySource = null)
		{
			Material material = new Material(shader);
			if (materialPropertySource != null)
			{
				material.CopyPropertiesFromMaterial(materialPropertySource);
				material.shaderKeywords = materialPropertySource.shaderKeywords;
			}
			material.mainTexture = t;
			AtlasPage page = material.ToSpineAtlasPage();
			float num = t.width;
			float num2 = t.height;
			AtlasRegion atlasRegion = new AtlasRegion();
			atlasRegion.name = t.name;
			atlasRegion.index = -1;
			atlasRegion.rotate = false;
			Vector2 zero = Vector2.zero;
			Vector2 vector = new Vector2(num, num2) * scale;
			atlasRegion.width = (int)num;
			atlasRegion.originalWidth = (int)num;
			atlasRegion.height = (int)num2;
			atlasRegion.originalHeight = (int)num2;
			atlasRegion.offsetX = num * (0.5f - InverseLerp(zero.x, vector.x, 0f));
			atlasRegion.offsetY = num2 * (0.5f - InverseLerp(zero.y, vector.y, 0f));
			atlasRegion.u = 0f;
			atlasRegion.v = 1f;
			atlasRegion.u2 = 1f;
			atlasRegion.v2 = 0f;
			atlasRegion.x = 0;
			atlasRegion.y = 0;
			atlasRegion.page = page;
			return atlasRegion;
		}

		public static AtlasRegion ToAtlasRegionPMAClone(this Texture2D t, Material materialPropertySource, TextureFormat textureFormat = TextureFormat.RGBA32, bool mipmaps = false)
		{
			return t.ToAtlasRegionPMAClone(materialPropertySource.shader, textureFormat, mipmaps, materialPropertySource);
		}

		public static AtlasRegion ToAtlasRegionPMAClone(this Texture2D t, Shader shader, TextureFormat textureFormat = TextureFormat.RGBA32, bool mipmaps = false, Material materialPropertySource = null)
		{
			Material material = new Material(shader);
			if (materialPropertySource != null)
			{
				material.CopyPropertiesFromMaterial(materialPropertySource);
				material.shaderKeywords = materialPropertySource.shaderKeywords;
			}
			Texture2D clone = t.GetClone(textureFormat, mipmaps);
			clone.ApplyPMA();
			clone.name = t.name + "-pma-";
			material.name = t.name + shader.name;
			material.mainTexture = clone;
			AtlasPage page = material.ToSpineAtlasPage();
			AtlasRegion atlasRegion = clone.ToAtlasRegion(shader);
			atlasRegion.page = page;
			return atlasRegion;
		}

		public static AtlasPage ToSpineAtlasPage(this Material m)
		{
			AtlasPage atlasPage = new AtlasPage();
			atlasPage.rendererObject = m;
			atlasPage.name = m.name;
			AtlasPage atlasPage2 = atlasPage;
			Texture mainTexture = m.mainTexture;
			if (mainTexture != null)
			{
				atlasPage2.width = mainTexture.width;
				atlasPage2.height = mainTexture.height;
			}
			return atlasPage2;
		}

		public static AtlasRegion ToAtlasRegion(this Sprite s, AtlasPage page)
		{
			if (page == null)
			{
				throw new ArgumentNullException("page", "page cannot be null. AtlasPage determines which texture region belongs and how it should be rendered. You can use material.ToSpineAtlasPage() to get a shareable AtlasPage from a Material, or use the sprite.ToAtlasRegion(material) overload.");
			}
			AtlasRegion atlasRegion = s.ToAtlasRegion();
			atlasRegion.page = page;
			return atlasRegion;
		}

		public static AtlasRegion ToAtlasRegion(this Sprite s, Material material)
		{
			AtlasRegion atlasRegion = s.ToAtlasRegion();
			atlasRegion.page = material.ToSpineAtlasPage();
			return atlasRegion;
		}

		public static AtlasRegion ToAtlasRegionPMAClone(this Sprite s, Material materialPropertySource, TextureFormat textureFormat = TextureFormat.RGBA32, bool mipmaps = false)
		{
			return s.ToAtlasRegionPMAClone(materialPropertySource.shader, textureFormat, mipmaps, materialPropertySource);
		}

		public static AtlasRegion ToAtlasRegionPMAClone(this Sprite s, Shader shader, TextureFormat textureFormat = TextureFormat.RGBA32, bool mipmaps = false, Material materialPropertySource = null)
		{
			Material material = new Material(shader);
			if (materialPropertySource != null)
			{
				material.CopyPropertiesFromMaterial(materialPropertySource);
				material.shaderKeywords = materialPropertySource.shaderKeywords;
			}
			Texture2D texture2D = s.ToTexture(textureFormat, mipmaps);
			texture2D.ApplyPMA();
			texture2D.name = s.name + "-pma-";
			material.name = texture2D.name + shader.name;
			material.mainTexture = texture2D;
			AtlasPage page = material.ToSpineAtlasPage();
			AtlasRegion atlasRegion = s.ToAtlasRegion(true);
			atlasRegion.page = page;
			return atlasRegion;
		}

		internal static AtlasRegion ToAtlasRegion(this Sprite s, bool isolatedTexture = false)
		{
			AtlasRegion atlasRegion = new AtlasRegion();
			atlasRegion.name = s.name;
			atlasRegion.index = -1;
			atlasRegion.rotate = s.packed && s.packingRotation != SpritePackingRotation.None;
			Bounds bounds = s.bounds;
			Vector2 vector = bounds.min;
			Vector2 vector2 = bounds.max;
			Rect rect = s.rect.SpineUnityFlipRect(s.texture.height);
			atlasRegion.width = (int)rect.width;
			atlasRegion.originalWidth = (int)rect.width;
			atlasRegion.height = (int)rect.height;
			atlasRegion.originalHeight = (int)rect.height;
			atlasRegion.offsetX = rect.width * (0.5f - InverseLerp(vector.x, vector2.x, 0f));
			atlasRegion.offsetY = rect.height * (0.5f - InverseLerp(vector.y, vector2.y, 0f));
			if (isolatedTexture)
			{
				atlasRegion.u = 0f;
				atlasRegion.v = 1f;
				atlasRegion.u2 = 1f;
				atlasRegion.v2 = 0f;
				atlasRegion.x = 0;
				atlasRegion.y = 0;
			}
			else
			{
				Texture2D texture = s.texture;
				Rect rect2 = TextureRectToUVRect(s.textureRect, texture.width, texture.height);
				atlasRegion.u = rect2.xMin;
				atlasRegion.v = rect2.yMax;
				atlasRegion.u2 = rect2.xMax;
				atlasRegion.v2 = rect2.yMin;
				atlasRegion.x = (int)rect.x;
				atlasRegion.y = (int)rect.y;
			}
			return atlasRegion;
		}

		public static void GetRepackedAttachments(List<Attachment> sourceAttachments, List<Attachment> outputAttachments, Material materialPropertySource, out Material outputMaterial, out Texture2D outputTexture, int maxAtlasSize = 1024, int padding = 2, TextureFormat textureFormat = TextureFormat.RGBA32, bool mipmaps = false, string newAssetName = "Repacked Attachments", bool clearCache = false, bool useOriginalNonrenderables = true)
		{
			if (sourceAttachments == null)
			{
				throw new ArgumentNullException("sourceAttachments");
			}
			if (outputAttachments == null)
			{
				throw new ArgumentNullException("outputAttachments");
			}
			Dictionary<AtlasRegion, int> dictionary = new Dictionary<AtlasRegion, int>();
			List<int> list = new List<int>();
			List<Texture2D> list2 = new List<Texture2D>();
			List<AtlasRegion> list3 = new List<AtlasRegion>();
			outputAttachments.Clear();
			outputAttachments.AddRange(sourceAttachments);
			int num = 0;
			int i = 0;
			for (int count = sourceAttachments.Count; i < count; i++)
			{
				Attachment attachment = sourceAttachments[i];
				if (IsRenderable(attachment))
				{
					Attachment clone = attachment.GetClone(true);
					AtlasRegion region = clone.GetRegion();
					int value;
					if (dictionary.TryGetValue(region, out value))
					{
						list.Add(value);
					}
					else
					{
						list3.Add(region);
						list2.Add(region.ToTexture());
						dictionary.Add(region, num);
						list.Add(num);
						num++;
					}
					outputAttachments[i] = clone;
				}
				else
				{
					outputAttachments[i] = ((!useOriginalNonrenderables) ? attachment.GetClone(true) : attachment);
					list.Add(-1);
				}
			}
			Texture2D texture2D = new Texture2D(maxAtlasSize, maxAtlasSize, textureFormat, mipmaps);
			texture2D.mipMapBias = -0.5f;
			texture2D.name = newAssetName;
			if (list2.Count > 0)
			{
				texture2D.anisoLevel = list2[0].anisoLevel;
			}
			Rect[] array = texture2D.PackTextures(list2.ToArray(), padding, maxAtlasSize);
			Shader shader = ((!(materialPropertySource == null)) ? materialPropertySource.shader : Shader.Find("Spine/Skeleton"));
			Material material = new Material(shader);
			if (materialPropertySource != null)
			{
				material.CopyPropertiesFromMaterial(materialPropertySource);
				material.shaderKeywords = materialPropertySource.shaderKeywords;
			}
			material.name = newAssetName;
			material.mainTexture = texture2D;
			AtlasPage atlasPage = material.ToSpineAtlasPage();
			atlasPage.name = newAssetName;
			List<AtlasRegion> list4 = new List<AtlasRegion>();
			int j = 0;
			for (int count2 = list3.Count; j < count2; j++)
			{
				AtlasRegion atlasRegion = list3[j];
				AtlasRegion item = UVRectToAtlasRegion(array[j], atlasRegion.name, atlasPage, atlasRegion.offsetX, atlasRegion.offsetY, atlasRegion.rotate);
				list4.Add(item);
			}
			int k = 0;
			for (int count3 = outputAttachments.Count; k < count3; k++)
			{
				Attachment attachment2 = outputAttachments[k];
				if (IsRenderable(attachment2))
				{
					attachment2.SetRegion(list4[list[k]]);
				}
			}
			if (clearCache)
			{
				ClearCache();
			}
			outputTexture = texture2D;
			outputMaterial = material;
		}

		public static Skin GetRepackedSkin(this Skin o, string newName, Material materialPropertySource, out Material outputMaterial, out Texture2D outputTexture, int maxAtlasSize = 1024, int padding = 2, TextureFormat textureFormat = TextureFormat.RGBA32, bool mipmaps = false, bool useOriginalNonrenderables = true)
		{
			return o.GetRepackedSkin(newName, materialPropertySource.shader, out outputMaterial, out outputTexture, maxAtlasSize, padding, textureFormat, mipmaps, materialPropertySource, useOriginalNonrenderables);
		}

		public static Skin GetRepackedSkin(this Skin o, string newName, Shader shader, out Material outputMaterial, out Texture2D outputTexture, int maxAtlasSize = 1024, int padding = 2, TextureFormat textureFormat = TextureFormat.RGBA32, bool mipmaps = false, Material materialPropertySource = null, bool clearCache = false, bool useOriginalNonrenderables = true)
		{
			if (o == null)
			{
				throw new NullReferenceException("Skin was null");
			}
			Dictionary<Skin.AttachmentKeyTuple, Attachment> attachments = o.Attachments;
			Skin skin = new Skin(newName);
			Dictionary<AtlasRegion, int> dictionary = new Dictionary<AtlasRegion, int>();
			List<int> list = new List<int>();
			List<Attachment> list2 = new List<Attachment>();
			List<Texture2D> list3 = new List<Texture2D>();
			List<AtlasRegion> list4 = new List<AtlasRegion>();
			int num = 0;
			foreach (KeyValuePair<Skin.AttachmentKeyTuple, Attachment> item2 in attachments)
			{
				Skin.AttachmentKeyTuple key = item2.Key;
				Attachment value = item2.Value;
				if (IsRenderable(value))
				{
					Attachment clone = value.GetClone(true);
					AtlasRegion region = clone.GetRegion();
					int value2;
					if (dictionary.TryGetValue(region, out value2))
					{
						list.Add(value2);
					}
					else
					{
						list4.Add(region);
						list3.Add(region.ToTexture());
						dictionary.Add(region, num);
						list.Add(num);
						num++;
					}
					list2.Add(clone);
					skin.AddAttachment(key.slotIndex, key.name, clone);
				}
				else
				{
					skin.AddAttachment(key.slotIndex, key.name, (!useOriginalNonrenderables) ? value.GetClone(true) : value);
				}
			}
			Texture2D texture2D = new Texture2D(maxAtlasSize, maxAtlasSize, textureFormat, mipmaps);
			texture2D.mipMapBias = -0.5f;
			texture2D.anisoLevel = list3[0].anisoLevel;
			texture2D.name = newName;
			Rect[] array = texture2D.PackTextures(list3.ToArray(), padding, maxAtlasSize);
			Material material = new Material(shader);
			if (materialPropertySource != null)
			{
				material.CopyPropertiesFromMaterial(materialPropertySource);
				material.shaderKeywords = materialPropertySource.shaderKeywords;
			}
			material.name = newName;
			material.mainTexture = texture2D;
			AtlasPage atlasPage = material.ToSpineAtlasPage();
			atlasPage.name = newName;
			List<AtlasRegion> list5 = new List<AtlasRegion>();
			int i = 0;
			for (int count = list4.Count; i < count; i++)
			{
				AtlasRegion atlasRegion = list4[i];
				AtlasRegion item = UVRectToAtlasRegion(array[i], atlasRegion.name, atlasPage, atlasRegion.offsetX, atlasRegion.offsetY, atlasRegion.rotate);
				list5.Add(item);
			}
			int j = 0;
			for (int count2 = list2.Count; j < count2; j++)
			{
				Attachment attachment = list2[j];
				if (IsRenderable(attachment))
				{
					attachment.SetRegion(list5[list[j]]);
				}
			}
			if (clearCache)
			{
				ClearCache();
			}
			outputTexture = texture2D;
			outputMaterial = material;
			return skin;
		}

		public static Sprite ToSprite(this AtlasRegion ar, float pixelsPerUnit = 100f)
		{
			return Sprite.Create(ar.GetMainTexture(), ar.GetUnityRect(), new Vector2(0.5f, 0.5f), pixelsPerUnit);
		}

		public static void ClearCache()
		{
			foreach (Texture2D cachedRegionTextures in CachedRegionTexturesList)
			{
				UnityEngine.Object.Destroy(cachedRegionTextures);
			}
			CachedRegionTextures.Clear();
			CachedRegionTexturesList.Clear();
		}

		public static Texture2D ToTexture(this AtlasRegion ar, TextureFormat textureFormat = TextureFormat.RGBA32, bool mipmaps = false)
		{
			Texture2D value;
			CachedRegionTextures.TryGetValue(ar, out value);
			if (value == null)
			{
				Texture2D mainTexture = ar.GetMainTexture();
				Rect unityRect = ar.GetUnityRect(mainTexture.height);
				int width = (int)unityRect.width;
				int height = (int)unityRect.height;
				Texture2D texture2D = new Texture2D(width, height, textureFormat, mipmaps);
				texture2D.name = ar.name;
				value = texture2D;
				CopyTexture(mainTexture, unityRect, value);
				CachedRegionTextures.Add(ar, value);
				CachedRegionTexturesList.Add(value);
			}
			return value;
		}

		private static Texture2D ToTexture(this Sprite s, TextureFormat textureFormat = TextureFormat.RGBA32, bool mipmaps = false)
		{
			Texture2D texture = s.texture;
			Rect textureRect = s.textureRect;
			Texture2D texture2D = new Texture2D((int)textureRect.width, (int)textureRect.height, textureFormat, mipmaps);
			CopyTexture(texture, textureRect, texture2D);
			return texture2D;
		}

		private static Texture2D GetClone(this Texture2D t, TextureFormat textureFormat = TextureFormat.RGBA32, bool mipmaps = false)
		{
			Texture2D texture2D = new Texture2D(t.width, t.height, textureFormat, mipmaps);
			CopyTexture(t, new Rect(0f, 0f, t.width, t.height), texture2D);
			return texture2D;
		}

		private static void CopyTexture(Texture2D source, Rect sourceRect, Texture2D destination)
		{
			if (SystemInfo.copyTextureSupport == CopyTextureSupport.None)
			{
				Color[] pixels = source.GetPixels((int)sourceRect.x, (int)sourceRect.y, (int)sourceRect.width, (int)sourceRect.height);
				destination.SetPixels(pixels);
				destination.Apply();
			}
			else
			{
				Graphics.CopyTexture(source, 0, 0, (int)sourceRect.x, (int)sourceRect.y, (int)sourceRect.width, (int)sourceRect.height, destination, 0, 0, 0, 0);
			}
		}

		private static bool IsRenderable(Attachment a)
		{
			return a is IHasRendererObject;
		}

		private static Rect SpineUnityFlipRect(this Rect rect, int textureHeight)
		{
			rect.y = (float)textureHeight - rect.y - rect.height;
			return rect;
		}

		private static Rect GetUnityRect(this AtlasRegion region)
		{
			return region.GetSpineAtlasRect().SpineUnityFlipRect(region.page.height);
		}

		private static Rect GetUnityRect(this AtlasRegion region, int textureHeight)
		{
			return region.GetSpineAtlasRect().SpineUnityFlipRect(textureHeight);
		}

		private static Rect GetSpineAtlasRect(this AtlasRegion region, bool includeRotate = true)
		{
			if (includeRotate && region.rotate)
			{
				return new Rect(region.x, region.y, region.height, region.width);
			}
			return new Rect(region.x, region.y, region.width, region.height);
		}

		private static Rect UVRectToTextureRect(Rect uvRect, int texWidth, int texHeight)
		{
			uvRect.x *= texWidth;
			uvRect.width *= texWidth;
			uvRect.y *= texHeight;
			uvRect.height *= texHeight;
			return uvRect;
		}

		private static Rect TextureRectToUVRect(Rect textureRect, int texWidth, int texHeight)
		{
			textureRect.x = Mathf.InverseLerp(0f, texWidth, textureRect.x);
			textureRect.y = Mathf.InverseLerp(0f, texHeight, textureRect.y);
			textureRect.width = Mathf.InverseLerp(0f, texWidth, textureRect.width);
			textureRect.height = Mathf.InverseLerp(0f, texHeight, textureRect.height);
			return textureRect;
		}

		private static AtlasRegion UVRectToAtlasRegion(Rect uvRect, string name, AtlasPage page, float offsetX, float offsetY, bool rotate)
		{
			Rect rect = UVRectToTextureRect(uvRect, page.width, page.height);
			Rect rect2 = rect.SpineUnityFlipRect(page.height);
			int x = (int)rect2.x;
			int y = (int)rect2.y;
			int num;
			int num2;
			if (rotate)
			{
				num = (int)rect2.height;
				num2 = (int)rect2.width;
			}
			else
			{
				num = (int)rect2.width;
				num2 = (int)rect2.height;
			}
			AtlasRegion atlasRegion = new AtlasRegion();
			atlasRegion.page = page;
			atlasRegion.name = name;
			atlasRegion.u = uvRect.xMin;
			atlasRegion.u2 = uvRect.xMax;
			atlasRegion.v = uvRect.yMax;
			atlasRegion.v2 = uvRect.yMin;
			atlasRegion.index = -1;
			atlasRegion.width = num;
			atlasRegion.originalWidth = num;
			atlasRegion.height = num2;
			atlasRegion.originalHeight = num2;
			atlasRegion.offsetX = offsetX;
			atlasRegion.offsetY = offsetY;
			atlasRegion.x = x;
			atlasRegion.y = y;
			atlasRegion.rotate = rotate;
			return atlasRegion;
		}

		private static Texture2D GetMainTexture(this AtlasRegion region)
		{
			Material material = region.page.rendererObject as Material;
			return material.mainTexture as Texture2D;
		}

		private static void ApplyPMA(this Texture2D texture, bool applyImmediately = true)
		{
			Color[] pixels = texture.GetPixels();
			int i = 0;
			for (int num = pixels.Length; i < num; i++)
			{
				Color color = pixels[i];
				float a = color.a;
				color.r *= a;
				color.g *= a;
				color.b *= a;
				pixels[i] = color;
			}
			texture.SetPixels(pixels);
			if (applyImmediately)
			{
				texture.Apply();
			}
		}

		private static float InverseLerp(float a, float b, float value)
		{
			return (value - a) / (b - a);
		}
	}
}
