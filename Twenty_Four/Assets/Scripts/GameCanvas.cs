using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvas : MonoBehaviour
{
    public GameObject readyPanel;
    public GameObject gamePanel;
    public Text timerTxt;

    MinigameController miniCtrl;

    public void SetReadyPanel(bool condition)
    {
        if (condition)
        {
            readyPanel.SetActive(true);
            gamePanel.SetActive(false);
        }
        else
        {
            readyPanel.SetActive(false);
            gamePanel.SetActive(true);
        }
    }

    private void OnEnable()
    {
        if(timerTxt != null)
        {
            UIManager.instance.timerTxt = timerTxt;
        }
        
    }
}
