using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSnapper : MonoBehaviour
{
    public float zDiffWithHole = 1f;
    public bool isObjectSnappedToAHole = false;

    bool isHoleCollidersIncreased = false;
   
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "HoleCube" && !isObjectSnappedToAHole)
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
            LevelData.levelData.holeCount -= 1;

            StartCoroutine(SnapObjectToThePosition(hole));
        }
    }


    IEnumerator SnapObjectToThePosition(GameObject hole)
    {
        Vector3 posToSnap = GetSnapPosition(hole);
        GetComponent<Rigidbody>().isKinematic = true;
        while (Vector3.Distance(posToSnap, transform.position) > 2f)
        {
            transform.position = Vector3.MoveTowards(transform.position, posToSnap, 5f);
            yield return new WaitForSecondsRealtime(0.005f);
        }

        transform.position = posToSnap;
        transform.rotation = Quaternion.identity;
        ColorizeTheObject(hole);
        hole.SetActive(false);

        //After snapping finishes request a vibration
        LevelData.levelData.vibrationQue.Enqueue((int)Random.value);

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
    }
}
