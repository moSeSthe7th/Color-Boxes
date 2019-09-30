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

    public float zValueOfOpposedWall = 300;
    public List<Hole> holes;

    public void GetLevelData()
    {
        List<Hole> innerHoles = new List<Hole>();

        innerHoles.Add(new Hole(new Vector3(5f, 35f, zValueOfOpposedWall),Color.red));
        innerHoles.Add(new Hole(new Vector3(15f, 45f, zValueOfOpposedWall), Color.red));
        innerHoles.Add(new Hole(new Vector3(-5f, 45f, zValueOfOpposedWall),Color.red));
        innerHoles.Add(new Hole(new Vector3(5f, 45f, zValueOfOpposedWall),Color.blue));
        innerHoles.Add(new Hole(new Vector3(5f, 55f, zValueOfOpposedWall),Color.blue));
        innerHoles.Add(new Hole(new Vector3(15f, 55f, zValueOfOpposedWall),Color.blue));
        innerHoles.Add(new Hole(new Vector3(-5f, 55f, zValueOfOpposedWall),Color.blue));
        innerHoles.Add(new Hole(new Vector3(25f, 55f, zValueOfOpposedWall),Color.red));
        innerHoles.Add(new Hole(new Vector3(-15f, 55f, zValueOfOpposedWall),Color.red));
        innerHoles.Add(new Hole(new Vector3(5f, 65f, zValueOfOpposedWall), Color.red));
        innerHoles.Add(new Hole(new Vector3(15f, 65f, zValueOfOpposedWall), Color.red));
        innerHoles.Add(new Hole(new Vector3(-5f, 65f, zValueOfOpposedWall), Color.red));
        innerHoles.Add(new Hole(new Vector3(25f, 65f, zValueOfOpposedWall), Color.red));
        innerHoles.Add(new Hole(new Vector3(-15f, 65f, zValueOfOpposedWall), Color.red));
        innerHoles.Add(new Hole(new Vector3(-25f, 65f, zValueOfOpposedWall), Color.red));
        innerHoles.Add(new Hole(new Vector3(35f, 65f, zValueOfOpposedWall), Color.red));

        innerHoles.Add(new Hole(new Vector3(5f, 105f, zValueOfOpposedWall), Color.red));
        innerHoles.Add(new Hole(new Vector3(15f, 95f, zValueOfOpposedWall), Color.red));
        innerHoles.Add(new Hole(new Vector3(-5f, 95f, zValueOfOpposedWall), Color.red));
        innerHoles.Add(new Hole(new Vector3(5f, 95f, zValueOfOpposedWall), Color.blue));
        innerHoles.Add(new Hole(new Vector3(5f, 85f, zValueOfOpposedWall), Color.blue));
        innerHoles.Add(new Hole(new Vector3(15f, 85f, zValueOfOpposedWall), Color.blue));
        innerHoles.Add(new Hole(new Vector3(-5f, 85f, zValueOfOpposedWall), Color.blue));
        innerHoles.Add(new Hole(new Vector3(25f, 85f, zValueOfOpposedWall), Color.red));
        innerHoles.Add(new Hole(new Vector3(-15f, 85f, zValueOfOpposedWall), Color.red));
        innerHoles.Add(new Hole(new Vector3(5f, 75f, zValueOfOpposedWall), Color.red));
        innerHoles.Add(new Hole(new Vector3(15f, 75f, zValueOfOpposedWall), Color.red));
        innerHoles.Add(new Hole(new Vector3(-5f, 75f, zValueOfOpposedWall), Color.red));
        innerHoles.Add(new Hole(new Vector3(25f, 75f, zValueOfOpposedWall), Color.red));
        innerHoles.Add(new Hole(new Vector3(-15f, 75f, zValueOfOpposedWall), Color.red));
        innerHoles.Add(new Hole(new Vector3(-25f, 75f, zValueOfOpposedWall), Color.red));
        innerHoles.Add(new Hole(new Vector3(35f, 75f, zValueOfOpposedWall), Color.red));

        holes = innerHoles;
    }
}
