using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    private UIManager uIManager;
    private void Start()
    {
        uIManager = FindObjectOfType(typeof(UIManager)) as UIManager;
    }

    public void NextLevelPressed()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        LevelData.levelData.IncreaseLevel();
    }
}
