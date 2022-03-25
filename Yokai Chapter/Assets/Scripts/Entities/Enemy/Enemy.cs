using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity
{   
    //The Target
    public Transform target;

    [Header("Enemy stats")]
    public EnemyTemplate enemyTemplate; //The template specific to enemies

    [Header("Enemy Movement")]
    public float waitTime; //How long it has been between firing
    public NavMeshAgent agent; //The pathfinding 

    
    //Called once before the start
    private void Awake() {
        health = enemyTemplate.health; 
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame, before update
    private void FixedUpdate() {
        Motion();           
    }

    //Called once every frame
    private void Update() {
        CheckSeePlayer();
    }
    
    /*  
        Usese raycasting to project a ray infront of the enemy "gun", if it hits a player, that means
        the enemy can see the player and therefore can fire their weapon or similar.
    */
    public virtual void CheckSeePlayer(){}

    
    /* Deals with moving the enemy towards the player and staying wihin a certain distance */
    public virtual void Motion(){}

    /*  
        It rotates the enemy to face the target (locking the y rotation). 
        Parameter: playerPosition, vector3 that sends the information  where to rotate towards
    */
    public void FaceTarget(Vector3 lookPosition){

        //Rotates the enemy to face the player/object it is tracking
        Vector3 rotationLocation = new Vector3(lookPosition.x, 0f, lookPosition.z);
        transform.LookAt(rotationLocation);
    }
}