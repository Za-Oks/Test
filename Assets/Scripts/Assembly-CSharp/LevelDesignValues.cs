using System;
using UnityEngine;

[Serializable]
public class LevelDesignValues
{
	public LevelDesign design;

	[Header("Values")]
	public GameObject levelDesignGO;

	public Sprite levelDesignSR;

	[Header("Fog")]
	public Color32 FogColor;

	public float FogDensity;

	[Header("Skybox")]
	public Material skyBox;
}
