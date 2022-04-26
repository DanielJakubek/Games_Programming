using UnityEngine;

/*  
    Short class that deals with dealing damage to the player whenever the 
    player enters the knife's trigger area. This commonly occurs
    when the enemy animation plays and timer to make sure the
    trigger can't be spammed.
*/
public class Knife : MonoBehaviour
{   
    private float waitTime; //How long it has been between firing
    public float damage = 10f;
    
    /*
        When the knife hits the player, deal damage to it. 
        Same timer as weapons to make sure the player doesn't just walk into the
        knife and take damage.

        collider, the object that this entitiy collided with
    */
    private void OnTriggerEnter(Collider collider) {

        if(collider.transform.tag == "Player" && Time.time > waitTime){
            waitTime = Time.time + 1f/2f;
            Player.playerInstance.UpdateHealth(damage);
        }
    }
}
