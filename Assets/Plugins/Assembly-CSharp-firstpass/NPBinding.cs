using System;
using UnityEngine;
using VoxelBusters.DesignPatterns;
using VoxelBusters.NativePlugins;
using VoxelBusters.Utility;

[RequireComponent(typeof(PlatformBindingHelper))]
public class NPBinding : SingletonPattern<NPBinding>
{
	private static GameServices gameServices;

	private static UI userInterface;

	private static Utility utility;

	public static GameServices GameServices
	{
		get
		{
			NPBinding nPBinding = SingletonPattern<NPBinding>.Instance;
			if (nPBinding == null)
			{
				return null;
			}
			if (gameServices == null)
			{
				gameServices = nPBinding.AddComponentBasedOnPlatformOnlyIfRequired<GameServices>();
			}
			return gameServices;
		}
	}

	public static UI UI
	{
		get
		{
			NPBinding nPBinding = SingletonPattern<NPBinding>.Instance;
			if (nPBinding == null)
			{
				return null;
			}
			if (userInterface == null)
			{
				userInterface = nPBinding.AddComponentBasedOnPlatformOnlyIfRequired<UI>();
			}
			return userInterface;
		}
	}

	public static Utility Utility
	{
		get
		{
			NPBinding nPBinding = SingletonPattern<NPBinding>.Instance;
			if (nPBinding == null)
			{
				return null;
			}
			if (utility == null)
			{
				utility = nPBinding.CachedGameObject.AddComponentIfNotFound<Utility>();
			}
			return utility;
		}
	}

	protected override void Init()
	{
		base.Init();
		if (!(SingletonPattern<NPBinding>.instance != this))
		{
			if (gameServices == null)
			{
				gameServices = AddComponentBasedOnPlatformOnlyIfRequired<GameServices>();
			}
			if (userInterface == null)
			{
				userInterface = AddComponentBasedOnPlatformOnlyIfRequired<UI>();
			}
			if (utility == null)
			{
				utility = base.CachedGameObject.AddComponentIfNotFound<Utility>();
			}
		}
	}

	private T AddComponentBasedOnPlatformOnlyIfRequired<T>() where T : MonoBehaviour
	{
		T component = GetComponent<T>();
		if (component != null)
		{
			return component;
		}
		Type typeFromHandle = typeof(T);
		string text = typeFromHandle.ToString();
		string text2 = null;
		text2 = text + "Android";
		if (!string.IsNullOrEmpty(text2))
		{
			Type type = typeFromHandle.Assembly.GetType(text2, false);
			return base.CachedGameObject.AddComponent(type) as T;
		}
		return base.CachedGameObject.AddComponent<T>();
	}
}
