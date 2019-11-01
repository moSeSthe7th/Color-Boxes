using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitiliazer : MonoBehaviour
{
    CameraSizeHandler camSizeHandler;
    void Start()
    {
        DontDestroyOnLoad(this);

        QualitySettings.vSyncCount = 1;
        Time.timeScale = 1f;

        camSizeHandler = new CameraSizeHandler();
        camSizeHandler.SetCameraFieldOfView(Camera.main);
    }


    
}
