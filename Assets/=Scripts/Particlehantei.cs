using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particlehantei:MonoBehaviour {
    GameObject dir;
    public AudioClip OkSE;
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
            dir.GetComponent<GameDirector>().nokori();
        }
    }
}
