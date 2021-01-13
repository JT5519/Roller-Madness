using UnityEngine;
using System.Collections;
//script to spawn gameobjects
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
			//only if seconds between spawns has been waited and number of enemies are less than 5 (i.e. maximum, 5 enemies at a time)
			if (Time.time - savedTime >= secondsBetweenSpawning && GameObject.FindGameObjectsWithTag("Enemy").Length < 5) // is it time to spawn again?
			{
				MakeThingToSpawn();
				savedTime = Time.time; // store for next spawn
				secondsBetweenSpawning = Random.Range(minSecondsBetweenSpawning, maxSecondsBetweenSpawning);
			}
		}
        else
        {
			//maximum coins from a particular spawner at a time = 1, total spawners 4 so total 4 coins at a time max
			//coins and the spawners they come from have the same tag. coin from spawner1 and spawner1 have tag spawner1, same for 2 and so on
			if (Time.time - savedTime >= secondsBetweenSpawning && GameObject.FindGameObjectsWithTag(gameObject.tag).Length < 2)
			{
				MakeThingToSpawn();
				savedTime = Time.time; // store for next spawn
				secondsBetweenSpawning = Random.Range(minSecondsBetweenSpawning, maxSecondsBetweenSpawning);
			}
		}
	}
	//spawn function
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
