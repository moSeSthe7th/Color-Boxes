using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //Currently just makes hole objects collider bigger if they remained under %90 of original count

    bool isIncreasedCollider;
    float increasedColliderRadius = 15f;


    int remainingHoleColliderIncreaseThreshold = 0;
    float threshold = 0.15f;

    private void Start()
    {
        isIncreasedCollider = false;
        remainingHoleColliderIncreaseThreshold = Mathf.RoundToInt(LevelData.levelData.holes.Count * threshold);

    }

    private void LateUpdate()
    {
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
        
        int i = 0;
        foreach (GameObject remainingHole in LevelData.levelData.holeCubes)
        {
            if (remainingHole.activeSelf)
                remainingHole.GetComponent<SphereCollider>().radius = increasedColliderRadius;

            i++;
        }

        Debug.Log("Changing sizes times " + i);
    }
}
