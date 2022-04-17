using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public float health = 100f;
    public float explosionRadius = 4f;
    public int damage;
    
    /*  

        Will need to play sounds and what not

    */
    
    // Update is called once per frame
    void Update(){
        if(health <= 0)
            ObjectExplode(); 
    }

    
    private void ObjectExplode(){
        
        Debug.Log("poggers");

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
        Destroy(gameObject);
    }

    public void UpdateHealth(float damage){
        this.health -= damage;
    }

}
