using UnityEngine;

/*  
    The purpose of this class is to play various enemy sounds but most
    importantly to keep track of enemy properties such as their health
*/
public class BossEnemy : Entity
{   
    [Header("Enemy stats")]
    public BossTemplate bossTemplate; //The template specific to enemies
    private Transform target;

    //Called once before the start  
    private void Awake() {
        health = bossTemplate.health; 
    }

    //called once at the start before the first frame
    private void Start() {

        //Player game object to follow/target
        target = Player.playerInstance.gameObject.transform;

        //Scuffed way to do it, but works for now
        AudioManager.mngInstance.StopSound("MusicStartArea", AudioManager.mngInstance.sounds);
        AudioManager.mngInstance.PlaySound("MusicBossArea", AudioManager.mngInstance.sounds);
    }

    //Called every time before the first frame
    private void FixedUpdate() {
        LookAt();
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

    //When this object is destroyed called the event 
    private void OnDestroy() {

        //Currenrly have two bosses so this works for now. Only the second boss death = complete game
        if(gameObject.name == "BossTwo(Clone)"){
            EventManager.eventMngr.LevelComplete();
        }

        AudioManager.mngInstance.PlaySound("MusicStartArea", AudioManager.mngInstance.sounds);
        AudioManager.mngInstance.StopSound("MusicBossArea", AudioManager.mngInstance.sounds);

        EventManager.eventMngr.CloseAllDoors(); //Opens all doors
        EventManager.eventMngr.FirstBossDeath();
    }
}