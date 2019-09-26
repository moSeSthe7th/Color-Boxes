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
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        iManager = new InputManager(Screen.width / 3f);
    }

    void Update()
    {
        iState = iManager.GetInputState();

        if(iState == InputManager.State.Dragging)
        {
            dragVec = iManager.GetDragVector();
            movementVec = CalculateMoveVector(-dragVec); // minus because drag vector calculated by subtracting current pos from starting point

            rb.AddForce(movementVec * Time.deltaTime, ForceMode.VelocityChange);

            //transform.position += movementVec;

           /* Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Moved)
            {
                Vector2 touchVector = touch.deltaPosition;
                Vector3 movementVec = new Vector3();

                movementVec = new Vector3(transform.position.x + touchVector.x, transform.position.y, transform.position.z + touchVector.y);

                transform.position = Vector3.Lerp(transform.position, movementVec, tornadoSpeed * Time.deltaTime / 15f);
            }*/
        }


        Vector3 CalculateMoveVector(Vector2 dv)
        {
            Vector3 tmp = new Vector3(dv.x, 0f, dv.y) * (tornadoSpeed / 100f);
            tmp.x = (Mathf.Abs(tmp.x) > maxSpd) ? Mathf.Sign(tmp.x) * maxSpd : tmp.x;
            tmp.z = (Mathf.Abs(tmp.z) > maxSpd) ? Mathf.Sign(tmp.z) * maxSpd : tmp.z;

            return tmp;
        }
    }
}
