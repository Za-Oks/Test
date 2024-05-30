using System;
using UnityEngine;

[Serializable]
public class RandomRotate : MonoBehaviour
{
	private Quaternion rotTarget;

	public float rotateEverySecond;

	private float lerpCounter;

	private Quaternion rotCache;

	public RandomRotate()
	{
		rotateEverySecond = 1f;
	}

	public virtual void Start()
	{
		randomRot();
		InvokeRepeating("randomRot", 0f, rotateEverySecond);
	}

	public virtual void Update()
	{
		transform.rotation = Quaternion.Lerp(transform.rotation, rotTarget, lerpCounter * Time.deltaTime);
		lerpCounter += 1f;
	}

	public virtual void randomRot()
	{
		rotTarget = UnityEngine.Random.rotation;
		rotCache = transform.rotation;
		lerpCounter = 0f;
	}

	public virtual void Main()
	{
	}
}
