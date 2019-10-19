using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShredderMachine : MonoBehaviour
{
    public int paperCount;
    public UselessPaper paper;
    public Queue<UselessPaper> papers;
    public List<UselessPaper> tempList;

    private void Awake()
    {
        papers = new Queue<UselessPaper>();
    }

    private void Start()
    {
        while (papers.Count != paperCount)
        {
            papers.Enqueue(Instantiate(paper, transform));
        }
    }
}
