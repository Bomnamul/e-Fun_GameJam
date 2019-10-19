using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    public GameObject readyPanel;
    public GameObject gamePanel;

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
}
