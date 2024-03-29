﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PaperSortController : MinigameController
{
    public PaperFactory factory;
    public Transform redPoint;
    public Transform greenPoint;
    public Transform bluePoint;

    float remaintime = 60;
    int score = 0;
    bool penalty = false;
    bool gameover = false;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GameManager.instance.gameStatus == GameManager.state.MiniReady && SceneMgr.instance.isLoadComplete)
        {
            GameManager.instance.SetGameState(GameManager.state.MiniStart);
        }

        if (GameManager.instance.gameStatus == GameManager.state.MiniStart)
        {
            remaintime -= Time.deltaTime;
            UIManager.instance.timerTxt.text = "Time : " + ((int)remaintime).ToString();

            if (factory.unsortedList.Count == 0 || remaintime <= 0) //gameover
            {
                gameover = true;
                GameManager.instance.AddScore(score);
                if (GameManager.instance.miniQueue.Count != 0 && GameManager.instance.gameStatus != GameManager.state.MiniReady)
                {
                    GameManager.instance.SetGameState(GameManager.state.MiniReady);
                }
                else
                    GameManager.instance.SetGameState(GameManager.state.Result);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) && !penalty && !gameover)
            {
                var tempPaper = factory.unsortedList[factory.unsortedList.Count - 1];

                if (tempPaper.transform.name == "rPaper(Clone)")    // 정답
                {
                    tempPaper.transform.DOMove(redPoint.position, 0.5f).OnComplete(() => tempPaper.SetActive(false));
                    score += 100;
                    factory.redPoolList.Add(tempPaper);
                    factory.unsortedList.Remove(tempPaper);
                    factory.transform.position -= new Vector3(0f, factory.paperDistance, 0f);
                }
                else    // 오답
                {
                    GameManager.instance.GetDamage(10);
                    anim.SetTrigger("OnHurt");
                    tempPaper.transform.DOShakePosition(duration: 0.5f, strength: 0.3f);
                    StartCoroutine(OnPenalty());
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && !penalty && !gameover)
            {
                var tempPaper = factory.unsortedList[factory.unsortedList.Count - 1];

                if (tempPaper.transform.name == "gPaper(Clone)")
                {
                    tempPaper.transform.DOMove(greenPoint.position, 0.5f).OnComplete(() => tempPaper.SetActive(false));
                    score += 100;
                    factory.redPoolList.Add(tempPaper);
                    factory.unsortedList.Remove(tempPaper);
                    factory.transform.position -= new Vector3(0f, factory.paperDistance, 0f);
                }
                else
                {
                    GameManager.instance.GetDamage(10);
                    anim.SetTrigger("OnHurt");
                    tempPaper.transform.DOShakePosition(duration: 0.5f, strength: 0.3f);
                    StartCoroutine(OnPenalty());
                }
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) && !penalty && !gameover)
            {
                var tempPaper = factory.unsortedList[factory.unsortedList.Count - 1];

                if (tempPaper.transform.name == "bPaper(Clone)")
                {
                    tempPaper.transform.DOMove(bluePoint.position, 0.5f).OnComplete(() => tempPaper.SetActive(false));
                    score += 100;
                    factory.redPoolList.Add(tempPaper);
                    factory.unsortedList.Remove(tempPaper);
                    factory.transform.position -= new Vector3(0f, factory.paperDistance, 0f);
                }
                else
                {
                    GameManager.instance.GetDamage(10);
                    anim.SetTrigger("OnHurt");
                    tempPaper.transform.DOShakePosition(duration: 0.5f, strength: 0.3f);
                    StartCoroutine(OnPenalty());
                }
            }
        }
    }

    IEnumerator OnPenalty()
    {
        penalty = true;
        yield return new WaitForSeconds(1f);
        penalty = false;
    }
}
