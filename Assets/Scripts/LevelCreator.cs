using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    public Transform holeCubeParent;
    Transform bilboard;

    private LevelData levelData;

    public GameObject holeCube;

    GameObject windParent;
    public GameObject wind;

    GameObject cubeParent;
    public GameObject throwableCube;

    List<Vector3> holeCubePositions = new List<Vector3>();

    void Start()
    {
        windParent = new GameObject("WindParent");
        DataScript.windObjects = ObjectPooler.instance.PooltheObjects(wind, 100,windParent.transform);
        DataScript.succesfullyOccupiedHoleCount = 0;

        throwableCube = (GameObject)Resources.Load("Prefabs/ThrownObject");
        cubeParent = new GameObject("CubeParent");
        int levelNumber = 3; //sadece gostermelik bi level simdilik

        levelData = new LevelData(levelNumber, throwableCube);
        Time.timeScale = 3f;

        bilboard = GameObject.FindGameObjectWithTag("Bilboard").transform;
        CreateLevel();
    }

    void CreateLevel()
    {
        //Create picture on billboard
        levelData.SetHoleWall();
        CreateHoleCubes();
        PositionHoleCubes();

        //CreateThrowableCubes
        levelData.SetThrowableCubes();
        CreateThrowableCubes();
        PositionThrowableCubes();
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

        DataScript.totalHoleCount = levelData.holes.Count;
        DataScript.remainingHoleColliderIncreaseThreshold = Mathf.RoundToInt(DataScript.totalHoleCount * 0.9f);
        
       
    }
   
    void PositionHoleCubes()
    {
        Transform board = bilboard.transform.GetChild(0);
        Vector3 tmpVec = new Vector3(0f, board.position.y, bilboard.transform.position.z - board.localScale.y + 10f);

        tmpVec.x -= (levelData.mapWidth / 2f) - (holeCube.transform.localScale.x / 2f); 
        tmpVec.y -= (levelData.mapHeight / 2f) + (holeCube.transform.localScale.y ); 

        holeCubeParent.position = tmpVec;
    }


    void CreateThrowableCubes()
    {
        foreach (Vector3 cubePos in levelData.ThrowableCubes)
        {
            GameObject currCube = Instantiate(throwableCube, cubePos, Quaternion.identity,cubeParent.transform);
        }
    }

    void PositionThrowableCubes()
    {
        Vector3 pos = levelData.platformPos;
        pos.x -= levelData.throwableWidth / 2.5f;
        pos.z -= levelData.throwableHeight / 1.5f;

        cubeParent.transform.position = pos;
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
