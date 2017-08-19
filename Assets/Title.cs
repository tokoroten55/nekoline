using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    private AsyncOperation async;
    GameObject LoadingUi;
    public Slider Slider;

    private void Start()
    {
        SaveData.Instance.kaishi = 1;
        LoadingUi = GameObject.Find("LoadPanel");
        LoadingUi.SetActive(false);
    }
    
    public void LoadNextScene()
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
}