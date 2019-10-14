using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    List<Rigidbody> thorwableRbs;
    Vector3 platformPos; //Defined a new gameobject which holds the value of altigens correct position

    void Start()
    {
        GameObject[] thorwableObjs = GameObject.FindGameObjectsWithTag("ThrownObject");
        
        //Calculate platform position
        GameObject platform = GameObject.FindGameObjectWithTag("Platform");
        platformPos = platform.transform.position;

        thorwableRbs = new List<Rigidbody>(thorwableObjs.Length);

        foreach (GameObject to in thorwableObjs)
        {
            thorwableRbs.Add(to.GetComponent<Rigidbody>());
        }
    }

    private void LateUpdate()
    {
        PlaceObject();
    }

    public void PlaceObject()
    {
        foreach(Rigidbody rb in thorwableRbs)
        {
            Vector3 distVec = rb.transform.position - platformPos;
            float dist = Vector3.Distance(rb.transform.position, platformPos);
            //Debug.Log(dist);
            if (dist > 200f)
            {
                rb.AddForce(-distVec * 2f);

            }
        }
    }
}
