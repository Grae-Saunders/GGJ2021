using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody body;

    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;
    public Vector3 rotationEuler;

    public float runSpeed = 20.0f;

    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down
    }

    void FixedUpdate()
    {
        if (horizontal == 0 && vertical == 0) // Check for diagonal movement
        {
            body.velocity = Vector3.zero;
        }
        else
        {

            if (horizontal != 0 && vertical != 0) // Check for diagonal movement
            {
                // limit movement speed diagonally, so you move at 70% speed
                horizontal *= moveLimiter;
                vertical *= moveLimiter;
            }
            var vel = new Vector3(horizontal * runSpeed, 0, vertical * runSpeed);
            body.velocity = vel;
            RotateBody(vel); 
        }
    }

    private void RotateBody(Vector3 targetPosition)
    {
        var angle = Mathf.Atan2(-targetPosition.z, targetPosition.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);


        //Vector3 lookDir = transform.position - targetPosition;
        //lookDir.y = 0;
        //transform.rotation = Quaternion.LookRotation(lookDir);


        rotationEuler = transform.rotation.eulerAngles;
    }
}
