using System.Collections.Generic;
using UnityEngine;

public class BotInfoSetUp : MonoBehaviour
{
	[Header("Level 0 GOs")]
	public GameObject[] Level_0_GOs;

	[Header("Level 1 GOs")]
	public GameObject[] Level_1_GOs;

	[Header("Level 2 GOs")]
	public GameObject[] Level_2_GOs;

	[Header("Team Values")]
	public SkinnedMeshRenderer[] bodies;

	public MeshRenderer[] equipments;

	public Material playerMaterial;

	public Material enemyMaterial;

	public SpriteRenderer spriteLvL;

	public bool isBoss;

	private List<GameObject> upgrades_level1;

	private List<GameObject> upgrades_level2;

	private List<GameObject> upgrades_level3;

	public void ResetGOs()
	{
		for (int i = 0; i < Level_0_GOs.Length; i++)
		{
			Level_0_GOs[i].SetActive(false);
		}
		for (int j = 0; j < Level_1_GOs.Length; j++)
		{
			Level_1_GOs[j].SetActive(false);
		}
		for (int k = 0; k < Level_2_GOs.Length; k++)
		{
			Level_2_GOs[k].SetActive(false);
		}
	}

	public void SetGOs(int statsLvL)
	{
		switch (statsLvL)
		{
		case 0:
		{
			for (int j = 0; j < Level_0_GOs.Length; j++)
			{
				Level_0_GOs[j].SetActive(true);
			}
			break;
		}
		case 1:
		{
			for (int k = 0; k < Level_1_GOs.Length; k++)
			{
				Level_1_GOs[k].SetActive(true);
			}
			break;
		}
		case 2:
		{
			for (int i = 0; i < Level_2_GOs.Length; i++)
			{
				Level_2_GOs[i].SetActive(true);
			}
			break;
		}
		}
	}

	public void SetTeam(bool isPlayer)
	{
		for (int i = 0; i < bodies.Length; i++)
		{
			if (isPlayer)
			{
				bodies[i].material = playerMaterial;
			}
			else
			{
				bodies[i].material = enemyMaterial;
			}
		}
		for (int j = 0; j < equipments.Length; j++)
		{
			if (isPlayer)
			{
				equipments[j].material = playerMaterial;
			}
			else
			{
				equipments[j].material = enemyMaterial;
			}
		}
	}

	public void SetSpriteLvL(Sprite tempSprite, bool isPlayer)
	{
		spriteLvL.gameObject.SetActive(true);
		spriteLvL.transform.localRotation = ((!isPlayer) ? Quaternion.Euler(50f, 90f, 0f) : Quaternion.Euler(50f, 270f, 0f));
		spriteLvL.sprite = tempSprite;
		if (isBoss)
		{
			spriteLvL.gameObject.SetActive(false);
		}
	}

	public void CloseSpriteLvL()
	{
		spriteLvL.gameObject.SetActive(false);
	}

	public void Init()
	{
		upgrades_level1 = new List<GameObject>();
		upgrades_level2 = new List<GameObject>();
		upgrades_level3 = new List<GameObject>();
		FindUpgrade(base.transform);
		Level_0_GOs = upgrades_level1.ToArray();
		Level_1_GOs = upgrades_level2.ToArray();
		Level_2_GOs = upgrades_level3.ToArray();
		upgrades_level1 = new List<GameObject>();
		upgrades_level2 = new List<GameObject>();
		upgrades_level3 = new List<GameObject>();
		if (bodies == null || bodies.Length == 0)
		{
			bodies = base.transform.GetComponentsInChildren<SkinnedMeshRenderer>(true);
		}
		spriteLvL = base.transform.Find("SpriteLvL").GetComponent<SpriteRenderer>();
	}

	private void FindUpgrade(Transform trans)
	{
		if (trans.name.ToLower().Contains("level1"))
		{
			upgrades_level1.Add(trans.gameObject);
		}
		else if (trans.name.ToLower().Contains("level2"))
		{
			upgrades_level2.Add(trans.gameObject);
		}
		else if (trans.name.ToLower().Contains("level3"))
		{
			upgrades_level3.Add(trans.gameObject);
		}
		foreach (Transform tran in trans)
		{
			FindUpgrade(tran);
		}
	}
}
