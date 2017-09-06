using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOMge : MonoBehaviour
{
    public float span = 5f;
    public float force = -200f;
    float delta = 0;
    public int zizen = 0;
    public float kankaku = 0;
    public GameObject per;
    public AudioClip BomSE2;
    GameObject go;

    // Use this for initialization
    void Awake()
    {
        for (int y = 0; y <= zizen; y++)
        {
            StartCoroutine(BOMBOM());
        }


    }

    // Update is called once per frame
    void Update()
    {
        this.delta += Time.deltaTime;

        if (this.delta > this.span)
        {
            this.delta = 0;
            StartCoroutine(BOMBOM());
        }
    }

    void bakuhatu()
    {
        GameObject pr = Instantiate(per) as GameObject;
        pr.transform.position = transform.position;
        AudioSource.PlayClipAtPoint(BomSE2, transform.position);
        this.gameObject.SetActive(false);

    }
    void OnParticleCollision(GameObject other)
    {

        bakuhatu();
    }


    IEnumerator BOMBOM()
    {
        Pool pool = GetComponent<Pool>();
        go = pool.GetInstance();
        if (go)
        {
            Vector3 startPos = transform.localPosition;
            go.transform.position = transform.position+ new Vector3(Random.Range(kankaku*-1, kankaku), 0, 0);
            

            //int xx = Random.Range(-5, 5);
            //go.transform.localPosition = go + new Vector3(Random.Range(-5, 5), 0, 0);

            Rigidbody2D goo = go.GetComponent<Rigidbody2D>();
            
            goo.AddForce(new Vector2(force, 0));
        }
        
        yield return null;

    }
}
