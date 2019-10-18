using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public enum state { Title, Ready, Run, Mini, Win, Lose }
    public state gameStatus;

    List<int> scoreList;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        scoreList = new List<int>();
    }

    void Start()
    {
        SetGameState(state.Ready);
    }

    void Update()
    {
        
    }

    public void SetGameState(state state)
    {
        switch (state)
        {
            case state.Title:
                gameStatus = state.Title;
                Debug.Log("State : Title");
                break;
            case state.Ready:
                gameStatus = state.Ready;
                Debug.Log("State : Ready");
                break;
            case state.Run:
                gameStatus = state.Run;
                Debug.Log("State : Run");
                break;
            case state.Mini:
                gameStatus = state.Mini;
                Debug.Log("State : Mini");
                break;
            case state.Win:
                gameStatus = state.Win;
                Debug.Log("State : Win");
                break;
            case state.Lose:
                gameStatus = state.Lose;
                Debug.Log("State : Lose");
                break;
            default:
                break;
        }
    }

    public void AddScore(int score)
    {
        scoreList.Add(score);
    }

    public void ClearScore()
    {
        scoreList.Clear();
    }
}
