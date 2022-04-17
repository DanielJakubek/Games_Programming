using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Deals with having the boss be
    on the rest animation/state where they 
    do nothing for x seconds
*/
public class BossRestState : BossState
{
    //Constructor Paramters: The ones under "Enemy Properties" above this method
    public BossRestState(BossTemplate bossTemplate, Animator enemyAnimator){
        this.bossTemplate = bossTemplate;
        this.enemyAnimator = enemyAnimator;
    }

    // Start is called before the first frame update
    public override void StartState(BossContext context){  
       if(enemyAnimator != null)
           enemyAnimator.SetInteger("Transition", 0);//Plays rest animation
    }

    // Update is called once per frame
    public override void UpdateState(BossContext context){
        //Wait few seconds before going into attack state, couratine called from boss context because it comes from monobehaviour
        context.StartCoroutine(WaitFewSeconds(context));   
    }

    //Waits for x seconds before switching states
    IEnumerator WaitFewSeconds(BossContext context){
        yield return new WaitForSeconds(bossTemplate.restLength); //Wait for x seconds
        context.SwitchStates(context.attackState);
    }
}
