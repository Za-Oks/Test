using System.Collections.Generic;
using UnityEngine;

public class CrossPoolManager : MonoBehaviour
{
	public class PoolData
	{
		public GameObject go;

		public Transform trans;

		public MeshRenderer renderer;
	}

	public GameObject cross;

	public int startNumber;

	private List<PoolData> list;

	public Material materialPlayer;

	public Material materialEnemy;

	private void Awake()
	{
		list = new List<PoolData>();
		for (int i = 0; i < startNumber; i++)
		{
			AddToList();
		}
	}

	public PoolData Trigger(Vector3 position, Quaternion rotation, Vector3 scale, bool isPlayer)
	{
		int count = list.Count;
		for (int i = 0; i < count; i++)
		{
			PoolData poolData = list[i];
			if (!poolData.go.activeInHierarchy)
			{
				Trigger(poolData, position, rotation, scale, isPlayer);
				return poolData;
			}
		}
		PoolData poolData2 = AddToList();
		Trigger(poolData2, position, rotation, scale, isPlayer);
		return poolData2;
	}

	private void Trigger(PoolData pd, Vector3 position, Quaternion rotation, Vector3 scale, bool isPlayer)
	{
		pd.trans.localScale = scale / base.transform.localScale.x;
		pd.trans.rotation = rotation;
		pd.trans.position = position;
		pd.renderer.material = ((!isPlayer) ? materialEnemy : materialPlayer);
		pd.go.SetActive(true);
	}

	public void ResetEverything()
	{
		int count = list.Count;
		for (int i = 0; i < count; i++)
		{
			PoolData poolData = list[i];
			poolData.go.SetActive(false);
		}
	}

	private PoolData AddToList()
	{
		GameObject go = Object.Instantiate(cross, base.transform);
		PoolData poolData = new PoolData();
		poolData.go = go;
		poolData.trans = poolData.go.GetComponent<Transform>();
		poolData.renderer = poolData.trans.GetChild(0).GetComponent<MeshRenderer>();
		poolData.go.SetActive(false);
		list.Add(poolData);
		return poolData;
	}
}
