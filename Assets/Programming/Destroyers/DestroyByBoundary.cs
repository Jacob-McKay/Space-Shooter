using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour {

	//When shots/hazards leave this bounding box, give em the axe
	//to save resources
	void OnTriggerExit(Collider other){
		Destroy (other.gameObject);
	}

}
