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
		
	public AudioClip recordScratch;

	private int score;
	private AudioSource backgroundMusicSource;

	void Start ()
	{
		//get a handle to the background music player
		backgroundMusicSource = GetComponent<AudioSource> ();
		Debug.Log ("Background Music source is: " + backgroundMusicSource);

		//our super slick GUI that prints the score
		score = 0;
		updateScoreText ();

		//this code will run apart from the game Update loop
		StartCoroutine (SpawnWaves ());
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
		StartCoroutine (gameOverSounds ());
	}

	IEnumerator gameOverSounds()
	{
		backgroundMusicSource.clip = recordScratch;
		backgroundMusicSource.loop = false;
		backgroundMusicSource.Play ();
		yield return new WaitForSeconds (0);
//		yield return new WaitForSeconds(backgroundMusicSource.clip.length);
//
//		GameObject wahwahObject = transform.Find ("WahhWahhWahhhhhSource").gameObject;
//		AudioSource wahwahSource = wahwahObject.GetComponent<AudioSource> ();
//		wahwahSource.Play ();
	}
}