using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovementScript : MonoBehaviour
{
    Vector3 cloudPositioner;
   
    void Start()
    {
        StartCoroutine(moveTheClouds());
    }

    IEnumerator moveTheClouds()
    {
        while (true)
        {

            cloudPositioner = transform.position;
            cloudPositioner.x += 0.3f;

            if (cloudPositioner.x >= 950f)
                cloudPositioner.x = -1400f;
           
            transform.position = cloudPositioner;
            yield return new WaitForEndOfFrame();
        }
    }

   
}
