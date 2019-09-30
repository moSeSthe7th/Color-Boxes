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

    Vector3 directionVec;

    private Vector3 explosionPos;

    private float WindForce;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        WindForce = GameObject.FindGameObjectWithTag("Wind").GetComponent<WindPhysicsScript>().windForce; //Zaten oyun icinde degeri degismiyor diye burada aliniyor.
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Wind")
        {
            explosionForce = WindForce / 50f;
            explosionRadius = WindForce;
            upwardsExplosionModifier = WindForce / Random.Range(10f,15f);

            Vector3 tmpRot = other.transform.rotation.eulerAngles;
            //Debug.Log(tmpRot);
            directionVec = this.transform.position - other.transform.position; //new Vector3(0f, upwardsExplosionModifier, explosionForce);
            directionVec.z *= explosionForce;
            //Debug.Log("Direction vec is : " + directionVec + " Explosion radius is : " + explosionRadius + " upwardsExplosionModifier is : " + upwardsExplosionModifier);

            explosionPos = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(this.transform.position); //other.gameObject.transform.position;
            isInWindZone = true;
        }
    }

    private void FixedUpdate()
    {
        if (isInWindZone)
        {
            //rb.AddExplosionForce(explosionForce, explosionPos, explosionRadius, upwardsExplosionModifier, ForceMode.Impulse); //Explosion Force gucu ileri dogru degil de etrafa sacarak dagitiyor objeler bundan yukari dogru yonlere dagiliyor bundan dagiliyor. 
            rb.AddForce(directionVec,ForceMode.Impulse);
            //rb.AddForceAtPosition(directionVec, explosionPos, ForceMode.Impulse);
            isInWindZone = false;
        }
    }
}
