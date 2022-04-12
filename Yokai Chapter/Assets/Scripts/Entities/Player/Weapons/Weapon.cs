using UnityEngine;

/*
    Parent class of each weapon, this subscribes each 
    weapon to the event that deals with updaing the HUD/UI with the
    correct weapon name and ammo count.

    But also with getting user input
*/
public class Weapon : MonoBehaviour
{   
    [Header("Weapon properties")]
    public WeaponTemplate weaponTemplate; //Template
    public RaycastHit hitTarget; //Stores the information of what was hit
    public float waitTime; //How long it has been between firing
    public AudioManager audioMng; //Sounsd manger
    public Animator weaponAnimator; //The animator
    public GameObject impactParticle; //The particle system for the muzzle flash
    public Camera playerCamera; //The player's camera

    private void Awake() {
        audioMng = AudioManager.mngInstance;
    }

    // Called once at the start
    public void Start() {
        EventManager.eventMngr.OnAmmoUse(weaponTemplate.ammo);
        EventManager.eventMngr.OnWeaponSwitch(weaponTemplate.name);

        audioMng = AudioManager.mngInstance;

    }

    // Called once on enable. Code repetition but can't think of a solution
    public void OnEnable() {
        EventManager.eventMngr.OnAmmoUse(weaponTemplate.ammo);
        EventManager.eventMngr.OnWeaponSwitch(weaponTemplate.name);
    }


    /*
        Will call the handleweapon function which allows the weapon to
        do it's thing. Can only be called once every x seconds
    */
    public virtual void getInput(){

        //Gets the user input
        if(Input.GetButtonDown("Fire1") && Time.time > waitTime){
            
            //Assigns the time to see if the player can shoot again
            waitTime = Time.time + 1f/weaponTemplate.bps;
            HandleWeapon();
        }
    }

    /* Handles weapon operation */
    public virtual void HandleWeapon(){}
}
