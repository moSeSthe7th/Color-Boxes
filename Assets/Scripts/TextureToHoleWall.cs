using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TextureToHoleWall 
{
    //public Texture2D tstSprite;
    Color32[] pixels;
    int HolePosModifier = 20;

    public struct SpriteMap
    {
        public int rowNumber;
        public int coloumnNumber;

        public float width;
        public float height;

        //public List<Tuple<Vector3,Color32>> holeData;
        public List<LevelData.Hole> holeData;
    }

    public SpriteMap spriteMap;

    public TextureToHoleWall(Texture2D sprite)
    {
        spriteMap = new SpriteMap();
        //spriteMap.holeData = new List<Tuple<Vector3, Color32>>();
        spriteMap.holeData = new List<LevelData.Hole>();

        spriteMap.rowNumber = sprite.height;
        spriteMap.coloumnNumber = sprite.width;

        pixels = sprite.GetPixels32();

        SetSpriteMap();
    }

    SpriteMap SetSpriteMap()
    {
        int count = 0;

        Vector3 currPos;

        int maxRows = 0;
        int maxCols = 0;

        for (int r = 0; r < spriteMap.rowNumber; r++)
        {
            for(int c = 0; c < spriteMap.coloumnNumber; c++)
            {
                //If pixel alpha is 0 dont set it
                if (Mathf.Approximately(0f, pixels[count].a))
                {
                    count++;
                    continue;
                }

                maxRows = (maxRows < r) ? r : maxRows;
                maxCols = (maxCols < c) ? c : maxCols;

                currPos = new Vector3(c * HolePosModifier,r * HolePosModifier ,0f);
                spriteMap.holeData.Add(new LevelData.Hole(currPos,pixels[count]));

                count++;
            }
        }

        spriteMap.height = (maxRows + 1) * HolePosModifier;
        spriteMap.width = (maxCols + 1)  * HolePosModifier;

        return spriteMap;
    }


}
