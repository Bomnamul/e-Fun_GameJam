using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamp_HandController : MonoBehaviour
{
    public Transform paperPos;
    public Transform deskPos;
    public Animator anim;
    public StampFactory factory;

    Transform stampPaper;
    

    void Start()
    {
        anim = GetComponent<Animator>();
        factory = GetComponentInParent<StampFactory>();
    }

    public void GameStart()
    {
        anim.SetTrigger("Setpaper");
    }


    void SetPaperOnhand()
    {
        stampPaper = factory.unstampedList[factory.unstampedList.Count - 1].transform;
        stampPaper.SetParent(paperPos);
        stampPaper.transform.localPosition = Vector3.zero;
    }

    void SetPaperOndesk()
    {
        stampPaper.SetParent(deskPos);
        stampPaper.transform.localPosition = Vector3.zero;
    }
}
