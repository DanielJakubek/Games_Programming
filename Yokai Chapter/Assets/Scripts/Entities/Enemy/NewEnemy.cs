using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemy : Entity
{   
    [Header("Enemy stats")]
    public EnemyTemplate enemyTemplate; //The template specific to enemies
    public Transform target;
    private GameObject thePlayer;

    //Called once before the start  
    private void Awake() {
        health = enemyTemplate.health; 
    }

    //Called once at the start 
    private void Start() {
        AudioManager.mngInstance.PlaySound("OkubiAmbient", AudioManager.mngInstance.sounds);
        thePlayer = Player.playerInstance.gameObject;

        target = thePlayer.transform;
    }

    //Called every frame before Update
    private void FixedUpdate() {
        FaceTarget(target.position);   
    }


    /*  
        It rotates the enemy to face the target (locking the y rotation). 
        Parameter: playerPosition, vector3 that sends the information  where to rotate towards
    */
    public void FaceTarget(Vector3 lookPosition){

        //Rotates the enemy to face the player/object it is tracking
        Vector3 rotationLocation = new Vector3(lookPosition.x, 0f, lookPosition.z);
        transform.LookAt(rotationLocation);
    }

    private void voidPlayOkubiSound(){
        AudioManager.mngInstance.PlaySound("OkubiAmbient", AudioManager.mngInstance.sounds);
    }

    //Plays enemy foot step sound if animator exsits as it's tied to the animation event
    private void EnemyStepSound(){
        //Generates a random number to choose a random foot step sound to play.
        AudioManager.mngInstance.PlaySound("EnemyStep"+Random.Range(0, 3), AudioManager.mngInstance.sounds);
    }

    //Plays enemy knife slash if animator exsits as it's tied to the animation event
    private void EnemyKnifeSlash(){
        //Generates a random number to choose a random foot step sound to play.
        AudioManager.mngInstance.PlaySound("KnifeSlash", AudioManager.mngInstance.sounds);
    }
}