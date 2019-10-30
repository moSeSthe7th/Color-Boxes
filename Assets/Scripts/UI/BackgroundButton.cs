using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundButton : MonoBehaviour
{
    UIManager uIManager;

    void Start()
    {
        uIManager = FindObjectOfType(typeof(UIManager)) as UIManager;
    }

    public void BackgroundButtonPressed()
    {
        uIManager.CloseAllPanels();
    }
}
