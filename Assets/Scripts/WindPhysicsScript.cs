using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindPhysicsScript : MonoBehaviour
{
    Rigidbody rb;
    
    public Vector3 initialPos;
    public float windForce;

    private void Start()
    {
        initialPos = transform.position;
        rb = GetComponent<Rigidbody>();
    }
    
    public void CreateWind(Transform gunTransform, float force, Vector3 directionVec)
    {
        rb.velocity = Vector3.zero;
        windForce = force;

        Vector3 gunTransformVec = gunTransform.position;
        gunTransformVec.y = initialPos.y;
        
        transform.position = gunTransformVec;
        transform.rotation = gunTransform.rotation;

        Vector3 forceVec = (directionVec - transform.position).normalized;

        rb.AddForce(forceVec * force);
    }

}
