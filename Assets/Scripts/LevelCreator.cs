using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    public Transform holeCubeParent;
    Transform bilboard;

    private LevelData levelData;

    public GameObject holeCube;

    List<Vector3> holeCubePositions = new List<Vector3>();

    void Start()
    {
        levelData = new LevelData();
        Time.timeScale = 3f;

        bilboard = GameObject.FindGameObjectWithTag("Bilboard").transform;

        CreateLevel();
        
    }

    void CreateLevel()
    {
        levelData.Set();
       
        CreateHoleCubes();
        PositionHoleCubes();
    }


    void CreateHoleCubes()
    {
        foreach(LevelData.Hole selectedHole in levelData.holes)
        {
            holeCubePositions.Add(selectedHole.position);
            GameObject currentHole = Instantiate(holeCube, selectedHole.position, Quaternion.identity);
            currentHole.GetComponent<HoleCubeScript>().holeColor = selectedHole.color;
            currentHole.transform.parent = holeCubeParent;
        }
       
    }

    void PositionHoleCubes()
    {
        Vector3 tmpVec = new Vector3(0f, 0f, 520f);

        tmpVec.x -= levelData.mapWidth * (5f / 10f);
        tmpVec.y -= levelData.mapHeight * (1f / 10f);

        holeCubeParent.position = tmpVec;
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
