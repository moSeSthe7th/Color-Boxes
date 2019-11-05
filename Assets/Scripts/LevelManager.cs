using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //Currently just makes hole objects collider bigger if they remained under %90 of original count
    //Added vibration handler

    VibrationHandler vibrationHandler;

    bool isIncreasedCollider;
    bool isSecondIncreasedCollider;
    float increasedColliderRadius = 20f;
    float secondIncreasedColliderRadius = 30f;

    public Vector3 cubeParentBlowPos;
    UIManager uIManager;

    bool isBlowCoroutineStarted = false;
    
    int remainingHoleColliderIncreaseThreshold = 0;
    int secondRemainingHoleColliderIncreaseThreshold = 0;
    float threshold = 0.15f;
    float secondThreshold = 0.02f;

    private void Start()
    {
        uIManager = FindObjectOfType(typeof(UIManager)) as UIManager;
        vibrationHandler = new VibrationHandler();

        isIncreasedCollider = false;
        isSecondIncreasedCollider = false;
        remainingHoleColliderIncreaseThreshold = Mathf.RoundToInt(LevelData.levelData.holes.Count * threshold);
        secondRemainingHoleColliderIncreaseThreshold = Mathf.RoundToInt(LevelData.levelData.holes.Count * secondThreshold);
    }

    private void LateUpdate()
    {
        //If there are any vibration request call vibration. Eger cok fazla titresim olursa burada bi limit belirleyebilir
     /*   if(LevelData.levelData.vibrationQue.Count > 0)
        {
            LevelData.levelData.vibrationQue.Dequeue();
            vibrationHandler.vibrate(LevelData.levelData.vibrationStyle);
        }*/

        if (shouldIncreaseCollSize())
        {
            Debug.Log("Increasing collider sizes");
            IncreaseSnapperCollidersSize();
        }

        if (AreAllCubesPlaced() && !isBlowCoroutineStarted)
        {
            isBlowCoroutineStarted = true;
            StartCoroutine(AllCubesArePlaced());
        }

        if(LevelData.levelData.isBlown)
        {
            uIManager.CloseBlowPanel();
            uIManager.LevelPassed();
        }
            
    }

    bool shouldIncreaseCollSize()
    {
        if (remainingHoleColliderIncreaseThreshold >= LevelData.levelData.holeCount && !isIncreasedCollider)
        {
            isIncreasedCollider = true;
            return true;
        }
        else if (secondRemainingHoleColliderIncreaseThreshold >= LevelData.levelData.holeCount && !isSecondIncreasedCollider)
        {
            isSecondIncreasedCollider = true;
            return true;
        }
        else
            return false;
           
    }

    void IncreaseSnapperCollidersSize()
    {
        float radius = (isSecondIncreasedCollider) ? secondIncreasedColliderRadius : increasedColliderRadius;

        foreach (GameObject remainingHole in LevelData.levelData.holeCubes)
        {
            if (remainingHole.activeSelf)
            {
                remainingHole.GetComponent<SphereCollider>().radius = radius;
                remainingHole.GetComponent<HoleCubeScript>().StartReflecting();
            }  
        }
        
    }

    bool AreAllCubesPlaced()
    {
        if (LevelData.levelData.holeCount == 0)
            return true;
        else
            return false;
    }

    IEnumerator AllCubesArePlaced()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        uIManager.OpenBlowPanel();
        GameObject cubeParent = GameObject.FindWithTag("CubeParent");

        GameObject[] colliders = GameObject.FindGameObjectsWithTag("Collider");

        foreach (GameObject coll in colliders)
        {
            coll.SetActive(false);
        }

        while (Vector3.Distance(cubeParent.transform.position, LevelData.levelData.CubeParentBlowPosition) > 1f)
        {
            cubeParent.transform.position = Vector3.MoveTowards(cubeParent.transform.position, LevelData.levelData.CubeParentBlowPosition, 10f);
            yield return new WaitForSecondsRealtime(0.008f);
        }
        yield return new WaitForSecondsRealtime(0.1f);
        cubeParent.GetComponent<BoxCollider>().enabled = true;
        LevelData.levelData.isBlowActive = true;
        
        StopCoroutine(AllCubesArePlaced());
        
    }

}
