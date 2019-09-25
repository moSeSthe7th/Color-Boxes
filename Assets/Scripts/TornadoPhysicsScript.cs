using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoPhysicsScript : MonoBehaviour
{
    public float overlapSphereRadius;

    public float explosionForce;
    public float explosionRadius;
    public float upwardsExplosionModifier;


    private void Update()
    {
        CreateExplosion();
    }

    void CreateExplosion()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, overlapSphereRadius);

        foreach (Collider nearbyObject in colliders)
        {
            Debug.Log("nearby object found :  " + nearbyObject.tag);

            float distance = (nearbyObject.gameObject.transform.position - transform.position).sqrMagnitude;

            Rigidbody rigidbody = nearbyObject.GetComponent<Rigidbody>();

            if (rigidbody != null && nearbyObject.gameObject.tag == "ThrownObject" && distance != 0)
            {
                rigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius, upwardsExplosionModifier / distance);
            }

        }
    }
}
