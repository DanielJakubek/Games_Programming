using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Boss")]
public class BossTemplate : ScriptableObject
{   
    [Header("Enemy stats")]
    public string bossName; //Name of the enemy
    public float health = 100f; //Health of enemy

    public float attackSpeed = 1f; //Attack Speed
    public float restLength = 5f; //How long the boss rests for

    public float damageOne = 25f; //The dmg the enemy deals
    public float damageTwo = 25f; //The dmg the enemy deals

    public float speed = 20f; //The speed of the enemy
    public float range = 3f; 
}
