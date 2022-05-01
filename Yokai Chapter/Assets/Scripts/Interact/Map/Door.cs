using UnityEngine;

///<summary>
///Deals with player and door interaction
///</summary>
public class Door : Interact
{   
    private bool isOpen; //Is door open
    private bool canOpen; //Can the door be opened

    //Called once at the start before the frame
    private void Start() {
        isOpen = false;
        canOpen = true;
        description = "Press 'E' To Open";

        EventManager.eventMngr.closeDoors += CloseDoor; //Subs function to event
    }

    ///<summary>
    ///Closes the door and makes it so it cannot be opened 
    ///</summar>
    public void CloseDoor(){
        canOpen = !canOpen;
        
        if(isOpen)
            transform.position = new Vector3(transform.position.x, transform.position.y-4, transform.position.z);         
    }

    /* Deals with opening the door, if it is closed, the door will be moved upwards */
    public override void DoInteract(){

        //Moves the door up and make it open
        if(!isOpen && canOpen)
            transform.position = new Vector3(transform.position.x, transform.position.y+4, transform.position.z);
       
        isOpen = !isOpen;
    }

    //Unsubscribe from event upon destruction
    private void OnDestroy() {
        EventManager.eventMngr.closeDoors -= CloseDoor; //un-Subs function to event
    }
}
