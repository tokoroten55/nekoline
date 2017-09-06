using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    static int Tpoint;
    GameObject tpoint;
    GameObject LIFEImage;
    GameObject TIMEtxt;
    GameObject COINImage;
    GameObject KANImage;
    //GameObject test;

    void Awake()
    {
        System.DateTime now = System.DateTime.Now;
        if (SaveData.Instance.kaishi == 1)
        {
            kidouchek();
        }
    }
    private void Start()
    {
        LIFEPoint();
        this.tpoint = GameObject.Find("LIFECanvas/LIFEPanel/POINTImage/POINTText");
        this.COINImage = GameObject.Find("LIFECanvas/LIFEPanel/COINImage/Text");
        this.KANImage = GameObject.Find("LIFECanvas/LIFEPanel/KANImage/Text");
        this.TIMEtxt = GameObject.Find("LIFECanvas/LIFEPanel/TIMEText");
        point();
        coinkan();

    }

    // Update is called once per frame
    void Update()
    {

        if (SaveData.Instance.Life < SaveData.Instance.MaxLife)
        {
            LIFETIME();
            return;
        }
        else
        {
            TIMEtxt.GetComponent<Text>().text = "";
            return;
        }
    }

    //トータルポイント取得
    public static int getPoint()
    {
        return Tpoint;
    }

    //ライフ取得/更新
    public void LIFEPoint()
    {
        this.LIFEImage = GameObject.Find("LIFECanvas/LIFEPanel/LIFEImage/Text");
        LIFEImage.GetComponent<Text>().text = "LIFE " + SaveData.Instance.Life + "/" + SaveData.Instance.MaxLife;
    }



    //トータルポイント取得/更新
    public void point()
    {

        //現在のポイント確認
        Tpoint = 0;
        for (int i = 0; i < SaveData.Instance.stage1.Length; i++)
        {
            Tpoint += SaveData.Instance.stage1[i];


        }

        for (int i = 0; i < SaveData.Instance.stage2.Length; i++)
        {
            Tpoint += SaveData.Instance.stage2[i];

        }
        for (int i = 0; i < SaveData.Instance.stage3.Length; i++)
        {
            Tpoint += SaveData.Instance.stage3[i];

        }
        SaveData.Instance.flag = Tpoint;
        this.tpoint.GetComponent<Text>().text = "point " + Tpoint;

    }






    private void syoki()
    {

        //データクリア///必要ないかも
        SaveData.Instance.syoki = 1;
        for (int i = 0; i < SaveData.Instance.stage1.Length; i++)
        {
            SaveData.Instance.stage1[i] = 0;

        }

        for (int i = 0; i < SaveData.Instance.stage2.Length; i++)
        {
            SaveData.Instance.stage2[i] = 0;

        }
        for (int i = 0; i < SaveData.Instance.stage3.Length; i++)
        {
            SaveData.Instance.stage3[i] = 0;
        }
    }

    //ライフ最大回復
    public void MAXLIFE()
    {
        SaveData.Instance.Life = SaveData.Instance.MaxLife;
        Debug.Log("max");
        LIFEPoint();
    }

    //ライフタイマー発動
    void LIFETIME()
    {
        if (SaveData.Instance.restStaminaTime <= 0)
        {
            SaveData.Instance.Life++;
            SaveData.Instance.restStaminaTime = 300;
            LIFEPoint();
            return;
        }
        else
        {
            int minutes = (int)SaveData.Instance.restStaminaTime / 60;
            int seconds = (int)SaveData.Instance.restStaminaTime % 60;
            string second = seconds.ToString();
            second = second.PadLeft(2, '0');
            // 表示するテキスト

            TIMEtxt.GetComponent<Text>().text = "あと" + minutes + ":" + second + "";
            SaveData.Instance.restStaminaTime -= Time.unscaledDeltaTime;
            //Debug.Log(SaveData.Instance.restStaminaTime);
        }
    }
    //起動時チェック
    void kidouchek()
    {
        //this.test = GameObject.Find("Canvas/test");        
        long testestes = SaveData.Instance.oldTicks;
        long newTicks = DateTime.Now.Ticks;
        long diff = ((newTicks - testestes));//  (1000 * 1000 * 10));
        float diff0=diff/ (1000 * 1000 * 10);
        float diff1 = SaveData.Instance.restStaminaTime;
        int diff2 = ((int)diff0) / 300;
        float diff3 = (float)diff0 % 300;
        float diff4 = diff1 - diff3;


        //残り時間がマイナスの場合300秒からその余りを引く
        if (diff4 <= 0)
        {
            diff2++;
            diff4 = 300 + diff4;

        }
        //一度セーブを反映
        SaveData.Instance.Life += diff2;
        SaveData.Instance.restStaminaTime = diff4;
        Debug.Log("経過時間" + testestes+"-"+ newTicks+"="+diff+"("+diff0);
        Debug.Log("回復量"+diff2);
        Debug.Log("あまり時間"+diff4);
        //max超えない
        if (SaveData.Instance.Life >= SaveData.Instance.MaxLife)
        {
            SaveData.Instance.Life = SaveData.Instance.MaxLife;
            SaveData.Instance.restStaminaTime = 300;
        }
        
        LIFEPoint();
        SaveData.Instance.kaishi = 0;
    }

    //ＣＯＩＮと猫缶表示
    public void coinkan()
    {
        COINImage.GetComponent<Text>().text = SaveData.Instance.Necoin + "";
        KANImage.GetComponent<Text>().text = SaveData.Instance.Lifekan + "";
    }


    //アプリ終了時保存
    void OnApplicationQuit()
    {
        SaveData.Instance.kaishi = 1;
        SaveData.Instance.Save();
        Debug.Log("save");
    }

    //データクリア
    public void dateclear()
    {
        syoki();
        SaveData.Instance.kaishi = 0;
        SaveData.Instance.Life = 5;
        SaveData.Instance.MaxLife = 5;
        SaveData.Instance.Lifekan = 99;
        SaveData.Instance.Necoin = 0;
        SaveData.Instance.restStaminaTime = 0;
        SaveData.Instance.syoki = 0;
        for (int i = 1; i < 61; i++) { SaveData.Instance.stageList[i] = 0; }
            SaveData.Instance.stageList[80] = 0;

        SceneManager.LoadScene("StartGame");
    }
    }