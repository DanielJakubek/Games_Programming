using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ProjectileState : EnemyAttackingState
{
    [Header("Enemy Properties")]
    private RaycastHit hitTarget; //Stores the information of what was hit
    private float waitTime; //How long to wait before next attack

    //Constructor Paramters: The ones under "Enemy Properties" above this method
    public ProjectileState(EnemyTemplate enemyTemplate, GameObject target, GameObject itSelf, Animator enemyAnimator, NavMeshAgent agent){
        this.enemyTemplate = enemyTemplate;
        this.target = target;
        this.itSelf = itSelf;
        this.enemyAnimator = enemyAnimator;
        this.agent = agent;
    }

    // Update is called once per frame
    public override void UpdateState(EnemyContex context){

        agent.velocity = Vector3.zero;
        agent.isStopped = true;

        //if out side of range go back to walking state
        if(Vector3.Distance(itSelf.transform.position, target.transform.position) > enemyTemplate.range){
            context.SwitchStates(context.walkingState);
        }

        //Changes to the flee state
        if(getDistanceBetween() <= enemyTemplate.fleeDistance){
            context.SwitchStates(context.fleeState);
            return;
        }

       CheckSeePlayer();
    }

    /*  
        Usese raycasting to project a ray infront of the enemy "gun", if it hits a player, that means
        the enemy can see the player and therefore can fire their weapon or similar.
    */
    public void CheckSeePlayer(){
        
        //Ray casting looks to see if player is in front 
        Vector3 temp = new Vector3(itSelf.transform.position.x, target.transform.position.y, itSelf.transform.position.z);

        //Shoots a ray from the enemy is hit will be stored in the out variable,
        if(Physics.Raycast(temp, itSelf.transform.forward, out hitTarget, enemyTemplate.range)){
            //if(hitTarget.transform.tag == "Player")   
            AttackTarget();
        } 
    }

    /* Shoots at the target whenever they are in sights at a fire rate*/
    private void AttackTarget(){

        //If is able to shoot
        if(Time.time > waitTime){

            //Update wait timer
            waitTime = Time.time + 1f/enemyTemplate.bps;

            //Finds the enemy weapon and fires it. Bit scuffed but this class does not derive from mono therefore hard to make it work
            GameObject enemyWeapon = itSelf.transform.GetChild(0).gameObject;
            if(enemyWeapon != null)
                enemyWeapon.GetComponent<Cannon>().ShootCannon(enemyTemplate.damage);
        }
    }
}
