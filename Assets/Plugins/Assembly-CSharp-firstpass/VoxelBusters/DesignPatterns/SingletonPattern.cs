using System;
using UnityEngine;

namespace VoxelBusters.DesignPatterns
{
	public class SingletonPattern<T> : MonoBehaviour, ISingleton where T : MonoBehaviour
	{
		protected static T instance = (T)null;

		protected static object instanceLock = new object();

		protected static bool destroyedOnApplicationQuit = false;

		private Transform m_transform;

		private GameObject m_gameObject;

		private bool m_isInitialized;

		private bool m_isForcefullyDestroyed;

		public static T Instance
		{
			get
			{
				Type typeFromHandle = typeof(T);
				if (destroyedOnApplicationQuit)
				{
					Debug.LogWarning(string.Concat("[SingletonPattern] ", typeFromHandle, " instance is already destroyed."));
					return (T)null;
				}
				lock (instanceLock)
				{
					if (instance == null)
					{
						T[] array = UnityEngine.Object.FindObjectsOfType(typeFromHandle) as T[];
						if (array.Length > 0)
						{
							instance = array[0];
							for (int i = 1; i < array.Length; i++)
							{
								UnityEngine.Object.Destroy(array[i].gameObject);
							}
						}
						if (instance == null)
						{
							string text = typeFromHandle.Name;
							GameObject gameObject = Resources.Load("Singleton/" + text, typeof(GameObject)) as GameObject;
							if (gameObject != null)
							{
								Debug.Log("[SingletonPattern] Creating singeton using prefab");
								instance = UnityEngine.Object.Instantiate(gameObject).GetComponent<T>();
							}
							else
							{
								instance = new GameObject().AddComponent<T>();
							}
							instance.name = text;
						}
					}
				}
				SingletonPattern<T> singletonPattern = (SingletonPattern<T>)(object)instance;
				if (!singletonPattern.m_isInitialized)
				{
					singletonPattern.Init();
				}
				return instance;
			}
			private set
			{
				instance = value;
			}
		}

		public Transform CachedTransform
		{
			get
			{
				if (m_transform == null)
				{
					m_transform = base.transform;
				}
				return m_transform;
			}
		}

		public GameObject CachedGameObject
		{
			get
			{
				if (m_gameObject == null)
				{
					m_gameObject = base.gameObject;
				}
				return m_gameObject;
			}
		}

		private void Awake()
		{
			if (!m_isInitialized)
			{
				Init();
			}
		}

		protected virtual void Start()
		{
		}

		protected virtual void Reset()
		{
			m_gameObject = null;
			m_transform = null;
			m_isInitialized = false;
			m_isForcefullyDestroyed = false;
		}

		protected virtual void OnEnable()
		{
		}

		protected virtual void OnDisable()
		{
		}

		protected virtual void OnDestroy()
		{
			if (instance == this && !m_isForcefullyDestroyed)
			{
				destroyedOnApplicationQuit = true;
			}
		}

		protected virtual void Init()
		{
			m_isInitialized = true;
			if (instance == null)
			{
				instance = this as T;
			}
			else if (instance != this)
			{
				UnityEngine.Object.Destroy(CachedGameObject);
				return;
			}
			UnityEngine.Object.DontDestroyOnLoad(CachedGameObject);
		}

		public void ForceDestroy()
		{
			m_isForcefullyDestroyed = true;
			UnityEngine.Object.Destroy(CachedGameObject);
		}
	}
}
