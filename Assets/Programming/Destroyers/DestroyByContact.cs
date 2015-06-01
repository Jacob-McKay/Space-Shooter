using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
	private GameController gameController;
	public int scoreValue;

	void Start ()
	{
		//we need a handle to the gameController so we can manipulate the score
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) 
		{
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		if (gameController == null) 
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void OnTriggerEnter(Collider other) 
	{
		//TODO refactor this to be a switch statement, this is ugly

		//do nothing if we "collide" with the boundary (being inside the boundary is coo brah)
		if (other.tag == "Boundary")
		{
			return;
		}

		//if we've collided with the player 
		if (other.tag == "Player")
		{
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			gameController.gameOverMan();
		}

		//will this update the score when the player dies too?
		gameController.updateScore (scoreValue);  //Yo dawg, we heard you like points, so we gave you 10 of em!
		Instantiate(explosion, transform.position, transform.rotation);
		Destroy(other.gameObject);
		Destroy(gameObject);
	}
}
