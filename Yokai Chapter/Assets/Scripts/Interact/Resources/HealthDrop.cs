using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDrop : Resource
{   
    public int healthInstance;

    //Activate action to increase hp if bababooey
    private void OnTriggerEnter(Collider other) {

        if(other.tag == "Player")
            EventManager.eventMngr.OnHealthPickUp(healthInstance);
        
    }
}
