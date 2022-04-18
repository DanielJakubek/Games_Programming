using UnityEngine;

/*
    This class deals with dealing damage to the player when they
    stand on the object.
*/
public class Lava : MonoBehaviour
{
    public int damage = 5; //How much damage the object does
    public float tick = 2f; //How often the object does damage

    private float waitTime; //How much time has passed
    private bool colliding = false; //Should the player take damage/is colliding with the object


    // Update is called once per frame
    void Update(){

        //If the player is in the zone, deal damage
        if(colliding)
            DealDamage();
    }

    /* Player enters the zone, therefore has to take damage now */
    private void OnTriggerEnter(Collider other) {
        if(other.transform.tag == "Player")
            colliding = true;
    }

    /* Player exits the zone, therefore cannot take dmg anymore */
    private void OnTriggerExit(Collider other) {
        if(other.transform.tag == "Player")
            colliding = false;
    }


    /* Deals damage per tick when the player is standing on the object */
    private void DealDamage(){

        //Can do damage every x seconds
        if(Time.time > waitTime){
            waitTime = Time.time + 1f/tick;
            Player.playerInstance.UpdateHealth(damage);
        }
    }
}
