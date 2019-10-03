using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData
{
    public struct Hole
    {
        public Vector3 position;
        public Color color;

        public Hole(Vector3 position,Color color)
        {
            this.position = position;
            this.color = color;
        }
    }

    public float zValueOfOpposedWall = 450f;
    public List<Hole> holes;

    public void GetLevelData()
    {
        List<Hole> innerHoles = new List<Hole>();

        innerHoles.Add(new Hole(new Vector3(5f, 25f, zValueOfOpposedWall),Color.red));
        innerHoles.Add(new Hole(new Vector3(25f, 45f, zValueOfOpposedWall), Color.red));
        innerHoles.Add(new Hole(new Vector3(-15f, 45f, zValueOfOpposedWall),Color.red));
        innerHoles.Add(new Hole(new Vector3(5f, 45f, zValueOfOpposedWall),Color.blue));
        innerHoles.Add(new Hole(new Vector3(5f, 65f, zValueOfOpposedWall),Color.blue));
        innerHoles.Add(new Hole(new Vector3(25f, 65f, zValueOfOpposedWall),Color.blue));
        innerHoles.Add(new Hole(new Vector3(-15f, 65f, zValueOfOpposedWall),Color.blue));
        innerHoles.Add(new Hole(new Vector3(45f, 65f, zValueOfOpposedWall),Color.red));
        innerHoles.Add(new Hole(new Vector3(-35f, 65f, zValueOfOpposedWall),Color.red));
        innerHoles.Add(new Hole(new Vector3(5f, 85f, zValueOfOpposedWall), Color.red));
        innerHoles.Add(new Hole(new Vector3(25f, 85f, zValueOfOpposedWall), Color.red));
        innerHoles.Add(new Hole(new Vector3(-15f, 85f, zValueOfOpposedWall), Color.red));
        innerHoles.Add(new Hole(new Vector3(45f, 85f, zValueOfOpposedWall), Color.red));
        innerHoles.Add(new Hole(new Vector3(-35f, 85f, zValueOfOpposedWall), Color.red));
        innerHoles.Add(new Hole(new Vector3(-55f, 85f, zValueOfOpposedWall), Color.red));
        innerHoles.Add(new Hole(new Vector3(65f, 85f, zValueOfOpposedWall), Color.red));

        innerHoles.Add(new Hole(new Vector3(5f, 165f, zValueOfOpposedWall), Color.red));
        innerHoles.Add(new Hole(new Vector3(25f, 145f, zValueOfOpposedWall), Color.red));
        innerHoles.Add(new Hole(new Vector3(-15f, 145f, zValueOfOpposedWall), Color.red));
        innerHoles.Add(new Hole(new Vector3(5f, 145f, zValueOfOpposedWall), Color.blue));
        innerHoles.Add(new Hole(new Vector3(5f, 125f, zValueOfOpposedWall), Color.blue));
        innerHoles.Add(new Hole(new Vector3(25f, 125f, zValueOfOpposedWall), Color.blue));
        innerHoles.Add(new Hole(new Vector3(-15f, 125f, zValueOfOpposedWall), Color.blue));
        innerHoles.Add(new Hole(new Vector3(45f, 125f, zValueOfOpposedWall), Color.red));
        innerHoles.Add(new Hole(new Vector3(-35f, 125f, zValueOfOpposedWall), Color.red));
        innerHoles.Add(new Hole(new Vector3(5f, 105f, zValueOfOpposedWall), Color.red));
        innerHoles.Add(new Hole(new Vector3(25f, 105f, zValueOfOpposedWall), Color.red));
        innerHoles.Add(new Hole(new Vector3(-15f, 105f, zValueOfOpposedWall), Color.red));
        innerHoles.Add(new Hole(new Vector3(45f, 105f, zValueOfOpposedWall), Color.red));
        innerHoles.Add(new Hole(new Vector3(-35f, 105f, zValueOfOpposedWall), Color.red));
        innerHoles.Add(new Hole(new Vector3(-55f, 105f, zValueOfOpposedWall), Color.red));
        innerHoles.Add(new Hole(new Vector3(65f, 105f, zValueOfOpposedWall), Color.red));

        holes = innerHoles;
    }
}
