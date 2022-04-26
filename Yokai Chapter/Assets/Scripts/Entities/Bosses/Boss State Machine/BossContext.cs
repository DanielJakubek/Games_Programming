using UnityEngine;
using UnityEngine.AI;

/*  
    The state machine is inspired by this video: 
    https://youtu.be/Vt8aZDPzRjI
    Accessed on the 1st April 2022
*/
public class BossContext : MonoBehaviour
{
    [Header("All the possible states")]
    public BossState currentState; //Current state
    public BossIdleState idleState; //Idle state
    public BossWalkState bossWalkState; //Walking state
    
    public BossRestState bossRestState; //Rest state

    public BossOneAttackState bossOneAttackState;
    public BossTwoAttackState bossTwoAttackState;


    [Header("Enemy properties")]
    public GameObject target; //The target's gameobject
    public Animator enemyAnimator; //The animator used to animate the enemy

    public BossTemplate bossTemplate; //Scriptable object containing enemy details
    public BossWeapons bossWeapons; //Struct holding the weapons of this boss

    public NavMeshAgent agent;

    // Start is called before the first frame update
    private void Start(){

        target = Player.playerInstance.gameObject; //Gets player game object to follow

        idleState = new BossIdleState(bossTemplate, enemyAnimator); //Sets up the idle state
        bossRestState = new BossRestState(bossTemplate, enemyAnimator); //Sets up the test state

        // public BossWalkState(BossTemplate bossTemplate, GameObject target, GameObject itSelf, Animator enemyAnimator, NavMeshAgent agent){
        bossWalkState = new BossWalkState(bossTemplate, target, gameObject, enemyAnimator, agent);

        bossOneAttackState = new BossOneAttackState(bossTemplate, target, gameObject, enemyAnimator, bossWeapons); //Sets up the attack state
        bossTwoAttackState = new BossTwoAttackState(bossTemplate, target, gameObject, enemyAnimator, bossWeapons.weaponOne); //Sets up the attack state

        currentState = idleState;
        currentState.StartState(this);
    }

    //Called once every frame before update
    private void FixedUpdate() {
        currentState.FixedUpdateState(this);
    }

    // Update is called once per frame
    private void Update(){
        currentState.UpdateState(this);
    }

    //Used to switch between states
    public void SwitchStates(BossState state){
        currentState = state;
        state.StartState(this);
    }

    /*  
        Deals with Instantiating an object from the boss' weapons.
        This has to be done here because the child is not from
        MonoBehaviour.

        Parameters:
        prefab, gameobject of what is going to be Instantiated;
        origin, gameobject, holding infromation on where the object is going to be Instantiated
        dmg, the number of damage the target will take
    */
    public void InstantiateObject(GameObject prefab, GameObject origin, float dmg){
        if(dmg > 0)
            Instantiate(prefab, origin.transform.position, origin.transform.rotation).GetComponent<Fireball>().SetDmg(dmg);
        else{
            var temp = Instantiate(prefab, origin.transform.position, origin.transform.rotation);
            Destroy(temp, 15f);
        }
    }

    ///<summary>
    ///Gets the current health of this enemy
    ///<summary>
    public float GetHealth(){
        return bossTemplate.health;
    }

    
    /// <summary>
    /// Used to get the distance between the target and this object
    /// </summary>
    public virtual float getDistanceBetween(){
        //Gets the vector 3 of the target adn then gets the distance between the target and itself
        Vector3 moveLocation = target.transform.position;
        return Vector3.Distance(gameObject.transform.position, target.transform.position);
    }

    //Stops loops sounds from this entity
    private void OnDestroy() {
        AudioManager.mngInstance.StopSound("Beam", AudioManager.mngInstance.sounds); //Stop sound
    }

    public virtual void SwitchToWalk(){}

}
/* Struct holding gameobject of Boss's weapons */
[System.Serializable]
public struct BossWeapons{
    public GameObject weaponOne;
    public GameObject weaponTwo;
    public GameObject weaponThree;
    public GameObject Fireball;
}