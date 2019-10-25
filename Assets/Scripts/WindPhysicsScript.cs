﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindPhysicsScript : MonoBehaviour
{
    Rigidbody rb;
    public float windForce; // ForceMode Impulse la eklendigi icin deger kuculebilir. su anda 200 editor de setlenmis
    
    public Vector3 forceVec;
    private Vector3 initialScale;

    

    private void Awake()
    {
        initialScale = transform.localScale ;
        transform.localScale = initialScale;
        rb = GetComponent<Rigidbody>();
    }


    private void LateUpdate()
    {
        if (transform.position.z > 500f)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Vector3 dummyScaler = transform.localScale;
            dummyScaler += Vector3.one * 1.5f;
            transform.localScale = dummyScaler;
        }


    }

    public void CreateWind(Transform gunTransform, Vector3 directionVec,float engineHeat)
    {
        transform.localScale = initialScale * engineHeat;
        rb.velocity = Vector3.zero;
      
        Vector3 gunTransformVec = gunTransform.position;
        //gunTransformVec.y = initialPos.y;
        //gunTransformVec.y += 25f;
        //gunTransformVec.z += 25f;

        transform.position = gunTransformVec;
        transform.rotation = gunTransform.rotation;

        forceVec = (directionVec - transform.position).normalized;

        rb.AddForce(forceVec * windForce, ForceMode.Impulse); //Impulse force u anlik hiz degistirerek uyguluyor
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if(other.gameObject.tag == "ThrownObject" && LevelData.levelData.isBlowActive && !LevelData.levelData.isBlown)
        {
            Time.timeScale = 5f;
            LevelData.levelData.isBlown = true;
            GameObject[] thrownObjects = GameObject.FindGameObjectsWithTag("ThrownObject");
            foreach (GameObject thrownObject in thrownObjects)
            {
                thrownObject.GetComponent<Rigidbody>().isKinematic = false;
                thrownObject.GetComponent<Rigidbody>().AddExplosionForce(42000f, transform.position, 100000f, 4000f);
            }
        }
    }
}