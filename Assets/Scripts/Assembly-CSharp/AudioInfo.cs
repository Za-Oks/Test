using System;
using UnityEngine;

[Serializable]
public class AudioInfo
{
	public Transform transform;

	[HideInInspector]
	public AudioSource audioSource;

	[HideInInspector]
	public float length;

	public AudioInfo(Transform transform)
	{
		this.transform = transform;
		audioSource = transform.GetComponent<AudioSource>();
		length = audioSource.clip.length;
	}

	public void Play(Vector3 position)
	{
		transform.position = position;
		audioSource.Play();
	}
}
