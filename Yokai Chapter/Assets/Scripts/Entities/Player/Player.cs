using UnityEngine;

/*
    This class deals with the player properies
    such as health and updating that health, but also
    player interaction with the world, such as doors.
*/
public class Player : Entity
{      
     public static Player playerInstance; 

    [Header("Player stats")]
    public float armour = 75f; //The players armour

    // Awake is called before the first frame update and before start
    void Awake(){

        //Makes sure there is a single player
        if(playerInstance == null){
            playerInstance = this;
        }else{
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    //Start is called before the first frame update
    private void Start() {
        EventManager.eventMngr.OnHealthUpdate(health);
        EventManager.eventMngr.OnArmourUpdate(armour);
    }

    //Called once every frame
    private void Update() {
        PlayerInteract();
    }

    /*
        This function is called every time the player is hit by an enemy
        and therefore is forced to take damage. Upon damage, their health
        is updated, a sound is played and the hud is updated.

        Parameter: float dmg, the number being subtracted from the player
    */
    public override void UpdateHealth(float dmg){
        
        //Take away from armour
        if(armour >= 0f)
            armour -= dmg;
        
        //Multiplies the armour value by -1 to make it positive and subtracts it from health
        if(armour < 0){
            armour *= -1f; 
            health = health-armour;

            armour = 0; //Make armour zero because it should be now.
        }

        //Play sound upon taking damage
        AudioManager.mngInstance.PlaySound("GotHit", AudioManager.mngInstance.sounds);

        //Action to update health and armour on HUD
        EventManager.eventMngr.OnHealthUpdate(health);
        EventManager.eventMngr.OnArmourUpdate(armour);
    }


    /*
        Deals with player interacting with doors and other "Interactable" items. This is done
        by shooting a ray cast infront of the player and if the tag matches "Interactable" then
        'e' can be pressed to interact
    */
    private void PlayerInteract(){
        //Shoots a ray from the player camera and whatever is hit will be stored in the out variable,
        if(Physics.Raycast(transform.position, transform.forward, out hitTarget, 10f)){
            if(hitTarget.transform.tag == "Interactable"){
                if(Input.GetKeyDown("e"))
                    hitTarget.transform.gameObject.GetComponent<Interact>().DoInteract();
            }
        }
    }
}