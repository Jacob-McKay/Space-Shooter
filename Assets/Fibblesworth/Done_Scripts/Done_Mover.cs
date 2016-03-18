using UnityEngine;
using System.Collections;

public class Done_Mover : MonoBehaviour
{
	public float speed;

	void Start ()
	{
		GetComponent<Rigidbody>().velocity = transform.forward * speed;
	}

	void Update ()
	{
		Debug.Log ("Velocity is: " + GetComponent<Rigidbody> ().velocity.ToString());
	}
}
