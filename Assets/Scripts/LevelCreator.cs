using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    public Transform holeCubeParent;
    Transform bilboard;

    private LevelData levelData;

    public GameObject holeCube;
    public GameObject wind;

    List<Vector3> holeCubePositions = new List<Vector3>();

    void Start()
    {

        DataScript.windObjects = ObjectPooler.instance.PooltheObjects(wind, 100);

        levelData = new LevelData();
        Time.timeScale = 3f;

        bilboard = GameObject.FindGameObjectWithTag("Bilboard").transform;
        Debug.Log(bilboard.lossyScale + " " + bilboard.localScale);
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

        DataScript.totalHoleCount = levelData.holes.Count;
        DataScript.remainingHoleColliderIncreaseThreshold = Mathf.RoundToInt(DataScript.totalHoleCount * 0.9f);
        
       
    }
    //- bilboard.transform.localScale.z
    void PositionHoleCubes()
    {
        Transform board = bilboard.transform.GetChild(0);
        Vector3 tmpVec = new Vector3(0f, board.position.y, bilboard.transform.position.z - board.localScale.y + 10f);

        tmpVec.x -= (levelData.mapWidth / 2f) - (holeCube.transform.localScale.x / 2f); 
        tmpVec.y -= (levelData.mapHeight / 2f) + (holeCube.transform.localScale.y ); 

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
