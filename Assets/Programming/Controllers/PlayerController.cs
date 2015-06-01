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
	private AudioSource shotAudio;

	void Start (){
		rb = GetComponent<Rigidbody>();
		shotAudio = GetComponent<AudioSource> ();
	}
	
	void Update() {

		//only shoot if mouse is clicked + throttle by fire-rate
		if (Input.GetButton("Fire1") && weCanShootAgain()) {

			//we can fire again after <fireRate> seconds from now <Time.time>
			nextFire = Time.time + fireRate;

			//just instantiate the bolt
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			//and let the mover script attached to the prefab adjust the shot's velocity

			//make some noiiise y'alllll BANG BANG!!
			shotAudio.Play();
		}
	}

	void FixedUpdate ()
	{
		//receive movement input (from the keyboard in this case)
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		//translate that into a vector we could use to adjust the player's velocity
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.velocity = movement * speed;

		//keep the player from going off the screen
		rb.position = new Vector3 (
			Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
		);

		//fancy bank the player's ship when going left or right
		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
	}

	bool weCanShootAgain(){
		return Time.time > nextFire;
	}
}