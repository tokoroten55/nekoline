using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class info : MonoBehaviour {
    float delta = 0;
    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        this.delta += Time.deltaTime;
        if (Input.GetMouseButton(0)&& delta >= 0.5f)
        {
            delta = 0;
            this.gameObject.SetActive(false);
        }
    }

}
