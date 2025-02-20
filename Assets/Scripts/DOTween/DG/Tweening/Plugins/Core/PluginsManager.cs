using System;
using System.Collections.Generic;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening.Plugins.Core
{
	internal static class PluginsManager
	{
		private static ITweenPlugin _floatPlugin;

		private static ITweenPlugin _doublePlugin;

		private static ITweenPlugin _intPlugin;

		private static ITweenPlugin _uintPlugin;

		private static ITweenPlugin _longPlugin;

		private static ITweenPlugin _ulongPlugin;

		private static ITweenPlugin _vector2Plugin;

		private static ITweenPlugin _vector3Plugin;

		private static ITweenPlugin _vector4Plugin;

		private static ITweenPlugin _quaternionPlugin;

		private static ITweenPlugin _colorPlugin;

		private static ITweenPlugin _rectPlugin;

		private static ITweenPlugin _rectOffsetPlugin;

		private static ITweenPlugin _stringPlugin;

		private static ITweenPlugin _vector3ArrayPlugin;

		private static ITweenPlugin _color2Plugin;

		private const int _MaxCustomPlugins = 20;

		private static Dictionary<Type, ITweenPlugin> _customPlugins;

		internal static ABSTweenPlugin<T1, T2, TPlugOptions> GetDefaultPlugin<T1, T2, TPlugOptions>() where TPlugOptions : struct, IPlugOptions
		{
			Type typeFromHandle = typeof(T1);
			Type typeFromHandle2 = typeof(T2);
			ITweenPlugin tweenPlugin = null;
			if (typeFromHandle == typeof(Vector3) && typeFromHandle == typeFromHandle2)
			{
				if (_vector3Plugin == null)
				{
					_vector3Plugin = new Vector3Plugin();
				}
				tweenPlugin = _vector3Plugin;
			}
			else if (typeFromHandle == typeof(Vector3) && typeFromHandle2 == typeof(Vector3[]))
			{
				if (_vector3ArrayPlugin == null)
				{
					_vector3ArrayPlugin = new Vector3ArrayPlugin();
				}
				tweenPlugin = _vector3ArrayPlugin;
			}
			else if (typeFromHandle == typeof(Quaternion))
			{
				if (typeFromHandle2 == typeof(Quaternion))
				{
					Debugger.LogError("Quaternion tweens require a Vector3 endValue");
				}
				else
				{
					if (_quaternionPlugin == null)
					{
						_quaternionPlugin = new QuaternionPlugin();
					}
					tweenPlugin = _quaternionPlugin;
				}
			}
			else if (typeFromHandle == typeof(Vector2))
			{
				if (_vector2Plugin == null)
				{
					_vector2Plugin = new Vector2Plugin();
				}
				tweenPlugin = _vector2Plugin;
			}
			else if (typeFromHandle == typeof(float))
			{
				if (_floatPlugin == null)
				{
					_floatPlugin = new FloatPlugin();
				}
				tweenPlugin = _floatPlugin;
			}
			else if (typeFromHandle == typeof(Color))
			{
				if (_colorPlugin == null)
				{
					_colorPlugin = new ColorPlugin();
				}
				tweenPlugin = _colorPlugin;
			}
			else if (typeFromHandle == typeof(int))
			{
				if (_intPlugin == null)
				{
					_intPlugin = new IntPlugin();
				}
				tweenPlugin = _intPlugin;
			}
			else if (typeFromHandle == typeof(Vector4))
			{
				if (_vector4Plugin == null)
				{
					_vector4Plugin = new Vector4Plugin();
				}
				tweenPlugin = _vector4Plugin;
			}
			else if (typeFromHandle == typeof(Rect))
			{
				if (_rectPlugin == null)
				{
					_rectPlugin = new RectPlugin();
				}
				tweenPlugin = _rectPlugin;
			}
			else if (typeFromHandle == typeof(RectOffset))
			{
				if (_rectOffsetPlugin == null)
				{
					_rectOffsetPlugin = new RectOffsetPlugin();
				}
				tweenPlugin = _rectOffsetPlugin;
			}
			else if (typeFromHandle == typeof(uint))
			{
				if (_uintPlugin == null)
				{
					_uintPlugin = new UintPlugin();
				}
				tweenPlugin = _uintPlugin;
			}
			else if (typeFromHandle == typeof(string))
			{
				if (_stringPlugin == null)
				{
					_stringPlugin = new StringPlugin();
				}
				tweenPlugin = _stringPlugin;
			}
			else if (typeFromHandle == typeof(Color2))
			{
				if (_color2Plugin == null)
				{
					_color2Plugin = new Color2Plugin();
				}
				tweenPlugin = _color2Plugin;
			}
			else if (typeFromHandle == typeof(long))
			{
				if (_longPlugin == null)
				{
					_longPlugin = new LongPlugin();
				}
				tweenPlugin = _longPlugin;
			}
			else if (typeFromHandle == typeof(ulong))
			{
				if (_ulongPlugin == null)
				{
					_ulongPlugin = new UlongPlugin();
				}
				tweenPlugin = _ulongPlugin;
			}
			else if (typeFromHandle == typeof(double))
			{
				if (_doublePlugin == null)
				{
					_doublePlugin = new DoublePlugin();
				}
				tweenPlugin = _doublePlugin;
			}
			if (tweenPlugin != null)
			{
				return tweenPlugin as ABSTweenPlugin<T1, T2, TPlugOptions>;
			}
			return null;
		}

		public static ABSTweenPlugin<T1, T2, TPlugOptions> GetCustomPlugin<TPlugin, T1, T2, TPlugOptions>() where TPlugin : ITweenPlugin, new() where TPlugOptions : struct, IPlugOptions
		{
			Type typeFromHandle = typeof(TPlugin);
			ITweenPlugin value;
			if (_customPlugins == null)
			{
				_customPlugins = new Dictionary<Type, ITweenPlugin>(20);
			}
			else if (_customPlugins.TryGetValue(typeFromHandle, out value))
			{
				return value as ABSTweenPlugin<T1, T2, TPlugOptions>;
			}
			value = new TPlugin();
			_customPlugins.Add(typeFromHandle, value);
			return value as ABSTweenPlugin<T1, T2, TPlugOptions>;
		}

		internal static void PurgeAll()
		{
			_floatPlugin = null;
			_intPlugin = null;
			_uintPlugin = null;
			_longPlugin = null;
			_ulongPlugin = null;
			_vector2Plugin = null;
			_vector3Plugin = null;
			_vector4Plugin = null;
			_quaternionPlugin = null;
			_colorPlugin = null;
			_rectPlugin = null;
			_rectOffsetPlugin = null;
			_stringPlugin = null;
			_vector3ArrayPlugin = null;
			_color2Plugin = null;
			if (_customPlugins != null)
			{
				_customPlugins.Clear();
			}
		}
	}
}
