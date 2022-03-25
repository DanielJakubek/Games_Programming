using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    public float health; //The enemy health
    public RaycastHit hitTarget; //Stores the information of what was hit


    /* Function called when updating hp */
    public virtual void UpdateHealth(float dmg){

        health -= dmg;
        
        //"kills" the enemy when their hp reaches
        if(health <= 0)
            Destroy(gameObject);
    }
    
}
