using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Pixelation.Scripts;

public class PokemonManager : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(ReadyToAnim());
    }
    
    IEnumerator ReadyToAnim()
    {
        if(SceneMgr.instance.isLoadComplete)
        {
            anim.SetTrigger("Start");
        }
        else
        {
            yield return null;
        }
    }

    void StartMinigame()
    {
        //SceneMgr.instance.LoadScene(GameManager.instance.miniIndex);
        GameManager.instance.SetGameState(GameManager.state.MiniReady);
    }
}
