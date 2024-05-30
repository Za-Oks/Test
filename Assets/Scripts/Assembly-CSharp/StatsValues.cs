using System;
using UnityEngine;

[Serializable]
public class StatsValues
{
	[Header("Stats Values")]
	public float healthPercent = 100f;

	public float damagePercent = 100f;

	public float attackSpeedPercent = 100f;

	public float movementSpeedPercent = 100f;

	[Header("Price")]
	public int price;
}
