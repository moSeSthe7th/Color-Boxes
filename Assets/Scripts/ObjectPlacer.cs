using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{

    private Vector3 initialPosition;
    private Rigidbody rb;
    
    void Start()
    {
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        PlaceObject();
    }

    public void PlaceObject()
    {
        if(transform.position.y < -200f)
        {
            transform.position = initialPosition;
            rb.velocity = Vector3.zero;
        }
    }
}
