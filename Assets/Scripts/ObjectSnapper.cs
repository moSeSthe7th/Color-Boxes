using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSnapper : MonoBehaviour
{
    public float zDiffWithHole = 6f;
    public bool isObjectSnappedToAHole = false;

    private UIManager uIManager;

    private void Start()
    {
        uIManager = FindObjectOfType(typeof(UIManager)) as UIManager;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("HoleCube") && !isObjectSnappedToAHole)
        {
            GoToHole(other.gameObject);
        }
    }

    void GoToHole(GameObject hole)
    {
        if (!hole.GetComponent<HoleCubeScript>().isOccupied)
        {
            isObjectSnappedToAHole = true;
            GetComponent<Collider>().enabled = false;
            hole.GetComponent<HoleCubeScript>().isOccupied = true;
            
            StartCoroutine(SnapObjectToThePosition(hole));
        }
    }


    IEnumerator SnapObjectToThePosition(GameObject hole)
    {
        Vector3 posToSnap = GetSnapPosition(hole);

        Rigidbody cubeRb = GetComponent<Rigidbody>();
        Vector3 rotSpd = cubeRb.angularVelocity;
        cubeRb.isKinematic = true;

        float distance = Vector3.Distance(posToSnap, transform.position);

        while (distance > 2f)
        {
            transform.position = Vector3.MoveTowards(transform.position, posToSnap, Mathf.Sqrt(distance));
            transform.Rotate(rotSpd);

            distance = Vector3.Distance(posToSnap, transform.position);

            yield return new WaitForSecondsRealtime(Time.fixedDeltaTime);
        }

        transform.position = posToSnap;
        transform.rotation = Quaternion.identity;
        ColorizeTheObject(hole);
        hole.SetActive(false);
        LevelData.levelData.holeCount -= 1;
        uIManager.IncreaseCubeCounterSlider();
       

        //After snapping finishes request a vibration
        //LevelData.levelData.vibrationQue.Enqueue((int)Random.value);
        if(LevelData.levelData.isVibrationActive == 1)
            LevelData.levelData.SimpleVibration();

        StopCoroutine(SnapObjectToThePosition(hole));
    }

    Vector3 GetSnapPosition(GameObject hole)
    {
        Vector3 snapPos = hole.transform.position;
        snapPos.z -= zDiffWithHole;
        return snapPos;
    }


    void ColorizeTheObject(GameObject hole)
    {
        GetComponent<Renderer>().material.SetColor("_BaseColor", hole.GetComponent<HoleCubeScript>().holeColor);
        GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(hole.GetComponent<HoleCubeScript>().holeColor.r, 
            hole.GetComponent<HoleCubeScript>().holeColor.g, 
            hole.GetComponent<HoleCubeScript>().holeColor.b));
    }
}
