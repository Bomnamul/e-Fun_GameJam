using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class ShredderController : MonoBehaviour
{
    public ShredderMachine shredder;

    List<UselessPaper> currentPaper;
    CinemachineImpulseSource impulse;
    int score = 100;

    private void Awake()
    {
        impulse = GetComponent<CinemachineImpulseSource>();
        currentPaper = new List<UselessPaper>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentPaper.Count == 0)
        {
            currentPaper.Add(shredder.papers.Dequeue());
            currentPaper[0].transform.DOMove(transform.position, 0.2f);
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
        }

        if (shredder.papers.Count == 0 && currentPaper.Count == 0)
        {
            Debug.Log("Shredding End");
            GameManager.instance.AddScore(score);
            if (GameManager.instance.miniQueue.Count != 0)
                SceneMgr.instance.LoadScene(GameManager.instance.miniQueue.Dequeue());
            else
                GameManager.instance.SetGameState(GameManager.state.Result);
        }
    }
}
