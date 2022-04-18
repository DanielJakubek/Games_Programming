using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Deals moving the camera where the player is looking
    but also shaking the camera when something happens
*/
public class PlayerCamera : MonoBehaviour
{
    [Header("Player Settings")]
    [Range(0f,400f)]
    public float mouseSen = 500f; //The sensitivty of the mouse

    [Header("Player Object")]
    public Transform thePlayer; //Stores the player gameObject

    private float xAxis, yAxis; //Mouse axis used for camera movement.
    private float rotateXAxis = 0f;

    [Header("Camera Shake")]
    private Vector3 startPosition; //Starting position of the camera
    private float strength = 0.1f; //How extreme the camera shake is
    private bool shake = false; //Should the camera shake or not

    // Awake is called before the first frame update
    private void Awake() {
        Cursor.lockState = CursorLockMode.Locked; //Locks the cursor and makes it invisible.
    }

    // Start is called before the first frame update
    private void Start() {
        //Gets the player transform
        thePlayer = Player.playerInstance.transform;

        //Subscribes functions to events
        EventManager.eventMngr.startCameraShake += ShakeOn;
        EventManager.eventMngr.stopCameraShake += ShakeOff;
    }

    // Update is called once per frame
    void Update(){
        MouseControl();

        if(shake)
            StartCoroutine(ShakeCam());
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


    /*
        Shakes the camera while shake is true. Does
        this by getting a random number between one and minus one 
        and setting the camera's x/y to that random value.
        After the shaking is done, the camera is reverted back to
        its original position.

        Inspired by: https://youtu.be/9A9yj8KnM8c
    */
    IEnumerator ShakeCam(){

        //Gets the starting position of the camera
        startPosition = transform.localPosition;
  
        while(shake){
            float x = Random.Range(-1f, 1f) * strength;
            float y = Random.Range(-1f, 1f) * strength;

            transform.localPosition = new Vector3(x,y,startPosition.z);
            yield return null;
        }

        transform.localPosition = startPosition;  
    }

    /*
        Tells the camera to start shaking
        Parameter: float Strength, how extreme the shake is
    */
    public void ShakeOn(float strength){
        shake = true;
        this.strength = strength;
    }

    /* Tells the camera to stop shaking */
    public void ShakeOff(){
        shake = false;
    }

    /* When this object is destroyed */
    private void OnDestroy() {
        EventManager.eventMngr.startCameraShake -= ShakeOn;
    }
}
