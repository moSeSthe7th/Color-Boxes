using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoController : MonoBehaviour
{
    Rigidbody rb;

    public float tornadoSpeed = 15f;
   

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Moved)
            {
                Vector2 touchVector = touch.deltaPosition;
                Vector3 movementVec = new Vector3();

                movementVec = new Vector3(transform.position.x + touchVector.x, transform.position.y, transform.position.z + touchVector.y);

                transform.position = Vector3.Lerp(transform.position, movementVec, tornadoSpeed * Time.deltaTime / 15f);
            }
        }
    }
}
