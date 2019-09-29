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

    public float zValueOfOpposedWall = 470f;
    public List<Hole> holes;

    public void GetLevelData()
    {
        List<Hole> innerHoles = new List<Hole>();

        innerHoles.Add(new Hole(new Vector3(5f, 25f, zValueOfOpposedWall),Color.red));
        innerHoles.Add(new Hole(new Vector3(15f, 35f, zValueOfOpposedWall), Color.red));
        innerHoles.Add(new Hole(new Vector3(-5f, 35f, zValueOfOpposedWall),Color.red));
        innerHoles.Add(new Hole(new Vector3(5f, 35f, zValueOfOpposedWall),Color.blue));
        innerHoles.Add(new Hole(new Vector3(5f, 45f, zValueOfOpposedWall),Color.blue));
        innerHoles.Add(new Hole(new Vector3(15f, 45f, zValueOfOpposedWall),Color.blue));
        innerHoles.Add(new Hole(new Vector3(-5f, 45f, zValueOfOpposedWall),Color.blue));
        innerHoles.Add(new Hole(new Vector3(25f, 45f, zValueOfOpposedWall),Color.red));
        innerHoles.Add(new Hole(new Vector3(-15f, 45f, zValueOfOpposedWall),Color.red));
        innerHoles.Add(new Hole(new Vector3(5f, 55f, zValueOfOpposedWall), Color.red));
        innerHoles.Add(new Hole(new Vector3(15f, 55f, zValueOfOpposedWall), Color.red));
        innerHoles.Add(new Hole(new Vector3(-5f, 55f, zValueOfOpposedWall), Color.red));
        innerHoles.Add(new Hole(new Vector3(25f, 55f, zValueOfOpposedWall), Color.red));
        innerHoles.Add(new Hole(new Vector3(-15f, 55f, zValueOfOpposedWall), Color.red));
        innerHoles.Add(new Hole(new Vector3(-25f, 55f, zValueOfOpposedWall), Color.red));
        innerHoles.Add(new Hole(new Vector3(35f, 55f, zValueOfOpposedWall), Color.red));

        holes = innerHoles;
    }
}
