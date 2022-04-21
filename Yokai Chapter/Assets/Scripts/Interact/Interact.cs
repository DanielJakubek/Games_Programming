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
}
