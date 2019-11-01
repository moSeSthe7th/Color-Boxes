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
                colors.throwableCubeColor = new Color32(206,255,0,255);
                colors.forceFieldColor = new Color32(43,0,191,255);
                colors.platformColor = new Color32(255,255,255,255);
                colors.fogColor = new Color32(160, 0, 255, 116);
                colors.gunColor = new Color32(152, 0, 255, 255);

                colors.emissionBillboardColor = new Color(0.3679245f, 0.3679245f, 0.3679245f);
                colors.emissionPlatformColor = new Color(0.4245283f, 0.4245283f, 0.4245283f);

                break;            
           
            case ColorMode.Cyan:
                colors.billboardColor = new Color32(255, 255, 255, 255);
                colors.throwableCubeColor = new Color32(243, 182, 155, 255);
                colors.forceFieldColor = new Color32(0, 157, 191, 255);
                colors.platformColor = new Color32(255, 255, 255, 255);
                colors.fogColor = new Color32(162, 251, 255, 116);
                colors.gunColor = new Color32(26, 211, 238, 255);

                colors.emissionBillboardColor = new Color(0.3679245f, 0.3679245f, 0.3679245f);
                colors.emissionPlatformColor = new Color(0.4245283f, 0.4245283f, 0.4245283f);
                break;
            case ColorMode.Green:
                colors.billboardColor = new Color32(255, 255, 255, 255);
                colors.throwableCubeColor = new Color32(255, 172, 199, 255);
                colors.forceFieldColor = new Color32(0, 191, 88, 255);
                colors.platformColor = new Color32(255, 255, 255, 255);
                colors.fogColor = new Color32(94, 219, 122, 116);
                colors.gunColor = new Color32(98, 214, 137, 255);

                colors.emissionBillboardColor = new Color(0.3679245f, 0.3679245f, 0.3679245f);
                colors.emissionPlatformColor = new Color(0.4245283f, 0.4245283f, 0.4245283f);
                break;
            case ColorMode.Yellow:
                colors.billboardColor = new Color32(255, 255, 255, 255);
                colors.throwableCubeColor = new Color32(109, 150, 255, 255);
                colors.forceFieldColor = new Color32(157, 191, 0, 255);
                colors.platformColor = new Color32(255, 255, 255, 255);
                colors.fogColor = new Color32(219, 236, 126, 116);
                colors.gunColor = new Color32(188, 197, 75, 255);

                colors.emissionBillboardColor = new Color(0.3679245f, 0.3679245f, 0.3679245f);
                colors.emissionPlatformColor = new Color(0.4245283f, 0.4245283f, 0.4245283f);
                break;
            case ColorMode.Orange:
                colors.billboardColor = new Color32(255, 255, 255, 255);
                colors.throwableCubeColor = new Color32(0, 168, 255, 255);
                colors.forceFieldColor = new Color32(191, 73, 0, 255);
                colors.platformColor = new Color32(255, 255, 255, 255);
                colors.fogColor = new Color32(255, 149, 90, 116);
                colors.gunColor = new Color32(197, 115, 75, 255);

                colors.emissionBillboardColor = new Color(0.3679245f, 0.3679245f, 0.3679245f);
                colors.emissionPlatformColor = new Color(0.4245283f, 0.4245283f, 0.4245283f);
                break;
            case ColorMode.Red:
                colors.billboardColor = new Color32(255, 255, 255, 255);
                colors.throwableCubeColor = new Color32(143, 192, 255, 255);
                colors.forceFieldColor = new Color32(191, 41, 28, 255);
                colors.platformColor = new Color32(255, 255, 255, 255);
                colors.fogColor = new Color32(255, 72, 68, 116);
                colors.gunColor = new Color32(219, 147, 141, 255);

                colors.emissionBillboardColor = new Color(0.3679245f, 0.3679245f, 0.3679245f);
                colors.emissionPlatformColor = new Color(0.4245283f, 0.4245283f, 0.4245283f);
                break;
            case ColorMode.Pink:
                colors.billboardColor = new Color32(255, 255, 255, 255);
                colors.throwableCubeColor = new Color32(143, 255, 217, 255);
                colors.forceFieldColor = new Color32(191, 28, 89, 255);
                colors.platformColor = new Color32(255, 255, 255, 255);
                colors.fogColor = new Color32(255, 69, 147, 116);
                colors.gunColor = new Color32(224, 121, 170, 255);

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
