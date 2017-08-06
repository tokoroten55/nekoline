using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour {
    Animator animator;

    // Use this for initialization
    void Start () {
        this.animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
    }

    public void panel()
    {
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        this.animator.SetTrigger("Panel Trigger");
    }

    public void panel2()
    {
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        this.animator.SetTrigger("Panel2 Trigger");
    }

}
