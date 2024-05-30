using System;
using System.Collections;
using UnityEngine;

namespace VoxelBusters.Utility
{
	public static class IDictionaryExtensions
	{
		public static bool ContainsKeyPath(this IDictionary _sourceDictionary, string _keyPath)
		{
			if (string.IsNullOrEmpty(_keyPath))
			{
				return false;
			}
			try
			{
				string[] array = _keyPath.Split('/');
				int num = array.Length;
				IDictionary dictionary = _sourceDictionary;
				for (int i = 0; i < num; i++)
				{
					string key = array[i];
					if (dictionary == null || !dictionary.Contains(key))
					{
						return false;
					}
					dictionary = dictionary[key] as IDictionary;
				}
				return true;
			}
			catch (Exception ex)
			{
				Debug.LogWarning("[IDictionaryExtensions] " + ex.Message);
				return false;
			}
		}

		public static T GetIfAvailable<T>(this IDictionary _sourceDictionary, string _key, T _defaultValue = default(T))
		{
			try
			{
				if (_key == null || !_sourceDictionary.Contains(_key))
				{
					return _defaultValue;
				}
				object obj = _sourceDictionary[_key];
				Type typeFromHandle = typeof(T);
				if (obj == null)
				{
					return _defaultValue;
				}
				if (typeFromHandle.IsInstanceOfType(obj))
				{
					return (T)obj;
				}
				if (typeFromHandle.IsEnum)
				{
					return (T)Enum.ToObject(typeFromHandle, obj);
				}
				return (T)Convert.ChangeType(obj, typeFromHandle);
			}
			catch (Exception ex)
			{
				Debug.LogError("Exception while converting for key : " + _key + " " + ex);
				return _defaultValue;
			}
		}

		public static T GetIfAvailable<T>(this IDictionary _sourceDictionary, string _key, string _path)
		{
			if (_path != null)
			{
				_path = _path.TrimStart('/').TrimEnd('/');
			}
			if (!string.IsNullOrEmpty(_key))
			{
				if (string.IsNullOrEmpty(_path))
				{
					return _sourceDictionary.GetIfAvailable<T>(_key);
				}
				string[] array = _path.Split('/');
				IDictionary dictionary = _sourceDictionary;
				string[] array2 = array;
				foreach (string key in array2)
				{
					if (dictionary.Contains(key))
					{
						dictionary = dictionary[key] as IDictionary;
						continue;
					}
					Debug.LogError("Path not found " + _path);
					return default(T);
				}
				return dictionary.GetIfAvailable<T>(_key);
			}
			return default(T);
		}

		public static string GetKey<T>(this IDictionary _sourceDictionary, T _value)
		{
			string result = null;
			if (_value != null)
			{
				ICollection keys = _sourceDictionary.Keys;
				foreach (string item in keys)
				{
					object obj = _sourceDictionary[item];
					if (obj != null && obj.Equals(_value))
					{
						result = item;
						break;
					}
				}
			}
			return result;
		}
	}
}
