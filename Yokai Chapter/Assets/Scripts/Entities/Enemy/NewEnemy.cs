using UnityEngine;

/*  
    The purpose of this class is to play various enemy sounds but most
    importantly to keep track of enemy properties such as their health
*/
public class NewEnemy : Entity
{   
    [Header("Enemy stats")]
    public EnemyTemplate enemyTemplate; //The template specific to enemies
    public Transform target;

    private GameObject thePlayer;
    private float oldHealth;

    //Called once before the start  
    private void Awake() {
        health = enemyTemplate.health; 
    }

    //Called once at the start 
    private void Start() {

        //Player game object to follow/target
        thePlayer = Player.playerInstance.gameObject;
        target = thePlayer.transform;
        oldHealth = health;
    }

    //Called every frame before Update
    private void FixedUpdate() {
        LookAt();  
    }

    //Called every frame frame
    private void Update() {
        DamageIndicator();  
    }

    ///<summary>
    ///Deals with playing the damage taken animation
    ///</summary>
    private void DamageIndicator(){

        /*If the health has changed then get this game objects animatior and play tranisition 3, which is the taken dmg aniamtion */
        if(health != oldHealth){
            
            var tempAnimator = gameObject.GetComponent<Animator> ();

            if(tempAnimator !=null){
                
                var tempInt = tempAnimator.GetInteger("Transition");
                tempAnimator.SetInteger("Transition", 3);
                tempAnimator.SetInteger("Transition", tempInt);
            }
            oldHealth = health;
        }
    }

    ///<summary>
    ///Improved on FaceTarget function as started using Rigidbodies. Makes entity face their target
    ///It rotates the enemy to face the target (locking the y rotation). 
    ///</summary>
    public void LookAt(){

        //Vector 3 between the target and this entity
        Vector3 lookAt = target.transform.position - transform.position;
        lookAt = new Vector3(lookAt.x, 0f, lookAt.z);
        
        //Where to rotate
        Quaternion rotate = Quaternion.LookRotation(lookAt);
        
        var temp = gameObject.GetComponent<Rigidbody>();
        if(temp !=null)
            temp.transform.rotation = rotate;
    }

    private void voidPlayOkubiSound(){
        AudioManager.mngInstance.PlaySound("OkubiAmbient", AudioManager.mngInstance.sounds);
    }

    //Plays enemy foot step sound if animator exsits as it's tied to the animation event
    private void EnemyStepSound(){
        //Generates a random number to choose a random foot step sound to play.
        AudioManager.mngInstance.PlaySound("EnemyStep"+Random.Range(0, 3), AudioManager.mngInstance.sounds);
    }

    //Plays enemy knife slash if animator exsits as it's tied to the animation event
    private void EnemyKnifeSlash(){
        //Generates a random number to choose a random foot step sound to play.
        AudioManager.mngInstance.PlaySound("KnifeSlash", AudioManager.mngInstance.sounds);
    }
}