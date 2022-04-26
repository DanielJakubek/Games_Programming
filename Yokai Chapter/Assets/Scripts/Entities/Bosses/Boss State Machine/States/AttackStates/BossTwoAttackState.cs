using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///Attacks for the second boss in the game
///</summary>
public class BossTwoAttackState : BossAttackState
{
    private bool attacking = false; //Is the boss attacking?
    private int attacks = 0; //How many attacks the boss has made
    private bool gooed = false;
    private GameObject goo;

    public BossTwoAttackState(BossTemplate bossTemplate, GameObject target, GameObject itSelf, Animator enemyAnimator, GameObject goo){
        this.bossTemplate = bossTemplate;
        this.target = target;
        this.itSelf = itSelf;
        this.enemyAnimator = enemyAnimator;
        this.goo = goo;
    }

    // Start is called before the first frame update
    public override void StartState(BossContext context){
        if(enemyAnimator != null)
            enemyAnimator.SetInteger("Transition", 0);
    }
            

    // Update is called once per frame
    public override void UpdateState(BossContext context){
        CheckRange(context);
    }

    ///<summary>
    ///Checks if the entity is still within range
    ///</summary>
    private void CheckRange(BossContext context){

        //Distance between the enemy and target
        float distanceBetweenEntities = context.getDistanceBetween();

        if(distanceBetweenEntities > bossTemplate.range && !attacking){
            enemyAnimator.SetInteger("Transition", 4);
            GooTarget(context);
            context.SwitchStates(context.bossWalkState);
        }
        else
            Attack();
    }

    private void GooTarget(BossContext context){
        float distanceBetweenEntities = context.getDistanceBetween();
        if(goo != null && !gooed){
            gooed = true;
            context.InstantiateObject(goo, itSelf, 0f);
        }
    }

    ///<summary>
    ///Attacks the target with a combo
    ///</summary>
    private void Attack(){
        if(attacks < 2)
            ChooseAttack(2); 
        else{
            ChooseAttack(3);
            if(enemyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1){ attacks = 0; }
        }     
    }

    ///<summary>
    ///Plays animation that was asked for, when it ends it will go back to the idle and the entity is no longer attacking.
    ///</summary>
    ///<param name="attack"> The animation to play</parm>
    private void ChooseAttack(int attack){

        attacking = true;

        if(enemyAnimator != null)
            enemyAnimator.SetInteger("Transition", attack);

        //Animation has stopped, therefore no longer attacking. http://answers.unity.com/answers/1306489/view.html
        if(enemyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1){
            enemyAnimator.SetInteger("Transition", 0);
            attacking = false;
            attacks++;
        }
    }
}
