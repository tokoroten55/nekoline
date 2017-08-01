using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kinematic : MonoBehaviour {

   // GameObject LINE;
    
    void Start () {
     //   LINE = GameObject.FindWithTag("line");
    }
	
	
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("line"))
        {

            Destroy(other.gameObject);
        }
    }
}
