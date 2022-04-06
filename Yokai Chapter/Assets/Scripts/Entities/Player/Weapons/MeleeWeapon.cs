using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    //Update is called once per frame
    private void Update(){
        getInput();
    }

    /*
        Handles the gun operation, this specifically deals with
        the ammo, if there is no then shoot, otherwise, play a sound
        to indicate no more bullets.
    */
    public override void HandleWeapon(){

        //weaponAnimator.Play("Swing");
        weaponVFX();

         //Shoots a ray from the player camera and whatever is hit will be stored in the out variable,
        if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hitTarget, weaponTemplate.range)){

            //Creates an object of the Enemy class
            NewEnemy enemy = hitTarget.transform.GetComponent<NewEnemy>();
            
            //If the player shot an enemy, make the enemy take damage
             if(enemy !=null){
                audioMng.PlaySound("EnemyHit", audioMng.sounds);
                enemy.UpdateHealth(weaponTemplate.dmg);
             }    
        }
    }

    /*
        Deals with enabling the muzzle flash and playing it. Also, 
        deals with playing the pistol gun shot when clicked and playing corresponding 
        animation
    */
    private void weaponVFX(){
      

        audioMng.PlaySound("KnifeSlash", audioMng.sounds);

        //Plays the shooting animation and then extis that animation
        weaponAnimator.Play("Swing");
        weaponAnimator.SetBool("isShooting", false);
    }
}
