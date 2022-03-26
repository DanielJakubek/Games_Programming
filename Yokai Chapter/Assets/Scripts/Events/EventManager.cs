using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager eventMngr; 

    //Actions
    public event Action onPlayerDamage; //Action played when player takes damage



    // Start is called before the first frame update
    void Awake(){

        //Makes sure there is a single audio manager
        if(eventMngr == null){
            eventMngr = this;
        }else{
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject); 
    }





    //Makes sure that the action is not null
    public void PlayerTakeDamage(){
        if(onPlayerDamage != null)
            onPlayerDamage();
    }
    

}
