using UnityEngine;
using UnityEngine.AI;

public class EnemyIdleState : EnemyState
{   
    float currentHp;
    public float health;

    //Constructor Paramters: The ones under "Enemy Properties" above this method
    public EnemyIdleState(EnemyTemplate enemyTemplate, GameObject target, GameObject itSelf, Animator enemyAnimator,  NavMeshAgent agent){
        this.enemyTemplate = enemyTemplate;
        this.target = target;
        this.itSelf = itSelf;
        this.enemyAnimator = enemyAnimator;
        this.agent = agent;
    }

    // Start is called before the first frame update
    public override void StartState(EnemyContex context){  
        agent.velocity = Vector3.zero;
        agent.isStopped = true;

        if(enemyAnimator != null)
            enemyAnimator.SetInteger("Transition", 0);//Plays idle animation
    }

    // Update is called once per frame
    public override void UpdateState(EnemyContex context){

        //Switches to the walking state when target enters attack zone
        if(EngageTarget()){
            AudioManager.mngInstance.PlaySound("Agro", AudioManager.mngInstance.sounds);
            context.SwitchStates(context.walkingState);
        }
    }

    /*
        Deals with getting the distance between this enemy and the target. If the distance between
        them is less than or equal to the desired distance then switch states.

        Returns: Bool, true to switch states, false to stay in current state.
    */
    private bool EngageTarget(){
        //Switches the states
        if(getDistanceBetween() < enemyTemplate.agroDistance )
            return true;
        else
            return false;
    }
}
