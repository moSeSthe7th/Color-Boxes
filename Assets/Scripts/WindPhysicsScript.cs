using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindPhysicsScript : MonoBehaviour
{
    Rigidbody rb;

    
    public float windForce; // ForceMode Impulse la eklendigi icin deger kuculebilir
    
    public Vector3 forceVec;
    private Vector3 initialScale;

    private bool isScalingActive;

    private void Start()
    {
        initialScale = transform.localScale * 20;
        transform.localScale = initialScale;
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        transform.localScale = initialScale;
        if(rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    private void Update()
    {
        if (transform.position.z > 500f)
        {
            gameObject.SetActive(false);
            isScalingActive = false;
        }
        else
        {
            Vector3 dummyScaler = transform.localScale;
            dummyScaler += Vector3.one * 2f;
            transform.localScale = dummyScaler;
        }

       /* Vector3 dummyScaler = transform.localScale;
        dummyScaler += Vector3.one * 2f;
        transform.localScale = dummyScaler;*/

    }

    public void CreateWind(Transform gunTransform, Vector3 directionVec)
    {
        isScalingActive = true;
        StartCoroutine(ScaleTheWind());
        rb.velocity = Vector3.zero;
      
        Vector3 gunTransformVec = gunTransform.position;
        //gunTransformVec.y = initialPos.y;
        gunTransformVec.y += 25f;
        gunTransformVec.z += 25f;

        transform.position = gunTransformVec;
        transform.rotation = gunTransform.rotation;

        forceVec = (directionVec - transform.position).normalized;

        rb.AddForce(forceVec * windForce * 1.3f, ForceMode.Impulse); //Impulse force u anlik hiz degistirerek uyguluyor
    }


    //Update
    IEnumerator ScaleTheWind()
    {
       
        while (isScalingActive)
        {
            Vector3 dummyScaler = transform.localScale;
            dummyScaler += Vector3.one * 2f;
            transform.localScale = dummyScaler;
            yield return new WaitForEndOfFrame();
        }
        StopCoroutine(ScaleTheWind());
    }

}