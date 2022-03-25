using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interact
{   
    private bool isOpen; //Is door open

    //Called once at the start before the frame
    private void Start() {
        isOpen = false;
        
        description = "Press 'E' To Open";
    }

    /* Deals with opening the door, if it is closed, the door will be moved upwards */
    public override void DoInteract(){

        if(!isOpen)
            transform.position = new Vector3(transform.position.x, transform.position.y+4, transform.position.z);

        isOpen = !isOpen;
    }
}
