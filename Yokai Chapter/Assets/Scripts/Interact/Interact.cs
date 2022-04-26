using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
    Parent class to all the other interaction 
    classes
*/
public class Interact : MonoBehaviour
{
    public string description; //Description of the interactable
    public TextMeshProUGUI descriptionText; // The text component for the health

    public virtual void DoInteract(){
        //Do stuff
    }

    //What is shown when the player hovers over an interactable item
    private void OnMouseOver() {

        //Only displays when player is within range
        if(Vector3.Distance(Player.playerInstance.transform.position, transform.position) <= 7f && descriptionText !=null)
            descriptionText.text = description.ToString();
        else
            OnMouseExit();
    }

    //What is shown when the player hovers over an interactable item
    private void OnMouseExit() {
        if(descriptionText !=null)
            descriptionText.text = "".ToString();
    }
}
