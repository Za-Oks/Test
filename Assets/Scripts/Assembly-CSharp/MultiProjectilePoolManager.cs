using System.Collections.Generic;
using UnityEngine;

public class MultiProjectilePoolManager : MonoBehaviour
{
	public class PoolData
	{
		public GameObject gameObject;

		public Transform transform;

		public Vector3 startScale;

		public Arrow arrow;

		public Arrow_Bomb arrow_bomb;

		public Bullet bullet;

		public Bullet_Cannon bulletCannon;

		public List<Transform> multiProjectileTransforms;

		public PoolData(GameObject gameObject)
		{
			this.gameObject = gameObject;
			transform = gameObject.GetComponent<Transform>();
			startScale = transform.localScale;
			arrow = gameObject.GetComponent<Arrow>();
			arrow_bomb = gameObject.GetComponent<Arrow_Bomb>();
			bullet = gameObject.GetComponent<Bullet>();
			bulletCannon = gameObject.GetComponent<Bullet_Cannon>();
			multiProjectileTransforms = new List<Transform>();
			foreach (Transform item in transform)
			{
				multiProjectileTransforms.Add(item);
			}
		}

		public void ResetEverything()
		{
			gameObject.SetActive(false);
			transform.localScale = startScale;
			if (arrow != null)
			{
				arrow.ResetEverything();
			}
			else if (bulletCannon != null)
			{
				bulletCannon.ResetEverything();
			}
			else if (bullet != null)
			{
				bullet.ResetEverything();
			}
			else if (arrow_bomb != null)
			{
				arrow_bomb.ResetEverything();
			}
		}
	}

	public GameObject item;

	public int startNumber;

	private List<PoolData> list;

	private void Awake()
	{
		list = new List<PoolData>();
		for (int i = 0; i < startNumber; i++)
		{
			AddToList();
		}
	}

	public PoolData Trigger(Vector3 position, Quaternion rotation, int nextMultiProjectileIndex)
	{
		GameObject gameObject = item;
		return Trigger(position, rotation, gameObject.transform.localScale, nextMultiProjectileIndex);
	}

	public PoolData Trigger(Vector3 position, Quaternion rotation, Vector3 scale, int nextMultiProjectileIndex)
	{
		List<PoolData> list = this.list;
		int count = list.Count;
		for (int i = 0; i < count; i++)
		{
			PoolData poolData = list[i];
			if (!poolData.gameObject.activeInHierarchy)
			{
				Trigger(poolData, position, rotation, scale, nextMultiProjectileIndex);
				return poolData;
			}
		}
		PoolData poolData2 = AddToList();
		Trigger(poolData2, position, rotation, scale, nextMultiProjectileIndex);
		return poolData2;
	}

	private void Trigger(PoolData pd, Vector3 position, Quaternion rotation, Vector3 scale, int nextMultiProjectileIndex)
	{
		pd.transform.localScale = scale / base.transform.localScale.x;
		pd.transform.rotation = rotation;
		pd.transform.position = position;
		for (int i = 0; i < pd.multiProjectileTransforms.Count; i++)
		{
			if (i == nextMultiProjectileIndex)
			{
				pd.multiProjectileTransforms[i].gameObject.SetActive(true);
			}
			else
			{
				pd.multiProjectileTransforms[i].gameObject.SetActive(false);
			}
		}
		pd.gameObject.SetActive(true);
	}

	public void ResetEverything()
	{
		int count = list.Count;
		for (int i = 0; i < count; i++)
		{
			PoolData poolData = list[i];
			poolData.ResetEverything();
		}
	}

	private PoolData AddToList()
	{
		GameObject gameObject = Object.Instantiate(item, base.transform);
		PoolData poolData = new PoolData(gameObject);
		poolData.gameObject.SetActive(false);
		list.Add(poolData);
		return poolData;
	}
}
