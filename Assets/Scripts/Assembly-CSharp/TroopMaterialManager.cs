using System;
using UnityEngine;

public class TroopMaterialManager : MonoBehaviour
{
	[Serializable]
	public class CustomMaterial
	{
		public Material material;

		public Texture2D[] texture_env = new Texture2D[5];
	}

	public CustomMaterial[] customMaterials;

	public void InitMaterials(int env)
	{
		if (env != 0)
		{
			env--;
			for (int i = 0; i < customMaterials.Length; i++)
			{
				customMaterials[i].material.mainTexture = customMaterials[i].texture_env[env];
			}
		}
	}
}
