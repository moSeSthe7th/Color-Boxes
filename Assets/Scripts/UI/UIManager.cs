using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject StartingPanel;
    public GameObject settingsPanel;
    public Button settingsButton;
    public GameObject levelPassedPanel;

    public GameObject blowPanel;

    public Button backgroundButton;

    
    void Start()
    {
        settingsPanel.SetActive(false);
        levelPassedPanel.SetActive(false);
        blowPanel.SetActive(false);
        backgroundButton.gameObject.SetActive(false);

        if (!LevelData.levelData.isLevelStarted)
            StartingPanel.SetActive(true);
        else
            StartingPanel.SetActive(false);
    }

    public void SettingsButtonPressed()
    {
        if (settingsPanel.activeSelf)
        {
            settingsPanel.SetActive(false);
            backgroundButton.gameObject.SetActive(false);
        }

        else
        {
            settingsPanel.SetActive(true);
            backgroundButton.gameObject.SetActive(true);
        }
            
    }

    public void LevelPassed()
    {
        levelPassedPanel.SetActive(true);
    }

    public void OpenBlowPanel()
    {
        blowPanel.SetActive(true);
    }

    public void CloseBlowPanel()
    {
        blowPanel.SetActive(false);
    }

    public void CloseStartingPanel()
    {
        LevelData.levelData.isLevelStarted = true;
        CloseAllPanels();
        settingsButton.gameObject.SetActive(false);
        
    }

    public void CloseAllPanels()
    {
        if (StartingPanel.activeSelf)
            StartingPanel.SetActive(false);
        if (settingsPanel.activeSelf)
            settingsPanel.SetActive(false);
    }

    
}
