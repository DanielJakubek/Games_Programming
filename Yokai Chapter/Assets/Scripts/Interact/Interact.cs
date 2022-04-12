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
    public virtual void Hover(){
        //Do stuff
    }

    //Clears when the player stops hovering over the interactable item
    public virtual void ClearHover(){
        //Do stuff
    }

    
}
