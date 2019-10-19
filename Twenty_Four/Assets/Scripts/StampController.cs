using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class StampController : MonoBehaviour
{
    public Transform deskPos;
    public Stamp_HandController hand;

    int stampCount;
    int score;
    Animator anim;
    Transform tempPaper;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GameManager.instance.gameStatus == GameManager.state.MiniReady)
        {
            GameManager.instance.SetGameState(GameManager.state.MiniStart);
        }

        if (GameManager.instance.gameStatus == GameManager.state.MiniStart)
        {
            if (Input.GetKeyDown(KeyCode.Space) && deskPos.childCount > 0)
            {
                anim.SetTrigger("OnStamp");
                stampCount++;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && deskPos.childCount > 0)
            {
                hand.factory.RemoveUnstampedLast();
                tempPaper = deskPos.GetChild(0);
                tempPaper.parent = null;
                tempPaper.DOMoveX(-5f, 0.5f).OnComplete(() => tempPaper.gameObject.SetActive(false));

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

            if (Input.GetKeyDown(KeyCode.RightArrow) && deskPos.childCount > 0)
            {
                hand.factory.RemoveUnstampedLast();
                tempPaper = deskPos.GetChild(0);
                tempPaper.parent = null;
                tempPaper.DOMoveX(5f, 0.5f).OnComplete(() => tempPaper.gameObject.SetActive(false));

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
