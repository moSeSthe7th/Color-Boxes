using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorData 
{
    enum ColorMode
    {
        Classic, // Sari mor klasik renk
        MaxColorMode
    }

    public struct ColorDataHolder
    {
        public Color32 billboardColor;
        public Color32 throwableCubeColor;
        public Color32 forceFieldColor;
        public Color32 platformColor;

        public Color emissionBillboardColor;
        public Color emissionPlatformColor;
    }

    ColorMode cMode;

    public ColorDataHolder SetLevelColors(int level)
    {
        ColorDataHolder colors = new ColorDataHolder();

        cMode = (ColorMode)(level % (int)ColorMode.MaxColorMode);

        switch(cMode)
        {
            case ColorMode.Classic:
            {
                colors.billboardColor = new Color32(255, 255, 255, 255);
                colors.throwableCubeColor = new Color32(255,255,255,255);
                colors.forceFieldColor = new Color32(43,0,191,255);
                colors.platformColor = new Color32(255,255,255,255);

                colors.emissionBillboardColor = new Color(0.3679245f, 0.3679245f, 0.3679245f);
                colors.emissionPlatformColor = new Color(0.4245283f, 0.4245283f, 0.4245283f);

                break;
            }
            default:
            {
                Debug.LogError("Wrong Color mode on SetLevelColors");
                break;
            }
        }
            

        return colors;
    }

}
