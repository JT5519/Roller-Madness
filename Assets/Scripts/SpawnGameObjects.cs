using UnityEngine;
using System.Collections;

public class SpawnGameObjects : MonoBehaviour {

	public GameObject spawnPrefab;

	public float minSecondsBetweenSpawning = 3.0f;
	public float maxSecondsBetweenSpawning = 6.0f;
	
	public Transform chaseTarget;
	
	private float savedTime;
	private float secondsBetweenSpawning;

	// Use this for initialization
	void Start () {
		savedTime = Time.time;
		secondsBetweenSpawning = Random.Range (minSecondsBetweenSpawning, maxSecondsBetweenSpawning);
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.tag == "EnemySpawner")
		{
			if (Time.time - savedTime >= secondsBetweenSpawning && GameObject.FindGameObjectsWithTag("Enemy").Length < 5) // is it time to spawn again?
			{
				MakeThingToSpawn();
				savedTime = Time.time; // store for next spawn
				secondsBetweenSpawning = Random.Range(minSecondsBetweenSpawning, maxSecondsBetweenSpawning);
			}
		}
        else
        {
			if (Time.time - savedTime >= secondsBetweenSpawning && GameObject.FindGameObjectsWithTag(gameObject.tag).Length < 2) // is it time to spawn again?
			{
				MakeThingToSpawn();
				savedTime = Time.time; // store for next spawn
				secondsBetweenSpawning = Random.Range(minSecondsBetweenSpawning, maxSecondsBetweenSpawning);
			}
		}
	}

	void MakeThingToSpawn()
	{
		// create a new gameObject
		GameObject clone = Instantiate(spawnPrefab, transform.position, transform.rotation) as GameObject;

		// set chaseTarget if specified
		if ((chaseTarget != null) && (clone.gameObject.GetComponent<Chaser> () != null))
		{
			clone.gameObject.GetComponent<Chaser>().SetTarget(chaseTarget);
		}
	}
}
