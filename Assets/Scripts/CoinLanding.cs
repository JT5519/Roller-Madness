using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinLanding : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag=="Floor")
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            //gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            gameObject.GetComponent<Rotate>().enabled = true;
            gameObject.GetComponent<TrailRenderer>().enabled = false;
        }
    }
}
