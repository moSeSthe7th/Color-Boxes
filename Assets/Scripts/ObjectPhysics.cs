using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPhysics : MonoBehaviour
{

    public bool isInWindZone = false;

    private Rigidbody rb;

    public float explosionForce;
    public float upwardsExplosionModifier;
    public float explosionRadius;

    private Vector3 explosionPos;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

 

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Wind")
        {
            explosionForce = other.gameObject.GetComponent<WindPhysicsScript>().windForce / 2;
            explosionRadius = other.gameObject.GetComponent<WindPhysicsScript>().windForce / 2;
            upwardsExplosionModifier = other.gameObject.GetComponent<WindPhysicsScript>().windForce / 400;

            explosionPos = other.gameObject.transform.position;
            isInWindZone = true;
        }
    }

    private void FixedUpdate()
    {
        if (isInWindZone)
        {
            rb.AddExplosionForce(explosionForce, explosionPos, explosionRadius, upwardsExplosionModifier, ForceMode.Acceleration);
            isInWindZone = false;
        }
    }
}
