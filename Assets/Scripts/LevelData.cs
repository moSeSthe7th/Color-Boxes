using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData
{
    public struct Hole
    {
        public Vector3 position;
        public Color32 color;

        public Hole(Vector3 position,Color32 color)
        {
            this.position = position;
            this.color = color;
        }

    }

    public static LevelData levelData;
    string level;

    private BillboardMapper Billboard;
    private Texture2D lvlSprite;
    private CubeSetter cubeSetter;

    public Vector3 platformPos = GameObject.FindGameObjectWithTag("Platform").transform.position; //Altigenin en tepesi

    public List<GameObject> holeCubes;
    public List<GameObject> windObjects;

    public Queue<int> vibrationQue;

    public bool isBlowActive = false;

    public LevelData(int lvlData,GameObject throwableCube)
    {
        level = lvlData.ToString();
        cube = throwableCube;

        vibrationQue = new Queue<int>();

        if(levelData == null)
        {
            levelData = this;
        }
    }

    GameObject cube; //throwableCube
    public List<Vector3> ThrowableCubes;
    public float throwableWidth;
    public float throwableHeight;

    public List<Hole> holes;
    public List<Vector3> FillerCubes;
    public float mapWidth;
    public float mapHeight;
    public int holeCount;

    public void SetBillboard()
    {
        lvlSprite = (Texture2D)Resources.Load("LevelSprites/LvlSp_" + level);
        Billboard = new BillboardMapper(lvlSprite);

        holes = Billboard.spriteMap.holeData;
        mapHeight = Billboard.spriteMap.totHeight;
        mapWidth = Billboard.spriteMap.totWidth;
        holeCount = Billboard.spriteMap.totBlockCount;

        FillerCubes = Billboard.fillerBlocks;
    }

    public CubeSetter.ThrowableConstruction throwableConst;

    public void SetThrowableCubes()
    {
        cubeSetter = new CubeSetter(holeCount, cube.transform.localScale.x);

        ThrowableCubes = cubeSetter.cubeConst.cubePoss;
        throwableHeight = cubeSetter.cubeConst.height;
        throwableWidth = cubeSetter.cubeConst.width;
    }

}
