using UnityEngine;

/*
    This class deals with whap happens when the player is
    using a sword as their weapon of choice. In this case, 
    the basic animation will be played upon input and damage
    will made applied to the enemy if the sword collides with
    it.
*/
public class MeleeWeapon : Weapon
{
    //Variables to make sure the collison occurs once and only when attacking
    private bool hasCollided = false;
    private bool isAttacking = false;

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

        //Plays the sound assocciated with the attack
        weaponVFX();

        //Variables that make sure the collision doesn't happen all the time.
        hasCollided = false;
        isAttacking = true;
    }

    /*
        Deals with executing code when the object has collided with
        something with a collider. If that item has the tag of enemy then it
        is deemed to be an enemy, and will apply this weapon's damage upon it. 
    */
    private void OnCollisionEnter(Collision collided) {

        if(!hasCollided && isAttacking){
            FindShotType(collided.transform); //Finds what enemy was hit and deals dmg to it

            //The weapon has collided and is no longer atacking
            hasCollided = true;
            isAttacking = false;
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
