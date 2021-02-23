using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    //Public Variales --------------->
    //Camera view speed
    public float cameraSensitivity = 5f;
    
    //Player
    public Transform playerBody;
    //-------------------------------<

    //Private variables------------->
    // Camera Up/Down
    float xRotation = 0f;

    //------------------------------<

    // Start is called before the first frame update
    void Start()
    {
        //Locks cursor during Editor playmode
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Left Right
        float mouseX = Input.GetAxis("Mouse X") * cameraSensitivity + Time.deltaTime;
        //Up down
        float mouseY = Input.GetAxis("Mouse Y") * cameraSensitivity + Time.deltaTime;

        //Not inversed: "-=" || inversed: "+="
        xRotation -= mouseY;

        //Restrict Up and down rotation to -90 and 90 degrees
        xRotation = Mathf.Clamp(xRotation,-90, 90);


        //Rotate camera up and down
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //Rotate player left and right
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
