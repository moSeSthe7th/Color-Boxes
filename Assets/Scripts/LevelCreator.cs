using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    public Transform holeCubeParent;

    private LevelData levelData;

    public GameObject holeCube;
    public GameObject opposedWallCube;

    public float zValueOfOpposedWall = 470f;

    List<Vector3> holeCubePositions = new List<Vector3>();

    private List<LevelData.Hole> holes;

    Transform HoleCubeParent;
    Transform OpposedWallParent;

    void Start()
    {
        levelData = new LevelData();
        Time.timeScale = 3f;
        /*CreateHoleCubes();

        foreach(Vector3 holePos in holeCubePositions)
        {
            Instantiate(holeCube, holePos, Quaternion.identity);
        }*/

        //CreateParents(HoleCubeParent,OpposedWallParent);
        CreateLevel();
        
    }

    void CreateLevel()
    {
        levelData.GetLevelData();
        holes = levelData.holes;
        CreateHoleCubes();
        //CreateOpposedWall();
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
            //currentHole.transform.parent = holeCubeParent;
        }
        
    }

    

    void CreateOpposedWall()
    {
        for (float x = -55; x <= 55; x += 10)
        {
            for (float y = 5f; y <= 145f; y += 10)
            {
                if (!holeCubePositions.Contains(new Vector3(x, y, zValueOfOpposedWall)))
                    Instantiate(opposedWallCube, new Vector3(x, y, zValueOfOpposedWall), Quaternion.identity);
            }
        }
    }

  /*  void CreateParents(params Transform[] transforms)
    {
        Debug.Log(transforms[0].);
        for(int i = 0; i < transforms.Length; i++)
        {
            transforms[i] = new GameObject { name = transforms[i].ToString() , tag = transforms[i].ToString() }.transform;
        }
    }*/

}
