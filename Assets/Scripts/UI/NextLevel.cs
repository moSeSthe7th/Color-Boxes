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
        LevelData.levelData.LevelPassed();
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
