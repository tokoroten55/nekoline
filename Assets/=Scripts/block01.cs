using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block01 : MonoBehaviour {


    Vector3 startPos;



    public float idouryou=0;
    public float speed=0;
    public float idouryou2=0;
    public float speed2=0;

    void Start () {
        startPos = transform.localPosition; 
	}
	
	// Update is called once per frame
	void Update () {
        float x = idouryou2 * Mathf.Sin(Time.time * speed2);
        float y = idouryou * Mathf.Sin(Time.time * speed);
        transform.localPosition = startPos + new Vector3(x, y,2);



	}
}
