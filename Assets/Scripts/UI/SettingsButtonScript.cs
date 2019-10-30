using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButtonScript : MonoBehaviour
{
    private UIManager uIManager;

    private void Start()
    {
        uIManager = FindObjectOfType(typeof(UIManager)) as UIManager;
    }

    public void SettingsButtonPressed()
    {
        uIManager.SettingsButtonPressed();
    }
}
