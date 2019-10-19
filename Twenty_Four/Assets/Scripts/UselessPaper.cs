using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UselessPaper : MonoBehaviour
{
    public int count;

    private void Start()
    {
        count = Random.Range(5, 21);
    }
}
