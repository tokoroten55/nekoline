﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block02 : MonoBehaviour {

    Vector3 startPos;



    public float idouryou;
    public float speed;

    void Start()
    {
        startPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float z = idouryou * Mathf.Sin(Time.time * speed);

        transform.localPosition = startPos + new Vector3(z, 0, 2);



    }
}