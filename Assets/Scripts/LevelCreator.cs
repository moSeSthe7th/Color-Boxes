﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    public Transform billboardCubeParent;
    Transform bilboard;

    private LevelData levelData;

    public GameObject holeCube;

    GameObject windParent;
    public GameObject wind;

    GameObject BillboardFillerBlock;

    GameObject cubeParent;
    GameObject throwableCube;

    void Start()
    {
        
        QualitySettings.vSyncCount = 1;
        Time.timeScale = 1f;

        cubeParent = new GameObject("CubeParent");
        cubeParent.tag = "CubeParent";
        throwableCube = (GameObject)Resources.Load("Prefabs/ThrownObject");

        levelData = new LevelData( throwableCube);
        Time.timeScale = 3f;
       
        BillboardFillerBlock = (GameObject)Resources.Load("Prefabs/BillboardFillerBlock");

        windParent = new GameObject("WindParent");
        levelData.windObjects = ObjectPooler.instance.PooltheObjects(wind, 100, windParent.transform);

        bilboard = GameObject.FindGameObjectWithTag("Bilboard").transform;
        SetLevelColors();
        CreateLevel();
    }

    void SetLevelColors()
    {
        ColorData.ColorDataHolder holder = levelData.SetColors();

        foreach(Renderer r in bilboard.GetComponentsInChildren<Renderer>())
        {
            r.material.SetColor("_BaseColor", holder.billboardColor);
        }

        BillboardFillerBlock.GetComponent<Renderer>().sharedMaterial.SetColor("_BaseColor", holder.billboardColor);
        throwableCube.GetComponent<Renderer>().sharedMaterial.SetColor("_BaseColor", holder.throwableCubeColor);

        GameObject.FindGameObjectWithTag("hexagonGround").GetComponent<Renderer>().material.SetColor("_BaseColor", holder.platformColor);
    }

    void CreateLevel()
    {
        //Create picture on billboard
        levelData.SetBillboard();
        CreateBillboard();
        PositionHoleCubes();

        //CreateThrowableCubes
        levelData.SetThrowableCubes();
        CreateThrowableCubes();
        PositionThrowableCubes();
    }


    void CreateBillboard()
    {
        List<GameObject> hCubes = new List<GameObject>(levelData.holeCount);

        foreach(LevelData.Hole selectedHole in levelData.holes)
        {
            GameObject currentHole = Instantiate(holeCube, selectedHole.position, Quaternion.identity,billboardCubeParent);
            currentHole.GetComponent<HoleCubeScript>().holeColor = selectedHole.color;

            hCubes.Add(currentHole);
        }

        levelData.holeCubes = hCubes;

        foreach (Vector3 pos in levelData.FillerCubes)
        {
            GameObject currfiller = Instantiate(BillboardFillerBlock,pos,Quaternion.identity,billboardCubeParent);
        }
       
    }
   
    void PositionHoleCubes()
    {
        Transform board = bilboard.transform.GetChild(1);
        Vector3 tmpVec = new Vector3(0f, board.position.y, bilboard.transform.position.z);

        tmpVec.x -= (levelData.mapWidth / 2f) - (holeCube.transform.localScale.x / 2f); 
        tmpVec.y -= (levelData.mapHeight / 2f) + (holeCube.transform.localScale.y ); 

        billboardCubeParent.position = tmpVec;
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
