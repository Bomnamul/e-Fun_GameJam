using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperFactory : MonoBehaviour
{
    public GameObject redPaper;
    public GameObject greenPaper;
    public GameObject bluePaper;
    public int paperCount;

    List<GameObject> redPoolList;
    List<GameObject> greenPoolList;
    List<GameObject> bluePoolList;
    List<GameObject> unsortedList;

    void Awake()
    {
        redPoolList = new List<GameObject>();
        greenPoolList = new List<GameObject>();
        bluePoolList = new List<GameObject>();
        unsortedList = new List<GameObject>();
    }

    private void Start()
    {
        while (unsortedList.Count != paperCount)
        {
            int rnd = Random.Range(0, 3);
            switch (rnd)
            {
                case 0:
                    unsortedList.Add(Instantiate(redPaper, transform));
                    break;
                case 1:
                    unsortedList.Add(Instantiate(greenPaper, transform));
                    break;
                case 2:
                    unsortedList.Add(Instantiate(bluePaper, transform));
                    break;
                default:
                    break;
            }
        }
    }
}
