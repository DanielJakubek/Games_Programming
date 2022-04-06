using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{   
    [Header("Resource properties")]
    public float smoothSpeed = 75f;
    
    private float bobSpeed = 2f; //How fast the bob happens
    private float bobValue = 0.1f; //How many times the bob occurs
    private float startingPosition = 1.2f; //The default transform y position
    private float metronome; //Used to keep the beat of the 

    // Update is called once per frame
    void FixedUpdate(){

        //Rotates the object on the y axis
        transform.Rotate(Vector3.up * Time.deltaTime*smoothSpeed); 

        //Updates the timer to make sure the speed is consistent
        metronome+= Time.deltaTime * bobSpeed;

        //Calculates the position of the bob via sine wave and then stores creates a new vector 3 with the new y position (1 or -1)
        float angle = startingPosition + Mathf.Sin(metronome) * bobValue;
        Vector3 temp = new Vector3(transform.localPosition.x, angle, transform.localPosition.z);

        //Moves the object up and down.
        transform.localPosition = temp;
    }
}
