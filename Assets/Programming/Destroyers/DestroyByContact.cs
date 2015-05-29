using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

//	//upon collision, destroy the bolt (other) and then this android
//	void OnTriggerEnter(Collider other) {
//
//		//the tutorial wanted you to do:
//		// if (other.tag == "Boundary"){ return; } but I think that's ugly
//
//		if (other.tag != "Boundary") {
//			Destroy	 (other.gameObject);
//			Destroy (gameObject);
//		}
//	}


	public GameObject explosion;
	public GameObject playerExplosion;
	
	void OnTriggerEnter(Collider other) 
	{
		if (other.tag == "Boundary")
		{
			return;
		}
		Instantiate(explosion, transform.position, transform.rotation);
		if (other.tag == "Player")
		{
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			//gameController.GameOver ();
		}
		Destroy(other.gameObject);
		Destroy(gameObject);
	}
}
