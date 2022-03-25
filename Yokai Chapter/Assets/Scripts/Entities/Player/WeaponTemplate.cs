using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon")]
public class WeaponTemplate : ScriptableObject
{   
    [Header("Weapon stats")]
    public float dmg = 50f; //Damage the weapon does to the enemy
    public float range = 60f; //The distance of the weapon
    public float bps = 1f; //Bullets per second
}
