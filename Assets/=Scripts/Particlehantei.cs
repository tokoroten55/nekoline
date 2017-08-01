using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particlehantei:MonoBehaviour {
    public GameDirector dir;
    public AudioClip OkSE;
    void Start()
    {
      
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(OkSE, transform.position);
            Object.Destroy(other.gameObject);
            dir.nokori();
        }
    }
}
