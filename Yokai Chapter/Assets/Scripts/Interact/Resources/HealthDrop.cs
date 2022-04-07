using UnityEngine;

/*
    Used to call call a check for a trigger.
    If the player enters the trigger and
    are not max hp they are healed to max hp
*/
public class HealthDrop : Resource
{   
    /*
        Checks if it's the player who entered the trigger
        and if they're not full hp, heal 
        to 100 (max hp). Then, call to play sound and destroy
        game object
    */
    private void OnTriggerEnter(Collider other) {

        if(other.tag == "Player"){
            if(Player.playerInstance.health < 100f){
                Player.playerInstance.health = 100;
                UpdateResource();
             }
        }   
    }
}
