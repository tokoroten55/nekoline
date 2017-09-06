using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particlehantei:MonoBehaviour {
    GameObject dir;
    public GameObject okParticle;
    public AudioClip OkSE;
    //Rigidbody2D rigid;

    void Start()
    {
      this.dir = GameObject.Find("GameDirector");
       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(OkSE, transform.position);
            Object.Destroy(other.gameObject);
            //rigid=other.gameObject.GetComponent<Rigidbody2D>();
            //rigid.AddForce(transform.up * 500);
            GameObject ok = Instantiate(okParticle) as GameObject;
            ok.transform.position = transform.position;
            
            other.GetComponent<CircleCollider2D>().enabled = false;
            dir.GetComponent<GameDirector>().nokori();
        }
        if (other.gameObject.CompareTag("nakama"))
        {
            AudioSource.PlayClipAtPoint(OkSE, transform.position);
            Object.Destroy(other.gameObject);
            GameObject ok = Instantiate(okParticle) as GameObject;
            ok.transform.position = transform.position;
            dir.GetComponent<GameDirector>().onakama();
        }
    }



}
