using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{

    static int Tpoint;
    GameObject tpoint;
    GameObject LIFEImage;
    GameObject TIMEtxt;
    
    void Start()
    {
        
        LIFEPoint();
        this.tpoint = GameObject.Find("LIFECanvas/LIFEPanel/POINTImage/POINTText");
        this.TIMEtxt = GameObject.Find("LIFECanvas/LIFEPanel/TIMEText");
        
        point();
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

    public void MAXLIFE()
    {
        SaveData.Instance.Life = SaveData.Instance.MaxLife;
        Debug.Log("test-max");
        LIFEPoint();
    }

    //ライフタイマー発動
    void LIFETIME()
    {
            if (SaveData.Instance.restStaminaTime <= 0)
            {
                SaveData.Instance.Life++;
                SaveData.Instance.restStaminaTime = 30;
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
                Debug.Log(SaveData.Instance.restStaminaTime);
            }
        }
    void OnApplicationQuit()
    {
        SaveData.Instance.Save();
    }
}