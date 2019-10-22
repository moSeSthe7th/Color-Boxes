using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanRotator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RotateFan());
    }
    
    public IEnumerator RotateFan()
    {
        while (true)
        {
            transform.Rotate(Vector3.forward * 6f,Space.Self);
            yield return new WaitForSeconds(0.001f);
        }
      
    }
}
