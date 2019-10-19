using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public List<GameObject> canvasList;
    public Canvas healthCanvas;
    public Image healthRemain;
    public Text timerTxt;
    public bool titleOn;
    public Text scoreTxt;

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

    private void Update()
    {
        if (healthCanvas.gameObject.activeSelf)
        {
            SetUIScore();
        }
    }

    public void SetUICanvas(GameManager.state state)
    {
        foreach (var canvas in canvasList)
        {
            if (canvas.tag != "Title")
                canvas.SetActive(false);
        }

        switch (state) // 스테이트마다 따로 ui로 보여줘야 할 것들 수정해야 함
        {
            case GameManager.state.Title:
                canvasList[0].SetActive(true);
                break;
            case GameManager.state.Ready:
                SetUIHealthbar(false);
                canvasList[1].SetActive(true);
                canvasList[1].GetComponent<GameCanvas>().SetReadyPanel(true);
                break;
            case GameManager.state.Run:
                SetUIHealthbar(true);
                canvasList[1].SetActive(true);
                canvasList[1].GetComponent<GameCanvas>().SetReadyPanel(false);
                break;
            case GameManager.state.MiniReady:
                break;
            case GameManager.state.MiniStart:
                break;
            case GameManager.state.Win:
                canvasList[3].SetActive(true);
                break;
            case GameManager.state.Lose:
                canvasList[3].SetActive(true);
                break;
            default:
                break;
        }
    }

    public void SetMiniUICanvas(GameManager.state state, int index)
    {
        foreach (var canvas in canvasList)
        {
            if (canvas.tag != "Title")
                canvas.SetActive(false);
        }

        switch (state)
        {
            case GameManager.state.MiniReady:
                SetUIHealthbar(false);
                switch (index)
                {
                    case 2:
                        canvasList[2].SetActive(true);
                        canvasList[2].GetComponent<GameCanvas>().SetReadyPanel(true);
                        break;
                    case 3:
                        canvasList[3].SetActive(true);
                        canvasList[3].GetComponent<GameCanvas>().SetReadyPanel(true);
                        break;
                    case 4:
                        canvasList[4].SetActive(true);
                        canvasList[4].GetComponent<GameCanvas>().SetReadyPanel(true);
                        break;
                    default:
                        break;
                }
                break;
            case GameManager.state.MiniStart:
                SetUIHealthbar(true);
                switch (index)
                {
                    case 2:
                        canvasList[2].SetActive(true);
                        canvasList[2].GetComponent<GameCanvas>().SetReadyPanel(false);
                        break;
                    case 3:
                        canvasList[3].SetActive(true);
                        canvasList[3].GetComponent<GameCanvas>().SetReadyPanel(false);
                        break;
                    case 4:
                        canvasList[4].SetActive(true);
                        canvasList[4].GetComponent<GameCanvas>().SetReadyPanel(false);
                        break;
                    default:
                        break;
                }
                break;
            case GameManager.state.MiniEnd:
                break;
            default:
                break;
        }
    }

    public void SetUIHealthbar(bool condition)
    {
        healthCanvas.gameObject.SetActive(condition);
    }

    public void SetUIHealthRemain(float remain)
    {
        healthRemain.fillAmount = remain / 100;
    }

    public void SetUIScore()
    {
        healthCanvas.GetComponentInChildren<Text>().text = "업무 평가\n" + GameManager.instance.TotalScore();
    }
}
