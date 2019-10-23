using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanRotator : MonoBehaviour
{
    void LateUpdate()
    {
        transform.Rotate(Vector3.forward * 6f, Space.Self);
    }

}
