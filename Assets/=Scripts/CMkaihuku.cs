using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMkaihuku : MonoBehaviour {

    GameObject PlayerData2;
    GameObject GameDirector2;


    void Start () {
        this.PlayerData2 = GameObject.Find("PlayerData");
        this.GameDirector2 = GameObject.Find("GameDirector");
    }

    // CMでライフ回復
    public void CMPanel () {
        PlayerData2.GetComponent<PlayerData>().MAXLIFE();
        PlayerData2.GetComponent<PlayerData>().LIFEPoint();
        PlayerData2.GetComponent<PlayerData>().point();
        GameDirector2.GetComponent<GameDirector>().closepanel();
    }

    //猫缶でライフ回復
    public void Nekokan()
    {
        if (SaveData.Instance.Lifekan >= 1)
        {
            GameDirector2.GetComponent<GameDirector>().Info01();
            SaveData.Instance.Lifekan--;
            PlayerData2.GetComponent<PlayerData>().MAXLIFE();
            PlayerData2.GetComponent<PlayerData>().LIFEPoint();
            PlayerData2.GetComponent<PlayerData>().point();
            GameDirector2.GetComponent<GameDirector>().closepanel();
        }
        else
        {
            GameDirector2.GetComponent<GameDirector>().Info02();
            Debug.Log("猫缶不足");
        }
    }


    //パネルを閉じる
    public void closPanel()
    {
        GameDirector2.GetComponent<GameDirector>().closepanel();
    }

}
