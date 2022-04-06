using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{   
    //Static ref to this class
    public static EventManager eventMngr; 


    //============UI-ACTIONS============//
    public event Action<GameObject> closeCurrentMenu; //Event when item is clicked on the UI
    public event Action<GameObject> openNextMenu; //Event to open the next menu
    public event Action closeGame; //Event to open the next menu

    //============RESOURCE-ACTIONS============//
    public event Action<int> pickUpHealth; //Event when the player picks up health

    //============RESOURCE-ACTIONS============//
    public event Action<float> updatePlayerHealth; //Event to update player health on HUD
    public event Action<float> updatePlayerArmour; //Event to update player health on HUD


    // Start is called before the first frame update
    private void Awake(){
        
        //Makes sure there is a single event manager
        if(eventMngr == null){
            eventMngr = this;
        }else{
            Destroy(gameObject);
            return; 
        }

        DontDestroyOnLoad(gameObject); 
    }


    //============ ACTION INVOKES IF NOT NUTLL //============
    public void CloseMenuPanel(GameObject currentMenu){
        if(closeCurrentMenu != null)
            closeCurrentMenu(currentMenu);
    }

    public void OpenMenuPanel(GameObject nextMenu){
        if(openNextMenu != null)
            openNextMenu(nextMenu);
    }

    public void CloseGame(){
        if(closeGame != null)
            closeGame();
    }

    public void OnHealthPickUp(int healthInstance){
        if(pickUpHealth != null)
            pickUpHealth(healthInstance);
    }

    public void OnHealthUpdate(float playerHealth){
        if(updatePlayerHealth != null)
            updatePlayerHealth(playerHealth);
    }

    public void OnArmourUpdate(float playerArmour){
        if(updatePlayerArmour != null)
            updatePlayerArmour(playerArmour);
    }
}
