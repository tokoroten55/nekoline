using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class STAGESELECT : MonoBehaviour {
    GameObject dir;
    GameObject PlayerData;

    int stageno;
    int nextstage;

    private void Start()
    {
        this.PlayerData = GameObject.Find("PlayerData");
        this.dir = GameObject.Find("GameDirector");
        stageno = dir.GetComponent<GameDirector>().stage;
        nextstage=stageno+1;
    }

    public void Stagegogo()
    {
        if (nextstage > 20) nextstage = 1;
        Invoke("stage", 1f);
    }
    public void stage()
    {
        if (SaveData.Instance.Life < 1)
        {
            dir.GetComponent<GameDirector>().openpanel();
        }
        else
        {
            SaveData.Instance.Life--;
            PlayerData.GetComponent<PlayerData>().LIFEPoint();
            SaveData.Instance.Save();
            SceneManager.LoadScene("Stage" + nextstage);
        }
    }

    //ステージセレクトへ
    public void selectgogo()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("_STAGE SELECT");
    }

    public void Retry()
    {
        if (SaveData.Instance.Life < 1)
        {
            dir.GetComponent<GameDirector>().openpanel();
        }
        else
        {
            SaveData.Instance.Life--;
            PlayerData.GetComponent<PlayerData>().LIFEPoint();
            SaveData.Instance.Save();
            SceneManager.LoadScene("Stage" + stageno);
        }
    }
}
