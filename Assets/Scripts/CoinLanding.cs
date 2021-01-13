using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//script attached to coins to define their behaviour as soon as they collide with the ground

//coins must land and rotate in place, once landed
public class CoinLanding : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag=="Floor")
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            gameObject.GetComponent<Rotate>().enabled = true;
            gameObject.GetComponent<TrailRenderer>().enabled = false;
        }
    }
}
