using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Player : Entity
{   
    [Header("Player stats")]
    public static Player playerInstance; 

    public TextMeshProUGUI TextPro;
     
    // Start is called before the first frame update
    void Awake(){

        //Makes sure there is a single player
        if(playerInstance == null){
            playerInstance = this;
        }else{
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    /*
        Function to call when enemy us trying to damange the player
    */
    public override void UpdateHealth(float dmg){

        AudioManager.mngInstance.PlaySound("GotHit", AudioManager.mngInstance.sounds);
        health = health-dmg;

        //"kills" the enemy when their hp reaches
        if(health <= 0){}
            //Destroy(gameObject);

        //Tells the event manager to update the UI
        //EventManager.eventMngr.PlayerTakeDamage();
    }

    private void Update() {
        //Shoots a ray from the player camera and whatever is hit will be stored in the out variable,
        if(Physics.Raycast(transform.position, transform.forward, out hitTarget, 10f)){
            if(hitTarget.transform.tag == "Interactable"){
                if(Input.GetKeyDown("e"))
                    hitTarget.transform.gameObject.GetComponent<Interact>().DoInteract();
            }
        }
    }
}