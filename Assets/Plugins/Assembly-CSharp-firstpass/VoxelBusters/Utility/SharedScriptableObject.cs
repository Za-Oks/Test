using UnityEngine;
using VoxelBusters.UnityEngineUtils;

namespace VoxelBusters.Utility
{
	public abstract class SharedScriptableObject<T> : ScriptableObject, ISaveAssetCallback where T : ScriptableObject
	{
		private static T instance;

		public static T Instance
		{
			get
			{
				if (instance == null)
				{
					string path = string.Format("Assets/Resources/SharedAssets/{0}.asset", typeof(T).Name);
					instance = ScriptableObjectUtils.LoadAssetAtPath<T>(path);
				}
				return instance;
			}
			set
			{
				instance = value;
			}
		}

		protected virtual void Reset()
		{
		}

		protected virtual void OnEnable()
		{
			if (instance == null)
			{
				instance = this as T;
			}
		}

		protected virtual void OnDisable()
		{
		}

		protected virtual void OnDestroy()
		{
		}

		public virtual void OnBeforeSave()
		{
		}
	}
}
