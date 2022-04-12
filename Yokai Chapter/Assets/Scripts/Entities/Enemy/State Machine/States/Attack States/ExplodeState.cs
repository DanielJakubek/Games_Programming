using System.Collections;
using UnityEngine;
using UnityEngine.AI;

/*
    Deals with exploding the enemy 
    and then damaging anything in a certain radius of the
    explosoion.
*/
public class ExplodeState : EnemyAttackingState
{   
    bool exploded = false; // To check if the enemy has exploded yet

    //Constructor Paramters: The ones under "Enemy Properties" above this method
    public ExplodeState(EnemyTemplate enemyTemplate, GameObject target, GameObject itSelf, Animator enemyAnimator,  NavMeshAgent agent){
        this.enemyTemplate = enemyTemplate;
        this.target = target;
        this.itSelf = itSelf;
        this.enemyAnimator = enemyAnimator;
        this.agent = agent;
    }


    //Used to carry out the state upon entry
    public override void StartState(EnemyContex context){   
        if(enemyAnimator != null)
            enemyAnimator.SetInteger("Transition", 2); //Plays explode animation     
    }

    // Update is called once per frame
    public override void UpdateState(EnemyContex context){

        //Makes sure that the enemy stops
        agent.velocity = Vector3.zero;
        agent.isStopped = true;

        //This calls the coroutine through another script. Scuffed, but it works..
        //https://www.reddit.com/r/Unity3D/comments/qetikf/coroutines_how_to_call_from_your_nonmonobehavior/
        AudioManager.mngInstance.StartCoroutine(HandleExplode());
    }


    /*
        Handles the enemy exploding. The enemy stops and waits for x amount of seconds
        and then if not already exploded it will create a spere around it to check if there are
        any entities that match the player tag, is so, the player tagged entitiy will be damaged.
        After that, this entiity destroys itself.
    */
    IEnumerator HandleExplode(){

        yield return new WaitForSeconds(enemyTemplate.bps); //Wait for x seconds

        //If not yet exploded
        if(!exploded){

            //Checks sphere of radius 4
            Collider[] hitColliders = Physics.OverlapSphere(itSelf.transform.position, 4f);

            //For everything that is in this sphere, check if player and then damage 
            foreach (var hitCollider in hitColliders){
                if(hitCollider.tag == "Player")
                    Player.playerInstance.UpdateHealth(enemyTemplate.damage);
            }
            
            exploded = true;

            //Gets the enemy script and applies damage to it's health variable
            NewEnemy enemy = itSelf.transform.GetComponent<NewEnemy>();
            enemy.UpdateHealth(1000);
        }
    }
}
