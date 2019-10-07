using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UtmostInput;

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

    
    private InputX inputX;

    Vector3 p0;
    Vector3 p1;
    Vector3 p2;
    Vector3 p3;

   
    bool isTouchEnded;

    float touchDeltaX;

    private IEnumerator routeFollower; //GoByTheRoute corountine i buna esitleniyor sonra bu corountine stoplaniyor

    void Start()
    {
        
        inputX = new InputX();

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
                touchDeltaX = 0;
            }
            else
            {
<<<<<<< Updated upstream
                touchDeltaX = (gInput.currentPosition.x - touchStartPos.x) / (Screen.width);
                
                //Debug.Log("TouchDeltax = " + touchDeltaX);
                if (coroutineAllowed)
                {
                    routeFollower = GoByTheRoute(touchDeltaX, gInput);
                    StartCoroutine(routeFollower);
                }
=======
               // touchDeltaX = (gInput.currentPosition.x - touchStartPos.x) / (Screen.width);
                touchDelta = (gInput.currentPosition - touchStartPos) / (Screen.width);

                GoByTheRoute(touchDelta);
                
>>>>>>> Stashed changes
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

    IEnumerator GoByTheRoute(float alphaX,GeneralInput currentGInput)
    {

        coroutineAllowed = false;

        if(touchDeltaX < 0)
        {

            while (currentGInput.phase != IPhase.Ended /*&& tParam >= touchDeltaX*/) // bu touchdeltax leri acınca parmağını durdurduğun yerde duruyo. ama oyunu biraz durağanlastırıyo
            {
               // Debug.Log("tParam = " + tParam + " touchDeltaX = " + touchDeltaX);
                tParam += speedModifier * Time.deltaTime * touchDeltaX;
                tParam = Mathf.Clamp01(tParam);

                SetGunPosition();

                yield return new WaitForEndOfFrame();
            }
        }
        else if (touchDeltaX > 0)
        {
            
            while (/*tParam <= touchDeltaX && */currentGInput.phase != IPhase.Ended )
            {
               // Debug.Log("tParam = " + tParam + " touchDeltaX = " + touchDeltaX);
                tParam += speedModifier * Time.deltaTime * touchDeltaX;
                tParam = Mathf.Clamp01(tParam);

                SetGunPosition();

                yield return new WaitForEndOfFrame();
            }
        }

        coroutineAllowed = true;
        StopCoroutine(routeFollower);
    }

    void CreateWind(float windForce)
    {
       
        GameObject wind = ObjectPooler.instance.GetPooledObject(DataScript.windObjects);
        if (wind!=null)
        {
            wind.SetActive(true);
            wind.GetComponent<WindPhysicsScript>().CreateWind(transform, lookPosition);
        }
        
    }

<<<<<<< Updated upstream
    IEnumerator StartPowerSliderAnimation( )
    {
        float powerValue = Random.Range(0,100f);

        while (!isTouchEnded)
        {
            powerValue = Mathf.PingPong(Time.time*50, 100);
            
            powerSlider.value = powerValue;
            powerSlider.GetComponent<GunSliderScript>().SetSliderPowerColors();
            yield return new WaitForSecondsRealtime(0.02f);
=======
    IEnumerator Fire()
    {
        while(!isTouchEnded)
        {
            CreateWind(100f);
            yield return new WaitForEndOfFrame();
>>>>>>> Stashed changes
        }
        StopCoroutine(Fire());
    }
<<<<<<< Updated upstream
=======

   

>>>>>>> Stashed changes
}
