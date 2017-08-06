using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class ScrollButton : MonoBehaviour
{
    [SerializeField]
    private GameObject[] btnPref; //ボタンプレハブ 

    GameObject fad;

    //ボタン表示数
    const int BUTTON_COUNT = 100;
    int Tno = 0;
    GameObject Star01;

    GameObject CharPanel;
    GameObject ShopPanel;
    GameObject MAINPanel;
    GameObject MAIN2Panel;
    GameObject SpecialPanel;
    GameObject ReviewPanel;

    

    public GameObject PlayerData;
    public GameObject Fade;
    public RectTransform[] content = new RectTransform[3];
    
    void Start()
    {

        SoundManager.Instance.PlayBGM(0);
        //Content取得(ボタンを並べる場所)
        content[0] = GameObject.Find("MAINPanel/ScrollView/Viewport/Content").GetComponent<RectTransform>();
        content[1] = GameObject.Find("MAIN2Panel/ScrollView/Viewport/Content").GetComponent<RectTransform>();
        //RectTransform content = GameObject.Find("Content").GetComponent<RectTransform>();
        //Contentの高さ決定
        //(ボタンの高さ+ボタン同士の間隔)*ボタン数
        //float btnSpace = content.GetComponent<VerticalLayoutGroup>().spacing;
        //float btnHeight = btnPref.GetComponent<LayoutElement>().preferredHeight;
        //content.sizeDelta = new Vector2(0, (btnHeight + btnSpace) * BUTTON_COUNT);

        //content.sizeDelta = new Vector2(15, 15);

        //this.Star01 = GameObject.Find("Star_1");
        //Star01.SetActive(false);


        this.PlayerData = GameObject.Find("PlayerData");

        int value1 = SaveData.Instance.flag;
        
        //ノーマルとハード
        for (int y = 0; y <= 1; y++) { 
            for (int i = 1; i <= BUTTON_COUNT; i++)
        {
            int no = i;Tno++;

            //データ確認

            int value2 = SaveData.Instance.stageList[Tno];
            int st01 = SaveData.Instance.stage1[Tno];
            int st02 = SaveData.Instance.stage2[Tno];
            int st03 = SaveData.Instance.stage3[Tno];
                //ボタン生成
                if (value1 < value2)
                {
                    GameObject btn1 = (GameObject)Instantiate(btnPref[0]); btn1.transform.SetParent(content[y], false);
                    btn1.transform.GetComponentInChildren<Text>().text = "<size=100>" + Tno+ "</size>\n point" + value2.ToString();

                }
                else
                {

                    if (st01 == 0 && st03 == 0 && st03 == 0)
                    {
                        GameObject btn = (GameObject)Instantiate(btnPref[5]); btn.transform.SetParent(content[y], false);
                        //ボタンのテキスト変更
                        btn.transform.GetComponentInChildren<Text>().text = "" + Tno.ToString();
                        btn.transform.GetComponent<Button>().onClick.AddListener(() => OnClick(no));
                    }
                    else
                    {
                        if (st01 == 1 && st02 == 0 && st03 == 0)
                        {
                            GameObject btn = (GameObject)Instantiate(btnPref[1]); btn.transform.SetParent(content[y], false);
                            btn.transform.GetComponentInChildren<Text>().text = "" + Tno.ToString();
                            btn.transform.GetComponent<Button>().onClick.AddListener(() => OnClick(no));
                        }
                        else
                        {
                            if (st01 == 1 && st02 == 1 && st03 == 0)
                            {
                                GameObject btn = (GameObject)Instantiate(btnPref[2]); btn.transform.SetParent(content[y], false);
                                btn.transform.GetComponentInChildren<Text>().text = "" + Tno.ToString();
                                btn.transform.GetComponent<Button>().onClick.AddListener(() => OnClick(no));
                            }
                            else
                            {
                                if (st01 == 1 && st02 == 0 && st03 == 1)
                                {
                                    GameObject btn = (GameObject)Instantiate(btnPref[3]); btn.transform.SetParent(content[y], false);
                                    btn.transform.GetComponentInChildren<Text>().text = "" + Tno.ToString();
                                    btn.transform.GetComponent<Button>().onClick.AddListener(() => OnClick(no));
                                }
                                else
                                {
                                    GameObject btn = (GameObject)Instantiate(btnPref[4]); btn.transform.SetParent(content[y], false);
                                    btn.transform.GetComponentInChildren<Text>().text = "" + Tno.ToString();
                                    btn.transform.GetComponent<Button>().onClick.AddListener(() => OnClick(no));
                                }
                            }
                        }
                    }
                }
            }
        }

        //パネル表示
        this.CharPanel = GameObject.Find("CharPanel");
        CharPanel.SetActive(false);
        this.ShopPanel = GameObject.Find("ShopPanel");
        ShopPanel.SetActive(false);
        this.MAINPanel = GameObject.Find("MAINPanel");
        MAINPanel.SetActive(true);
        this.MAIN2Panel = GameObject.Find("MAIN2Panel");
        MAIN2Panel.SetActive(false);
        this.SpecialPanel = GameObject.Find("SpecialPanel");
        SpecialPanel.SetActive(false);
        this.ReviewPanel = GameObject.Find("ReviewPanel");
        ReviewPanel.SetActive(false);

        
    }


    public void OnClick(int no)
    {
        SoundManager.Instance.PlaySE(1);

        if (SaveData.Instance.Life < 1)
        {
            return;
        }
        else
        {
            SoundManager.Instance.PlayVoice(2);
            SaveData.Instance.Life--;
            PlayerData.GetComponent<PlayerData>().LIFEPoint();
            SaveData.Instance.Save();
        }
            //フェード
        this.Fade = GameObject.Find("FadeCanvas");
        Fade.GetComponent<Fade>().NextStage();
        StartCoroutine(DelayMethod(2f, () =>
        {
            SceneManager.LoadScene("Stage" + no);
        }));

    }
    private IEnumerator DelayMethod(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }

    public void MAINButton()
    {
        SoundManager.Instance.PlaySE(1);
        CharPanel.SetActive(false);
        ShopPanel.SetActive(false);
        MAINPanel.SetActive(true);
        MAIN2Panel.SetActive(false);
        SpecialPanel.SetActive(false);
        ReviewPanel.SetActive(false);
    }
    public void CharButton()
    {
        SoundManager.Instance.PlaySE(1);
        CharPanel.SetActive(true);
        ShopPanel.SetActive(false);
        MAINPanel.SetActive(false);
        MAIN2Panel.SetActive(false);
        SpecialPanel.SetActive(false);
        ReviewPanel.SetActive(false);
    }
    public void ShopButton()
    {
        SoundManager.Instance.PlaySE(1);
        CharPanel.SetActive(false);
        ShopPanel.SetActive(true);
        MAINPanel.SetActive(false);
        MAIN2Panel.SetActive(false);
        SpecialPanel.SetActive(false);
        ReviewPanel.SetActive(false);
    }
    public void SpecialButton()
    {
        SoundManager.Instance.PlaySE(1);
        CharPanel.SetActive(false);
        ShopPanel.SetActive(false);
        MAINPanel.SetActive(false);
        MAIN2Panel.SetActive(false);
        SpecialPanel.SetActive(true);
        ReviewPanel.SetActive(false);
    }
    public void ReviewButton()
    {
        SoundManager.Instance.PlaySE(1);
        CharPanel.SetActive(false);
        ShopPanel.SetActive(false);
        MAINPanel.SetActive(false);
        MAIN2Panel.SetActive(false);
        SpecialPanel.SetActive(false);
        ReviewPanel.SetActive(true);
    }
    public void hard()
    {
        SoundManager.Instance.PlaySE(1);
        CharPanel.SetActive(false);
        ShopPanel.SetActive(false);
        MAINPanel.SetActive(false);
        MAIN2Panel.SetActive(true);
        SpecialPanel.SetActive(false);
        ReviewPanel.SetActive(false);
    }

}
