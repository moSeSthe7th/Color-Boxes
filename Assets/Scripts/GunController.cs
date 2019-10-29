using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UtmostInput;

public class GunController : MonoBehaviour
{
    [SerializeField]
    private Transform Xroute;
    [SerializeField]
    private Transform Yroute;

    private float tParamX;
    private float tParamY;
    private Vector3 gunPosition;
    public float speedModifier;

    public Vector3 lookPosition;

    private Vector2 touchStartPos;
    private InputX inputX;

    Vector3 Xp0;
    Vector3 Xp1;
    Vector3 Xp2;
    Vector3 Xp3;

    Vector3 Yp0;
    Vector3 Yp1;
    Vector3 Yp2;
    Vector3 Yp3;

   
    bool isTouch;
    Vector2 touchDelta;

    private float engineHeat; //bu degere gore ilk basta az donmesi ve kucuk ruzgar atmasi saglaniyor. Min 0 max 1
    private float rotationSpeed;

    Coroutine fireLoop;

    void Start()
    {
        inputX = new InputX();

        touchDelta = Vector2.zero;

        Xp0 = Xroute.GetChild(0).position;
        Xp1 = Xroute.GetChild(1).position;
        Xp2 = Xroute.GetChild(2).position;
        Xp3 = Xroute.GetChild(3).position;

        Yp0 = Yroute.GetChild(0).position;
        Yp1 = Yroute.GetChild(1).position;
        Yp2 = Yroute.GetChild(2).position;
        Yp3 = Yroute.GetChild(3).position;

        tParamX = 0.5f;
        tParamY = 0.5f;
        speedModifier = 15f;

        isTouch = false;
        engineHeat = 0f;
        rotationSpeed = 10f;

        SetGunPosition();
    }
    
    void Update()
    {

       // if (!LevelData.levelData.isLevelStarted)
         //   return;

        if (inputX.IsInput())
        {

            GeneralInput gInput = inputX.GetInput(0);
          
            if(gInput.phase == IPhase.Began)
            {
                isTouch = true;

                if (fireLoop != null)
                    StopCoroutine(fireLoop);        
    
                fireLoop = StartCoroutine(Fire());
                touchStartPos = gInput.currentPosition;
            }
            else if(gInput.phase == IPhase.Ended)
            {
                isTouch = false;             
                touchDelta = Vector2.zero;
            }
            else
            {
                touchDelta = (gInput.currentPosition - touchStartPos) / (Screen.width / 2f);
                touchStartPos = gInput.currentPosition;
                GoByTheRoute(touchDelta);
            }
        }

    }

    void SetGunPosition()
    {
        Vector3 xPos = Mathf.Pow(1 - tParamX, 3) * Xp0 +
                    3 * Mathf.Pow(1 - tParamX, 2) * tParamX * Xp1 +
                    3 * (1 - tParamX) * Mathf.Pow(tParamX, 2) * Xp2 +
                    Mathf.Pow(tParamX, 3) * Xp3;

        Vector3 yPos = Mathf.Pow(1 - tParamY, 3) * Yp0 +
                    3 * Mathf.Pow(1 - tParamY, 2) * tParamY * Yp1 +
                    3 * (1 - tParamY) * Mathf.Pow(tParamY, 2) * Yp2 +
                    Mathf.Pow(tParamY, 3) * Yp3;

       
        gunPosition = new Vector3(xPos.x, xPos.y - (xPos.y - yPos.y), (xPos.z + yPos.z) / 2f);
        //gunPosition.y = yPos.y;
        transform.position = gunPosition;

        Quaternion lookRotation = Quaternion.LookRotation(lookPosition - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed);
    }

    public void GoByTheRoute(Vector2 tDelta)
    {
        tParamX += speedModifier * Time.deltaTime * tDelta.x;
        tParamX = Mathf.Clamp01(tParamX);

        tParamY += speedModifier * 1.5f * Time.deltaTime * tDelta.y;
        tParamY = Mathf.Clamp01(tParamY);

        SetGunPosition();
    }


    void CreateWind(float eHeat)
    {
        GameObject wind = ObjectPooler.instance.GetPooledObject(LevelData.levelData.windObjects);
        if (wind != null)
        {
            wind.SetActive(true);
            wind.GetComponent<WindPhysicsScript>().CreateWind(transform, lookPosition,engineHeat);
        }
    }

    IEnumerator Fire()
    {
        bool shooterLoop = true;
        while (shooterLoop)
        {
            //Rotate object in z axis
            transform.GetChild(0).Rotate(Vector3.forward * engineHeat * rotationSpeed, Space.Self);

            if (isTouch)
            {
                engineHeat = (engineHeat < 1f) ? engineHeat + 0.01f : engineHeat;
                CreateWind(engineHeat);
            }
            else
            {
                if(engineHeat < 0.01f)
                {
                    shooterLoop = false;
                }
                
                engineHeat = (engineHeat > 0f) ? engineHeat + -0.01f : engineHeat;
            }

            yield return new WaitForSecondsRealtime(0.008f);
        }
        StopCoroutine(Fire());
    }

         
}
