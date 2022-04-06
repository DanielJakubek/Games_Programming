using UnityEngine;

/*  
    The parent class for each entity that
    holds all the basic properties the entities will
    have, such as health and updating that health
*/
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
