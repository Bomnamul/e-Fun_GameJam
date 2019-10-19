using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using UnityEngine.UI;

public class ShredderController : MonoBehaviour
{
    public ShredderMachine shredder;

    Animator anim;
    List<UselessPaper> currentPaper;
    CinemachineImpulseSource impulse;
    int score = 100;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        impulse = GetComponent<CinemachineImpulseSource>();
        currentPaper = new List<UselessPaper>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GameManager.instance.gameStatus == GameManager.state.MiniReady)
        {
            GameManager.instance.SetGameState(GameManager.state.MiniStart);
        }

        if (GameManager.instance.gameStatus == GameManager.state.MiniStart)
        {
            if (Input.GetKeyDown(KeyCode.Space) && currentPaper.Count == 0)
            {
                anim.SetBool("OnShred", true);
                currentPaper.Add(shredder.papers.Dequeue());
                currentPaper[0].transform.DOMove(transform.position, 0.2f);
                UIManager.instance.canvasList[2].GetComponent<GameCanvas>().gamePanel.GetComponentInChildren<Text>().text = currentPaper[0].count.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.Space) && currentPaper.Count != 0)
            {
                Debug.Log("Freezing");
                score -= 10;
                // Freezing
            }

            if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) && currentPaper.Count != 0)
            {
                currentPaper[0].count--;
                impulse.GenerateImpulse();
                UIManager.instance.canvasList[2].GetComponent<GameCanvas>().gamePanel.GetComponentInChildren<Text>().text = currentPaper[0].count.ToString();
            }
            else if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) && currentPaper.Count == 0)
            {
                Debug.Log("Freezing");
                score -= 10;
            }

            if (currentPaper.Count != 0 && currentPaper[0].count == 0)
            {
                currentPaper[0].gameObject.SetActive(false);
                shredder.tempList.Add(currentPaper[0]);
                currentPaper.Clear();
                anim.SetBool("OnShred", false);
            }

            if (shredder.papers.Count == 0 && currentPaper.Count == 0)
            {
                Debug.Log("Shredding End");
                GameManager.instance.AddScore(score);
                if (GameManager.instance.miniQueue.Count != 0)
                    GameManager.instance.SetGameState(GameManager.state.MiniReady);
                else
                    GameManager.instance.SetGameState(GameManager.state.Result);
            }
        }
    }
}
