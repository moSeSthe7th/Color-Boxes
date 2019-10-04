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

    private TextureToHoleWall HoleWall;
    private Texture2D lvlSprite;

    public LevelData()
    {
        lvlSprite = (Texture2D)Resources.Load("Image_Sprite/PBoy");
        HoleWall = new TextureToHoleWall(lvlSprite);
    }

    public List<Hole> holes;
    public float mapWidth;
    public float mapHeight;

    public void Set()
    {
        holes = HoleWall.spriteMap.holeData;
        mapHeight = HoleWall.spriteMap.height;
        mapWidth = HoleWall.spriteMap.width;
    }
}
