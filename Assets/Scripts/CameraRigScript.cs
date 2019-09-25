using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRigScript : MonoBehaviour
{
    public GameObject tornado;
  
    void Update()
    {
        transform.position = tornado.transform.position;
    }
}
