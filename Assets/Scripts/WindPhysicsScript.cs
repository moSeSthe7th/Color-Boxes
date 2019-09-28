﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindPhysicsScript : MonoBehaviour
{
    Rigidbody rb;
    
    public Vector3 initialPos;
    public float windForce = 20000f;
    private Vector3 initialScale;
    

    private void Start()
    {
        initialPos = transform.position;
        rb = GetComponent<Rigidbody>();
        initialScale = transform.localScale;
    }
    
    public void CreateWind(Transform gunTransform, float scale, Vector3 directionVec)
    {
        rb.velocity = Vector3.zero;
        transform.localScale = initialScale;
        
        Vector3 gunTransformVec = gunTransform.position;
        gunTransformVec.y = initialPos.y;
        
        transform.position = gunTransformVec;
        transform.rotation = gunTransform.rotation;

        Vector3 dummyScaler = transform.localScale;
        dummyScaler.x = dummyScaler.x * scale;
        transform.localScale = dummyScaler;

        Vector3 forceVec = (directionVec - transform.position).normalized;

        rb.AddForce(forceVec * windForce);
    }

}
