using UnityEngine;
using System.Collections;

public class GoofinChangeDirection : MonoBehaviour {

	public Vector3 randomDirection;
	private Rigidbody rb; 
	
	void Start (){
		rb = GetComponent<Rigidbody>();
		randomDirection = new Vector3 (Random.Range (-1, 2), 0, Random.Range (-1, 2)) * 2;
		Debug.Log ("Starting Random direction was: " + randomDirection);
	}

	// Update is called once per frame
	void Update () {

		Debug.DrawLine (transform.position, (transform.position + randomDirection), Color.red);
		Vector3 forwardVector = transform.forward - transform.localPosition;

		Debug.DrawLine (transform.position, transform.forward  * 2, Color.green);

		transform.LookAt (transform.position + randomDirection);

		Debug.Log ("transform is: " + transform);
		Debug.Log ("position is: " + transform.position);
		Debug.Log ("local pos is: " + transform.localPosition);
	}
}
