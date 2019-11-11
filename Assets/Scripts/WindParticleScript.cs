using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindParticleScript : MonoBehaviour
{
    public GameObject gun;

    void Start()
    {
        StartCoroutine(WindRotator());
    }

   private IEnumerator WindRotator()
    {
        while (true)
        {
            transform.position = gun.transform.position;
            transform.rotation = gun.transform.rotation;
            
            transform.Rotate(new Vector3(0, 0, 30f));
            yield return new WaitForSecondsRealtime(0.5f);
        }
        
    }
}
