using UnityEngine;
using UnityEngine.AI;

public class EnemyWalkingState : EnemyState
{      
    private bool isAgro = false; // If the enemy should be following player player

    //Constructor Paramters: The ones under "Enemy Properties" above this method
    public EnemyWalkingState(EnemyTemplate enemyTemplate, GameObject target, GameObject itSelf, Animator enemyAnimator, NavMeshAgent agent){
        this.enemyTemplate = enemyTemplate;
        this.target = target;
        this.itSelf = itSelf;
        this.enemyAnimator = enemyAnimator;
        this.agent = agent;
    }

    //Used to carry out the state upon entry
    public override void StartState(EnemyContex context){   
        if(enemyAnimator != null)
            enemyAnimator.SetInteger("Tranisiton", 1); //Plays walking animation
    }

    // Update is called once per frame
    public override void UpdateState(EnemyContex context){
        Motion(context);
    }

    /*
        Deals with moving the enemy towards the player and staying wihin a certain distance. 
        This is done by getting the distance between the two targets and if the distance is too great, 
        follow the target, else go to the attacking state

        Parameter: contect, EnemyContext, the context class
    */
    public void Motion(EnemyContex context){

        //Distance between the enemy and target
        float distanceBetweenEntities = getDistanceBetween();

        //Need to update each frame otherwise it does not want to change
        agent.speed = enemyTemplate.speed;

        //Go to attacking state when in attack range
        if(distanceBetweenEntities <= enemyTemplate.range){

            //Stops moving and switches states
            agent.velocity = Vector3.zero;
            agent.isStopped = true;

            GetAttackState(context);
    
        //Go follow the player when the player is in agro range
        }else if(distanceBetweenEntities <= enemyTemplate.agroDistance){
            isAgro = true;
        }
        else if(distanceBetweenEntities <= enemyTemplate.fleeDistance){
            //switch to flee state
        }

        //Once agroed, always agroed
        if(isAgro){
            agent.isStopped = false;
            agent.destination = target.transform.position;
        }
    }

    /*
        Used to get the distance between the target and this object
        Returns: Float, the distance between the two entities
    */
    private float getDistanceBetween(){
        //Gets the vector 3 of the target adn then gets the distance between the target and itself
        Vector3 moveLocation = target.transform.position;
        return Vector3.Distance(itSelf.transform.position, target.transform.position);
    }

    /*  
        Deals with switching to the correct attack state depending on the enemy attack type
    */
    private void GetAttackState(EnemyContex context){
        switch(enemyTemplate.enemyAtckType){
            case "Melee":
                context.SwitchStates(context.meleeState);
                break;
            case "Hitscan":
                context.SwitchStates(context.hitScanState);
                break;
            case "Projectile":
                context.SwitchStates(context.projectileState);
                break;
            default:
                context.SwitchStates(context.idleState);
                break;
        }
    }
}
