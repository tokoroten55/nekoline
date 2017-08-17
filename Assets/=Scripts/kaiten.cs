using UnityEngine;
using System.Collections;


public class kaiten : MonoBehaviour
{
    public GameObject obj;
    float count;
    public float speed = 3f;
    int hrag = 0;
    public int houkou = 1;
    public AudioClip boSE;
    SpriteRenderer MainSpriteRenderer;
    public Sprite StandbySprite;
    
    void Start()
    {
        MainSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update() {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
            if (hrag == 0　&& other.gameObject.CompareTag("Player"))
            {
                hrag = 1;
            AudioSource.PlayClipAtPoint(boSE, transform.position);
            //スプライト切り替え
            MainSpriteRenderer.sprite = StandbySprite;
            StartCoroutine(maware());
            }
           }

    //でバック用ボタン
    public void kai()
    {
        StartCoroutine(maware());
    }


    public IEnumerator maware()
    {
        for (count = 0; count <= 90f; count += speed)
        {
            obj.transform.Rotate(new Vector3( 0f, 0f ,- speed*houkou), Space.World);
            yield return null;
        }
        obj.transform.Rotate(new Vector3( 0f, 0f, count - 90f+houkou), Space.World);
    }
}