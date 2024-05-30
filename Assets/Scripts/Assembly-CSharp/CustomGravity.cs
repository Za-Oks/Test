using UnityEngine;

public class CustomGravity : MonoBehaviour
{
	public float gravity = 9.81f;

	private Rigidbody thisRB;

	private void Awake()
	{
		thisRB = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		thisRB.AddForce(Vector3.down * gravity, ForceMode.Force);
	}
}
