using System.Collections;
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
        else //if(transform.localScale.x < initialScale.x * 1f)
        {
            Vector3 dummyScaler = transform.localScale;
            dummyScaler += Vector3.one * 2f;
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

}