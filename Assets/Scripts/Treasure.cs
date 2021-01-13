using UnityEngine;
using System.Collections;
//script attached to coins, used to handle event when coin is collected
public class Treasure : MonoBehaviour {

	public int value = 10;
	public GameObject explosionPrefab;

	void OnTriggerEnter (Collider other) //coin trigger
	{
		if (other.gameObject.tag == "Player") {
			if (GameManager.gm!=null)
			{
				// tell the game manager to Collect
				GameManager.gm.Collect (value); //value added to score 
			}
			
			// explode if specified
			if (explosionPrefab != null) {
				Instantiate (explosionPrefab, transform.position, Quaternion.identity);
			}
			
			// destroy after collection
			Destroy (gameObject);
		}
	}
}
