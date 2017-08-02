using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kinematic : MonoBehaviour {

    // GameObject LINE;
    public GameObject hoshi;

    void Start () {
     //   LINE = GameObject.FindWithTag("line");
    }
	
	
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("line"))
        {
            GameObject obj = Instantiate(hoshi, other.transform.position, other.transform.rotation) as GameObject;    
        Destroy(other.gameObject);
        }
    }
}
