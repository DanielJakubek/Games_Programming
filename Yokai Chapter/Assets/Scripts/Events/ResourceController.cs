using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    [Header("Health Properties")]
    public int healthInstance; //What instance of this entity it is

    // Start is called before the first frame update
    private void Start(){
        EventManager.eventMngr.pickUpHealth += HealthPickUp;
    }

    /*
        Increases the player's max hp to 100%. This is done by checking if the player is 
        under 100%, and if so then sets the hp to 100 and deletes this gameobject

        Int healthInstance, is an int which keeps track of which instance of this entity is being
        picked up,
    */
    public void HealthPickUp(int healthInstance){

        //If correct health drop has been collected
        if(this.healthInstance == healthInstance && Player.playerInstance.health < 100f){

            AudioManager.mngInstance.PlaySound("PickUp", AudioManager.mngInstance.sounds); //Pick up sound

            Player.playerInstance.health = 100f;
            Destroy(gameObject);
        }  
    }

    //Unsubscribes the events from list upon their destruction
    private void OnDestroy() {
        EventManager.eventMngr.pickUpHealth -= HealthPickUp;  
    }
}
