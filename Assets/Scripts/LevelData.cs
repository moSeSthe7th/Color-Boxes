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

    public LevelData(int lvlData)
    {
        level = lvlData.ToString();

        if(levelData == null)
        {
            levelData = this;
        }
    }

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
        cubeSetter = new CubeSetter(holeCount);


    }

}
