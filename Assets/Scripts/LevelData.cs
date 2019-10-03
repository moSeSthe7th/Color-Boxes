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
        lvlSprite = (Texture2D)Resources.Load("");
        HoleWall = new TextureToHoleWall(lvlSprite);
    }

    public float zValueOfOpposedWall = 450f;
    public List<Hole> holes;

    public void Set()
    {
        holes = HoleWall.spriteMap.holeData;
    }
}
