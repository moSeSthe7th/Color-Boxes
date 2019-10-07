using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindPhysicsScript : MonoBehaviour
{
    Rigidbody rb;

    
    public float windForce; // ForceMode Impulse la eklendigi icin deger kuculebilir
    
    public Vector3 forceVec;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (transform.position.z > 500f)
            gameObject.SetActive(false);
    }

    public void CreateWind(Transform gunTransform, Vector3 directionVec)
    {
        rb.velocity = Vector3.zero;
      
        Vector3 gunTransformVec = gunTransform.position;
        //gunTransformVec.y = initialPos.y;

        transform.position = gunTransformVec;
        transform.rotation = gunTransform.rotation;

        forceVec = (directionVec - transform.position).normalized;

        rb.AddForce(forceVec * windForce * 1.3f, ForceMode.Impulse); //Impulse force u anlik hiz degistirerek uyguluyor
    }

}