using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Public variables --------------->

    //Player controller object
    public CharacterController PlayerController;


    //Location of the ground
    public Transform groundCheck;
    //How far to check
    public float groundDistance = 0.4f;
    //What counts as ground
    public LayerMask groundMask;



    //Jump height
    public float jumpHeight = 2.5f;

    //Player movement speed
    public float playerSpeed = 10f;

    //Gravity (used for how fast the player falls)
    public float gravity = -20f;

    //--------------------------------<

    //Private Variables-------------->
    //Player falling speed
    Vector3 velocity;

    //Is player touch the ground
    bool isGrounded;

    //-------------------------------<

    // Update is called once per frame
    void Update()
    {
        //Check if player is touching the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded & velocity.y < 0)
        {
            //Not 0 just to make sure the player is not floating above the ground
            velocity.y = -2f;
        }

        //Left Right
        float x = Input.GetAxis("Horizontal");
        //Forward Backwards
        float z = Input.GetAxis("Vertical");

        //Assign direction to move towards
        Vector3 move = transform.right * x + transform.forward * z;

        //Move player
        PlayerController.Move(move * playerSpeed * Time.deltaTime);

        //Check if spacebar is pressed and player is not flying
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            //Jump
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        //Keep falling
        velocity.y += gravity * Time.deltaTime;

        //This move call here Allows to change direction mid-air
        PlayerController.Move(velocity * Time.deltaTime);
    }
}
