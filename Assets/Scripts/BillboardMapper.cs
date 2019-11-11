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

        public int actualSpriteRow;
        public int actualSpriteColumn;


        //Map holds data of hole cubes position in actual sprite and hole cubes corresponding index of holeCubes list.
        //vector2 which holds the position is in the format of (column index, row index)
        public List<Tuple<Vector2, int>> Map;

        //public List<Tuple<Vector3,Color32>> holeData;
        public List<LevelData.Hole> holeData { get; set; }
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

        spriteMap.actualSpriteColumn = 0;
        spriteMap.actualSpriteRow = 0;

        //Create new filler block object
        fillerBlocks = new List<Vector3>();

        pixels = sprite.GetPixels32();

        spriteMap.Map = new List<Tuple<Vector2, int>>();

        SetSpriteMap();
        SetColliderData();
    }

    SpriteMap SetSpriteMap()
    {
        int count = 0;
        Vector3 currPos;

        int maxRows = 0;
        int maxCols = 0;


        for (int r = 0; r < spriteMap.rowNumber; r++) 
        {
            maxCols = 0;

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
                    //eger obje throwablesa z posizyonunu biraz daha geriye al
                    currPos.z += 5f;

                    //Calculate actual sprites Row Column. This assumes sprite ıs a rectangle
                    if(maxRows < r + 1)
                    {
                        maxRows ++;
                        spriteMap.actualSpriteRow++;
                    }

                    if(maxCols < c + 1)
                    {
                        maxCols++;
                        if(spriteMap.actualSpriteColumn < maxCols)
                        {
                            spriteMap.actualSpriteColumn++;
                        }
                    }

                    spriteMap.Map.Add(new Tuple<Vector2, int>(new Vector2(c, spriteMap.actualSpriteRow),spriteMap.totBlockCount));


                    //Setted Collider round arranged in SetColliderData() function
                    LevelData.ColliderRound round = LevelData.ColliderRound.LastColliders;

                    spriteMap.holeData.Add(new LevelData.Hole(currPos, pixels[count],round,0));
                    spriteMap.totBlockCount += 1;
                }
                
                count++;
            }
        }

       // spriteMap.totHeight = (maxRows + 1) * HolePosModifier;
       // spriteMap.totWidth = (maxCols + 1)  * HolePosModifier;

        return spriteMap;
    }

    public int roundCount; //kac kere collider acilcagi. sprite buyudukce buyumesi lazim
    public int cubePerRound;

    public int firstColliderCount = 0;
    public int secondColliderCount = 0;
    public int thirdColliderCount = 0;

    void SetColliderData()
    {
        roundCount = spriteMap.totBlockCount / 100; //kac kere collider acilcagi. sprite buyudukce buyumesi lazim
        cubePerRound = spriteMap.totBlockCount / roundCount;
        Debug.Log("Collider round count is : " + roundCount);
       
        int indexer = 0;

        float topColliderField = spriteMap.actualSpriteRow - (spriteMap.actualSpriteRow / roundCount);

        foreach (Tuple<Vector2, int> holeTuple in spriteMap.Map)
        {
            int index = holeTuple.Item2;
            Vector2 hPosOnMap = holeTuple.Item1;

            LevelData.Hole tmpHole = spriteMap.holeData[index];

            // find round of given hole
            for (int colRound = 0; colRound < roundCount; colRound ++)
            {
                if (hPosOnMap.y > topColliderField) // eger en ustteyse maks collider round da acilcak demek
                {
                    spriteMap.holeData[index] = new LevelData.Hole(tmpHole.position, tmpHole.color, tmpHole.cRound, roundCount - 1);
                    break;
                }
                else
                {                                                                                   //burasi loop icinde gittikce kuvulcek    
                    float baseForRound  = spriteMap.actualSpriteRow - (spriteMap.actualSpriteRow * (roundCount - colRound));

                    //round icin olan en alt sinir
                    if (hPosOnMap.y <= baseForRound)
                    {
                        spriteMap.holeData[index] = new LevelData.Hole(tmpHole.position, tmpHole.color, tmpHole.cRound,  colRound);
                        break;
                    }
                    else
                    {
                        //buraya kadar geldiyse o round icin belirlenen karenin icinde mi ona bak
                        float maxRowForRound = spriteMap.actualSpriteRow - (spriteMap.actualSpriteRow * (roundCount - colRound));

                    }
                }
            }

        }



            for (int r = 0; r < roundCount; r ++)
            { 
            for(int cpr = 0; cpr < cubePerRound; cpr++)
            {
                LevelData.Hole tmp = spriteMap.holeData[indexer + cpr];
                spriteMap.holeData[indexer + cpr] = new LevelData.Hole(tmp.position, tmp.color, tmp.cRound,r);
                Debug.Log("Round is : " + r);
            }
            indexer += cubePerRound;
            foreach(Tuple<Vector2, int> tuple in spriteMap.Map)
            {
                float topColliderField = spriteMap.actualSpriteRow - (spriteMap.actualSpriteRow / roundCount);

                if ((float)tuple.Item1.y < topColliderField)
                {
                    float baseHeight = spriteMap.actualSpriteRow - spriteMap.actualSpriteRow * 0.2f;

                    if ((float)tuple.Item1.y < spriteMap.actualSpriteRow * 0.4f && ((float)tuple.Item1.x > spriteMap.actualSpriteColumn * 0.3f && (float)tuple.Item1.x < spriteMap.actualSpriteColumn * 0.7f) || (float)tuple.Item1.y < baseHeight)
                    { }
                }
                else
                {

                }
            }
        }

      /*  foreach(Tuple<Vector2,int> tuple in ColliderMap)
        {
            if((float)tuple.Item1.y < spriteMap.actualSpriteRow * 0.6f)
            {
                if((float)tuple.Item1.y < spriteMap.actualSpriteRow * 0.4f && ((float)tuple.Item1.x > spriteMap.actualSpriteColumn * 0.3f && (float)tuple.Item1.x < spriteMap.actualSpriteColumn * 0.7f) || (float)tuple.Item1.y < spriteMap.actualSpriteRow * 0.2f)
                {
                    LevelData.Hole tmp = spriteMap.holeData[tuple.Item2];
                    tmp.cRound = LevelData.ColliderRound.FirstRound;
                    spriteMap.holeData[tuple.Item2] = new LevelData.Hole(tmp.position, tmp.color, tmp.cRound);

                    firstColliderCount++;
                }
                else if((float)tuple.Item1.y >= spriteMap.actualSpriteRow * 0.4f)
                {
                    LevelData.Hole tmp = spriteMap.holeData[tuple.Item2];
                    tmp.cRound = LevelData.ColliderRound.ThirdRound;
                    spriteMap.holeData[tuple.Item2] = new LevelData.Hole(tmp.position, tmp.color, tmp.cRound);

                    thirdColliderCount++;
                }
                else
                {
                    LevelData.Hole tmp = spriteMap.holeData[tuple.Item2];
                    tmp.cRound = LevelData.ColliderRound.SecondRound;
                    spriteMap.holeData[tuple.Item2] = new LevelData.Hole(tmp.position, tmp.color, tmp.cRound);

                    secondColliderCount++;
                }
            }
            else
            {
                LevelData.Hole tmp = spriteMap.holeData[tuple.Item2];
                tmp.cRound = LevelData.ColliderRound.LastColliders;
                spriteMap.holeData[tuple.Item2] = new LevelData.Hole(tmp.position, tmp.color, tmp.cRound);
            }
        }*/
    }


}
