using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperFactory : MonoBehaviour
{
    public GameObject redPaper;
    public GameObject greenPaper;
    public GameObject bluePaper;
    public int paperCount;
    public float paperDistance;

    public List<GameObject> redPoolList;
    public List<GameObject> greenPoolList;
    public List<GameObject> bluePoolList;
    public List<GameObject> unsortedList;

    Vector3 spawnPoint;
    int paperSpriteOrder = 0;

    void Awake()
    {
        redPoolList = new List<GameObject>();
        greenPoolList = new List<GameObject>();
        bluePoolList = new List<GameObject>();
        unsortedList = new List<GameObject>();
        paperDistance = 0.5f;
        spawnPoint = new Vector3(0f, paperCount - paperDistance*paperCount, 0f);
    }

    private void Start()
    {
        while (unsortedList.Count != paperCount)
        {
            int rnd = Random.Range(0, 3);
            switch (rnd)
            {
                case 0:
                    unsortedList.Add(Instantiate(redPaper, transform.position + spawnPoint, Quaternion.identity, transform));
                    unsortedList[unsortedList.Count - 1].GetComponent<SpriteRenderer>().sortingOrder = paperSpriteOrder++;
                    spawnPoint.y -= paperDistance;
                    break;
                case 1:
                    unsortedList.Add(Instantiate(greenPaper, transform.position + spawnPoint, Quaternion.identity, transform));
                    unsortedList[unsortedList.Count - 1].GetComponent<SpriteRenderer>().sortingOrder = paperSpriteOrder++;
                    spawnPoint.y -= paperDistance;
                    break;
                case 2:
                    unsortedList.Add(Instantiate(bluePaper, transform.position + spawnPoint, Quaternion.identity, transform));
                    unsortedList[unsortedList.Count - 1].GetComponent<SpriteRenderer>().sortingOrder = paperSpriteOrder++;
                    spawnPoint.y -= paperDistance;
                    break;
                default:
                    break;
            }
        }
    }
}
