using UnityEngine;
using UnityEngine.AI;

/*  
    The state machine is inspired by this video: 
    https://youtu.be/Vt8aZDPzRjI
    Accessed on the 1st April 2022
*/
public class EnemyContex : MonoBehaviour
{
    [Header("All the possible states")]
    public EnemyState currentState;
    public EnemyIdleState idleState;
    public EnemyWalkingState walkingState;
    public MeleeState meleeState;
    public HitScanState hitScanState;
    public ProjectileState projectileState;
    public EnemyFleeState fleeState;

    [Header("Enemy properties")]
    public EnemyTemplate enemyTemplate; //Scriptable object containing enemy details
    public GameObject target; //The target's gameobject
    public Animator enemyAnimator; //The animator used to animate the enemy
    public NavMeshAgent agent; //The pathfinding 
    

    //Called once before the start method
    private void Awake() {
        
        target = Player.playerInstance.gameObject; //Gets player game object to follow

        idleState = new EnemyIdleState(enemyTemplate, target, gameObject, enemyAnimator, agent);
        walkingState = new EnemyWalkingState(enemyTemplate, target, gameObject, enemyAnimator, agent);

        switch(enemyTemplate.enemyAtckType){
            
            case "Melee":
                meleeState = new MeleeState(enemyTemplate, target, gameObject, enemyAnimator, agent);
            break;
            case "Hitscan":
                //set up Hitscan class
            break;
            case "Projectile":
                projectileState = new ProjectileState(enemyTemplate, target, gameObject, enemyAnimator, agent);
            break;

        }

        fleeState = new EnemyFleeState();
    }
        
    // Start is called before the first frame update
    private void Start(){
        currentState = idleState;
        currentState.StartState(this);
    }

    // Update is called once per frame
    private void Update(){
        currentState.UpdateState(this);
    }
    
    //Used to switch between states
    public void SwitchStates(EnemyState state){
        currentState = state;
        state.StartState(this);
    }


    //Crap Code but gets the job done for now. Fix later

    //Sets the boolean for the attacking state
    private void ExitAttackAnimation(){
        meleeState.setIsAttacking(false);
    }

    //Sets the boolean for the attacking state
    private void SetAttackingTrue(){
        meleeState.setIsAttacking(true);
    }

}
