using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cat : MonoBehaviour
{

    public GameObject Dire;
    public GameObject bakuhatsu;
    Rigidbody2D rigid2D;
    Animator animator;
    float walkForce = 20f;
    float upForce = 0;
    float key = 1;
    float size;
    float delta;
    bool deathh=true;
    public AudioClip shibouSE;

    //public float d2;
    private const float RAY_DISPLAY_TIME = 3;


    void Start()
    {
        this.Dire = GameObject.Find("GameDirector");
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        size=transform.localScale.x;
        key = size;
    }

    void Update()
    {
        this.delta += Time.deltaTime;


        float pos = transform.position.y;
        if (pos <= -5.2)
        {
            death();
        }


        //猫速度
        float speedx = Mathf.Abs(this.rigid2D.velocity.x);

        if (Time.timeScale == 1 && speedx <= 3)
        {
            this.rigid2D.AddForce(transform.right * key * this.walkForce);

            if (upForce == 0)
            {
                upForce = 1;
                this.rigid2D.AddForce(transform.up * 20);
            }
        }

        this.animator.speed = speedx / 2f;

        if (speedx <= 0.10f && delta >= 0.1)
        {
            key = key * -1;
            transform.localScale = new Vector3(key, size, 1);
            delta = 0; upForce = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("teki")) { death(); }
    }

    void death()
    {
        if (deathh)
        {
            deathh = false;
            AudioSource.PlayClipAtPoint(shibouSE, transform.position);
            Destroy(gameObject);
            GameObject go = Instantiate(bakuhatsu) as GameObject;
            go.transform.position = transform.position;

            if (gameObject.CompareTag("Player"))
            {
                Dire.GetComponent<GameDirector>().hukkatu();
            }
        }

    }
    void OnParticleCollision(GameObject other)
    {
        death();
        }
    }
