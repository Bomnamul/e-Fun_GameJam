using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneMgr : MonoBehaviour
{
    public static SceneMgr instance;
    public bool isLoadComplete = true;
    public Image fade;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    void Start()
    {

    }

    public void LoadScene(string sceneIndex)
    {
        // 애니메이션 이후 호출되어 씬 전환
    }

    public void LoadSceneAdditive(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Additive);
    }

    public void LoadScene(int sceneIndex)
    {
        //SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
        StartCoroutine(AsyncLoadScene(sceneIndex));
    }

    public void LoadScene(int sceneIndex, int delay)
    {
        //StartCoroutine(LoadSceneCR(sceneIndex, delay));
        StartCoroutine(AsyncLoadScene(sceneIndex, delay));
    }

    //IEnumerator LoadSceneCR(int sceneIndex, int delay)
    //{
    //    yield return new WaitForSeconds(delay);
    //    LoadScene(sceneIndex);
    //}

    IEnumerator AsyncLoadScene(int sceneIndex)
    {
        isLoadComplete = false;
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
        ao.allowSceneActivation = false;

        fade.gameObject.SetActive(true);
        fade.DOFade(1f, 1f);
        yield return new WaitForSeconds(1f);
        while (ao.progress < 0.9f)
            yield return null;
        fade.DOFade(0f, 1f).OnComplete(() => { fade.gameObject.SetActive(false); });
        ao.allowSceneActivation = true;

        isLoadComplete = true;
        UIManager.instance.SetMiniUICanvas(GameManager.instance.gameStatus, GameManager.instance.miniIndex);
        UIManager.instance.SetUICanvas(GameManager.instance.gameStatus);
    }

    IEnumerator AsyncLoadScene(int sceneIndex, int delay)
    {
        isLoadComplete = false;
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
        ao.allowSceneActivation = false;

        yield return new WaitForSeconds(delay);

        fade.gameObject.SetActive(true);
        fade.DOFade(1f, 1f);
        yield return new WaitForSeconds(1f);
        while (ao.progress < 0.9f)
            yield return null;
        fade.DOFade(0f, 1f).OnComplete(() => { fade.gameObject.SetActive(false); });
        ao.allowSceneActivation = true;

        isLoadComplete = true;
        UIManager.instance.SetMiniUICanvas(GameManager.instance.gameStatus, GameManager.instance.miniIndex);
    }
}
