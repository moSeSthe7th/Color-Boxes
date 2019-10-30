using System.Collections;
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

    public Material forceFieldMaterial;

    void Start()
    {
        
        QualitySettings.vSyncCount = 1;
        Time.timeScale = 1f;

        cubeParent = (GameObject)Resources.Load("Prefabs/CubeParent");
        cubeParent = Instantiate(cubeParent);

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
            r.material.SetColor("_EmissionColor", holder.emissionBillboardColor);
        }

        throwableCube.GetComponent<Renderer>().sharedMaterial.SetColor("_BaseColor", holder.throwableCubeColor);

        Material billboardFiller = BillboardFillerBlock.GetComponent<Renderer>().sharedMaterial;
        billboardFiller.SetColor("_BaseColor", holder.billboardColor);
        billboardFiller.SetColor("_EmissionColor", holder.emissionBillboardColor);

        Material PlatformMaterial = GameObject.FindGameObjectWithTag("hexagonGround").GetComponent<Renderer>().material;
        PlatformMaterial.SetColor("_BaseColor", holder.platformColor);
        PlatformMaterial.SetColor("_EmissionColor", holder.emissionPlatformColor);

        Material fogMaterial = GameObject.FindGameObjectWithTag("Fog").GetComponent<Renderer>().material;
        fogMaterial.SetColor("_BaseColor", holder.fogColor);

        Material gunMaterial = GameObject.FindWithTag("Gun").GetComponent<Renderer>().sharedMaterial;
        gunMaterial.SetColor("_BaseColor", holder.gunColor);

        forceFieldMaterial.SetColor("Color_CDE56FBB", holder.forceFieldColor);
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

        //Set cube parents position for blowing
        SetCubeParentBlowPosition();
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

    void SetCubeParentBlowPosition()
    {
        //z position is -250
        Vector3 blowPos = new Vector3(-20f, 900f, -250f);

        blowPos.y += (levelData.mapHeight); //- 
        //blowPos.x -= (levelData.mapWidth / 16f); //billboardCubeParent.position.x; //- 
        blowPos.x = (levelData.mapWidth / 32f > 5f) ? blowPos.x - levelData.mapWidth / 32f : blowPos.x;
        blowPos.y = (blowPos.y > 1000) ? 1000 : blowPos.y;


        levelData.CubeParentBlowPosition = blowPos;
    }


    void CreateThrowableCubes()
    {
        List<GameObject> tCubes = new List<GameObject>(levelData.holeCount);

        foreach (Vector3 cubePos in levelData.ThrowableCubePositions)
        {
            GameObject currCube = Instantiate(throwableCube, cubePos, Quaternion.identity,cubeParent.transform);
            tCubes.Add(currCube);
        }

        levelData.throwableCubes = tCubes;

    }

    void PositionThrowableCubes()
    {
        Vector3 pos = levelData.platformPos;
        pos.x -= levelData.throwableWidth / 2.5f;
        pos.z -= levelData.throwableHeight / 1.5f;

        cubeParent.transform.position = pos;
    }

}
