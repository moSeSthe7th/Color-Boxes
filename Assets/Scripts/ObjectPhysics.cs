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

    private WindPhysicsScript windPhysicsScript;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wind")
        {

            windPhysicsScript = other.gameObject.GetComponent<WindPhysicsScript>();
            explosionForce = windPhysicsScript.windForce * 20f;
            explosionRadius = windPhysicsScript.windForce * 20f;
            upwardsExplosionModifier =windPhysicsScript.windForce / 2000f;

            explosionPos = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(this.transform.position); //other.gameObject.transform.position;

            /*//find contact point and apply force from here
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward * -1, out hit))
            {
                explosionPos = hit.point;
            }
            //*/


            //explosionPos = other.gameObject.transform.position;
            isInWindZone = true;
        }
    }

    private void FixedUpdate()
    {
        if (isInWindZone)
        {
            rb.AddExplosionForce(explosionForce, explosionPos, explosionRadius, upwardsExplosionModifier, ForceMode.Acceleration);
            if (windPhysicsScript != null)
                rb.AddForce(windPhysicsScript.forceVec * 140 ,ForceMode.Impulse);
            isInWindZone = false;
        }
    }
}
