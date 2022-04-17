using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Deals with player and door interaction
*/
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

    /* Closes the door and makes it so it cannot be opened */
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

    //What is shown when the player hovers over an interactable item
    private void OnMouseOver() {

        //Only displays when player is within range
        if(Vector3.Distance(Player.playerInstance.transform.position, transform.position) <= 7f)
            descriptionText.text = description.ToString();
        else
            OnMouseExit();
    }

    //What is shown when the player hovers over an interactable item
    private void OnMouseExit() {
        descriptionText.text = "".ToString();
    }


    //Unsubscribe from event upon destruction
    private void OnDestroy() {
        EventManager.eventMngr.closeDoors -= DoInteract; //Subs function to event
    }
}
