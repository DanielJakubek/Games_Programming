using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy")]
public class EnemyTemplate : ScriptableObject
{   
    [Header("Enemy stats")]
    public string enemyName; //Name of the enemy
    public float health = 100f; //Health of enemy
    public float range = 20f; //Range of enemy
    public float bps = 1f; //Attack Speed
    public float damage = 25f; //The dmg the enemy deals
    public float speed = 20f; //The speed of the enemy
    public float agroDistance = 20f; //The distance the target needs to enter before attacking
    public float fleeDistance = 15f; //The distance the before the enemy flees
    public string enemyAtckType = ""; // The attack type of the enemy
}
