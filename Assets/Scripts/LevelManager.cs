using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //Currently just makes hole objects collider bigger if they remained under %90 of original count
    //Added vibration handler

    VibrationHandler vibrationHandler;

    bool isIncreasedCollider;
    float increasedColliderRadius = 15f;


    int remainingHoleColliderIncreaseThreshold = 0;
    float threshold = 0.15f;

    private void Start()
    {
        vibrationHandler = new VibrationHandler();

        isIncreasedCollider = false;
        remainingHoleColliderIncreaseThreshold = Mathf.RoundToInt(LevelData.levelData.holes.Count * threshold);
    }

    private void LateUpdate()
    {
        //If there are any vibration request call vibration. Eger cok fazla titresim olursa burada bi limit belirleyebilir
        if(LevelData.levelData.vibrationQue.Count > 0)
        {
            LevelData.levelData.vibrationQue.Dequeue();
            vibrationHandler.vibrate();
        }

        if(!isIncreasedCollider && shouldIncreaseCollSize())
        {
            isIncreasedCollider = true;
            IncreaseSnapperCollidersSize();
        }
    }

    bool shouldIncreaseCollSize()
    {
        if (remainingHoleColliderIncreaseThreshold >= LevelData.levelData.holeCount)
            return true;
        else
            return false;
    }

    void IncreaseSnapperCollidersSize()
    {
        foreach (GameObject remainingHole in LevelData.levelData.holeCubes)
        {
            if (remainingHole.activeSelf)
            {
                remainingHole.GetComponent<SphereCollider>().radius = increasedColliderRadius;
                StartCoroutine(remainingHole.GetComponent<HoleCubeScript>().HoleCubeReflector());
            }
                
        }
        
    }
}
