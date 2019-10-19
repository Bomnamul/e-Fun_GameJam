using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;

public class StampController : MinigameController
{
    public Transform deskPos;
    public Stamp_HandController hand;
    public ParticleSystem stampFX;

    float remaintime = 50f;
    int stampCount;
    int score;
    Animator anim;
    Transform tempPaper;
    bool gameover = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GameManager.instance.gameStatus == GameManager.state.MiniReady && SceneMgr.instance.isLoadComplete)
        {
            GameManager.instance.SetGameState(GameManager.state.MiniStart);
            hand.GameStart();
        }

        if (GameManager.instance.gameStatus == GameManager.state.MiniStart)
        {
            remaintime -= Time.deltaTime;
            UIManager.instance.timerTxt.text = "Time : " + ((int)remaintime).ToString();
            UIManager.instance.canvasList[4].GetComponent<GameCanvas>().gamePanel.transform.GetChild(1).GetComponent<Text>().text = hand.factory.unstampedList.Count.ToString();

            if (hand.factory.unstampedList.Count == 0 || remaintime <= 0)
            {
                gameover = true;
                GameManager.instance.AddScore(score);
                if (GameManager.instance.miniQueue.Count != 0 && GameManager.instance.gameStatus != GameManager.state.MiniReady)
                    GameManager.instance.SetGameState(GameManager.state.MiniReady);
                else
                    GameManager.instance.SetGameState(GameManager.state.Result);
            }

            if (Input.GetKeyDown(KeyCode.Space) && deskPos.childCount > 0 && !gameover)
            {
                stampFX.Play();
                anim.SetTrigger("OnStamp");
                stampCount++;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && deskPos.childCount > 0 && !gameover)
            {
                hand.factory.RemoveUnstampedLast();
                tempPaper = deskPos.GetChild(0);
                tempPaper.parent = null;
                tempPaper.DOMoveX(-5f, 0.5f).OnComplete(() => tempPaper.gameObject.SetActive(false));
                tempPaper.DOScale(0.5f, 0.4f);

                if (tempPaper.transform.name == "1StampPaper(Clone)" && stampCount == 1)
                {
                    score += 100;
                    hand.factory.stampedList.Add(tempPaper.gameObject);
                    stampCount = 0;
                }
                else
                {
                    score -= 50;
                    StartCoroutine(Miss());
                    hand.factory.missStampedList.Add(tempPaper.gameObject);
                    stampCount = 0;
                }
                StartCoroutine(SetNewPaper());
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) && deskPos.childCount > 0 && !gameover)
            {
                hand.factory.RemoveUnstampedLast();
                tempPaper = deskPos.GetChild(0);
                tempPaper.parent = null;
                tempPaper.DOMoveX(5f, 0.5f).OnComplete(() => tempPaper.gameObject.SetActive(false));
                tempPaper.DOScale(0.5f, 0.4f);

                if (tempPaper.transform.name == "2StampPaper(Clone)" && stampCount == 2)
                {
                    score += 100;
                    hand.factory.stampedList.Add(tempPaper.gameObject);
                    stampCount = 0;
                }
                else
                {
                    score -= 50;
                    StartCoroutine(Miss());
                    hand.factory.missStampedList.Add(tempPaper.gameObject);
                    stampCount = 0;
                }
                StartCoroutine(SetNewPaper());
            }
        }
    }

    IEnumerator Miss()
    {
        print("Miss");
        GameManager.instance.GetDamage(10);
        if (anim.GetBool("OnMiss"))
        {
            yield return null;
        }
        else
        {
            anim.SetBool("OnMiss", true);
            yield return new WaitForSeconds(2f);
            anim.SetBool("OnMiss", false);
        }
    }

    IEnumerator SetNewPaper()
    {
        yield return null;
        hand.anim.SetTrigger("Setpaper");
    }
}
