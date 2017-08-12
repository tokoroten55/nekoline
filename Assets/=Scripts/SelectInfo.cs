using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SelectInfo : MonoBehaviour
{
    GameObject infoPanel;
    GameObject infotext;
    GameObject PlayerData;
    GameObject REPanel;

    void Start()
    {
        this.infotext = GameObject.Find("infoPanel/Text");
        this.infoPanel = GameObject.Find("infoPanel");
        infoPanel.SetActive(false);
        this.PlayerData = GameObject.Find("PlayerData");
        this.REPanel = GameObject.Find("REPanel");
        REPanel.SetActive(false);
    }


    //インフォパネル
    public void Info01()
    {
        SoundManager.Instance.PlaySE(2);
        this.infotext.GetComponent<Text>().text = "ライフ回復したよ♪";
        infoPanel.SetActive(true);
    }
    public void Info02()
    {
        SoundManager.Instance.PlaySE(2);
        this.infotext.GetComponent<Text>().text = "猫缶が足りません|дﾟ)";
        infoPanel.SetActive(true);
    }
    public void Info03()
    {
        SoundManager.Instance.PlaySE(2);
        this.infotext.GetComponent<Text>().text = "猫缶を購入しました";
        infoPanel.SetActive(true);
    }
    public void Info04()
    {
        SoundManager.Instance.PlaySE(2);
        this.infotext.GetComponent<Text>().text = "ライフの上限が1増えました！";
        infoPanel.SetActive(true);
    }
    public void Info05()
    {
        SoundManager.Instance.PlaySE(2);
        this.infotext.GetComponent<Text>().text = "お金が足りません";
        infoPanel.SetActive(true);
    }

    public void Info06()
    {
        SoundManager.Instance.PlaySE(2);
        this.infotext.GetComponent<Text>().text = "ネコ小判300枚購入しました。";
        infoPanel.SetActive(true);
    }

    public void Info07()
    {
        SoundManager.Instance.PlaySE(2);
        this.infotext.GetComponent<Text>().text = "ネコ小判1000枚購入しました。";
        infoPanel.SetActive(true);
    }

    public void Info08()
    {
        SoundManager.Instance.PlaySE(2);
        this.infotext.GetComponent<Text>().text = "購入できる上限です。";
        infoPanel.SetActive(true);
    }
    public void Info09()
    {
        SoundManager.Instance.PlaySE(2);
        this.infotext.GetComponent<Text>().text = "只今販売していないアイテム( ˘ω˘)ｽﾔｧ";
        infoPanel.SetActive(true);
    }
    //ショップ
    public void SHOPnekokan01()
    {
        if (SaveData.Instance.Necoin >= 70)
        {
            SaveData.Instance.Necoin -= 70;
            SaveData.Instance.Lifekan++;
            PlayerData.GetComponent<PlayerData>().coinkan();
            Info03();
        }
        else Info05();
    }
    public void SHOPnekokan02()
    {
        if (SaveData.Instance.Necoin >= 200)
        {
            SaveData.Instance.Necoin -= 200;
            SaveData.Instance.Lifekan += 3;
            PlayerData.GetComponent<PlayerData>().coinkan();
            Info03();
        }
        else Info05();
    }

    public void SHONekoinn01()
    {
        if (SaveData.Instance.Necoin <= 10000)
        {
            SaveData.Instance.Necoin += 300;
            PlayerData.GetComponent<PlayerData>().coinkan();
            Info06();
        }
        else Info08();
    }
    public void SHONekoinn02()
    {
        if (SaveData.Instance.Necoin <= 10000)
        {
            SaveData.Instance.Necoin += 1000;
            PlayerData.GetComponent<PlayerData>().coinkan();
            Info07();
        }
        else Info08();
    }

    public void SHOPMAXLIFE()
    {
        if (SaveData.Instance.MaxLife < 99)
        {
            SaveData.Instance.MaxLife ++;
            SaveData.Instance.Life++;
            PlayerData.GetComponent<PlayerData>().LIFEPoint();
            Info04();
        }
        else Info08();
    }

    public void nekoItem01()
    {
        Info09();
    }


    //ねこばん獲得
    public void CMbotton()
    {
        int coin;
        int tyusen = Random.Range(1, 10);
        if (tyusen >= 8) {
            coin = Random.Range(51, 100);
            SoundManager.Instance.PlaySE(3);
            SoundManager.Instance.PlaySE(3);
            SoundManager.Instance.PlaySE(3);
        }
        else
        {
            SoundManager.Instance.PlaySE(3);
            coin = Random.Range(10, 50);
        }
        
        this.infotext.GetComponent<Text>().text = "ネコ小判"+ coin+"獲得しました！";
        SaveData.Instance.Necoin += coin;
        PlayerData.GetComponent<PlayerData>().coinkan();
        infoPanel.SetActive(true);
    }

    //ライフ回復用パネルオープン
    public void rePnel()
    {
        REPanel.SetActive(true);
    }
    //ライフ回復用パネルオープンクローズ
    public void rePnelx()
    {
        REPanel.SetActive(false);
    }
    // CMでライフ回復
    public void CMPanel()
    {
        this.infotext.GetComponent<Text>().text = "CM";
        infoPanel.SetActive(true);
        PlayerData.GetComponent<PlayerData>().MAXLIFE();
        PlayerData.GetComponent<PlayerData>().LIFEPoint();
        PlayerData.GetComponent<PlayerData>().point();
        REPanel.SetActive(false);
    }

    //猫缶でライフ回復
    public void Nekokan()
    {
        if (SaveData.Instance.Lifekan >= 1)
        {
            this.infotext.GetComponent<Text>().text = "ライフ回復したよ♪";
            infoPanel.SetActive(true);
            SaveData.Instance.Lifekan--;
            PlayerData.GetComponent<PlayerData>().MAXLIFE();
            PlayerData.GetComponent<PlayerData>().LIFEPoint();
            PlayerData.GetComponent<PlayerData>().coinkan();
            PlayerData.GetComponent<PlayerData>().point();
            REPanel.SetActive(false);
        }
        else
        {
            this.infotext.GetComponent<Text>().text = "猫缶が足りません|дﾟ)";
            infoPanel.SetActive(true);
            Debug.Log("猫缶不足");
        }
    }

}