using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    private LevelData levelData;

    public GameObject holeCube;
    public GameObject opposedWallCube;

    public float zValueOfOpposedWall = 470f;

    List<Vector3> holeCubePositions = new List<Vector3>();

    private List<LevelData.Hole> holes;

    void Start()
    {
        levelData = GetComponent<LevelData>();
        Time.timeScale = 3f;
        /*CreateHoleCubes();

        foreach(Vector3 holePos in holeCubePositions)
        {
            Instantiate(holeCube, holePos, Quaternion.identity);
        }*/


        CreateLevel();
        
    }

    void CreateLevel()
    {
        levelData.GetLevelData();
        holes = levelData.holes;
        CreateHoleCubes();
        CreateOpposedWall();
    }

    List<Vector3> GetHoleCubePositions()
    {
        return holeCubePositions;
    }

    void CreateHoleCubes()
    {
        foreach(LevelData.Hole selectedHole in holes)
        {
            holeCubePositions.Add(selectedHole.position);
            GameObject currentHole = Instantiate(holeCube, selectedHole.position, Quaternion.identity);
            currentHole.GetComponent<HoleCubeScript>().holeColor = selectedHole.color;
        }
        
    }

    

    void CreateOpposedWall()
    {
        for (float x = -85f; x <= 85f; x += 10)
        {
            for (float y = 5f; y <= 145f; y += 10)
            {
                if (!holeCubePositions.Contains(new Vector3(x, y, zValueOfOpposedWall)))
                    Instantiate(opposedWallCube, new Vector3(x, y, zValueOfOpposedWall), Quaternion.identity);
            }
        }
    }
}
