using UnityEngine;
using UnityEngine.AI;

/*
    This class deals with making 
    the enemy walk towards the target and then changing to
    an appropriate state whenever in close reach of the target
*/
public class EnemyWalkingState : EnemyState
{      
    private bool isAgro = true; // If the enemy should be following player player
    private Vector3 currnetLocation; //Used to see if player has moved positions

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
            enemyAnimator.SetInteger("Transition", 1); //Plays walking animation

        currnetLocation = target.transform.position;
    }

    // Update is called once per frame
    public override void UpdateState(EnemyContex context){
       // Motion(context);
    }

    /// <summary> Update is called once per frame 
    /// </summary>
    /// <param name="context">The current state </param>
    public override void FixedUpdateState(EnemyContex context){
        Motion(context);
    }


    /// <summary>
    /// Deals with moving the enemy towards the player and staying wihin a certain distance. 
    /// This is done by getting the distance between the two targets and if the distance is too great, 
    /// follow the target, else go to the attacking state
    // Parameter: contect, EnemyContext, the context class
    /// </summary>
    /// <param name="context">The current state</param>
    public void Motion(EnemyContex context){

        //Distance between the enemy and target
        float distanceBetweenEntities = getDistanceBetween();

        if(distanceBetweenEntities <= enemyTemplate.fleeDistance){
            context.SwitchStates(context.fleeState);
            return;
        }

        //Go to attacking state when in attack range
        if(distanceBetweenEntities <= enemyTemplate.range){

            //Stops moving and switches states
            agent.velocity = Vector3.zero;
            agent.isStopped = true;

            GetAttackState(context);
        }

        //Once agroed, always agroed
        if(isAgro){
            agent.speed = enemyTemplate.speed; //Need to update each frame otherwise it does not want to change
            CheckMovement();
        }
    }

    /// <summary>
    /// Used to see if there is any change in the target's 
    /// position, if there isthen, make the enemy stop in it's tracks
    /// and go towards the new position. This function came about
    /// because the enemies where sliding on ice whenever
    /// the target moved, it was wack.
    /// </summary>
    private void CheckMovement(){

        //position has changed
       // if(currnetLocation.x != target.transform.position.x && currnetLocation.z != target.transform.position.z){
            
            //Stops the enemy from sliding about
            agent.velocity =  target.transform.position - itSelf.transform.position;
            agent.isStopped = false;

            //Stop current on currnet tracks
            currnetLocation = target.transform.position;
            
            //Moves the enemy towards the player
            agent.SetDestination(currnetLocation);
        //}


        if(enemyTemplate.name =="Okubi")
            OkubiMotion();
    }

    /// <summary>
    /// Specific motion for the flying tpye enemy, Okubi. 
    /// This function checks under the enemy to see if it is hitting the ground, if it is, then
    /// it will increase the nav mesh agent offset, therefore making the object go up while
    /// the agent stays on the navmesh. If it is hitting the ground then the offset is
    /// decreased.
    /// </summary>
    private void OkubiMotion(){

        RaycastHit hitTarget;

        //Shoots a ray from the enemy to see how far from the ground it is, if touching the ground then increase enemy offset, otherwise decrease
        if(Physics.Raycast(itSelf.transform.position, -itSelf.transform.up, out hitTarget, 5f+target.transform.position.y))
            agent.baseOffset += 0.001f;    
        else
          agent.baseOffset -= 0.001f;
    }

    /// <summary>
    ///Deals with switching to the correct attack state depending on the enemy attack type
    /// </summary>
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
            case "Explode":
                context.SwitchStates(context.explodeState);
                break;
            default:
                context.SwitchStates(context.idleState);
                break;
        }
    }
}
