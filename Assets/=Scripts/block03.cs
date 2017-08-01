using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block03 : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            Invoke("Fall",1.0f);
        }
        
    }
    void Fall()
    {
        GetComponent<Rigidbody2D>().isKinematic = false;
    }
}
