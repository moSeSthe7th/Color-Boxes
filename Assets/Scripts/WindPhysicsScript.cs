using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindPhysicsScript : MonoBehaviour
{
    public float windForce; // ForceMode Impulse la eklendigi icin deger kuculebilir. su anda 200 editor de setlenmis
    
    public Vector3 forceVec;
    private void Awake()
    {
        transform.localScale = LevelData.levelData.initialWindScale;
    }

    private void OnEnable()
    {
        transform.localScale = LevelData.levelData.initialWindScale;
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
            dummyScaler += Vector3.one * 3f;
            transform.localScale = dummyScaler;

            this.transform.Translate(forceVec * windForce * Time.deltaTime,Space.World);
        }
    }

    public void CreateWind(Transform gunTransform, Vector3 directionVec,float engineHeat)
    {
        //transform.localScale = initialScale * engineHeat;      
        //Vector3 gunTransformVec = gunTransform.position;

        transform.position = gunTransform.position;
        transform.rotation = gunTransform.rotation;

        forceVec = (directionVec - transform.position).normalized;
        //forceVec.z *= 1.3f;
    }

}