using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{   
    public WeaponTemplate weaponTemplate; //Template

    private RaycastHit hitTarget; //Stores the information of what was hit
    private Vector3 startRotatation; //The starting rotation of the weapon
    private float waitTime; //How long it has been between firing
    
    //Sounds and aniamtor
    public AudioManager audioMng;
    public Animator gunAnimator;

    //The particle system for the muzzle flash
    public ParticleSystem muzzleFlash; 
    public GameObject impactParticle;

    //The player's camera
    public Camera playerCamera; 


    //Update is called once per frame
    private void Update(){
        getInput();
    }

    /*
        Gets user mouse input, upon right mouse click the fucntion will recoil the gun by a set amount and then
        call the shootGun function that deals with the gun shooting.
        Upon mouse relase, the gun is set back to the default location
    */
    private void getInput(){

        //Gets the user input
        if(Input.GetButtonDown("Fire1") && Time.time > waitTime){

            //Assigns the time to see if the player can shoot again
            waitTime = Time.time + 1f/weaponTemplate.bps;
            ShootGun();
        }
    }

    /*  
        Usese raycasting to project a ray infront of the player/player camera within a set range. If the ray hits
        something, it will be stored in the raycast variable hitTarget.
    */
    private void ShootGun(){

        //Plays the particle system of the muzzle flash
        muzzleFlash.Play();

        audioMng.PlaySound("Pistol", audioMng.sounds);

        //Plays the shooting animation and then extis that animation
        gunAnimator.Play("Shoot");
        gunAnimator.SetBool("isShooting", false);

        //Shoots a ray from the player camera and whatever is hit will be stored in the out variable,
        if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hitTarget, weaponTemplate.range)){

            //Creates an object of the Enemy class
            NewEnemy enemy = hitTarget.transform.GetComponent<NewEnemy>();
            
            //If the player shot an enemy, make the enemy take damage
             if(enemy !=null)
                 enemy.UpdateHealth(weaponTemplate.dmg);

            //The impact particle when a gun is fired, it is destroyed after 1 second
            GameObject temp = Instantiate(impactParticle, hitTarget.point, Quaternion.LookRotation(hitTarget.normal));
            Destroy(temp,1);
        }
    }
}
