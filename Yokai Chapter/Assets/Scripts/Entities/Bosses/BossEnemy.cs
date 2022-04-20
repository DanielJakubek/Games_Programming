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

    //When this object is destroyed called the event 
    private void OnDestroy() {
        EventManager.eventMngr.FirstBossDeath();
    }
}