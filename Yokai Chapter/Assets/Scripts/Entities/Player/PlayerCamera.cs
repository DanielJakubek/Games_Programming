using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Player Settings")]
    [Range(0f,400f)]
    public float mouseSen = 500f; //The sensitivty of the mouse


    [Header("Player Object")]
    public Transform thePlayer; //Stores the player gameObject

    float xAxis, yAxis; //Mouse axis used for camera movement.
    float rotateXAxis = 0f;


    // Start is called before the first frame update
    private void Awake() {
        
        //Gets the player by searching for the player tag and finds its Transform.
        thePlayer = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Transform>();

        //Assigns the character object to the transfrom variable
        Cursor.lockState = CursorLockMode.Locked; //Locks the cursor and makes it invisible.
    }

    // Update is called once per frame
    void Update(){
        
        MouseControl();
    }



    /*  
        Used to get the player mouse inputs and then it roates the camera and player 
        respectviely.
    */
    private void MouseControl(){

        //Gets the player mouse movement
        xAxis = Input.GetAxis("Mouse X") * mouseSen * Time.deltaTime;
        yAxis = Input.GetAxis("Mouse Y") * mouseSen * Time.deltaTime;

        //Make the -/+ flips the y
        rotateXAxis = rotateXAxis - yAxis;

        //Locks the rotation so that we can't go 360 around the player
        rotateXAxis = Mathf.Clamp(rotateXAxis, -90f, 90f);

        //Rotates the object relative to its parent object
        transform.localRotation = Quaternion.Euler(rotateXAxis, 0f, 0f);
        thePlayer.Rotate(Vector3.up * xAxis);
    }
}
