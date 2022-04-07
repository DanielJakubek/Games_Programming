using UnityEngine;

/*
    Parent class to other resources
    Holds the common properties of other items and
    makes the entity spin and go up and down.
*/
public class Resource : MonoBehaviour
{   
    [Header("Resource properties")]
    public float smoothSpeed = 75f;
    
    private float bobSpeed = 2f; //How fast the bob happens
    private float bobValue = 0.1f; //How many times the bob occurs
    private float startingPosition = 1.2f; //The default transform y position
    private float metronome; //Used to keep the beat of the 

    // Update is called once per frame
    private void FixedUpdate(){

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

    /*
        Plays a sound when this object is picked up and then checks what
        value to update, currently updates the health or armour
    */
    public void UpdateResource(){

        AudioManager.mngInstance.PlaySound("PickUp", AudioManager.mngInstance.sounds); //Pick up sound
        Destroy(gameObject);
    }
}
