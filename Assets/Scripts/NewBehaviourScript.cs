using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardspeed;

    private int desiredLane = 1;//0:left 1:middle 2:right
    public float laneDistance = 4;//the distance between two lanes

    public float jumpForce;
    public float Gravity = -20;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        direction.z = forwardspeed;

        direction.y += Gravity * Time.deltaTime;
        if (controller.isGrounded)
        {

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
            }
            //Gather the inputs on which lane we should be
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
        }
        //Calculate where we should be in the future

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        transform.position = targetPosition;

    }
    private void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
    }
    
    private void Jump()
    {
        direction.y = jumpForce;
    }
}
