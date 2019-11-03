﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSetter
{
    int minTowerHeight = 8;
    int maxTowerHeight = 12; //maksimum kac tane kup ustuste konulabilir
    float cScale; //scale of cube

    public enum CubeArrengment
    {
        Square,
        SquareFilled,
        Circle,
        CircularSpiral,
        Default
    }

    public struct ThrowableConstruction
    {
        public int throwableCount;
        public List<Vector3> cubePoss;
        public CubeArrengment arrengment;

        public float width;
        public float height;

        public ThrowableConstruction(int count)
        {
            this.throwableCount = count;
            cubePoss = new List<Vector3>(count);
            arrengment = CubeArrengment.SquareFilled;

            width = 0;
            height = 0;
        }
    }

    public ThrowableConstruction cubeConst;

    public CubeSetter(int cubeCount, float cubeScale)
    {
        cubeConst = new ThrowableConstruction(cubeCount);
        cScale = cubeScale;

        SetCubeConstruction();
    }

    void SetCubeConstruction()
    {
        //Debug.Log("Cube construction begins. Cube count is : " + cubeConst.throwableCount);
        //Calculate how many lines of cubes(tower) there will be
        int totalTowerCount = 0;
        int towerHeight; //height of actual tower

        int extraCubes = 0; // tam kule yapmak icin yeterli olmayan kupler
        int buildedExtraCubes = 0; //for keeping track

        /*  for(towerHeight = minTowerHeight;towerHeight <= maxTowerHeight;towerHeight++)
          {
              float tmpTowerCount = (float)cubeConst.throwableCount / (float)towerHeight;
              float floatingPoint = tmpTowerCount - (int)tmpTowerCount;

              if (Mathf.Approximately(floatingPoint,0f))
              {
                  totalTowerCount = (int)tmpTowerCount; 
                  extraCubes = 0;
                  break;
              }
          }
          //eger yukarida herhangi bir deger alamadiysa demek ki (kup sayisi / tower yuksekligi) tam bolunemiyor. 
          //Sayi olarak dengesiz towerlar olusmamsi icin kalan kupleri cikartarak bi sayi belirle
          if(totalTowerCount == 0)
          {
              towerHeight = 10;

              float tmpTowerCount = (float)cubeConst.throwableCount / (float)towerHeight;
              float floatingPoint = tmpTowerCount - (int)tmpTowerCount;
              totalTowerCount = (int)tmpTowerCount;
              extraCubes = Mathf.RoundToInt(floatingPoint * towerHeight);
              Debug.Log("Extra remaining cube count : " + extraCubes);
          }*/

        towerHeight = 10;

        float tmpTowerCount = (float)cubeConst.throwableCount / (float)towerHeight;
        float floatingPoint = tmpTowerCount - (int)tmpTowerCount;
        totalTowerCount = (int)tmpTowerCount;
        extraCubes = Mathf.RoundToInt(floatingPoint * towerHeight);
        Debug.Log("Extra remaining cube count : " + extraCubes);


        int width; //her bir cizgide kac tane tower oldugu
        int height;
        //Set side length of rectangle
        float squareLength;

        float distBtwTowers = cScale * 1.1f;
        float yDistBtwCubes = cScale;

        switch (cubeConst.arrengment)
        {
            //Beta rectangle placement
            case CubeArrengment.Square:
                {
                    Debug.Log("Build Square");

                    squareLength = totalTowerCount / 4f;
                    float floatingPTower = squareLength - (int)squareLength; // 4 secenek var 0.0 , 0.25 , 0.50 , 0.75

                    width = (int)squareLength;
                    height = (int)squareLength;

                    extraCubes += Mathf.RoundToInt(floatingPTower * 4 * towerHeight); // 4 kenar var 

                    Debug.Log("Width Tower count : " + width + ". Height tower count : " + height);
                    Debug.Log("Exrta cubes that cannot be setted on Rectangle count : " + extraCubes);

                    cubeConst.height = height * distBtwTowers;
                    cubeConst.width = width * distBtwTowers;


                    //Start from left down corner to left upper corner. First build height than width than heigth again than width
                    for (int i = 0; i < towerHeight; i++)
                    {
                        for (int j = 0; j < width; j++)
                        {
                            Vector3 lowerCube = new Vector3(j * distBtwTowers, (cScale / 2f) + (i * yDistBtwCubes), distBtwTowers);

                            // z degerini 2 tane aralikla toplama sebebi araya kurulcak olan height bloklarina mesafe birakmak icin
                            Vector3 upperCube = new Vector3(j * distBtwTowers, (cScale / 2f) + (i * yDistBtwCubes), cubeConst.width + (2 * distBtwTowers));

                            cubeConst.cubePoss.Add(lowerCube);
                            cubeConst.cubePoss.Add(upperCube);
                        }

                        for (int z = 0; z < height; z++)
                        {
                            Vector3 leftCube = new Vector3(0f, (cScale / 2f) + (i * yDistBtwCubes), (z * distBtwTowers) + (2 * distBtwTowers));

                            Vector3 rightCube = new Vector3(cubeConst.height - distBtwTowers, (cScale / 2f) + (i * yDistBtwCubes), (z * distBtwTowers) + (2 * distBtwTowers));

                            cubeConst.cubePoss.Add(leftCube);
                            cubeConst.cubePoss.Add(rightCube);
                        }

                        //Build extra cubes as towers
                        if (extraCubes > 0)
                        {
                            for (int e = 0; e < extraCubes / towerHeight; e++)
                            {
                                Vector3 extraCube = new Vector3(e * distBtwTowers, (cScale / 2f) + (i * yDistBtwCubes), cubeConst.width + (3 * distBtwTowers));
                                cubeConst.cubePoss.Add(extraCube);
                                buildedExtraCubes += 1;
                            }
                        }
                    }
                    Debug.Log("Square consruction done. Cube count is : " + cubeConst.cubePoss.Count);

                    break;
                }
            case CubeArrengment.SquareFilled:
                {
                    Debug.Log("Build FilledSquare");

                    //x is width , y is height
                    Vector2 sides = FindFilledSquareSideLenghts(totalTowerCount);

                    float floatingPTower = totalTowerCount - (sides.x * sides.y);

                    width = (int)sides.x;
                    height = (int)sides.y;

                    extraCubes += Mathf.RoundToInt(floatingPTower * towerHeight); // 4 kenar var 

                    Debug.Log("Width Tower count : " + width + ". Height tower count : " + height);
                    Debug.Log("Exrta cubes that cannot be setted on Rectangle count : " + extraCubes);

                    cubeConst.height = height * distBtwTowers;
                    cubeConst.width = width * distBtwTowers;

                    for (int towerH = 0; towerH < towerHeight; towerH++)
                    {
                        for (int w = 0; w < width; w++)
                        {
                            for (int h = 0; h < height; h++)
                            {
                                Vector3 cubePos = new Vector3(w * distBtwTowers, (cScale / 2f) + towerH * yDistBtwCubes, h * distBtwTowers);
                                cubeConst.cubePoss.Add(cubePos);
                            }
                        }
                    }

                    //Build extra cubes as towers
                    if (extraCubes / towerHeight > 0)
                    {
                        int extraHeight = extraCubes / (towerHeight * width) + 1;

                        Vector3 lastCube = cubeConst.cubePoss[cubeConst.cubePoss.Count - 1];

                        for (int towerH = 0; towerH < towerHeight; towerH++)
                        {
                            for (int h = 0; h < extraHeight; h++)
                            {
                                for (int w = 0; w < width; w++)
                                {
                                    if (buildedExtraCubes < extraCubes)
                                    {
                                        float x = lastCube.x - (w * distBtwTowers);
                                        float y = (cScale / 2f) + towerH * yDistBtwCubes;
                                        float z = lastCube.z + ((h + 1) * distBtwTowers);

                                        Vector3 extraCubePos = new Vector3(x, y, z);
                                        cubeConst.cubePoss.Add(extraCubePos);
                                        buildedExtraCubes += 1;
                                    }
                                }
                            }
                        }
                    }

                    break;
                }
            default:
                {
                    break;
                }
        }


        //en son kalan blok varsa en ustlere yerlestir
        extraCubes -= buildedExtraCubes;
        //tower olarak yetersiz extra cubeler 
        if (extraCubes > 0)
        {
            for (int e = 0; e < extraCubes; e++)
            {
                Vector3 currPos = cubeConst.cubePoss[cubeConst.cubePoss.Count - extraCubes];
                currPos.y += (yDistBtwCubes);
                cubeConst.cubePoss.Add(currPos);
            }
        }


    }


    Vector2 FindFilledSquareSideLenghts(int towerCount)
    {
        //x is width , y is height
        Vector2 sides = Vector2.zero;

        bool found = false;

        //first assume height is 6 and look if its acceptable
        int height = 6;
        while (!found)
        {
            int width = towerCount / height;

            if (height - 1 <= width && width <= height + 1)
            {
                if (width > height)
                {
                    sides.x = width;
                    sides.y = height;
                }
                else
                {
                    sides.x = height;
                    sides.y = width;
                }
                found = true;
                continue;
            }

            //eger buraya geldiyse demek ki heigth widt orani tutmuyor
            //width cok kucukse heigth i kucult. width cok buyukse heigth i yukselt
            if (width < height - 1)
            {
                height -= 1;
            }
            else //if(width > height + 1)
            {
                height += 1;
            }

        }


        return sides;
    }

}
