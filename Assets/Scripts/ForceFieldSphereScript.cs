using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldSphereScript : MonoBehaviour
{
    Material material;
    float fresnelPower;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        fresnelPower = 1.5f;
        StartCoroutine(StartForceField());
    }

    IEnumerator StartForceField()
    {
        material.SetFloat("Vector1_E4C38D86", fresnelPower);
        yield return new WaitForSecondsRealtime(1.5f);
        while (fresnelPower < 10f)
        {
            fresnelPower += 0.2f;
            material.SetFloat("Vector1_E4C38D86", fresnelPower);
            yield return new WaitForEndOfFrame();
        }
        StopCoroutine(StartForceField());
    }

}
