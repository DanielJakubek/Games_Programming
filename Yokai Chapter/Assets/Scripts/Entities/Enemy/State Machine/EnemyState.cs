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


    /// <summary>
    /// Used to get the distance between the target and this object
    /// Returns: Float, the distance between the two entities
    /// </summary>
    public virtual float getDistanceBetween(){
        //Gets the vector 3 of the target adn then gets the distance between the target and itself
        Vector3 moveLocation = target.transform.position;
        return Vector3.Distance(itSelf.transform.position, target.transform.position);
    }
}
