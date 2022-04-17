using UnityEngine;

public abstract class BossState
{   
    public BossTemplate bossTemplate; //Scriptable object containing enemy details
    public GameObject target; //The enemy's target
    public GameObject itSelf; //The enemy's gameobject
    public Animator enemyAnimator; //The Animator

    //Used to carry out the state upon entry
    public virtual void StartState(BossContext context){   
    }

    // Update is called once per frame
    public virtual void UpdateState(BossContext context){
    }

    // Update is called once per frame
    public virtual void FixedUpdateState(BossContext context){
    }



}
