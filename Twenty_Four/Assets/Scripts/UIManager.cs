using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public List<GameObject> canvasList;
    public Image healthRemain;

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

    public void SetUICanvas(GameManager.state state)
    {
        foreach (var canvas in canvasList)
        {
            canvas.SetActive(false);
        }

        switch (state) // 스테이트마다 따로 ui로 보여줘야 할 것들 수정해야 함
        {
            case GameManager.state.Title:
                canvasList[0].SetActive(true);
                break;
            case GameManager.state.Ready:
                canvasList[1].SetActive(true);
                break;
            case GameManager.state.Run:
                canvasList[1].SetActive(true);
                break;
            case GameManager.state.Mini:
                canvasList[2].SetActive(true);
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

    public void SetUIHealthRemain(float remain)
    {
        healthRemain.fillAmount = remain / 100;
    }
}
