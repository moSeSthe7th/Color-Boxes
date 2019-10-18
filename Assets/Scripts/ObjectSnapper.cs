using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSnapper : MonoBehaviour
{
    public float zDiffWithHole = 5f;
    bool isObjectSnappedToAHole = false;

    bool isHoleCollidersIncreased = false;
   
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "HoleCube" && !isObjectSnappedToAHole)
        {
            GoToHole(other.gameObject);
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
    }

    

    void GoToHole(GameObject hole)
    {
        if (!hole.GetComponent<HoleCubeScript>().isOccupied)
        {
            isObjectSnappedToAHole = true;
            hole.GetComponent<HoleCubeScript>().isOccupied = true;
            //Debug.Log("Occupied a hole");
            StartCoroutine(SnapObjectToThePosition(hole));
            DataScript.succesfullyOccupiedHoleCount++;
            if (!isHoleCollidersIncreased && (DataScript.succesfullyOccupiedHoleCount >= DataScript.remainingHoleColliderIncreaseThreshold))
                IncreaseSnapDistanceOfRemainingHoles();
        }
    }

    Vector3 GetSnapPosition(GameObject hole)
    {
        Vector3 snapPos = hole.transform.position;
        snapPos.z -= zDiffWithHole;
        return snapPos;
    }

    void ColorizeTheObject(GameObject hole)
    {
        //GetComponent<Renderer>().material.color = hole.GetComponent<HoleCubeScript>().holeColor;      open this if lwrp is not used
        GetComponent<Renderer>().material.SetColor("_BaseColor", hole.GetComponent<HoleCubeScript>().holeColor);
    }


    //Bu degismeli her bir obje tekrar tekrar cagiriyor bu fonksiyonu gereksiz fonksiyon cagirilmis oluyor her birinde. isHoleCollidersIncreased her birinde tutuluyor
    void IncreaseSnapDistanceOfRemainingHoles()
    {
        if (!isHoleCollidersIncreased)
        {
            Debug.Log("Snap distance increased");
            GameObject[] remainingHoles = GameObject.FindGameObjectsWithTag("HoleCube");
            foreach(GameObject remainingHole in remainingHoles)
            {
                if(!remainingHole.GetComponent<HoleCubeScript>().isOccupied)
                    remainingHole.GetComponent<SphereCollider>().radius = 17f;
            }
        }
       
    }
}
