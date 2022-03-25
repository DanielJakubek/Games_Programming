using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Okubi : Enemy
{
    public GameObject fireBall; //The projectile the enemy shoots
    public float runAwayDistance;

    /*  
        Usese raycasting to project a ray infront of the enemy "gun", if it hits a player, that means
        the enemy can see the player and therefore can fire their weapon or similar.
    */
    public override void CheckSeePlayer(){
        
        //Ray casting looks to see if player is in front 
        Vector3 temp = new Vector3(transform.position.x, target.position.y, transform.position.z);

        //Shoots a ray from the enemy is hit will be stored in the out variable,
        if(Physics.Raycast(temp, transform.forward, out hitTarget, enemyTemplate.range))
            AttackTarget();  
    }
    
    /* Shoots at the target whenever they are in sights at a fire rate*/
    private void AttackTarget(){

        //If is able to shoot
        if(Time.time > waitTime && agent.isStopped == true){

            //Update wait timer
            waitTime = Time.time + 1f/enemyTemplate.bps;

            //Make a new instance of the fireball from the enemy's cannon (child object)
            Instantiate(fireBall, transform.Find("Cannon").position, transform.rotation).GetComponent<Fireball>().SetDmg(enemyTemplate.damage);
        }
    }

    /*
        Deals with moving the enemy towards the player and staying wihin a certain distance
    */
    public override void Motion(){

        float distanceBetweenEntities; //Distance between the enemy and target

        FaceTarget(target.position); 

        //Need to update each frame otherwise it does not want to change
        agent.speed = enemyTemplate.speed;

        //Gets the vector 3 of the target adn then gets the distance between the target and itself
        Vector3 moveLocation = target.position;
        distanceBetweenEntities = Vector3.Distance(transform.position, moveLocation);

        //If the enemy is not within the desired distance, move towards the player, otherwise, stop. If target too close, run away
        if(distanceBetweenEntities > enemyTemplate.range){
            agent.isStopped = false;
            agent.destination = target.position;

        }else if(distanceBetweenEntities < runAwayDistance){
            agent.isStopped = false;
            agent.SetDestination(transform.position + (transform.position - target.position));
            FaceTarget(transform.position + (transform.position - target.position)); 


        }else{
            agent.velocity = Vector3.zero;
            agent.isStopped = true;
        }
    }
}
