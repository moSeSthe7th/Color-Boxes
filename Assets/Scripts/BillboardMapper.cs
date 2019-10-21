using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

//Su anki Billboard un buyukulugu genlislik x yukseklik olarak 48x27 oluyor. (bloklarin buyuklugunu 10 olarak kabul edersek) 
//Sol alt blok pozisyon -240 , 1020 , 220
//Sag alt blok pozisyon  240 , 1020 , 220
//Sag ust blok pozisyon  240 , 1290 , 220
//Sol ust blok pozisyon -240 , 1290 , 220

public class BillboardMapper 
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
    public List<Vector3> fillerBlocks;

    public BillboardMapper(Texture2D sprite)
    {
        //Set sprite map parameters;
        spriteMap = new SpriteMap();
        //spriteMap.holeData = new List<Tuple<Vector3, Color32>>();
        spriteMap.holeData = new List<LevelData.Hole>();

        spriteMap.rowNumber = sprite.height;
        spriteMap.coloumnNumber = sprite.width;

        spriteMap.totHeight = spriteMap.rowNumber * HolePosModifier;
        spriteMap.totWidth = spriteMap.coloumnNumber * HolePosModifier;

        spriteMap.totBlockCount = 0;

        //Create new filler block object
        fillerBlocks = new List<Vector3>();

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
                currPos = new Vector3(c * HolePosModifier, r * HolePosModifier, 0f);

                //If pixel alpha is 0 dont set is as spriteMap but set as empty fillberBlock
                if (Mathf.Approximately(0f, pixels[count].a))
                {
                    fillerBlocks.Add(currPos);
                }
                else //if pixel is not alpha its a hole in wall
                {
                    spriteMap.holeData.Add(new LevelData.Hole(currPos, pixels[count]));
                    spriteMap.totBlockCount += 1;
                }
                
                count++;
            }
        }

       // spriteMap.totHeight = (maxRows + 1) * HolePosModifier;
       // spriteMap.totWidth = (maxCols + 1)  * HolePosModifier;

        return spriteMap;
    }


}
