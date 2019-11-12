using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelData
{
    public struct Hole
    {
        public Vector3 position;
        public Color32 color;
        public int colliderRound;

        public Hole(Vector3 position,Color32 color,int r)
        {
            this.position = position;
            this.color = color;

            colliderRound = r;

        }
    }

    public static LevelData levelData;
    int level;

    private BillboardMapper Billboard;
    private Texture2D lvlSprite;
    private CubeSetter cubeSetter;
    private ColorData cData;

    public Vector3 platformPos = GameObject.FindGameObjectWithTag("Platform").transform.position; //Altigenin en tepesi

    public List<GameObject> throwableCubes;
    public List<GameObject> holeCubes;
    public List<GameObject> windObjects;

    public Vector3 initialWindScale;

    public Queue<int> vibrationQue;

    //En sonda patlatılan cube lerin hangi konuma alınacağı
    public Vector3 CubeParentBlowPosition; 

    public bool isBlowActive = false;
    public bool isBlown = false;

    public bool isLevelStarted = false;
    public int vibrationStyle = 1;

    public int isVibrationActive;

    float ID;
    public Colliders colliders;
    public struct Colliders
    {
        public List<Collider> AllColliders;

        public int threshold;

        public List<List<Collider>> ColliderMap;

        public int roundCount;
        public int cubePerRound;

        public void changeThreshold(int currentEnteredCount)
        {
            threshold -= (int)(cubePerRound / 5f);
        }

        public Colliders(int cubeCount,int roundCount,int cpr)
        {   
            AllColliders = new List<Collider>(cubeCount);
            ColliderMap = new List<List<Collider>>(roundCount);

            cubePerRound = cpr;
            this.roundCount = roundCount;
            threshold = cubeCount - (int)(cubePerRound / 7f);
   

            Debug.Log("Collider round count is : " + this.roundCount + " Cube per round is : " + cubePerRound + " Threshold is : " + threshold);
        }
    };

    public LevelData(GameObject throwableCube)
    {
        ID = UnityEngine.Random.value;
        level = GetLevel();

        if (levelData == null)
        {
            Debug.Log("Initial level data created ID : " + ID);
            levelData = this;
            //isLevelStarted = false;
        }
        else if (levelData.ID != ID) //that means level has changed reset current leveldata and create new one
        {
            Debug.Log("This is a new level ID : " + ID);
            levelData.ResetLevelData();
            levelData = this;
            //eğer bi sonraki bölümse starting panel i hiç açma.
            //isLevelStarted = true;
        }
        else //Wrong levelData creation
        {
            Debug.LogError("There is a already a level data in current level. Cant create a new one.");
        }

        cube = throwableCube;
        vibrationQue = new Queue<int>();

        vibrationHandler = new VibrationHandler();
    }

    ~LevelData()
    {
        Debug.LogWarning("Level destroyed ID : " + ID);
    }

    void ResetLevelData()
    {
        if (levelData != null)
        {
            levelData = null;
        }
    }

    GameObject cube; //throwableCube
    public List<Vector3> ThrowableCubePositions;
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
        //simdilik dursun diye eger bossa ilk bolume al
        if (lvlSprite == null)
        {
            level = 1;
            PlayerPrefs.SetInt("Level", 1);
            lvlSprite = (Texture2D)Resources.Load("LevelSprites/LvlSp_" + level);
        }

        Billboard = new BillboardMapper(lvlSprite);

        holes = Billboard.spriteMap.holeData;
        mapHeight = Billboard.spriteMap.totHeight;
        mapWidth = Billboard.spriteMap.totWidth;
        holeCount = Billboard.spriteMap.totBlockCount;

        FillerCubes = Billboard.fillerBlocks;

        colliders = new Colliders(holeCount,Billboard.roundCount,Billboard.cubePerRound);
    }

    public CubeSetter.ThrowableConstruction throwableConst;

    public void SetThrowableCubes()
    {
        cubeSetter = new CubeSetter(holeCount, cube.transform.localScale.x);

        ThrowableCubePositions = cubeSetter.cubeConst.cubePoss;
        throwableHeight = cubeSetter.cubeConst.height;
        throwableWidth = cubeSetter.cubeConst.width;
    }

    public ColorData.ColorDataHolder SetColors()
    {
        cData = new ColorData();
        ColorData.ColorDataHolder dataHolder = cData.SetLevelColors(level);

        return dataHolder;
    }

    public void IncreaseWindScale(int round)
    {
        float increaseRate = 1f / (float)colliders.roundCount;

        Vector3 increaseVec = new Vector3(64f, 120f, 0f) * increaseRate;
        levelData.initialWindScale += round * increaseVec; 
    }

    public void IncreaseLevel()
    {
        PlayerPrefs.SetInt("Level", level + 1);
    }

    int GetLevel()
    {
        if (PlayerPrefs.HasKey("Level"))
        {
            level = PlayerPrefs.GetInt("Level");
        }
        //That means it`s first game played
        else
        {
            level = 1;
            PlayerPrefs.SetInt("Level", level);
        }

        return level;
    }

    VibrationHandler vibrationHandler;
    public void SimpleVibration()
    {
        vibrationHandler.vibrate(levelData.vibrationStyle);
    }

    public void CustomVibration(DebugScript.VibrationStyle style)
    {
        vibrationHandler.vibrate((int)style);
    }

}
