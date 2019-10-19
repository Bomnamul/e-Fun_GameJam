using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public enum state { Title, Ready, Run, MiniReady, MiniStart, MiniEnd, Result, Win, Lose }
    public state gameStatus;
    public Queue<int> miniQueue;

    List<int> scoreList;
    int miniIndex;
    bool useRoulette = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        scoreList = new List<int>();
        miniQueue = new Queue<int>();
    }

    void Start()
    {
        SetGameState(state.Ready);
    }

    public void SetGameState(state state)
    {
        UIManager.instance.SetUICanvas(state);

        switch (state)
        {
            case state.Title:
                gameStatus = state.Title;
                Debug.Log("State : Title");
                break;
            case state.Ready:
                gameStatus = state.Ready;
                UIManager.instance.SetUICanvas(gameStatus);
                Debug.Log("State : Ready");
                break;
            case state.Run:
                gameStatus = state.Run;
                UIManager.instance.SetUICanvas(gameStatus);
                Debug.Log("State : Run");
                break;
            case state.MiniReady:
                gameStatus = state.MiniReady;
                if (!useRoulette)
                {
                    miniRoulette();
                    useRoulette = true;
                }

                miniIndex = miniQueue.Dequeue();
                // UI로 랜덤 3개 보여줄 것
                SceneMgr.instance.LoadScene(miniIndex, 3);
                UIManager.instance.SetMiniUICanvas(gameStatus, miniIndex);
                Debug.Log("State : MiniReady");
                break;
            case state.MiniStart:
                gameStatus = state.MiniStart;
                UIManager.instance.SetMiniUICanvas(gameStatus, miniIndex);
                Debug.Log("State : MiniStart");
                break;
            case state.Result:
                gameStatus = state.Result;
                SceneMgr.instance.LoadScene("Result");
                Debug.Log("State : Result");
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

    void miniRoulette()
    {
        for (int i = 0; i < 3; i++)
        {
            int rnd = Random.Range(2, 5);
            miniQueue.Enqueue(rnd);
        }
    }
}
