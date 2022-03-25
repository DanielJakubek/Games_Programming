using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public string description; //Description of the interactable

    public virtual void DoInteract(){
        //do stuff
    }

    //What is shown when the player hovers over an interactable item
    public virtual void Hover(){
        Debug.Log(description);
    }
}
