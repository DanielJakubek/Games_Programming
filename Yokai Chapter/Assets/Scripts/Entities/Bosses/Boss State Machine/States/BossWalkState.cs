using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossWalkState : BossState
{
    private NavMeshAgent agent;

    //Cunstructor
    public BossWalkState(BossTemplate bossTemplate, GameObject target, GameObject itSelf, Animator enemyAnimator, NavMeshAgent agent){
        this.bossTemplate = bossTemplate;
        this.target = target;
        this.itSelf = itSelf;
        this.enemyAnimator = enemyAnimator;
        this.agent = agent;
    }

    // Start is called before the first frame update
    public override void StartState(BossContext context){
         if(enemyAnimator != null)
            enemyAnimator.SetInteger("Transition", 1);
    }
            
    // FixedUpdate is called once per frame
    public override void FixedUpdateState(BossContext context){
        Motion(context);
    }

    private Vector3 currnetLocation; //Used to see if player has moved positions


    /// <summary>
    /// Deals with moving the enemy towards the player and staying wihin a certain distance. 
    /// This is done by getting the distance between the two targets and if the distance is too great, 
    /// follow the target, else go to the attacking state
    // Parameter: contect, EnemyContext, the context class
    /// </summary>
    /// <param name="context">The current state</param>
    public void Motion(BossContext context){

        //Distance between the enemy and target
        float distanceBetweenEntities = context.getDistanceBetween();

   
        //Go to attacking state when in attack range
        if(distanceBetweenEntities <= bossTemplate.range){

            //Stops moving and switches states
            agent.velocity = Vector3.zero;
            agent.isStopped = true;

            context.SwitchStates(context.bossTwoAttackState);
        }
        else{

            //Stops the enemy from sliding about
            agent.velocity =  target.transform.position - itSelf.transform.position;
            agent.isStopped = false;

            //Stop current on currnet tracks
            currnetLocation = target.transform.position;
            
            //Moves the enemy towards the player
            agent.SetDestination(currnetLocation);
        } 
    }
}
