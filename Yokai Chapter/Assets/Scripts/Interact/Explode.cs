using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Deals with an object that deals AOE damage
    upon death.
*/
public class Explode : MonoBehaviour
{
    public float health = 100f;
    public float explosionRadius = 4f;
    public int damage;
    public GameObject deathParticle;


    // Update is called once per frame
    void Update(){
        if(health <= 0)
            ObjectExplode(); 
    }

    /*
        Creates a sphere around the object and anything inside
        it will take damage, the damage is decreased depending on the 
        entity taking damage.
    */
    private void ObjectExplode(){
        
        //Checks sphere of radius explosionRadius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);

        //For everything that is in this sphere, check if player and then damage 
        foreach (var hitCollider in hitColliders){

            switch(hitCollider.tag){

                case "Player":
                    Player.playerInstance.UpdateHealth(damage/4);
                    break;

                case "Enemy":
                    NewEnemy enemy = hitCollider.GetComponent<NewEnemy>();
                    if(enemy != null)
                        enemy.UpdateHealth(damage/2);
                break;

                case "Boss":
                    BossEnemy boss = hitCollider.GetComponent<BossEnemy>();
                        if(boss != null)
                            boss.UpdateHealth(damage);
                break;

            }
        }
        AudioManager.mngInstance.PlaySound("Canistar", AudioManager.mngInstance.sounds); //Play sound
        KillParticle();
        Destroy(gameObject);
    }

    //Updates its health
    public void UpdateHealth(float damage){
        this.health -= damage;
    }


    /*
        Deals with playing the particle when the
        entity is "killed"
    */
    private void KillParticle(){

        if(deathParticle !=null ){
            GameObject temp = Instantiate(deathParticle, transform.position+Vector3.up, Quaternion.Euler(-90f, 0f, 0f));
            gameObject.transform.parent = temp.transform;
            Destroy(temp,2);
        }
        
    }
}
