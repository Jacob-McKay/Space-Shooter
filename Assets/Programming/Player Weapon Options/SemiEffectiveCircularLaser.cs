using UnityEngine;
using System.Collections;

public class SemiEffectiveCircularLaser : MonoBehaviour {

	public GameObject shot;
	public Transform shotSpawn;
	public int shotsPerFire = 1;
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

			//but since I'm an idiot, I'm gonna spawn a ton of shots in random directions :D
			for (int i = 0; i < shotsPerFire; i++) {

				GameObject projectile = (GameObject)Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
				Rigidbody projectileRB = projectile.GetComponent<Rigidbody> ();

				Vector3	randomDirection = Random.insideUnitSphere;
				//float randomSpeed = Random.Range (minProjectileSpeed, maxProjectileSpeed);

				//orient projectile so that it doesn't fly sideways/awkwardly
				Transform projTranny = projectile.transform;
				Vector3 directionFromProjectile = new Vector3 (projTranny.position.x + randomDirection.x, projTranny.position.y, projTranny.position.z + randomDirection.z);
				projTranny.LookAt (directionFromProjectile);

				//since this is a pseudo 2D game, randomness in the Y direction is silly, so zero it out
				//projectileRB.velocity = new Vector3 (randomDirection.x, 0, randomDirection.z) * randomSpeed;
				GetComponent<AudioSource>().Play ();
			}
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
