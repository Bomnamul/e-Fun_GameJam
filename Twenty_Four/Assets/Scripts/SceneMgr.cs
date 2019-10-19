using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour
{
    public static SceneMgr instance;

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
        SceneManager.LoadScene("In_Game", LoadSceneMode.Single);
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
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }

    public void LoadScene(int sceneIndex, int delay)
    {
        StartCoroutine(LoadSceneCR(sceneIndex, delay));
    }

    IEnumerator LoadSceneCR(int sceneIndex, int delay)
    {
        yield return new WaitForSeconds(delay);
        LoadScene(sceneIndex);
    }
}
