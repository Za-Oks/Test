using System.Collections.Generic;
using UnityEngine;

public class BasicPoolManager : MonoBehaviour
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

		public PoolData(GameObject gameObject)
		{
			this.gameObject = gameObject;
			transform = gameObject.GetComponent<Transform>();
			startScale = transform.localScale;
			arrow = gameObject.GetComponent<Arrow>();
			arrow_bomb = gameObject.GetComponent<Arrow_Bomb>();
			bullet = gameObject.GetComponent<Bullet>();
			bulletCannon = gameObject.GetComponent<Bullet_Cannon>();
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

	public GameObject item_level1;

	public GameObject item_level2;

	public GameObject item_level3;

	public int startNumber;

	private List<PoolData> list_level1;

	private List<PoolData> list_level2;

	private List<PoolData> list_level3;

	private int maxLevel = 1;

	private void Awake()
	{
		if (item_level3 != null)
		{
			maxLevel = 3;
		}
		else if (item_level2 != null)
		{
			maxLevel = 2;
		}
		list_level1 = new List<PoolData>();
		list_level2 = new List<PoolData>();
		list_level3 = new List<PoolData>();
		for (int i = 0; i < startNumber; i++)
		{
			AddToList(1);
			if (maxLevel >= 2)
			{
				AddToList(2);
			}
			if (maxLevel >= 3)
			{
				AddToList(3);
			}
		}
	}

	public PoolData Trigger(Vector3 position, Quaternion rotation, int level)
	{
		if (level > maxLevel)
		{
			level = maxLevel;
		}
		GameObject gameObject;
		switch (level)
		{
		case 2:
			gameObject = item_level2;
			break;
		case 3:
			gameObject = item_level3;
			break;
		default:
			gameObject = item_level1;
			break;
		}
		return Trigger(position, rotation, level, gameObject.transform.localScale);
	}

	public PoolData Trigger(Vector3 position, Quaternion rotation, int level, Vector3 scale)
	{
		if (level > maxLevel)
		{
			level = maxLevel;
		}
		List<PoolData> list;
		switch (level)
		{
		case 2:
			list = list_level2;
			break;
		case 3:
			list = list_level3;
			break;
		default:
			list = list_level1;
			break;
		}
		int count = list.Count;
		for (int i = 0; i < count; i++)
		{
			PoolData poolData = list[i];
			if (!poolData.gameObject.activeInHierarchy)
			{
				Trigger(poolData, position, rotation, scale);
				return poolData;
			}
		}
		PoolData poolData2 = AddToList(level);
		Trigger(poolData2, position, rotation, scale);
		return poolData2;
	}

	private void Trigger(PoolData pd, Vector3 position, Quaternion rotation, Vector3 scale)
	{
		pd.transform.localScale = scale / base.transform.localScale.x;
		pd.transform.rotation = rotation;
		pd.transform.position = position;
		pd.gameObject.SetActive(true);
	}

	public void ResetEverything()
	{
		int count = list_level1.Count;
		for (int i = 0; i < count; i++)
		{
			PoolData poolData = list_level1[i];
			poolData.ResetEverything();
		}
		count = list_level2.Count;
		for (int j = 0; j < count; j++)
		{
			PoolData poolData = list_level2[j];
			poolData.ResetEverything();
		}
		count = list_level3.Count;
		for (int k = 0; k < count; k++)
		{
			PoolData poolData = list_level3[k];
			poolData.ResetEverything();
		}
	}

	private PoolData AddToList(int level)
	{
		GameObject gameObject;
		switch (level)
		{
		case 2:
			gameObject = Object.Instantiate(item_level2, base.transform);
			break;
		case 3:
			gameObject = Object.Instantiate(item_level3, base.transform);
			break;
		default:
			gameObject = Object.Instantiate(item_level1, base.transform);
			break;
		}
		PoolData poolData = new PoolData(gameObject);
		poolData.gameObject.SetActive(false);
		switch (level)
		{
		case 2:
			list_level2.Add(poolData);
			break;
		case 3:
			list_level3.Add(poolData);
			break;
		default:
			list_level1.Add(poolData);
			break;
		}
		return poolData;
	}
}
