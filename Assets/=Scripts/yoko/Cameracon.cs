using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameracon : MonoBehaviour {

    public GameObject player;
	void Start () {
    }
    
    void Update () {
        Vector3 playerPos = this.player.transform.position;
        transform.position = new Vector3(playerPos.x, transform.position.y,-10f);
	}
}
