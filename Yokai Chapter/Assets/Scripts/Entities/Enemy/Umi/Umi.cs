using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Umi : Enemy
{
    //Used for detecting movement
    Vector3 temp;
    Vector3 oldPosition;

    public Animator enemyAnimator;
    bool isAttacking = false;

    //Called once per frame
    private void Update() {

        temp = transform.position;

        //If the state does not change, do nothing
        if(oldPosition == temp && !isAttacking){
            enemyAnimator.SetInteger("Tranisiton", 0);
            return;
            
        }else if(isAttacking)
            AttackTarget();
        
        if(agent.isStopped == false)
            playWalkingAnimation();
           
    }

    //Play walking animation
    void playWalkingAnimation(){
        enemyAnimator.SetInteger("Tranisiton", 1);
        oldPosition = temp;
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
        if(distanceBetweenEntities > enemyTemplate.range && !isAttacking){
            agent.isStopped = false;
            agent.destination = target.position;
        }else{
            agent.velocity = Vector3.zero;
            agent.isStopped = true;
            isAttacking = true;
        }  
    }
    
    private void SwitchAttacking(){
        isAttacking = false;
    }

    private void AttackTarget(){
        if(agent.isStopped == true && isAttacking){
            enemyAnimator.SetInteger("Tranisiton", 2);
        }
    }
}
