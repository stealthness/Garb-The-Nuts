using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScorePanelText;

    public GameObject highScorePanel;
    public GameObject instructionPanel;
    public GameObject menuPanel;


    public void UpdateScoreAndTimeText(float time, int score)
    {
        timeText.text = string.Format("Time : {0} ", (int)time);
        scoreText.text = string.Format("Score : {0}", score);
    }

    public void SetText(Panels panel, string messageText)
    {
        if (panel == Panels.HighScore)
        {
            highScorePanelText.text = messageText;
        }
        else
        {
            Debug.LogWarning("panel not found");
        }
        
    }


    internal void ActivatePanel(Panels panel)
    {
        if (panel == Panels.HighScore)
        {
            instructionPanel.SetActive(false);
            highScorePanel.SetActive(true);
            menuPanel.SetActive(false);
        }else if (panel == Panels.Menu)
        {
            menuPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("panel not found");
        }

    }
}

public enum Panels
{
    HighScore, Instructions, Menu
}
