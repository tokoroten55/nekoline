using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{

    static int Tpoint;
    GameObject tpoint;
    GameObject LIFEImage;

    void Start()
    {
        
        LIFEPoint();
        this.tpoint = GameObject.Find("LIFECanvas/LIFEPanel/POINTImage/POINTText");

        point();
    }

    // Update is called once per frame
    void Update()
    {

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
        SaveData.Instance.Save();
    }

    public void MAXLIFE()
    {
        SaveData.Instance.Life = SaveData.Instance.MaxLife;
        Debug.Log("test-max");
        LIFEPoint();
    }


}