﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Ball;
//script that handles health of objects it is attached to
public class Health : MonoBehaviour {
	
	public enum deathAction {loadLevelWhenDead,doNothingWhenDead};
	private Rigidbody rb;
	public float healthPoints = 1f;
	public float respawnHealthPoints = 1f;		//base health points
	
	public int numberOfLives = 1;					//lives and variables for respawning
	public bool isAlive = true;	

	public GameObject explosionPrefab;
	
	public deathAction onLivesGone = deathAction.doNothingWhenDead;
	
	public string LevelToLoad = "";
	
	private Vector3 respawnPosition;
	private Quaternion respawnRotation;
	

	// Use this for initialization
	void Start () 
	{
		// store initial position as respawn location
		respawnPosition = transform.position;
		respawnRotation = transform.rotation;
		
		if (LevelToLoad=="") // default to current scene 
		{
			LevelToLoad = Application.loadedLevelName;
		}
		rb = GetComponent<Rigidbody>();
		StartCoroutine(HealthCo()); //routine to handle health
	}


	// Update is called once per frame
	IEnumerator HealthCo() 
	{
		bool x = true;
		while (x)
		{
			if (healthPoints <= 0)
			{               // if the object is 'dead'
				numberOfLives--;                    // decrement # of lives, update lives GUI
				if (explosionPrefab != null)
				{
					Instantiate(explosionPrefab, transform.position, Quaternion.identity);
				}
				if (numberOfLives > 0)
				{ // respawn
					gameObject.GetComponent<MeshRenderer>().enabled = false;
					gameObject.GetComponent<TrailRenderer>().enabled = false;
					gameObject.GetComponent<BallUserControlM>().enabled = false;
					gameObject.GetComponent<Rigidbody>().useGravity = false;
					gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
					if (gameObject.tag == "Player")
					{
						yield return new WaitForSeconds(2f);
					}
					gameObject.GetComponent<MeshRenderer>().enabled = true;
					gameObject.GetComponent<TrailRenderer>().enabled = true;
					gameObject.GetComponent<BallUserControlM>().enabled = true;
					gameObject.GetComponent<Rigidbody>().useGravity = true;
					transform.position = respawnPosition;   // reset the player to respawn position
					transform.rotation = respawnRotation;
					rb.velocity = Vector3.zero;
					healthPoints = respawnHealthPoints; // give the player full health again
				}
				else
				{ // here is where you do stuff once ALL lives are gone)
					isAlive = false;

					switch (onLivesGone)
					{
						case deathAction.loadLevelWhenDead:
							Application.LoadLevel(LevelToLoad);
							break;
						case deathAction.doNothingWhenDead:
							// do nothing, death must be handled in another way elsewhere
							break;
					}
					x = false;
					Destroy(gameObject);
				}
			}
			yield return null;
		}
	}
	
	public void ApplyDamage(float amount)
	{	
		healthPoints = healthPoints - amount;	
	}
	
	public void ApplyHeal(float amount)
	{
		healthPoints = healthPoints + amount;
	}

	public void ApplyBonusLife(int amount)
	{
		numberOfLives = numberOfLives + amount;
	}
	
	public void updateRespawn(Vector3 newRespawnPosition, Quaternion newRespawnRotation) {
		respawnPosition = newRespawnPosition;
		respawnRotation = newRespawnRotation;
	}
}
