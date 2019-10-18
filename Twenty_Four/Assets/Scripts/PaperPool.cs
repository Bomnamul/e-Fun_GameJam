using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperPool : MonoBehaviour
{
    public GameObject redPaper;
    public GameObject greenPaper;
    public GameObject bluePaper;
    public int poolCount;

    List<GameObject> redPoolList;
    List<GameObject> greenPoolList;
    List<GameObject> bluePoolList;
    List<GameObject> unsortedList;

    private void Awake()
    {
        redPoolList = new List<GameObject>();
        greenPoolList = new List<GameObject>();
        bluePoolList = new List<GameObject>();
    }

    private void Start() // Instantiate 하기 전에 반드시 Active Scene을 바꿔줘야 함
    {
        for (int i = 0; i < poolCount; i++)
        {
            var rtemp = Instantiate(redPaper, transform);
            rtemp.SetActive(false);
            redPoolList.Add(rtemp);
            var gtemp = Instantiate(greenPaper, transform);
            gtemp.SetActive(false);
            greenPoolList.Add(gtemp);
            var btemp = Instantiate(bluePaper, transform);
            btemp.SetActive(false);
            bluePoolList.Add(btemp);
        }
    }
}
