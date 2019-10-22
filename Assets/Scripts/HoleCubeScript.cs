using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleCubeScript : MonoBehaviour
{
    public bool isOccupied = false;
    public Color holeColor;

    public IEnumerator HoleCubeReflector()
    {
        Material holeCubeMat = GetComponent<Renderer>().material;
        Color color = holeCubeMat.GetColor("_BaseColor");
        while (true)
        {
            holeCubeMat.SetColor("_BaseColor", Color.red);
            yield return new WaitForSecondsRealtime(0.3f);
            holeCubeMat.SetColor("_BaseColor", color);
            yield return new WaitForSecondsRealtime(0.3f);
        }
       
    }
}
