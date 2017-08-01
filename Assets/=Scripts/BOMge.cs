using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOMge : MonoBehaviour {
    public float span = 5f;
    public float force = -200f;
    float delta = 0;
    public GameObject BOMPrefab;
    public GameObject per;
    public AudioClip BomSE2;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.delta += Time.deltaTime;
        
        if (this.delta > this.span)
        {
            this.delta = 0;

            GameObject go = Instantiate(BOMPrefab) as GameObject;
            go.transform.position = transform.position+new Vector3(0, 0, 2);
            Rigidbody2D goo = go.GetComponent<Rigidbody2D>();
            goo.AddForce(new Vector2(force, 0));

        }
    }

    void bakuhatu()
    {
        GameObject pr = Instantiate(per) as GameObject;
        pr.transform.position = transform.position;
        AudioSource.PlayClipAtPoint(BomSE2, transform.position);
        Destroy(gameObject);

    }
    void OnParticleCollision(GameObject other)
    {

        bakuhatu();
    }
}
