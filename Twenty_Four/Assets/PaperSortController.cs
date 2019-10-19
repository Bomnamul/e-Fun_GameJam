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

    int score = 0;
    bool penalty = false;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow) && !penalty)
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
                tempPaper.transform.DOShakePosition(0.5f);
                StartCoroutine(OnPenalty());
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && !penalty)
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
                tempPaper.transform.DOShakePosition(0.5f);
                StartCoroutine(OnPenalty());
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && !penalty)
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
                tempPaper.transform.DOShakePosition(0.5f);
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
