using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	//upon collision, destroy the bolt (other) and then this android
	void OnTriggerEnter(Collider other) {

		//the tutorial wanted you to do:
		// if (other.tag == "Boundary"){ return; } but I think that's ugly

		if (other.tag != "Boundary") {
			Destroy	 (other.gameObject);
			Destroy (gameObject);
		}
	}
	
}
