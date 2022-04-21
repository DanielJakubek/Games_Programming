using UnityEngine;

/*  
    The purpose of this class is to play various enemy sounds but most
    importantly to keep track of enemy properties such as their health
*/
public class BossEnemy : Entity
{   
    [Header("Enemy stats")]
    public BossTemplate bossTemplate; //The template specific to enemies

    //Called once before the start  
    private void Awake() {
        health = bossTemplate.health; 
    }

    //called once at the start before the first frame
    private void Start() {

        //Scuffed way to do it, but works for now
        AudioManager.mngInstance.DecreaseVolume("MusicStartArea", AudioManager.mngInstance.sounds);
        AudioManager.mngInstance.PlaySound("MusicBossArea", AudioManager.mngInstance.sounds);
    }

    //When this object is destroyed called the event 
    private void OnDestroy() {
        EventManager.eventMngr.FirstBossDeath();
    }
}