using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyState
{
    public EnemyTemplate enemyTemplate; //The scriptable object for specific enemy
    public GameObject target; //The enemy's target
    public GameObject itSelf; //The enemy's gameobject
    public Animator enemyAnimator; //The Animator
    public NavMeshAgent agent; //The pathfinding 

    //Used to carry out the state upon entry
    public virtual void StartState(EnemyContex context){   
    }

    // Update is called once per frame
    public virtual void UpdateState(EnemyContex context){
    }

    // Update is called once per frame
    public virtual void FixedUpdateState(EnemyContex context){
    }

}
