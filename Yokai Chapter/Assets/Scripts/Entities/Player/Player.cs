using UnityEngine;
using System.Collections;

/*
    This class deals with the player properies
    such as health and updating that health, but also
    player interaction with the world, such as doors.
*/
public class Player : Entity
{      
    public static Player playerInstance; 

    public GameObject bloodSplatterUI; //The taking damage splatter effect
    private float waitDamageUI = 0f; //How long before the splatter effect disappears

    [Header("Player stats")]
    public float armour = 75f; //The players armour
    private float oldHealth;
    private float oldArmour;

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

        oldHealth = health;
        oldArmour = armour;

        EventManager.eventMngr.OnHealthUpdate(health);
        EventManager.eventMngr.OnArmourUpdate(armour);
    }

    //Called once every frame
    private void Update() {

        PlayerInteract();

        //The health or armour changed value
        if(health != oldHealth || armour != oldArmour){
            EventManager.eventMngr.OnHealthUpdate(playerInstance.health);
            EventManager.eventMngr.OnArmourUpdate(playerInstance.armour);

            oldHealth = health;
            oldArmour = armour;
        }

        if(bloodSplatterUI.activeSelf)
            StartCoroutine(BloodSplatterUI());
    }

    /*
        This function is called every time the player is hit by an enemy
        and therefore is forced to take damage. Upon damage, their health
        is updated and a sound is played.

        Parameter: float dmg, the number being subtracted from the player
    */
    public override void UpdateHealth(float dmg){
        
        //Take away from armour
        if(playerInstance.armour >= 0f)
            playerInstance.armour -= dmg;
        
        //Multiplies the armour value by -1 to make it positive and subtracts it from health
        if(playerInstance.armour < 0){
            playerInstance.armour *= -1f; 
            playerInstance.health -= armour;

            playerInstance.armour = 0; //Make armour zero because it should be now.
        }

        //Show blood splatter effect if exists
        if(bloodSplatterUI !=null){
            waitDamageUI += 5f;
            bloodSplatterUI.SetActive(true);
        }
           
        //Play sound upon taking damage
        AudioManager.mngInstance.PlaySound("GotHit", AudioManager.mngInstance.sounds);
    }

    /*
        Deals with player interacting with doors and other "Interactable" items. This is done
        by shooting a ray cast infront of the player and if the tag matches "Interactable" then
        'e' can be pressed to interact
    */
    private void PlayerInteract(){
        //Shoots a ray from the player camera and whatever is hit will be stored in the out variable,
        if(Physics.Raycast(transform.position, transform.forward, out hitTarget, 6f)){
            if(hitTarget.transform.tag == "Interactable"){
                if(Input.GetKeyDown("e"))
                    hitTarget.transform.gameObject.GetComponent<Interact>().DoInteract();
            }
        }
    }

    /* Hides the blood splatter effect after x seconds*/
    IEnumerator BloodSplatterUI(){
        
        yield return new WaitForSeconds(waitDamageUI);

        if(bloodSplatterUI !=null)
            bloodSplatterUI.SetActive(false);
    }
}