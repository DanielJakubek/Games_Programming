using UnityEngine;

/* 
    Scriptable object that holds the basic information
    on the weapon, this makes it easier to make more weapons.
*/
[CreateAssetMenu(fileName = "New Weapon")]
public class WeaponTemplate : ScriptableObject
{   
    [Header("Weapon stats")]
    public float dmg = 50f; //Damage the weapon does to the enemy
    public float range = 60f; //The distance of the weapon
    public float bps = 1f; //Bullets per second
    public int ammo = 25; //The starting number of bullets
}
