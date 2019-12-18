using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repeater : MonoBehaviour
{
    Coroutine co_Repeat = null;

    private void Start()
    {
        if (co_Repeat != null)
            StopCoroutine(co_Repeat);

        StartCoroutine(Repeat());
    }

    IEnumerator Repeat()
    {
        yield return new WaitForSeconds(5f);
        SceneMgr.instance.LoadScene(0);
    }
}
