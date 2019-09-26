using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    Vector2 StartingPoint;
    Vector2 DraggedPoint;

    GameObject startingPointIndicator;
    Vector3 indicatorPosition;

    //DirectionZone allowed max and min input zone. Parmak bastıktan sonra sağa ve sola inputun maximum alacağı konum. 
    float DirectionZone; //When player presses the position is counted as zero and directionZone diveded to two for calculating direction of object

    float CurrentDirection; // For holding direction value. When player removes finger this value will start next input state resume from where it lefts

    public InputManager(float InputBuffer)// InputBuffer ekranın genişliğinin üçte biri bu alana giriyor
    {
        IState = State.Idle;
        StartingPoint = Vector2.zero;
        DraggedPoint = Vector2.zero;

        startingPointIndicator = GameObject.Instantiate((GameObject)Resources.Load("Prefabs/Startingpoint"));
        startingPointIndicator.SetActive(false);
        indicatorPosition = new Vector3(0f, 0f, Camera.main.transform.position.z);
        DirectionZone = InputBuffer;
        Debug.Log("Input zone is : " + DirectionZone);
    }


    public enum State
    {
        Idle,
        Dragging,
        Release,
        Canceled
    }

    State IState;

    public Vector2 GetDragVector()
    {
        return StartingPoint - DraggedPoint;
    }

    //Gives 1 for maximum left and minus -1 for maximum right
    public Vector2 GetDirection()
    {
        Vector2 direction = Vector2.up;

        float diff = StartingPoint.x - DraggedPoint.x;
        CurrentDirection = diff;

        if (Mathf.Abs(diff) > DirectionZone / 2f)
        {
            direction.x = diff / Mathf.Abs(diff);
        }
        else
        {
            direction.x = diff / (DirectionZone / 2f);
        }

        return direction;
    }

    public State GetInputState()
    {
        switch (IState)
        {
            case State.Idle:

                if (ExtendedInput.isInputEntered())
                {
                    IState = State.Dragging;

                    StartingPoint = ExtendedInput.GetPoint();
                    DraggedPoint = StartingPoint;


                    if (startingPointIndicator)
                    {
                        indicatorPosition.x = StartingPoint.x;
                        indicatorPosition.y = StartingPoint.y;

                        indicatorPosition = Camera.main.ScreenToWorldPoint(indicatorPosition);
                        indicatorPosition.z = 0f;

                        startingPointIndicator.transform.position = indicatorPosition;
                        startingPointIndicator.SetActive(true);
                    }

                }

                break;
            case State.Dragging:

                if (ExtendedInput.isInputExited())
                {
                    IState = State.Release;
                }

                DraggedPoint = ExtendedInput.GetPoint();

                break;
            case State.Release:

                StartingPoint = Vector2.zero;
                DraggedPoint = Vector2.zero;

                IState = State.Idle;

                if (startingPointIndicator)
                {
                    startingPointIndicator.SetActive(false);
                }

                break;
            default:

                Debug.LogError("Unsupported input state");

                break;
        }

        return IState;
    }


    class ExtendedInput
    {
#if UNITY_EDITOR || UNITY_IPHONE
        public static bool isInputEntered()
        {
            if (Input.GetMouseButtonDown(0))
                return true;

            return false;
        }

        public static bool isInput()
        {
            if (Input.GetMouseButton(0))
                return true;

            return false;
        }

        public static bool isInputExited()
        {
            if (Input.GetMouseButtonUp(0))
                return true;

            return false;
        }

        public static Vector2 GetPoint()
        {
            return Input.mousePosition;
        }
#elif UNITY_IPHONE && !UNITY_EDITOR
        public static bool isInputEntered()
        {
            if (Input.touchCount > 0)
                return true;

            return false;
        }

        public static bool isInput()
        {
            if (Input.touchCount > 0)
                return true;

            return false;
        }


        public static bool isInputExited()
        {
            if (Input.touchCount <= 0)
                return true;

            return false;
        }

        public static Vector2 GetPoint()
        {
            return Input.touches[0].position;
        }
#endif
    }
}
