using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerData : MonoBehaviour
{
    
    static int Tpoint;
    GameObject tpoint;
    GameObject LIFEImage;
    GameObject TIMEtxt;

    //GameObject test;

    void Start()
    {
        LIFEPoint();
        this.tpoint = GameObject.Find("LIFECanvas/LIFEPanel/POINTImage/POINTText");
        this.TIMEtxt = GameObject.Find("LIFECanvas/LIFEPanel/TIMEText");
        point();
        if (SaveData.Instance.kaishi == 1)
        {
            kidouchek();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (SaveData.Instance.Life < SaveData.Instance.MaxLife) {
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
            Tpoint +=SaveData.Instance.stage2[i];
            
        }
        for (int i = 0; i < SaveData.Instance.stage3.Length; i++)
        {
            Tpoint +=SaveData.Instance.stage3[i];
            
        }
        SaveData.Instance.flag = Tpoint;

        SaveData.Instance.Save();
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

        SaveData.Instance.kaishi = 0;
        long testestes = SaveData.Instance.oldTicks;
        long newTicks = DateTime.Now.Ticks;
        long diff = ((newTicks - testestes) / (1000 * 1000 * 10));
        int diff2 = ((int)diff)/300;
        float diff3 = (float)diff % 300;
        float diff4 = SaveData.Instance.restStaminaTime- diff3;

        if (diff4<=0){
            diff2++;
            diff4 = 300 + diff4;
            
        }
        if (diff2 > 0)
        {
            SaveData.Instance.Life += diff2;
            
        }
        
        SaveData.Instance.restStaminaTime = diff4;

        //max超えない
        if (SaveData.Instance.Life >= SaveData.Instance.MaxLife) {
            SaveData.Instance.Life = SaveData.Instance.MaxLife;
            SaveData.Instance.restStaminaTime = 300;
        }
        
        //test.GetComponent<Text>().text = "前回の起動より" + diff+"秒\n"+ diff2+"回復\n"+ diff3+"余り"+"\n"+20%300;


        LIFEPoint();
    }




    //アプリ終了時保存
    void OnApplicationQuit()
    {
        SaveData.Instance.kaishi = 1;
        SaveData.Instance.SetClose();
        SaveData.Instance.Save();
    }
}