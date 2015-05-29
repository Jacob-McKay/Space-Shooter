using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
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
	private float nextFire = 0.0f; //used to keep lazer state

	void Start (){
		rb = GetComponent<Rigidbody>();
	}
	
	void Update() {

		//only shoot if mouse is clicked + throttle by fire-rate
		if (Input.GetButton("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;

			GameObject projectile = (GameObject) Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			Rigidbody projectileRB =  projectile.GetComponent<Rigidbody>();
			Debug.Log (projectileRB);
			Vector3	randomDirection = Random.insideUnitSphere;
			float randomSpeed = Random.Range (0, 100);

			projectileRB.velocity = (randomDirection * randomSpeed);
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
}