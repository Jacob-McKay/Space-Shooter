using UnityEngine;
using System.Collections;

public class RegularLaser : MonoBehaviour {

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate = 1;

	public bool leftMouseTrigger = true;
	public bool rightMouseTrigger = false;
	public bool middleMouseTrigger = false;

	private float nextFire;
	
	void Update ()
	{
		if (TriggerWasPulled() && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			GetComponent<AudioSource>().Play ();
		}
	}

	private bool TriggerWasPulled(){
		return (
			(leftMouseTrigger && Input.GetMouseButton (0)) ||
			(rightMouseTrigger && Input.GetMouseButton (1)) ||
			(middleMouseTrigger && Input.GetMouseButton (2))
		);
	}
}
