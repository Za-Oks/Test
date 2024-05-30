using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Utility
{
	public static class TextureExtensions
	{
		public enum EncodeTo
		{
			JPG = 0,
			PNG = 1
		}

		public static string ToString(this Texture2D _img, EncodeTo _encodeTo = EncodeTo.PNG)
		{
			byte[] inArray = ((_encodeTo != 0) ? _img.EncodeToPNG() : _img.EncodeToJPG());
			return Convert.ToBase64String(inArray);
		}

		public static string Serialise(this Texture2D _img, EncodeTo _encodeTo = EncodeTo.PNG)
		{
			IDictionary dictionary = new Dictionary<string, object>();
			dictionary["width"] = _img.width;
			dictionary["height"] = _img.height;
			dictionary["texture"] = _img.ToString(_encodeTo);
			return JSONUtility.ToJSON(dictionary);
		}

		public static Texture2D Deserialise(string _strImg)
		{
			IDictionary dictionary = JSONUtility.FromJSON(_strImg) as Dictionary<string, object>;
			if (dictionary != null)
			{
				int width = (int)dictionary["width"];
				int height = (int)dictionary["height"];
				string texDataB = dictionary["texture"] as string;
				return CreateTexture(texDataB, width, height);
			}
			return null;
		}

		public static Texture2D CreateTexture(string _texDataB64, int _width, int _height)
		{
			byte[] data = Convert.FromBase64String(_texDataB64);
			Texture2D texture2D = new Texture2D(_width, _height);
			texture2D.LoadImage(data);
			return texture2D;
		}

		public static IEnumerator TakeScreenshot(Action<Texture2D> _onCompletionHandler)
		{
			yield return new WaitForEndOfFrame();
			Texture2D _texture = new Texture2D(Screen.width, Screen.height);
			_texture.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height), 0, 0);
			_texture.Apply();
			if (_onCompletionHandler != null)
			{
				_onCompletionHandler(_texture);
			}
		}

		public static Texture2D Rotate(this Texture2D _inputTex, float _angle)
		{
			int width = _inputTex.width;
			int height = _inputTex.height;
			float num = (_angle - 45f) % 360f;
			if (num < 0f)
			{
				num += 360f;
			}
			int num2 = (int)(num / 90f) + 1;
			int num3;
			int num4;
			if (num2 == 1 || num2 == 3)
			{
				num3 = height;
				num4 = width;
			}
			else
			{
				num3 = width;
				num4 = height;
			}
			Vector2 vector = new Vector2((float)width * 0.5f, (float)height * 0.5f);
			Vector2 vector2 = new Vector2((float)(-num3) * 0.5f, (float)(-num4) * 0.5f).Rotate(_angle) + vector;
			Vector2 vector3 = Vector2.right.Rotate(_angle);
			Vector2 vector4 = Vector2.up.Rotate(_angle);
			Vector2 vector5 = vector2;
			Color32[] pixels = _inputTex.GetPixels32(0);
			Color32[] array = new Color32[pixels.Length];
			for (int i = 0; i < num4; i++)
			{
				Vector2 coordinate = vector5;
				int num5 = i * num3;
				for (int j = 0; j < num3; j++)
				{
					array[num5++] = _inputTex.GetPixel(pixels, coordinate);
					coordinate += vector3;
				}
				vector5 += vector4;
			}
			Texture2D texture2D = new Texture2D(num3, num4, _inputTex.format, false);
			texture2D.SetPixels32(array, 0);
			texture2D.Apply();
			return texture2D;
		}

		private static Color GetPixel(this Texture2D _inputTex, Color32[] _pixels, Vector2 _coordinate)
		{
			int num = (int)_coordinate.x;
			int num2 = (int)_coordinate.y;
			if (num >= _inputTex.width || num < 0)
			{
				return Color.clear;
			}
			if (num2 >= _inputTex.height || num2 < 0)
			{
				return Color.clear;
			}
			return _pixels[num2 * _inputTex.width + num];
		}

		public static Texture2D MirrorTexture(this Texture2D _inputTex, bool _mirrorHorizontally, bool _mirrorVertically)
		{
			int width = _inputTex.width;
			int num = width - 1;
			int height = _inputTex.height;
			int num2 = height - 1;
			Color32[] pixels = _inputTex.GetPixels32(0);
			Color32[] array = new Color32[pixels.Length];
			for (int i = 0; i < height; i++)
			{
				int num3 = i * width;
				int num4 = ((!_mirrorVertically) ? i : (num2 - i));
				int num5 = num4 * width;
				for (int j = 0; j < width; j++)
				{
					int num6 = ((!_mirrorHorizontally) ? j : (num - j));
					array[num5 + num6] = pixels[num3++];
				}
			}
			Texture2D texture2D = new Texture2D(width, height, _inputTex.format, false);
			texture2D.SetPixels32(array, 0);
			texture2D.Apply();
			Debug.Log("[TextureExtensions:Mirror] Output W=" + texture2D.width + " H=" + texture2D.height);
			return texture2D;
		}

		public static Texture2D Scale(this Texture2D _inputTex, float _scaleFactor)
		{
			if (_scaleFactor == 1f)
			{
				return _inputTex;
			}
			if (_scaleFactor == 0f)
			{
				return Texture2D.blackTexture;
			}
			int num = Mathf.RoundToInt((float)_inputTex.width * _scaleFactor);
			int num2 = Mathf.RoundToInt((float)_inputTex.height * _scaleFactor);
			Color[] array = new Color[num * num2];
			for (int i = 0; i < num2; i++)
			{
				float v = (float)i / ((float)num2 * 1f);
				int num3 = i * num;
				for (int j = 0; j < num; j++)
				{
					float u = (float)j / ((float)num * 1f);
					array[num3 + j] = _inputTex.GetPixelBilinear(u, v);
				}
			}
			Texture2D texture2D = new Texture2D(num, num2, _inputTex.format, false);
			texture2D.SetPixels(array, 0);
			texture2D.Apply();
			return texture2D;
		}
	}
}
