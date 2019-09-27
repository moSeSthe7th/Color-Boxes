using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoController : MonoBehaviour
{
    Rigidbody rb;
    InputManager iManager;
    InputManager.State iState = 0;

    [Header("Tornado speed : 1-100")]
    public float tornadoSpeed;
    [Header("Max speed : 1-100")]
    public float maxSpd;

    Vector2 dragVec;
    Vector3 movementVec = Vector3.zero;

    public float maxDeltaX;
    public float maxDeltaY;
    public float minDeltaX;
    public float minDeltaY;
    public float thresholdX;
    public float thresholdY;

    Vector2 startOfTouch;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        iManager = new InputManager(Screen.width / 3f);
    }

    void Update()
    {
       

        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                startOfTouch = touch.position;
            }
            else
            {
                Vector2 touchDelta = touch.position - startOfTouch;
                touchDelta = CalculateDelta(touchDelta);
                
                Vector3 movementVec = new Vector3();

                movementVec = new Vector3(transform.position.x + touchDelta.x, transform.position.y, transform.position.z + touchDelta.y);
               

                transform.position = Vector3.Lerp(transform.position, movementVec, tornadoSpeed * Time.deltaTime / 15f);

            }
            /*if (touch.phase == TouchPhase.Moved)
            {
                Vector2 touchVector = touch.deltaPosition;
                Vector3 movementVec = new Vector3();

                movementVec = new Vector3(transform.position.x + touchVector.x, transform.position.y, transform.position.z + touchVector.y);

                transform.position = Vector3.Lerp(transform.position, movementVec, tornadoSpeed * Time.deltaTime / 15f);
            }*/
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        //Debug.Log("Velocity: " + Vector3.Magnitude(rb.velocity));
        
    }

   

    Vector2 CalculateDelta(Vector2 deltaVec)
    {
        /*if (deltaVec.x > maxDeltaX)
            deltaVec.x = maxDeltaX;
        else if (deltaVec.x < (maxDeltaX * -1))
            deltaVec.x = (maxDeltaX * -1);
        else if (deltaVec.x < minDeltaX && deltaVec.x > 0 && deltaVec.x > thresholdX)
            deltaVec.x = minDeltaX;
        else if (deltaVec.x > (minDeltaX * -1) && deltaVec.x < 0 && deltaVec.x < (thresholdX*-1))
            deltaVec.x = (minDeltaX * -1);
        if (deltaVec.y > maxDeltaY)
            deltaVec.y = maxDeltaY;
        else if (deltaVec.y < (maxDeltaY * -1))
            deltaVec.y = (maxDeltaY * -1);
        else if (deltaVec.y < minDeltaY && deltaVec.y > 0 && deltaVec.y > thresholdY)
            deltaVec.y = minDeltaY;
        else if (deltaVec.y > (minDeltaY * -1) && deltaVec.y < 0 && deltaVec.y < (thresholdY*-1))
            deltaVec.y = (minDeltaY * -1);*/

        Debug.Log("Initial was " + deltaVec.magnitude);
        deltaVec = Vector2.ClampMagnitude(deltaVec, 300f);
        Debug.Log("ClampedTo: " + deltaVec.magnitude);

        return deltaVec;
    }

    /*void SetVelocity()
    {
        Debug.Log("Set Velocity");
        Vector3 velocityVec = rb.velocity;
        rb.AddForce(new Vector3(velocityVec.x * -1f, 0, velocityVec.z * -1f) * 100f);
    }*/

    /*iState = iManager.GetInputState();   put it inside update

       if(iState == InputManager.State.Dragging)
       {
           dragVec = iManager.GetDragVector();
           movementVec = CalculateMoveVector(-dragVec); // minus because drag vector calculated by subtracting current pos from starting point

           rb.AddForce(movementVec * Time.deltaTime, ForceMode.VelocityChange);*/



    //transform.position += movementVec;

    /*Vector3 CalculateMoveVector(Vector2 dv)
    {
        Vector3 tmp = new Vector3(dv.x, 0f, dv.y) * (tornadoSpeed / 100f);
        tmp.x = (Mathf.Abs(tmp.x) > maxSpd) ? Mathf.Sign(tmp.x) * maxSpd : tmp.x;
        tmp.z = (Mathf.Abs(tmp.z) > maxSpd) ? Mathf.Sign(tmp.z) * maxSpd : tmp.z;

        return tmp;
    }*/

}
