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

    public ParticleSystem levelPassedParticleSystem;

    private bool isLevelPassed;

    public Slider cubeCounterSlider;

    
    void Start()
    {
        isLevelPassed = false;
        settingsPanel.SetActive(false);
        levelPassedPanel.SetActive(false);
        blowPanel.SetActive(false);
        backgroundButton.gameObject.SetActive(false);

        cubeCounterSlider.maxValue = LevelData.levelData.holeCount;

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
        if(!isLevelPassed)
            StartCoroutine(LevelPassedEnum());
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

    public IEnumerator LevelPassedEnum()
    {
        isLevelPassed = true;

        yield return new WaitForSecondsRealtime(1f);
        levelPassedParticleSystem.gameObject.SetActive(true);
        levelPassedParticleSystem.Play();
        yield return new WaitForSecondsRealtime(0.7f);
        levelPassedPanel.SetActive(true);
        StopCoroutine(LevelPassedEnum());
    }

    public void IncreaseCubeCounterSlider()
    {
        cubeCounterSlider.value += 1;
    }
}
