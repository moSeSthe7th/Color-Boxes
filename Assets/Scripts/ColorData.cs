using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorData 
{
    enum ColorMode
    {
        Purple, // Sari mor klasik renk
        Blue,
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
        public Color32 backgroundColor;
        public Color32 streetColor;
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
                
                colors.throwableCubeColor = new Color32(184,255,200,255);
                colors.backgroundColor = new Color32(188,172,255,255);
                colors.streetColor = new Color32(135, 51, 134, 255);
                colors.gunColor = new Color32(184, 255, 200, 255);

                colors.billboardColor = new Color32(220, 26, 238, 255);
                colors.forceFieldColor = new Color32(79, 0, 191, 255);
                colors.emissionBillboardColor = new Color(0.3679245f, 0.3679245f, 0.3679245f);
                colors.emissionPlatformColor = new Color(0.4245283f, 0.4245283f, 0.4245283f);

                break;
            case ColorMode.Blue:
                colors.throwableCubeColor = new Color32(255, 148, 0, 255);
                colors.backgroundColor = new Color32(191, 222, 255, 255);
                colors.streetColor = new Color32(0, 125, 255, 255);
                colors.gunColor = new Color32(255, 148, 0, 255);

                colors.billboardColor = new Color32(220, 26, 238, 255);
                colors.forceFieldColor = new Color32(79, 0, 191, 255);
                colors.emissionBillboardColor = new Color(0.3679245f, 0.3679245f, 0.3679245f);
                colors.emissionPlatformColor = new Color(0.4245283f, 0.4245283f, 0.4245283f);
                break;
           
            case ColorMode.Cyan:
                colors.throwableCubeColor = new Color32(255, 0, 27, 255);
                colors.backgroundColor = new Color32(206, 254, 255, 255);
                colors.streetColor = new Color32(0, 255, 246, 116);
                colors.gunColor = new Color32(255, 0, 27, 255);

                colors.billboardColor = new Color32(220, 26, 238, 255);
                colors.forceFieldColor = new Color32(79, 0, 191, 255);
                colors.emissionBillboardColor = new Color(0.3679245f, 0.3679245f, 0.3679245f);
                colors.emissionPlatformColor = new Color(0.4245283f, 0.4245283f, 0.4245283f);
                break;
            case ColorMode.Green:
                colors.throwableCubeColor = new Color32(227, 133, 185, 255);
                colors.backgroundColor = new Color32(197, 229, 218, 255);
                colors.streetColor = new Color32(0, 255, 181, 116);
                colors.gunColor = new Color32(227, 133, 185, 255);

                colors.billboardColor = new Color32(220, 26, 238, 255);
                colors.forceFieldColor = new Color32(79, 0, 191, 255);
                colors.emissionBillboardColor = new Color(0.3679245f, 0.3679245f, 0.3679245f);
                colors.emissionPlatformColor = new Color(0.4245283f, 0.4245283f, 0.4245283f);
                break;
            case ColorMode.Yellow:
                colors.throwableCubeColor = new Color32(105, 133, 255, 255);
                colors.backgroundColor = new Color32(230, 233, 215, 255);
                colors.streetColor = new Color32(255, 248, 150, 116);
                colors.gunColor = new Color32(105, 133, 255, 255);

                colors.billboardColor = new Color32(220, 26, 238, 255);
                colors.forceFieldColor = new Color32(79, 0, 191, 255);
                colors.emissionBillboardColor = new Color(0.3679245f, 0.3679245f, 0.3679245f);
                colors.emissionPlatformColor = new Color(0.4245283f, 0.4245283f, 0.4245283f);
                break;
            case ColorMode.Orange:
                colors.throwableCubeColor = new Color32(106, 181, 255, 255);
                colors.backgroundColor = new Color32(217, 188, 175, 255);
                colors.streetColor = new Color32(255, 109, 0, 116);
                colors.gunColor = new Color32(106, 181, 255, 255);

                colors.billboardColor = new Color32(220, 26, 238, 255);
                colors.forceFieldColor = new Color32(79, 0, 191, 255);
                colors.emissionBillboardColor = new Color(0.3679245f, 0.3679245f, 0.3679245f);
                colors.emissionPlatformColor = new Color(0.4245283f, 0.4245283f, 0.4245283f);
                break;
            case ColorMode.Red:
                colors.throwableCubeColor = new Color32(106, 255, 250, 255);
                colors.backgroundColor = new Color32(255, 158, 162, 255);
                colors.streetColor = new Color32(255, 0, 27, 116);
                colors.gunColor = new Color32(106, 255, 250, 255);

                colors.billboardColor = new Color32(220, 26, 238, 255);
                colors.forceFieldColor = new Color32(79, 0, 191, 255);
                colors.emissionBillboardColor = new Color(0.3679245f, 0.3679245f, 0.3679245f);
                colors.emissionPlatformColor = new Color(0.4245283f, 0.4245283f, 0.4245283f);
                break;
            case ColorMode.Pink:
                colors.throwableCubeColor = new Color32(124, 255, 204, 255);
                colors.backgroundColor = new Color32(255, 206, 236, 255);
                colors.streetColor = new Color32(255, 129, 223, 116);
                colors.gunColor = new Color32(124, 255, 204, 255);

                colors.billboardColor = new Color32(220, 26, 238, 255);
                colors.forceFieldColor = new Color32(79, 0, 191, 255);
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
