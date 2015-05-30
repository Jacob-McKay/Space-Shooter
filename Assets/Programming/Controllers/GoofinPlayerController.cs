//This version of PlayerController shoots a crap-ton of shots in random directions
//make sure you remove the mover script from the bolt prefab for this to work as
//designed


using UnityEngine;
using System.Collections;

[System.Serializable]
public class GoofinBoundary
{
	public float xMin, xMax, zMin, zMax;
}

public class GoofinPlayerController : MonoBehaviour
{
	//player crap
	public float speed;
	public float tilt;
	public Boundary boundary;
	private Rigidbody rb;

	//laser crap
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	public int shotsPerFire;
	public int minProjectileSpeed;
	public int maxProjectileSpeed;
	private float nextFire = 0.0f; //used to keep lazer state

	void Start (){
		rb = GetComponent<Rigidbody>();
	}
	
	void Update() {

		//only shoot if mouse is clicked + throttle by fire-rate
		if (Input.GetButton("Fire1") && weCanShootAgain()) {

			//we can fire again after <fireRate> seconds from now <Time.time>
			nextFire = Time.time + fireRate;

			//before I started messing around, we'd just instantiate the bolt
			//Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			//and let the mover script attached to the prefab do the shooting


			//but since I'm an idiot, I'm gonna spawn a crap-ton of shots in random directions :D
			for(int i = 0; i < shotsPerFire; i++){

				GameObject projectile = (GameObject) Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
				Rigidbody projectileRB =  projectile.GetComponent<Rigidbody>();
		
				Vector3	randomDirection = Random.insideUnitSphere;
				float randomSpeed = Random.Range (minProjectileSpeed, maxProjectileSpeed);
				Debug.Log ("RandomSpeed was: " + randomSpeed);

				//orient projectile so that it doesn't fly sideways/awkwardly
				Transform projTranny = projectile.transform;
				Vector3 directionFromProjectile = new Vector3(projTranny.position.x + randomDirection.x, projTranny.position.y, projTranny.position.z + randomDirection.z);
				projTranny.LookAt(directionFromProjectile);

				//since this is a pseudo 2D game, randomness in the Y direction is silly, so zero it out
				projectileRB.velocity = new Vector3(randomDirection.x, 0, randomDirection.z) * randomSpeed;
			}
		}
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.velocity = movement * speed;

		rb.position = new Vector3 (
			Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
		);
		
		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
	}

	bool weCanShootAgain(){
		return Time.time > nextFire;
	}
}