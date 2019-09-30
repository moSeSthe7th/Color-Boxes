using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindPhysicsScript : MonoBehaviour
{
    Rigidbody rb;
    
    public Vector3 initialPos;
    public float windForce; // ForceMode Impulse la eklendigi icin deger kuculebilir
    private Vector3 initialScale;

    public Vector3 forceVec;

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

        forceVec = (directionVec - transform.position).normalized;

        rb.AddForce(forceVec * windForce * 1.3f ,ForceMode.Impulse); //Impulse force u anlik hiz degistirerek uyguluyor
    }

}
