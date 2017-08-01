using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;

public class StaminaManager : MonoBehaviour
{

    public Text staminatext;    //現在のスタミナを表示する
    public Text staminacounter;     //現在の残り時間を表示する
    private float fillAmount;　 //ゲージのfillamount値

    public Image staminaBar;            // スタミナバーの画像
    public int recoveryTimePerStamina; // 1スタミナ回復するのに必要な時間[秒]
    public int maxStaminaValue;               // スタミナの上限数
    public int nowStaminaValue;               // 現在のスタミナ数
    private int restStaminaTime;             // スタミナが1回復するまでの残り時間

    //テストコード
    public int spendStaminaValue;            //スタミナの消費量
    public Text staminaspend;    //スタミナ消費テキスト
    public Text staminarecover_all;        //全回復テキスト

    void Start()
    {
        AwakeStaminaDefine();
        StaminaDecrease();
    }

    void Update()
    {
        updateData();
        updateStamina();
    }

    void StaminaDecrease()
    {
        //1秒ごとのインヴォークで、残り時間から１ずつ引いていく。
        this.restStaminaTime -= 1;
        Invoke("StaminaDecrease", 1);
    }


    void updateStamina()
    {
        // テストコード
        staminaspend.text = "スタミナ消費" + spendStaminaValue;
        staminarecover_all.text = "スタミナ全回復";

        //スタミナゲージの減り具合
        staminaBar.fillAmount = fillAmount;
        fillAmount = (float)nowStaminaValue / (float)maxStaminaValue;

        // スタミナ回復残り時間を分と秒で表示するための変数
        int minutes = this.restStaminaTime / 60;
        int seconds = this.restStaminaTime % 60;
        // 表示するテキスト
        staminacounter.text = "あと" + String.Format("{0:D2}", minutes) + ":" + String.Format("{0:D2}", seconds) + "   ";
        staminatext.text = nowStaminaValue + " / " + this.maxStaminaValue;
        // スタミナがマックスの場合
        if (nowStaminaValue == this.maxStaminaValue)
        {
            //this.staminaMax();
            staminacounter.text = "";
            this.restStaminaTime = recoveryTimePerStamina;
            return;
        }
        // 最後の00:00の部分は見せないため、00:00秒になったら時間をリセットする
        if (this.restStaminaTime == 0)
        {
            nowStaminaValue += 1;
            this.restStaminaTime = recoveryTimePerStamina;
            return;
        }
    }

    void updateData()
    {
        //現在の時刻をGameInformationに記録
        // 現在の時刻を取得
        System.DateTime now = System.DateTime.Now;
        // 文字列に変換して保存
       // GameInformation.nowStaminaValue = this.nowStaminaValue;
       // GameInformation.restStaminaTime = this.restStaminaTime;

       // PlayerPrefs.SetString("lasttime", now.ToString());
       // PlayerPrefs.SetInt("NSV", GameInformation.nowStaminaValue);
       // PlayerPrefs.SetInt("RST", GameInformation.restStaminaTime);
    }

    // スタミナ消費
    // スタミナが足りない場合はfalseを返す
    public void staminacomponent()
    {
        this.spendStamina(spendStaminaValue);
    }
    public bool spendStamina(int spendStaminaValue)
    {
        bool isSuccess = false;
        if (nowStaminaValue < spendStaminaValue)
        {
            Debug.Log("スタミナが足りません");
            return isSuccess;
        }
        else
        {
            nowStaminaValue -= spendStaminaValue;
        }
        isSuccess = true;
        return isSuccess;
    }

    //起動時の処理
    public void AwakeStaminaDefine()
    {
        //PlayerPrefsからセーブ時のデータを取得
        // 初回起動時は取得できないので代わりに後ろの代数をいれる
        this.nowStaminaValue = PlayerPrefs.GetInt("NSV", this.maxStaminaValue);
        this.restStaminaTime = PlayerPrefs.GetInt("RST", this.recoveryTimePerStamina);
        string timestring = PlayerPrefs.GetString("lasttime", System.DateTime.Now.ToString());
        // 保存しておいた時刻は文字列なので時刻(DateTime)に変換
        System.DateTime datetime = System.DateTime.Parse(timestring);
        // 現在の時刻-保存しておいた時刻で経過時間を求める
        System.TimeSpan span = System.DateTime.Now - datetime;
        // 経過時間を秒で取得
        double spantime = span.TotalSeconds;
        // 差分時間によって回復できるスタミナ数と、その余りの時間
        int recoveryStaminaNum = (int)spantime / recoveryTimePerStamina;
        int resttime = (int)spantime % recoveryTimePerStamina;

        //スタミナ数を加算
        nowStaminaValue += recoveryStaminaNum;

        // スタミナが1回復するまでの残り時間を余りの時間で引く
        // 残り時間より余りの時間のほうが大きかった場合、マイナスになってしまうので、繰り上げる
        if (this.restStaminaTime >= resttime)
        {
            this.restStaminaTime -= resttime;
        }
        else
        {
            this.restStaminaTime = recoveryTimePerStamina + this.restStaminaTime - resttime;
        }

        // スタミナ上限値よりも超えた場合はマックスにする。余りの時間も0にする
        if (this.maxStaminaValue < nowStaminaValue)
        {
            nowStaminaValue = this.maxStaminaValue;
            resttime = 0;
        }
    }

    // スタミナを全回復
    public void recoverMax()
    {
        nowStaminaValue = this.maxStaminaValue;
        this.restStaminaTime = this.recoveryTimePerStamina;
    }

}