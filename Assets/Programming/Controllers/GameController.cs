using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public Text scoreText;
	public Text restartText;
	public Text gameOverText;
		
	private AudioSource backgroundMusicSource;
	public AudioClip recordScratch;
	public AudioClip wahhWahhhWahhhWahhhhhhhhhhhh;

	private bool gameOver;
	private bool restart;
	private int score;



	void Start ()
	{
		//set game state
		gameOver = false;
		restart = false;
		score = 0;

		//setup our super sophistocated UI
		updateScoreText ();
		restartText.text = "";
		gameOverText.text = "";

		//get a handle to the background music player
		backgroundMusicSource = GetComponent<AudioSource> ();

		//this code will run apart from the game Update loop
		StartCoroutine (SpawnWaves ());
	}

	void Update ()
	{
		if (restart) 
		{
			if (Input.GetKeyDown (KeyCode.R))
			{
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}
	
	IEnumerator SpawnWaves ()
	{
		//give the player some time to get ready for the waves
		yield return new WaitForSeconds (startWait);

		//for the rest of the game
		while (true)
		{

			//instantiate an asteroid after <spawnWait> seconds up to <hazardCount> asteroids
			for (int i = 0; i < hazardCount; i++)
			{
				//at a random horizontal location, off the top of the screen, create the asteroid
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);

				//wait <spawnWait> seconds to create the next astroid, so the asteroids don't come down in one horizontal line
				yield return new WaitForSeconds (spawnWait);
			}

			//wait <waveWait> seconds to spawn the next wave
			yield return new WaitForSeconds (waveWait);

			if(gameOver)
			{
				restartText.text = "Press 'R' for Restart";
				restart = true;

				//bust us up out of this infinite while loop
				break;
			}
		}
	}

	/// <summary>
	/// Adds the changeInScore value (or subtracts if it's negative) to the score
	/// and updates the sophistocated UI
	/// </summary>
	/// <param name="changeInScore">Change in score.</param>
	public void updateScore (int changeInScore)
	{
		score += changeInScore;
		updateScoreText ();
	}

	private void updateScoreText()
	{
		scoreText.text = "Score: " + score;
	}
	
	public void gameOverMan()
	{
		//update UI and state
		gameOverText.text = "GAME OVER MAN!!!1";
		gameOver = true;
		StartCoroutine (gameOverSounds ());
	}

	IEnumerator gameOverSounds()
	{

		//abruptly stop our crappy techno music
		backgroundMusicSource.clip = recordScratch;
		backgroundMusicSource.loop = false;
		backgroundMusicSource.Play ();

		//when that's over, play the sad trombone, you loser...
		yield return new WaitForSeconds(recordScratch.length);
		backgroundMusicSource.clip = wahhWahhhWahhhWahhhhhhhhhhhh;
		backgroundMusicSource.Play ();
	}
}