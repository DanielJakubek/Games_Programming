using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFleeState : EnemyState
{
    //Constructor Paramters: The ones under "Enemy Properties" above this method
    public EnemyFleeState(EnemyTemplate enemyTemplate, GameObject target, GameObject itSelf, Animator enemyAnimator, NavMeshAgent agent){
        this.enemyTemplate = enemyTemplate;
        this.target = target;
        this.itSelf = itSelf;
        this.enemyAnimator = enemyAnimator;
        this.agent = agent;
    }


    //Used to carry out the state upon entry
    public override void StartState(EnemyContex context){
        if(enemyAnimator != null)
            enemyAnimator.SetInteger("Transition", 1); //Plays walking animation
    }

    //Called once every frame
    public override void UpdateState(EnemyContex context){
        FleeMotion(context);
    }


    /*
        Used to move the enemy away from the player until a safe distance
        When in safe distance, switch to the walking state
    */
    private void FleeMotion(EnemyContex context){


        //Flee
        agent.SetDestination(itSelf.transform.position + (itSelf.transform.position - target.transform.position));
        agent.isStopped = false;
       // agent.SetDestination(Vector3.zero);

        //Go to attacking state when in attack range
        if(getDistanceBetween() > enemyTemplate.fleeDistance){

            //Stops moving and switches states
            agent.velocity = Vector3.zero;
            agent.isStopped = true;

            context.SwitchStates(context.walkingState);
        }
    }
}
