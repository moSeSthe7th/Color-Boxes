using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    [SerializeField]
    private Transform route;

    private float tParam;
    private Vector3 gunPosition;
    public float speedModifier;
    private bool coroutineAllowed;

    public Vector3 lookPosition;
    public float rotationSpeed;

    private Vector2 touchStartPos;
    private Vector2 touchCurrentPos;

    public GameObject wind;
    private WindPhysicsScript windPhysicsScript;

    Vector3 p0;
    Vector3 p1;
    Vector3 p2;
    Vector3 p3;

    Slider powerSlider;
    bool isTouchEnded;


    float touchDeltaX;
    
    void Start()
    {
        powerSlider = GetComponentInChildren<Slider>();
        windPhysicsScript = FindObjectOfType(typeof(WindPhysicsScript)) as WindPhysicsScript;

        p0 = route.GetChild(0).position;
        p1 = route.GetChild(1).position;
        p2 = route.GetChild(2).position;
        p3 = route.GetChild(3).position;

        tParam = 0.5f;
        speedModifier = 1.5f;
        coroutineAllowed = true;

        SetGunPosition();
    }
    
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
          
            if(touch.phase == TouchPhase.Began)
            {
                isTouchEnded = false;
                StartCoroutine(StartPowerSliderAnimation(touch));
                touchStartPos = touch.position;
            }
            else if(touch.phase == TouchPhase.Ended)
            {
                isTouchEnded = true;
                touchDeltaX = 0;
            }
            else
            {
                touchDeltaX = (touch.position.x - touchStartPos.x) / (Screen.width / 2);
                
                //Debug.Log("TouchDeltax = " + touchDeltaX);
                if (coroutineAllowed)
                    StartCoroutine(GoByTheRoute(touchDeltaX,touch));
            }
        }
    }

    void SetGunPosition()
    {
        gunPosition = Mathf.Pow(1 - tParam, 3) * p0 +
                    3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                    3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                    Mathf.Pow(tParam, 3) * p3;

        transform.position = gunPosition;

        Quaternion lookRotation = Quaternion.LookRotation(lookPosition - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed);
    }

    IEnumerator GoByTheRoute(float alphaX,Touch currentTouch)
    {

        coroutineAllowed = false;

        if(touchDeltaX < 0)
        {

            while (currentTouch.phase != TouchPhase.Ended /*&& tParam >= touchDeltaX*/) // bu touchdeltax leri acınca parmağını durdurduğun yerde duruyo. ama oyunu biraz durağanlastırıyo
            {
                Debug.Log("tParam = " + tParam + " touchDeltaX = " + touchDeltaX);
                tParam += speedModifier * Time.deltaTime * touchDeltaX;
                tParam = Mathf.Clamp01(tParam);

                SetGunPosition();

                yield return new WaitForEndOfFrame();
            }
        }
        else if (touchDeltaX > 0)
        {
            
            while (/*tParam <= touchDeltaX && */currentTouch.phase != TouchPhase.Ended )
            {
                Debug.Log("tParam = " + tParam + " touchDeltaX = " + touchDeltaX);
                tParam += speedModifier * Time.deltaTime * touchDeltaX;
                tParam = Mathf.Clamp01(tParam);

                SetGunPosition();

                yield return new WaitForEndOfFrame();
            }
        }

        coroutineAllowed = true;
    }

    void CreateWind(float windForce)
    {
        //get that force from a slider...
        windPhysicsScript.CreateWind(transform, windForce, lookPosition);
    }

    IEnumerator StartPowerSliderAnimation(Touch currentTouch)
    {
        float powerValue = Random.Range(0,100f);

        while (!isTouchEnded)
        {
            powerValue = Mathf.PingPong(Time.time*50, 100);
            
            powerSlider.value = powerValue;
            powerSlider.GetComponent<GunSliderScript>().SetSliderPowerColors();
            yield return new WaitForSecondsRealtime(0.02f);
        }

        CreateWind(powerSlider.value * 200);
    }
}
