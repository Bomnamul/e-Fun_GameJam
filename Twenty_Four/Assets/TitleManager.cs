using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    private void Start()
    {
        UIManager.instance.titleOn = true;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && GameManager.instance.gameStatus == GameManager.state.Title)
        {

            GameManager.instance.SetGameState(GameManager.state.Ready);
            SceneMgr.instance.LoadScene(1);
        }
    }
}
