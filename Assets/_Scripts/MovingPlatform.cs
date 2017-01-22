using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
    public Transform movingPlatform;
    public Transform position1;
    public Transform position2;
    public Vector3 newPosition;
    private State? currentState = null;
    public float smooth;
    public float resetTime;

    void Start()
    {
        ChangeTarget();
    }

    void FixedUpdate()
    {
        movingPlatform.position = Vector3.Lerp(movingPlatform.position, newPosition, smooth * Time.deltaTime);
    }

    void ChangeTarget()
    {
        if (currentState.HasValue && currentState.Value == State.MovingToPosition1)
        {
            currentState = State.MovingToPosition2;
            newPosition = position2.position;
        }
        else if (currentState.HasValue && currentState == State.MovingToPosition2)
        {
            currentState = State.MovingToPosition1;
            newPosition = position1.position;
        }
        else
        {
            currentState = State.MovingToPosition2;
            newPosition = position2.position;
        }
        Invoke("ChangeTarget", resetTime);
    }

    private enum State
    {
        MovingToPosition1,
        MovingToPosition2,
    }
}
