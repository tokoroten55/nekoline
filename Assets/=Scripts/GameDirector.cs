using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour {
    public int stage = 1;
    int stagehyouki;
    int nextstage;
    enum State
    {
        Ready, Play, GameOver, Clear
    }
    State state;

    GameObject hukidashi;
    GameObject catpointText;
    GameObject catpoint;


    GameObject ani1;
    GameObject ani2;
    GameObject ani3;

    GameObject timerText;
    GameObject nokoriText;
    GameObject StartPanelStageText1;
    GameObject StartPanelStageText2;
    GameObject StartPanelStageText3;
    GameObject timeover;
    GameObject annaiText;
    public AudioClip timeSE;

    GameObject PausePanel;
    GameObject stageclear;
    GameObject StartPanel;
    GameObject ClearPanel;
    GameObject ClearPanelStageText1;
    GameObject ClearPanelStageText2;

    GameObject ClearPanelkoban;
    GameObject LIFEPanel;
    GameObject PlayerData;
    GameObject REPanel;
    GameObject PerfectBonus;
    GameObject nakamaPanel;

    GameObject ClearPanelFast;
    GameObject ClearPanelDeath;
    public AudioClip ClearSE;

    GameObject ContinuePanel;

    GameObject infoPanel;
    GameObject infotext;
    //ロード
    private AsyncOperation async;
    GameObject LoadingUi;
    public Slider Slider;//スライダー





    //表ステージ用public
    public float timeOmote = 90f;//ゲーム時間
    public float spanOmote = 1.5f;//猫出現速度
    public int nekosuuOmote = 1;//ステージの最大猫数
    //裏ステージ用public
    public float timeUra = 60f;//ゲーム時間
    public float spanUra = 0.2f;//猫出現速度
    public int nekosuUra = 3;//ステージの最大猫数

    public GameObject kamihubuki;//紙吹雪
    public GameObject nekoPrefab;
    float span = 1.5f;
    float delta = 10;

    float time;//ゲーム時間
    float time2 = 0f;//クリアまでの時間
    int died = 0;

    int zan = 0;
    int zan2;//ステージの最大猫数
    int maxcat;
    //int zancat;

    //パーフェクトクリアチェック
    int Perfect1 = 0;
    int Perfect2 = 0;

    //ねこばん
    int koban = 0;
    //仲間
    int nakama = 0;
    AudioSource audiox;

    string textSt;
    List<string> TextST = new List<string>() {"",
       "画面にラインを引いて\nねこをゴールへ誘導しよう！",//01
        "穴に落下しないように\nうまく誘導してね！",//02
        "ラインの寿命は基本5秒間だけど\n指2本でタップすれば、すぐに崩すことが出来るよ！",//03
        "ラインを書けないパネルがあるよ！\n書けないパネルはうまくかわして誘導しよう！",//04
        "ラインが崩れる1秒前になると\nラインの色がピンクになるので注意してね！",//05
        "余分なラインを書いてしまうと\nねこの進行を妨げてしまうから慎重にね！",//06
        "爆弾に触れると、3秒後に爆発するよ！\n着火したらすぐに離れてね！",//07
        "動くブロックにのって！\nそのままゴールしよう！",//08
        "足場ラインを引くコツは、\nねこの移動速度と同じくらいがいいかも！？",//09
        "仲間が捕まっているよ\n仲間を救出してゴールへ誘導してね！",//10
        "壁面から針が飛び出してるよ\nラインでガードしてゴールを目指そう！！",//11
        "爆弾をうまく蹴飛ばして\n下の爆弾に誘爆させよう！！",//12
        "落下するブロックがあるよ\n落下箇所を見極めて攻略しよう！",//13
        "水が勢いよくながれてるね\nラインで流れをうまく変えてみて！",//14
        "\n",//15
        "\n",//16
        "\n",//17
        "\n",//18
        "\n",//19
        "\n",//20
        "\n",//21
        "\n",//22
        "\n",//23
        "\n",//24
        "\n",//25
        "\n",//26
        "\n",//27
        "\n",//28
        "\n",//29
        "\n",//30
        "\n",//31
        "\n",//32
        "\n",//33
        "\n",//34
        "\n",//35
        "\n",//36
        "\n",//37
        "\n",//38
        "\n",//39
        "\n",//40
        "\n",//41
        "\n",//42
        "\n",//43
        "\n",//44
        "\n",//45
        "\n",//46
        "\n",//47
        "\n",//48
        "\n",//49
        "\n",//50
        "\n",//51
        "\n",//52
        "\n",//53
        "\n",//54
        "\n",//55
        "\n",//56
        "\n",//57
        "\n",//58
        "\n",//59
        "\n",//60
        "\n",//61
        "\n",//62
        "\n",//63
        "\n",//64
        "\n",//65
        "\n",//66
        "\n",//67
        "\n",//68
        "\n",//69
        "\n",//70
        "\n",//71
        "\n",//72
        "\n",//73
        "\n",//74
        "\n",//75
        "\n",//76
        "\n",//77
        "\n",//78
        "\n",//79
        "\n",//80
        "\n",//81
        "\n",//82
        "\n",//83
        "\n",//84
        "\n",//85
        "\n",//86
        "\n",//87
        "\n",//88
        "\n",//89
        "\n",//90
        "\n",//91
        "\n",//92
        "\n",//93
        "\n",//94
        "\n",//95
        "\n",//96
        "\n",//97
        "\n",//98
        "\n",//99
        "\n",//100

        "\n",//sp1
        "\n",//sp2
        "\n",//sp3
        "\n",//sp4
        "\n",//sp5
        "\n",//sp6
        "\n",//sp7
        "\n",//sp8
        "\n",//sp9
        "\n",//sp10

    };




    private void Awake()
    {
        //スタートテキスト
        if (stage <= 100)
        {
            textSt = TextST[stage];
        }
        else
        {
            textSt = "スペシャルステージ";
        }

        //裏表判別
            if (SaveData.Instance.uraomote == 2)
        {//裏ステージ
            time = timeUra;
            span = spanUra;
            zan2 = nekosuUra;
            stagehyouki = stage + 100;
        }
        else
        {//表ステージ
            time = timeOmote;
            span = spanOmote;
            zan2 = nekosuuOmote;
            stagehyouki = stage;
        }

        nextstage = stage + 1;





        //ライフパネル
        this.LIFEPanel = GameObject.Find("LIFEPanel");
        this.PlayerData = GameObject.Find("PlayerData");
        
        this.REPanel = GameObject.Find("REPanel");//ライフ不足時のパネル
        REPanel.SetActive(false);
        this.timerText = GameObject.Find("Time");
        this.nokoriText = GameObject.Find("nokori");
        this.timeover = GameObject.Find("TimeOver");
        timeover.SetActive(false);
        this.stageclear = GameObject.Find("StageClear");
        stageclear.SetActive(false);

        this.StartPanelStageText1 = GameObject.Find("StartPanel/TextStage");
        this.StartPanelStageText1.GetComponent<Text>().text = "Stage" + stagehyouki;

        this.StartPanelStageText2 = GameObject.Find("StartPanel/kihonImage/Text");
        this.StartPanelStageText2.GetComponent<Text>().text = "×" + zan2 + "　　制限時間" + time + "秒";

        this.StartPanelStageText3 = GameObject.Find("StartPanel/kakutoku/Text");
        this.StartPanelStageText3.GetComponent<Text>().text = "1、ステージクリア \n2、制限時間"+(time-10f)+"秒以内にクリア\n3、ノーミスでクリア";
        this.annaiText = GameObject.Find("annaiText");
        this.annaiText.GetComponent<Text>().text = textSt;

        //ポーズ
        this.PausePanel = GameObject.Find("PausePanel");
        PausePanel.SetActive(false);

        //スタートパネル
        this.StartPanel = GameObject.Find("StartPanel");
        StartPanel.SetActive(true);

        //クリアパネル
        this.ClearPanel = GameObject.Find("ClearPanel");
        //仲間
        this.nakamaPanel = GameObject.Find("nakamaPanel");
        nakamaPanel.SetActive(false);

        this.StartPanelStageText1 = GameObject.Find("ClearPanel/TextStage");
        this.StartPanelStageText1.GetComponent<Text>().text = "Stage" + stagehyouki + " Clear";

        this.ClearPanelFast = GameObject.Find("ClearPanel/Image2/Text");
        this.ClearPanelFast.GetComponent<Text>().text = "Fast clear";
        this.ClearPanelDeath = GameObject.Find("ClearPanel/Image3/Text");
        this.ClearPanelDeath.GetComponent<Text>().text = "No Died";
        this.ClearPanelkoban = GameObject.Find("ClearPanel/COINText");
        this.PerfectBonus = GameObject.Find("ClearPanel/Perfect Bonus");
        PerfectBonus.SetActive(false);

        this.ani1 = GameObject.Find("ClearPanel/Image1");
        ani1.SetActive(false);
        this.ani2 = GameObject.Find("ClearPanel/Image2");
        ani2.SetActive(false);
        this.ani3 = GameObject.Find("ClearPanel/Image3");
        ani3.SetActive(false);

        ClearPanel.SetActive(false);

        //コンティニューパネル
        this.ContinuePanel = GameObject.Find("ContinuePanel");
        ContinuePanel.SetActive(false);

        //インフォパネル
        this.infotext = GameObject.Find("infoPanel/Text");
        this.infoPanel = GameObject.Find("infoPanel");
        infoPanel.SetActive(false);

        //ロードパネル
        LoadingUi = GameObject.Find("LoadPanel");
        LoadingUi.SetActive(false);

        //キャットポイント
        catpoint = GameObject.Find("CATpoint");
        //hukidashi = GameObject.Find("CATpoint/hukidashi");
        catpointText = GameObject.Find("CATpoint/hukidashi/deru");
        
    }




    void Start()
    {
        maxcat = zan2;
        this.catpointText.GetComponent<TextMesh>().text = "×"+maxcat;


        this.timerText.GetComponent<Text>().text = this.time.ToString("F0");
        this.nokoriText.GetComponent<TextMesh>().text = zan + "/" + zan2;

        //音楽設定
        if (GameObject.Find("SoundManager") != null)
        {
            float st = stagehyouki % 20;
            if (st==0)
            {
                SoundManager.Instance.PlayBGM(1);
                

            } else
            {
                SoundManager.Instance.PlayBGM(0);
            }
        }
    }

    void Update() {

        switch (state)
        {
            case State.Ready:
                Time.timeScale = 0;
                //開始前　クリックでスタート
                if (Input.GetButtonUp("Fire1"))
                {
                    if (LIFEPanel) { LIFEPanel.GetComponent<Panel>().panel(); }
                    StartPanel.SetActive(false);
                    Time.timeScale = 1;
                    state = State.Play;

                }
                break;


            case State.Play:

                //猫生成
                this.delta += Time.deltaTime;
                this.time2 += Time.deltaTime;
                if (this.delta > this.span && maxcat >= 1)
                {
                    this.delta = 0;
                    maxcat--;
                    this.catpointText.GetComponent<TextMesh>().text = "×" + maxcat;
                    GameObject go = Instantiate(nekoPrefab,catpoint.transform.position, Quaternion.identity) as GameObject;
                    go.transform.position = transform.position;

                    if (maxcat == 0) { catpoint.SetActive(false); }


                }
                //時間管理
                this.time -= Time.deltaTime;
                if (time <= 0)
                {
                    Timeover();
                }
                else
                {
                    this.timerText.GetComponent<Text>().text = this.time.ToString("F0");
                }
                //state = State.GameOver;
                break;


            case State.GameOver:

                break;


            case State.Clear:

                break;
        }


    }

    //猫残数確認・クリア判定
    public void nokori()
    {
        zan++;
        this.nokoriText.GetComponent<TextMesh>().text = zan + "/" + zan2;
        if (zan >= zan2)
        {
            Stageclear();
        }
    }
    public void onakama()
    {
        nakama++;
    }

    //猫残数復活
    public void hukkatu()
    {
        this.delta = -0.5f;
        died++;
        maxcat++;
        catpoint.SetActive(true);
        this.catpointText.GetComponent<TextMesh>().text = "×" + maxcat;
    }
    //小判get
    public void kobanget()
    {
        koban++;
    }


    //タイムオーバー
    void Timeover()
    {
        timeover.SetActive(true);
        AudioSource.PlayClipAtPoint(timeSE, transform.position);
        Invoke("conStage", 4.0f);
        state = State.GameOver;
    }



    //シーン切り替え
    void Stageclear()
    {
        state = State.Clear;
        stageclear.SetActive(true);
        AudioSource.PlayClipAtPoint(ClearSE, transform.position);
        GameObject go = Instantiate(kamihubuki) as GameObject;
        go.transform.position = new Vector3(0, -5, 0);
        //ステージ進捗更新



        SaveData.Instance.stage1[stagehyouki] = 1;

        if (10 <= time)
        {
            SaveData.Instance.stage2[stagehyouki] = 1;
            Perfect1 = 1;
        }
        if (died == 0)
        {
            SaveData.Instance.stage3[stagehyouki] = 1;
            Perfect2 = 1;
        }
        if (Perfect1 == 1 && Perfect2 == 1)
        {
            lifeplus();
        }
        SaveData.Instance.Necoin += koban;
        SaveData.Instance.Save();


        Invoke("select", 2.0f);
    }

    //クリア後セレクト
    void select()
    {
        if (nakama >= 1)
        {
            nakamaPanel.SetActive(true);
        }
        Invoke("select1", 2.0f);
    }
    void select1() { 
        //クリアパネル出現
        if (LIFEPanel) LIFEPanel.GetComponent<Panel>().panel2();
        ClearPanel.SetActive(true);
        nakamaPanel.SetActive(false);
        Invoke("select2", 1.0f);
    }
    void select2() { 
        ani1.SetActive(true);
        this.ClearPanelkoban.GetComponent<Text>().text = "GET ×" + koban;
        this.StartPanelStageText2 = GameObject.Find("ClearPanel/Text");
        this.StartPanelStageText2.GetComponent<Text>().text = "Clear Time " + time2.ToString("f2");
        //ボタン演出
        Invoke("button2", 1.0f);
    }
    void button2()
    {
        PlayerData.GetComponent<PlayerData>().LIFEPoint();
        PlayerData.GetComponent<PlayerData>().point();
        PlayerData.GetComponent<PlayerData>().coinkan();
        if (10 >= time)
        {
            this.ClearPanelFast.GetComponent<Text>().text = "<color=#ff0000>Not fast</color>";
        }
        ani2.SetActive(true);
        Invoke("button3", 1.0f);
    }

    void button3()
    {

        if (died > 0)
        {
            this.ClearPanelDeath.GetComponent<Text>().text = "<color=#ff0000><B>" + died + "</B> Died</color>";
        }
        ani3.SetActive(true);
        Invoke("button4", 1.0f);
    }
    void button4()
    {
    if (Perfect1 == 1 && Perfect2 == 1)
    {
            this.PerfectBonus.GetComponent<Text>().text = "Perfect Bonus\nLIFE +<size=150>1</size>";
            PerfectBonus.SetActive(true);
        }
    }




    //ステージクリアのライフ処理
    void lifeplus()
    {
        SaveData.Instance.Life++;
        if (SaveData.Instance.Life >= SaveData.Instance.MaxLife)
        {
            SaveData.Instance.restStaminaTime = 300;
        }
        return;
    }



    //リトライ
    void conStage()
    {
        LIFEPanel.GetComponent<Panel>().panel2();
        ContinuePanel.SetActive(true);
    }

    //LIFEが足りないパネルを
    public void closepanel()
    {
        REPanel.SetActive(false);//閉じる
    }
    public void openpanel()
    {
        REPanel.SetActive(true);//開く
    }


    //インフォパネル
    public void Info01() {
        this.infotext.GetComponent<Text>().text = "ライフ回復したよ♪";
        infoPanel.SetActive(true);
    }
    public void Info02()
    {
        this.infotext.GetComponent<Text>().text = "猫缶が足りません|дﾟ)";
        infoPanel.SetActive(true);
    }

    //ポーズボタン
    public void onClick()
    {
       // if (GameObject.Find("SoundManager") != null) { } else { SoundManager.Instance.PlaySE(2); }
        //Time.timeScale=0の場合停止する
        if (Time.timeScale == 0)
        {
            LIFEPanel.GetComponent<Panel>().panel();
            Time.timeScale = 1;
            PausePanel.SetActive(false);
            
        }
        else
        {
            LIFEPanel.GetComponent<Panel>().panel2();
            Time.timeScale = 0;
            PausePanel.SetActive(true);
        }
    }

    //次のステージへ
    public void Stagegogo()
    {
        if (nextstage >= 61) nextstage = 1;//最終ステージチェック
        Invoke("stagego", 0.1f);
    }
    void stagego()
    {
        if (SaveData.Instance.Life < 1)
        {
            openpanel();
        }
        else
        {
            SaveData.Instance.Life--;
            PlayerData.GetComponent<PlayerData>().LIFEPoint();
            SaveData.Instance.Save();
            LoadNextScene();
        }
    }
    void LoadNextScene()
    {
        LoadingUi.SetActive(true);
        StartCoroutine(LoadScene2());
    }

    IEnumerator LoadScene2()
    {
        async = SceneManager.LoadSceneAsync("Stage" + nextstage);
        while (!async.isDone)
        {
            Slider.value = async.progress;
            yield return null;
        }
    }


    //ステージセレクトへ
    public void selectgogo()
    {
        Time.timeScale = 1;
        selectScene();
    }
    public void selectScene()
    {
        LoadingUi.SetActive(true);
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        async = SceneManager.LoadSceneAsync("_STAGE SELECT");
        while (!async.isDone)
        {
            Slider.value = async.progress;
            yield return null;
        }
    }

    //リトライ
    public void Retry()
    {
       // if (GameObject.Find("SoundManager") != null) { } else { SoundManager.Instance.PlaySE(2); }
        if (SaveData.Instance.Life < 1)
        {
            openpanel();
        }
        else
        {
            SaveData.Instance.Life--;
            PlayerData.GetComponent<PlayerData>().LIFEPoint();
            SaveData.Instance.Save();
            RetryScene();
        }
    }
    void RetryScene()
    {
        LoadingUi.SetActive(true);
        StartCoroutine(RetryScene2());
    }

    IEnumerator RetryScene2()
    {
        async = SceneManager.LoadSceneAsync("Stage" + stage);
        while (!async.isDone)
        {
            Slider.value = async.progress;
            yield return null;
        }
    }
}
