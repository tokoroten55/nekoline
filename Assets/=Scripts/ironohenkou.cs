using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ironohenkou : MonoBehaviour {

    float delta = 0;
    
    void Start()
    {

    }

    
    void Update()
    {
        //ラインの寿命
        this.delta += Time.deltaTime;
        
        if (delta >= 3) {
            float xxx = this.transform.localScale.x;
            float yyy = this.transform.localScale.y;
            GetComponent<Renderer>().material.color = Color.blue;
            this.transform.localScale = new Vector3(xxx, yyy, 1) * 0.98f;
            kakenai();
        }else
        {
            if (delta > 2.5) {
                
                GetComponent<Renderer>().material.color = Color.magenta;

            }
        }

    }
    void kakenai() {


        this.GetComponent<Rigidbody2D>().isKinematic = false;
        Destroy(this.gameObject, 2f);
    }
}