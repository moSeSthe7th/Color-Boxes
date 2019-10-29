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

    public Vector3 cubeParentBlowPos;
    UIManager uIManager;

    bool isBlowCoroutineStarted = false;
    
    int remainingHoleColliderIncreaseThreshold = 0;
    float threshold = 0.15f;

    private void Start()
    {
        uIManager = FindObjectOfType(typeof(UIManager)) as UIManager;
        vibrationHandler = new VibrationHandler();

        isIncreasedCollider = false;
        remainingHoleColliderIncreaseThreshold = Mathf.RoundToInt(LevelData.levelData.holes.Count * threshold);
    }

    private void LateUpdate()
    {
        //If there are any vibration request call vibration. Eger cok fazla titresim olursa burada bi limit belirleyebilir
     /*   if(LevelData.levelData.vibrationQue.Count > 0)
        {
            LevelData.levelData.vibrationQue.Dequeue();
            vibrationHandler.vibrate(LevelData.levelData.vibrationStyle);
        }*/

        if (!isIncreasedCollider && shouldIncreaseCollSize())
        {
            isIncreasedCollider = true;
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
            cubeParent.transform.position = Vector3.MoveTowards(cubeParent.transform.position, LevelData.levelData.CubeParentBlowPosition, 5f);
            yield return new WaitForEndOfFrame();
        }

        cubeParent.GetComponent<BoxCollider>().enabled = true;
        LevelData.levelData.isBlowActive = true;
        
        StopCoroutine(AllCubesArePlaced());
        
    }

}
