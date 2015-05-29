using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	
	void Start ()
	{
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
}