using UnityEngine;

///<summary>
///This class deals with whap happens when the player is using a sword as their weapon of choice. In this case, 
///the basic animation will be played upon input and damage will made applied to the enemy if the sword collides with it.
///</summary>
public class MeleeWeapon : Weapon
{
    private bool isAttacking = false; //Is the weapon attacking
    private GameObject enemyHit; //What object was hit

    //Update is called once per frame
    private void Update(){
        getInput();
    }

    ///<summary>
    ///What to do when attack input is triggered - makes attacking true and plays all the effects of the weapon
    ///</summary>
    public override void HandleWeapon(){
        isAttacking = true;
        weaponVFX();
    }

    ///<summary>
    ///No need for a fire rate here imo, therefore it is overloaded. Gets userinput for attacking
    ///</summary>
    public override void getInput(){
        if(Input.GetButtonDown("Fire1"))
            HandleWeapon();
    }

    ///<summary>
    ///The "Swing" animation call this function to let the sword know when to no longer deal damage
    ///</summary>
    public void StopAttacking(){
        isAttacking = false;
    }

    ///<summary>
    ///Deals with executing code when the object has collided with something with a collider.
    ///</summary>
    private void OnTriggerEnter(Collider other) {
        if(isAttacking)
            FindShotType(other.transform); //Finds what enemy was hit and deals dmg to it     
    }

    ///<summary>
    ///Plays the effects for the weapon
    ///</summary>
    private void weaponVFX(){
        audioMng.PlaySound("KnifeSlash", audioMng.sounds); //Sound effect
        weaponAnimator.Play("Swing"); //Swing effect
    }


    ///<summary>
    ///When an enemy is hit this fucntion will spawn a particle at the area the enemy was hit at
    ///</summary>
    public override void ImpactParticleInstantiate(bool hitEnemy, GameObject enemyHit){

        GameObject temp; //The object to be created

        /* If an enemy was hit then play the enemy hit particle, otherwise play non enemy hit particle */
        if(hitEnemy){
            audioMng.PlaySound("EnemyHit", audioMng.sounds);
            temp = Instantiate(hitParticle, enemyHit.transform.forward, Quaternion.LookRotation(enemyHit.transform.forward));
            temp.transform.localScale *= 15f;
        }
        else
            temp = Instantiate(impactParticle, enemyHit.transform.forward, Quaternion.LookRotation(Vector3.forward));

        Destroy(temp,1);
    }
}
