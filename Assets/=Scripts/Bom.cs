﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bom : MonoBehaviour {
    float rimi = 3.0f;
    bool BBom = false;

    public GameObject bomText;
    public GameObject per;
    public AudioClip BomSE;
    // Use this for initialization
    void Start () {
        //this.bomText = GameObject.Find("BOM/BOMText");
        

    }
	
	// Update is called once per frame
	void Update () {

        if (BBom)
        {
            this.rimi -= Time.deltaTime;
            this.bomText.GetComponent<TextMesh>().text = rimi.ToString("0");

            if (rimi <= 0) bakuhatu();


        }

        float pos = transform.position.y;
        if (pos <= -5.5)
        {
            gameObject.SetActive(false);
       }


    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player")) {
        BBom=true;
        }
    }
    void bakuhatu()
    {
        GameObject pr = Instantiate(per) as GameObject;
        pr.transform.position = transform.position;
        AudioSource.PlayClipAtPoint(BomSE, transform.position);
        //this.gameObject.SetActive(false);
        Destroy(gameObject);

    }
    void OnParticleCollision(GameObject other)
    {

        bakuhatu();
    }
}

