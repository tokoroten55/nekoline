using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour {
    public int stage = 1;
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

    GameObject timeover;
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


    public GameObject kamihubuki;//紙吹雪
    public GameObject nekoPrefab;
    public float span = 1.5f;
    float delta = 10;

    public float time = 30f;//ゲーム時間

    public float time2 = 0f;
    int died = 0;

    int zan = 0;
    public int zan2 = 3;//ステージの最大猫数
    int maxcat;
    //int zancat;

    //パーフェクトクリアチェック
    int Perfect1 = 0;
    int Perfect2 = 0;

    //ねこばん
    int koban = 0;



    private void Awake()
    {
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
        this.StartPanelStageText1.GetComponent<Text>().text = "Stage" + stage;

        this.StartPanelStageText2 = GameObject.Find("StartPanel/Text");
        this.StartPanelStageText2.GetComponent<Text>().text = "Time limit  " + time + "\n Required quantity" + zan2 + " ";

        //ポーズ
        this.PausePanel = GameObject.Find("PausePanel");
        PausePanel.SetActive(false);

        //スタートパネル
        this.StartPanel = GameObject.Find("StartPanel");
        StartPanel.SetActive(true);

        //クリアパネル
        this.ClearPanel = GameObject.Find("ClearPanel");


        this.StartPanelStageText1 = GameObject.Find("ClearPanel/TextStage");
        this.StartPanelStageText1.GetComponent<Text>().text = "Stage" + stage + " Clear";

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
            if (stage == 20)
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



        SaveData.Instance.stage1[stage] = 1;

        if (10 <= time)
        {
            SaveData.Instance.stage2[stage] = 1;
            Perfect1 = 1;
        }
        if (died == 0)
        {
            SaveData.Instance.stage3[stage] = 1;
            Perfect2 = 1;
        }
        if (Perfect1 == 1 && Perfect2 == 1)
        {
            lifeplus();
        }
        SaveData.Instance.Necoin += koban;
        SaveData.Instance.Save();


        Invoke("select", 3.0f);
    }

    //クリア後セレクト
    void select()
    {
        //クリアパネル出現
        if (LIFEPanel) LIFEPanel.GetComponent<Panel>().panel2();
        ClearPanel.SetActive(true);
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
            this.ClearPanelFast.GetComponent<Text>().text = "✖   <color=#000000>Not fast</color>";
        }
        ani2.SetActive(true);
        Invoke("button3", 1.0f);
    }

    void button3()
    {

        if (died > 0)
        {
            this.ClearPanelDeath.GetComponent<Text>().text = "✖   <color=#000000><B>" + died + "</B> Died</color>";
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
        if (nextstage > 20) nextstage = 1;//最終ステージチェック
        Invoke("stagego", 1f);
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
