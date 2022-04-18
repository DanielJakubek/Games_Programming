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
    public GameObject impactParticle;

    /* Function called when updating hp */
    public virtual void UpdateHealth(float dmg){

        health -= dmg;
        
        //"kills" the enemy when their hp reaches
        if(health <= 0){
            
            KillParticle();

            int temp = EnemyCounter.enmyCounterInstace.getAmountOfEnemies() -1;
            EnemyCounter.enmyCounterInstace.setAmountOfEnemies(temp);

            AudioManager.mngInstance.PlaySound("Canistar", AudioManager.mngInstance.sounds); //Play sound
            Destroy(gameObject);
        }     
    }

    /*
        Deals with playing the particle when the
        entity is "killed"
    */
    private void KillParticle(){
        GameObject temp = Instantiate(impactParticle, transform.position+Vector3.up, Quaternion.Euler(-90f, 0f, 0f));
        gameObject.transform.parent = temp.transform;
        Destroy(temp,2);
    }
}
