using UnityEngine;

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
    public BossAttackState attackState; //Attack state
    public BossRestState bossRestState; //Rest state

    [Header("Enemy properties")]
    public GameObject target; //The target's gameobject
    public Animator enemyAnimator; //The animator used to animate the enemy

    public BossTemplate bossTemplate; //Scriptable object containing enemy details

    //public EnemyTemplate enemyTemplate; //Scriptable object containing enemy details
    public BossWeapons bossWeapons; //Struct holding the weapons of this boss

  
    //Called once before the start method
    private void Awake() {
        target = Player.playerInstance.gameObject; //Gets player game object to follow

        idleState = new BossIdleState(bossTemplate, enemyAnimator); //Sets up the idle state
        bossRestState = new BossRestState(bossTemplate, enemyAnimator); //Sets up the test state
        attackState = new BossAttackState(bossTemplate, target, gameObject, enemyAnimator, bossWeapons); //Sets up the attack state
    }
        
    // Start is called before the first frame update
    private void Start(){
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
        Instantiate(prefab, origin.transform.position, origin.transform.rotation).GetComponent<Fireball>().SetDmg(dmg);
    }
}
/* Struct holding gameobject of Boss's weapons */
[System.Serializable]
public struct BossWeapons{
    public GameObject weaponOne;
    public GameObject weaponTwo;
    public GameObject weaponThree;
    public GameObject Fireball;
}