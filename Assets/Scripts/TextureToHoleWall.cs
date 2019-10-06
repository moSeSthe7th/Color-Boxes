using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TextureToHoleWall 
{
    //public Texture2D tstSprite;
    Color32[] pixels;
    int HolePosModifier = 10;

    public struct SpriteMap
    {
        public int rowNumber;
        public int coloumnNumber;

        public float totWidth;
        public float totHeight;

        public int totBlockCount;

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

        spriteMap.totHeight = spriteMap.rowNumber * HolePosModifier;
        spriteMap.totWidth = spriteMap.coloumnNumber * HolePosModifier;

        spriteMap.totBlockCount = 0;

        pixels = sprite.GetPixels32();

        SetSpriteMap();
    }

    SpriteMap SetSpriteMap()
    {
        int count = 0;

        Vector3 currPos;

        //int maxRows = 0;
        //int maxCols = 0;

        for (int r = 2; r < spriteMap.rowNumber + 2; r++)  // buraya +2 yi yukarı cıkarmak icin ekledik... kaldırılabilir
        {
            for(int c = 0; c < spriteMap.coloumnNumber; c++)
            {
                //If pixel alpha is 0 dont set it
                if (Mathf.Approximately(0f, pixels[count].a))
                {
                    count++;
                    continue;
                }

               // maxRows = (maxRows < r) ? r : maxRows;
               // maxCols = (maxCols < c) ? c : maxCols;

                currPos = new Vector3(c * HolePosModifier,r * HolePosModifier ,0f);
                spriteMap.holeData.Add(new LevelData.Hole(currPos,pixels[count]));
                spriteMap.totBlockCount += 1;
                count++;
            }
        }

       // spriteMap.totHeight = (maxRows + 1) * HolePosModifier;
       // spriteMap.totWidth = (maxCols + 1)  * HolePosModifier;

        return spriteMap;
    }


}
