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

    private Vector3 appliedForce;
    private WindPhysicsScript windPhysicsScript;

    float windZoneTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        windZoneTimer = 0f;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Wind") && !LevelData.levelData.isBlowActive)
        {

            windPhysicsScript = other.gameObject.GetComponent<WindPhysicsScript>();
            explosionForce = windPhysicsScript.windForce;
            explosionRadius = windPhysicsScript.windForce;
            upwardsExplosionModifier = 20f;

            explosionPos = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(this.transform.position); //other.gameObject.transform.position;

            appliedForce += windPhysicsScript.forceVec;

            //appliedForce = windPhysicsScript.forceVec * 50f;
            windZoneTimer += Time.fixedDeltaTime * 5f;
            //explosionPos = other.gameObject.transform.position;
            if (!isInWindZone)
            {
                appliedForce += windPhysicsScript.forceVec * 20f; // bir defalik yuksek guc ekle
                isInWindZone = true;
            }
                
        }
    }

    private void FixedUpdate()
    {
        if (windZoneTimer > 0.05f)
        {
            windZoneTimer -= Time.fixedDeltaTime;

            if (windPhysicsScript != null)
            {
                rb.AddForceAtPosition(appliedForce, explosionPos, ForceMode.VelocityChange);
               // rb.AddForce(appliedForce, ForceMode.VelocityChange);
                //rb.AddExplosionForce(explosionForce, explosionPos, explosionRadius, upwardsExplosionModifier, ForceMode.Acceleration);
            }    
        }
        else
        {
            if (isInWindZone)
            {
                appliedForce = Vector3.zero;
                isInWindZone = false;
                windZoneTimer = 0f;
            }
        }

      /*  if(transform.position.y < LevelData.levelData.platformPos.y - 20f)
        {
            this.transform.position = LevelData.levelData.platformPos;
        }*/
        
    }

}
