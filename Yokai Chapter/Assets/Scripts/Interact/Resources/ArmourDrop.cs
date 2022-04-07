using UnityEngine;

/*
    Used to call call a check for a trigger.
    If the player enters the trigger and
    are not max armour they are healed to max armour
*/
public class ArmourDrop : Resource
{   
    /*
        Checks if it's the player who entered the trigger
        and if they're not full armour, heal to max armour 
        Then, call to play sound and destroy
        game object
    */
    private void OnTriggerEnter(Collider other) {

        if(other.tag == "Player"){
            if(Player.playerInstance.armour < 75f){
                Player.playerInstance.armour = 75;
                UpdateResource();
             }
        }   
    }
}
