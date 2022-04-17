using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{   
    //Static ref to this class
    public static EventManager eventMngr; 

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

    //Main Menu
    public event Action<GameObject> closeCurrentMenu; //Event when item is clicked on the UI
    public void CloseMenuPanel(GameObject currentMenu){
        if(closeCurrentMenu != null)
            closeCurrentMenu(currentMenu);
    }

    public event Action<GameObject> openNextMenu; //Event to open the next menu
    public void OpenMenuPanel(GameObject nextMenu){
        if(openNextMenu != null)
            openNextMenu(nextMenu);
    }

    public event Action closeGame; //Event to open the next menu
    public void CloseGame(){
        if(closeGame != null)
            closeGame();
    }
    
    //HUD changes
    public event Action<float> updatePlayerHealth; //Event to update player health on HUD
    public void OnHealthUpdate(float playerHealth){
        if(updatePlayerHealth != null)
            updatePlayerHealth(playerHealth);
    }

    public event Action<float> updatePlayerArmour; //Event to update player health on HUD
    public void OnArmourUpdate(float playerArmour){ 
        if(updatePlayerArmour != null)
            updatePlayerArmour(playerArmour);
    }

    public event Action<int> updateGunAmmo; //Event to update player health on HUD
    public void OnAmmoUse(int ammo){
        if(updateGunAmmo != null)
            updateGunAmmo(ammo);
    }

    public event Action<string> updateWeaponName; //Event to update player health on HUD
    public void OnWeaponSwitch(string weaponName){
        if(updateWeaponName != null)
            updateWeaponName(weaponName);
    }  

    //Area Manager
    public event Action<string, int> startWave; //Event to spawn enemies
    public void StartWave(string area, int wave){
        if(startWave != null)
            startWave(area, wave); 
    }

    public event Action closeDoors; //Event to close all doors
    public void CloseAllDoors(){
        if(closeDoors != null)
            closeDoors();
    }  
}
