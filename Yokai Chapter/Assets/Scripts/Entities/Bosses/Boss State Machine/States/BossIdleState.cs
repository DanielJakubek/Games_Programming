using System.Collections;
using UnityEngine;

public class BossIdleState : BossState
{
    //Constructor Paramters: The ones under "Enemy Properties" above this method
    public BossIdleState(BossTemplate bossTemplate, Animator enemyAnimator){
        this.bossTemplate = bossTemplate;
        this.enemyAnimator = enemyAnimator;
    }

    // Start is called before the first frame update
    public override void StartState(BossContext context){  
        
        if(enemyAnimator != null)
           enemyAnimator.SetInteger("Transition", 0);//Plays idle animation

        IdleStart();
    }

    // Update is called once per frame
    public override void UpdateState(BossContext context){
        ThroughoutIdle(context);
    }

    ///<summary>
    ///Chooses what boss does what at the start of this state
    ///</summary>
    private void IdleStart(){

        //Chooses what to do depending on the boss
        if(bossTemplate !=null){
            switch(bossTemplate.name){

            //First Boss
            case "BossOne":
                AudioManager.mngInstance.PlaySound("SteamHiss", AudioManager.mngInstance.sounds); //Stop sound
                break;

            //Second boss
            case "BossTwo":
            break;

            default:   
            break;
            }
        }
    }

    ///<summary>
    ///Chooses what to do in idle depending what boss it is.
    ///</summary>
    private void ThroughoutIdle(BossContext context){
        switch(bossTemplate.name){

            //First Boss
            case "BossOne":
                //Wait few seconds before going into attack state, couratine called from boss context because it comes from monobehaviour
                context.StartCoroutine(WaitFewSeconds(context));  
            break;

            //Second boss
            case "BossTwo":
                BossTwoIdle(context);
            break;

            default:
                Debug.Log("DEFAULT: ");
            break;
        }
    }

    ///<summary>
    ///What the second boss does when idle. Gets the boss' current health and checks if it's higher than 800, if so, go attack.
    ///Really bad to do it this way but eh.
    ///</summary>
    private void BossTwoIdle(BossContext context){
        float health = context.GetComponent<BossEnemy>().health;
        if(health >= 800f || health <= 500f)
            context.SwitchStates(context.bossWalkState);  
    }

    //Waits for x seconds before switching states
    IEnumerator WaitFewSeconds(BossContext context){
        yield return new WaitForSeconds(bossTemplate.attackSpeed); //Wait for x seconds
        context.SwitchStates(context.bossOneAttackState);
    }

    
}
