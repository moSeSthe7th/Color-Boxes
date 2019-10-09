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
    private bool coroutineAllowed;

    public Vector3 lookPosition;
    public float rotationSpeed;

    private Vector2 touchStartPos;
    private Vector2 touchCurrentPos;

    private InputX inputX;

    Vector3 Xp0;
    Vector3 Xp1;
    Vector3 Xp2;
    Vector3 Xp3;

    Vector3 Yp0;
    Vector3 Yp1;
    Vector3 Yp2;
    Vector3 Yp3;

   
    bool isTouchEnded;

    Vector2 touchDelta;

    //float touchDeltaX;

    private IEnumerator routeFollower; //GoByTheRoute corountine i buna esitleniyor sonra bu corountine stoplaniyor

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
        coroutineAllowed = true;

        SetGunPosition();
    }
    
    void Update()
    {
        if(inputX.IsInput())
        {
            GeneralInput gInput = inputX.GetInput(0);
          
            if(gInput.phase == IPhase.Began)
            {
                isTouchEnded = false;
                StartCoroutine(Fire());
                touchStartPos = gInput.currentPosition;
            }
            else if(gInput.phase == IPhase.Ended)
            {
                isTouchEnded = true;

                if(routeFollower!=null)
                    StopCoroutine(routeFollower); //Corountine inputla beraber bitmediginden burada durduruluyor. yukarda alinan input corountine sokuluyor ama sonradan editlenmiyor. 

                coroutineAllowed = true;
             
                touchDelta = Vector2.zero;
            }
            else
            {
               
                touchDelta = (gInput.currentPosition - touchStartPos) / (Screen.width);
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

        gunPosition = xPos + yPos;

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


    void CreateWind()
    {
        GameObject wind = ObjectPooler.instance.GetPooledObject(DataScript.windObjects);
        if (wind != null)
        {
            wind.SetActive(true);
            wind.GetComponent<WindPhysicsScript>().CreateWind(transform, lookPosition);
        }
    }

    IEnumerator Fire()
    {
        while (!isTouchEnded)
        {
            CreateWind();
            yield return new WaitForEndOfFrame();
        }
        StopCoroutine(Fire());
    }

         
}
