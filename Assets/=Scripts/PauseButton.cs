using UnityEngine;
using System.Collections;

public class PauseButton : MonoBehaviour
{
    //GameObject Dire;
    void Awake()
    {
        //ポーズパネル
    //    this.Dire = GameObject.Find("GameDirector");
    }

    void Update()
    {

    }

    public void onClick()
    {
        //Time.timeScale=0の場合停止する
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            //PausePanel.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            //PausePanel.SetActive(true);
        }
    }
}