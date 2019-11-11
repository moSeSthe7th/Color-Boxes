using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleCubeScript : MonoBehaviour
{
    public bool isOccupied = false;
    public Color holeColor;

    float ping = 0.2f;
    bool startedReflecting = false;

    public void StartReflecting()
    {
        
        if(!startedReflecting)
        {
            StartCoroutine(HoleCubeReflector());
        }
        else
        {
            ping += 0.15f;
        }

        startedReflecting = true;
    }

    IEnumerator HoleCubeReflector()
    {
        
        Material holeCubeMat = GetComponent<Renderer>().material;
        Color origColor = holeCubeMat.GetColor("_BaseColor");

        Color tmpColor = origColor;

        bool toRed = true;
        Color toColor = Color.red; //start from converting to red

        while (gameObject.activeSelf)
        {
            //for pinging around red and orig color. Check grayScale to compare colors
            if (toRed && tmpColor.grayscale <= Color.red.grayscale + 0.1f)
            {
                toColor = origColor;
                toRed = false;
                
            }
            else if(!toRed && tmpColor.grayscale >= origColor.grayscale - 0.05f)
            {
                toColor = Color.red;
                toRed = true;
                
            }

            tmpColor = Color.Lerp(tmpColor,toColor, ping);
            holeCubeMat.SetColor("_BaseColor", tmpColor);

            yield return null;

        }

        holeCubeMat.SetColor("_BaseColor", origColor);

        StopCoroutine(HoleCubeReflector());
    }
}
