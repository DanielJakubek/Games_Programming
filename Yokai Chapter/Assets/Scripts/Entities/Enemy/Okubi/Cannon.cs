using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject fireBall; //The projectile the enemy shoots

    public void ShootCannon(float dmg){
        //Make a new instance of the fireball from the enemy's cannon (child object)
        Instantiate(fireBall, transform.position, transform.rotation).GetComponent<Fireball>().SetDmg(dmg);
    }
}
