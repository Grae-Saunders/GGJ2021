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

    public bool hasControl = true;
    bool hasFoundTreasure;
    Transform treasure;
    public float landingSpeed;


    public GameManager manager;

    new Collider collider;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    void Update()
    {
        if (hasFoundTreasure)
        {
            MoveTowardTreasure();
            return;
        }
        if (!hasControl)
            return;
        if (horizontal == 0 && vertical == 0) // Check for diagonal movement
        {
            body.velocity = Vector3.zero;
        }
        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down
    }

    void FixedUpdate()
    {
        if (!hasControl)
            return;
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

        rotationEuler = transform.rotation.eulerAngles;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Treasure")
        {
            Debug.Log("win");
            hasControl = false;
            hasFoundTreasure = true;
            manager.WinCondition();
            collider.enabled = false;
            treasure = other.transform.parent;
            body.velocity = Vector3.zero;

        }
        if (other.tag == "Hazard")
        {
            manager.GameOver();
        }
    }

    private void MoveTowardTreasure()
    {

        transform.position = Vector3.Lerp(transform.position, treasure.position, Time.deltaTime * landingSpeed);
        RotateBody(transform.position - treasure.position);
    }
    public void NewRoundOrRestart()
    {
        hasControl = true;
        hasFoundTreasure = false;
        collider.enabled = true;

    }
}
