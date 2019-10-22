using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject settingsPanel;
    public Button settingsButton;
    public GameObject levelPassedPanel;

    
    void Start()
    {
        settingsPanel.SetActive(false);
        levelPassedPanel.SetActive(false);
    }

    
    void Update()
    {
        
    }

    public void OpenSettingsPanel()
    {
        settingsPanel.SetActive(true);
    }
    
    public void CloseSettingsPanel()
    {
        settingsPanel.SetActive(false);
    }

    public void LevelPassed()
    {
        levelPassedPanel.SetActive(true);
    }
}
