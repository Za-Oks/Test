using System;
using UnityEngine;

[Serializable]
public class RagdollPart
{
	public Transform transform;

	public GameObject gameObject;

	public Rigidbody rigidbody;

	public Collider collider;

	public CharacterJoint joint;

	public RagdollPart(Transform tr, Rigidbody rb)
	{
		transform = tr;
		gameObject = tr.gameObject;
		rigidbody = rb;
		collider = tr.GetComponent<Collider>();
		collider.isTrigger = false;
		collider.enabled = false;
		joint = tr.GetComponent<CharacterJoint>();
	}
}
