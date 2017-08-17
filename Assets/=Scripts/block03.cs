using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block03 : MonoBehaviour {

    public float time = 1.0f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            Invoke("Fall",time);
        }
        
    }
    void Fall()
    {
        GetComponent<Rigidbody2D>().isKinematic = false;
    }
}
