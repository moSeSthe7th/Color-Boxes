using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    public Transform holeCubeParent;

    private LevelData levelData;

    public GameObject holeCube;

    List<Vector3> holeCubePositions = new List<Vector3>();

    private List<LevelData.Hole> holes;

    void Start()
    {
        levelData = new LevelData();
        Time.timeScale = 3f;

        CreateLevel();
        
    }

    void CreateLevel()
    {
        levelData.Set();
        holes = levelData.holes;
        CreateHoleCubes();
    }


    void CreateHoleCubes()
    {
        foreach(LevelData.Hole selectedHole in holes)
        {
            holeCubePositions.Add(selectedHole.position);
            GameObject currentHole = Instantiate(holeCube, selectedHole.position, Quaternion.identity);
            currentHole.GetComponent<HoleCubeScript>().holeColor = selectedHole.color;
            currentHole.transform.parent = holeCubeParent;
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
