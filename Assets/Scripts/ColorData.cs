using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorData 
{
    enum ColorMode
    {
        Purple, // Sari mor klasik renk
        Cyan,
        Green,
        Yellow,
        Orange,
        Red,
        Pink,
        MaxColorMode
    }

    public struct ColorDataHolder
    {
        public Color32 billboardColor;
        public Color32 throwableCubeColor;
        public Color32 forceFieldColor;
        public Color32 platformColor;
        public Color32 fogColor;
        public Color32 gunColor;

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
            case ColorMode.Purple:
                colors.billboardColor = new Color32(255, 255, 255, 255);
                colors.throwableCubeColor = new Color32(213,255,216,255);
                colors.forceFieldColor = new Color32(79,0,191,255);
                colors.platformColor = new Color32(255,255,255,255);
                colors.fogColor = new Color32(135, 51, 134, 116);
                colors.gunColor = new Color32(220, 26, 238, 255);

                colors.emissionBillboardColor = new Color(0.3679245f, 0.3679245f, 0.3679245f);
                colors.emissionPlatformColor = new Color(0.4245283f, 0.4245283f, 0.4245283f);

                break;            
           
            case ColorMode.Cyan:
                colors.billboardColor = new Color32(255, 255, 255, 255);
                colors.throwableCubeColor = new Color32(243, 195, 185, 255);
                colors.forceFieldColor = new Color32(27, 58, 132, 255);
                colors.platformColor = new Color32(255, 255, 255, 255);
                colors.fogColor = new Color32(31, 38, 135, 116);
                colors.gunColor = new Color32(100, 136, 255, 255);

                colors.emissionBillboardColor = new Color(0.3679245f, 0.3679245f, 0.3679245f);
                colors.emissionPlatformColor = new Color(0.4245283f, 0.4245283f, 0.4245283f);
                break;
            case ColorMode.Green:
                colors.billboardColor = new Color32(255, 255, 255, 255);
                colors.throwableCubeColor = new Color32(255, 189, 223, 255);
                colors.forceFieldColor = new Color32(31, 118, 87, 255);
                colors.platformColor = new Color32(255, 255, 255, 255);
                colors.fogColor = new Color32(31, 130, 91, 116);
                colors.gunColor = new Color32(89, 161, 132, 255);

                colors.emissionBillboardColor = new Color(0.3679245f, 0.3679245f, 0.3679245f);
                colors.emissionPlatformColor = new Color(0.4245283f, 0.4245283f, 0.4245283f);
                break;
            case ColorMode.Yellow:
                colors.billboardColor = new Color32(255, 255, 255, 255);
                colors.throwableCubeColor = new Color32(168, 192, 255, 255);
                colors.forceFieldColor = new Color32(107, 111, 25, 255);
                colors.platformColor = new Color32(255, 255, 255, 255);
                colors.fogColor = new Color32(160, 166, 35, 116);
                colors.gunColor = new Color32(159, 157, 47, 255);

                colors.emissionBillboardColor = new Color(0.3679245f, 0.3679245f, 0.3679245f);
                colors.emissionPlatformColor = new Color(0.4245283f, 0.4245283f, 0.4245283f);
                break;
            case ColorMode.Orange:
                colors.billboardColor = new Color32(255, 255, 255, 255);
                colors.throwableCubeColor = new Color32(187, 211, 255, 255);
                colors.forceFieldColor = new Color32(111, 71, 25, 255);
                colors.platformColor = new Color32(255, 255, 255, 255);
                colors.fogColor = new Color32(166, 80, 35, 116);
                colors.gunColor = new Color32(147, 111, 29, 255);

                colors.emissionBillboardColor = new Color(0.3679245f, 0.3679245f, 0.3679245f);
                colors.emissionPlatformColor = new Color(0.4245283f, 0.4245283f, 0.4245283f);
                break;
            case ColorMode.Red:
                colors.billboardColor = new Color32(255, 255, 255, 255);
                colors.throwableCubeColor = new Color32(203, 255, 252, 255);
                colors.forceFieldColor = new Color32(111, 27, 25, 255);
                colors.platformColor = new Color32(255, 255, 255, 255);
                colors.fogColor = new Color32(166, 35, 38, 116);
                colors.gunColor = new Color32(183, 28, 49, 255);

                colors.emissionBillboardColor = new Color(0.3679245f, 0.3679245f, 0.3679245f);
                colors.emissionPlatformColor = new Color(0.4245283f, 0.4245283f, 0.4245283f);
                break;
            case ColorMode.Pink:
                colors.billboardColor = new Color32(255, 255, 255, 255);
                colors.throwableCubeColor = new Color32(180, 255, 230, 255);
                colors.forceFieldColor = new Color32(123, 40, 120, 255);
                colors.platformColor = new Color32(255, 255, 255, 255);
                colors.fogColor = new Color32(188, 81, 165, 116);
                colors.gunColor = new Color32(217, 111, 202, 255);

                colors.emissionBillboardColor = new Color(0.3679245f, 0.3679245f, 0.3679245f);
                colors.emissionPlatformColor = new Color(0.4245283f, 0.4245283f, 0.4245283f);
                break;
            default:
            {
                Debug.LogError("Wrong Color mode on SetLevelColors");
                break;
            }
        }
            

        return colors;
    }

}
