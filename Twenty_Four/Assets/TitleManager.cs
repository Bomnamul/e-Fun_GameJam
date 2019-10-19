using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public Canvas title;
    
    Animator titleAnim;

    private void Start()
    {
        UIManager.instance.titleOn = true;
        title.gameObject.SetActive(true);
        titleAnim = title.GetComponent<Animator>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && GameManager.instance.gameStatus == GameManager.state.Title)
        {

            GameManager.instance.SetGameState(GameManager.state.Ready);
            titleAnim.SetTrigger("GameStart");
            SceneMgr.instance.LoadScene(1, 1);
        }
    }
}
