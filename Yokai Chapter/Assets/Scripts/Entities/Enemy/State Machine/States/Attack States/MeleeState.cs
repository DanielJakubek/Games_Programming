using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeState : EnemyAttackingState
{
    //Constructor Paramters: The ones under "Enemy Properties" above this method
    public MeleeState(EnemyTemplate enemyTemplate, GameObject target, GameObject itSelf, Animator enemyAnimator, NavMeshAgent agent){
        this.enemyTemplate = enemyTemplate;
        this.target = target;
        this.itSelf = itSelf;
        this.enemyAnimator = enemyAnimator;
        this.agent = agent;
    }

    //Used to carry out the state upon entry
    public override void StartState(EnemyContex context){   
        enemyAnimator.SetInteger("Tranisiton", 2); //Plays attacking animation
    }

    // Update is called once per frame
    public override void UpdateState(EnemyContex context){

        //Makes sure that the enemy stops
        agent.velocity = Vector3.zero;
        agent.isStopped = true;

        //Follow the target if they move too far away
        if(getDistanceBetween() > enemyTemplate.range && !isAttacking)
            context.SwitchStates(context.walkingState);
    }

    /*
        Used to get the distance between the target and this object
        Returns: Float, the distance between the two entities
    */
    public float getDistanceBetween(){
        return Vector3.Distance(itSelf.transform.position, target.transform.position);
    }
}
