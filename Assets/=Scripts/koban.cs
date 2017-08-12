using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class koban : MonoBehaviour {
    public float speed=20;
    bool kobanget = true;
    public AudioClip kobanSE;
    public GameObject kirakira;

    void Start () {
		
	}

	void Update () {
        //回転
        float x = Mathf.Sin(Time.time * speed)/2;
        transform.localScale = new Vector3(x, 0.5f, 1);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player")) { getget(); }
    }

    void getget()
    {
        if (kobanget)
        {
            kobanget = false;
            AudioSource.PlayClipAtPoint(kobanSE, transform.position);
            Destroy(gameObject);
            GameObject go = Instantiate(kirakira) as GameObject;
            go.transform.position = transform.position;

            //if (gameObject.CompareTag("Player")) {Dire.GetComponent<GameDirector>().hukkatu();}
        }

    }

    }
