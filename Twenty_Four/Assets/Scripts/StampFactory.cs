using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampFactory : MonoBehaviour
{
    public GameObject oneStampPaper;
    public GameObject twoStampPaper;
    public int paperCount;
    public float paperDistance; 

    public List<GameObject> stampedList;
    public List<GameObject> missStampedList;
    public List<GameObject> unstampedList;

    Vector3 spawnPoint;

    private void Awake()
    {
        stampedList = new List<GameObject>();
        missStampedList = new List<GameObject>();
        unstampedList = new List<GameObject>();
        //spawnPoint = new Vector3(0f, paperCount + paperDistance * paperCount, 0f);
    }

    void Start()
    {
        while (unstampedList.Count != paperCount)
        {
            int rnd = Random.Range(0, 2);
            switch (rnd)
            {
                case 0:
                    unstampedList.Add(Instantiate(oneStampPaper, transform.position, Quaternion.Euler(180f, 0f, 0f), transform));
                    //unstampedList[unstampedList.Count - 1].GetComponent<SpriteRenderer>().sortingOrder = paperSpriteOrder++;
                    //spawnPoint.y -= paperDistance;
                    break;
                case 1:
                    unstampedList.Add(Instantiate(twoStampPaper, transform.position, Quaternion.Euler(180f, 0f, 0f), transform));
                    //unstampedList[unstampedList.Count - 1].GetComponent<SpriteRenderer>().sortingOrder = paperSpriteOrder++;
                    //spawnPoint.y -= paperDistance;
                    break;                
                default:
                    break;
            }
        }
    }

    public void RemoveUnstampedLast()
    {
        unstampedList.Remove(unstampedList[unstampedList.Count - 1]);
    }
}
