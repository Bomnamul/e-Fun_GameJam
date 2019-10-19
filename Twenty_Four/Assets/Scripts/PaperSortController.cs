using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PaperSortController : MonoBehaviour
{
    public PaperFactory factory;
    public Transform redPoint;
    public Transform greenPoint;
    public Transform bluePoint;

    float remaintime = 30;
    int score = 0;
    bool penalty = false;
    bool gameover = false;

    void Update()
    {
        remaintime -= Time.deltaTime;
        print((int)remaintime);

        if(factory.unsortedList.Count == 0 || remaintime <= 0)
        {
            gameover = true;
            GameManager.instance.AddScore(score);
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow) && !penalty && !gameover)
        {
            var tempPaper = factory.unsortedList[factory.unsortedList.Count - 1];
            
            if (tempPaper.transform.name == "rPaper(Clone)")
            {
                tempPaper.transform.DOMove(redPoint.position, 0.5f).OnComplete(() => tempPaper.SetActive(false));
                score += 100;
                factory.redPoolList.Add(tempPaper);
                factory.unsortedList.Remove(tempPaper);
                factory.transform.position -= new Vector3(0f, factory.paperDistance, 0f);
            }
            else
            {
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
                tempPaper.transform.DOShakePosition(duration: 0.5f, strength: 0.3f);
                StartCoroutine(OnPenalty());
            }
        }
    }

    IEnumerator OnPenalty()
    {
        penalty = true;
        yield return new WaitForSeconds(1.5f);
        penalty = false;
    }
}
