using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{   
    public WeaponTemplate weaponTemplate; //Template

    public RaycastHit hitTarget; //Stores the information of what was hit
    public float waitTime; //How long it has been between firing
    
    //Sounds and aniamtor
    public AudioManager audioMng;
    public Animator weaponAnimator;

    //The particle system for the muzzle flash
    public GameObject impactParticle;

    //The player's camera
    public Camera playerCamera; 

    // Called once at the start
    public void Start() {
        EventManager.eventMngr.OnAmmoUse(weaponTemplate.ammo);
        EventManager.eventMngr.OnWeaponSwitch(weaponTemplate.name);
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
