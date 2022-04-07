using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HitScanState : EnemyAttackingState
{
    //Constructor Paramters: The ones under "Enemy Properties" above this method
    public HitScanState(EnemyTemplate enemyTemplate, GameObject target, GameObject itSelf, Animator enemyAnimator,  NavMeshAgent agent){
        this.enemyTemplate = enemyTemplate;
        this.target = target;
        this.itSelf = itSelf;
        this.enemyAnimator = enemyAnimator;
        this.agent = agent;
    }
}
