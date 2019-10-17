using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData
{
    public struct Hole
    {
        public Vector3 position;
        public Color32 color;

        public Hole(Vector3 position,Color32 color)
        {
            this.position = position;
            this.color = color;
        }

    }

    public static LevelData levelData;
    string level;

    private TextureToHoleWall HoleWall;
    private Texture2D lvlSprite;

    private CubeSetter cubeSetter;
    GameObject cube; //throwableCube
    public Vector3 platformPos = GameObject.FindGameObjectWithTag("Platform").transform.position; //Altigenin en tepesi

    public LevelData(int lvlData,GameObject throwableCube)
    {
        level = lvlData.ToString();
        cube = throwableCube;

        if(levelData == null)
        {
            levelData = this;
        }
    }

    public List<Vector3> ThrowableCubes;
    public float throwableWidth;
    public float throwableHeight;

    public List<Hole> holes;
    public float mapWidth;
    public float mapHeight;
    public int holeCount;

    public void SetHoleWall()
    {
        lvlSprite = (Texture2D)Resources.Load("LevelSprites/LvlSp_" + level);
        HoleWall = new TextureToHoleWall(lvlSprite);

        holes = HoleWall.spriteMap.holeData;
        mapHeight = HoleWall.spriteMap.totHeight;
        mapWidth = HoleWall.spriteMap.totWidth;
        holeCount = HoleWall.spriteMap.totBlockCount;
    }

    public CubeSetter.ThrowableConstruction throwableConst;

    public void SetThrowableCubes()
    {
        cubeSetter = new CubeSetter(holeCount, cube.transform.localScale.x);

        ThrowableCubes = cubeSetter.cubeConst.cubePoss;
        throwableHeight = cubeSetter.cubeConst.height;
        throwableWidth = cubeSetter.cubeConst.width;
    }

}
