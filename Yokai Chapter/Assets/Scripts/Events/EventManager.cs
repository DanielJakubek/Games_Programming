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
        if(eventMngr == null)
            eventMngr = this;
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


    public event Action<float> startCameraShake; //Event to start camera shaking
    public void StartCameraShake(float strength){
        if(startCameraShake != null)
            startCameraShake(strength);
    } 

    public event Action stopCameraShake; //Event to start camera shaking
    public void StopCameraShake(){
        if(stopCameraShake != null)
            stopCameraShake();
    } 

    public event Action firstBossDeath; //Event to start whatever after first boss' death
    public void FirstBossDeath(){
        if(firstBossDeath != null)
            firstBossDeath();
    } 

    public event Action playerDeath; //Event to start whatever the player dies
    public void PlayerDeath(){
        if(playerDeath != null)
            playerDeath();
    } 

    public event Action levelComplete; //Event to start whatever the player completes a level
    public void LevelComplete(){
        if(levelComplete != null)
            levelComplete();
    }  

    public event Action<int, int> leverPuzzle; //Event that deals with setting up the combination puzzle
    public void LeverPuzzle(int puzzleNumber, int newInput){
        if(leverPuzzle != null)
            leverPuzzle(puzzleNumber, newInput);
    } 

    public event Action<int, int> lockPuzzle; //Event that deals with locking the lever after completing the puzzle
    public void LockPuzzle(int puzzleNumber, int newInput){
        if(lockPuzzle != null)
            lockPuzzle(puzzleNumber, newInput);
    }

    public event Action<int, int ,bool> puzzleReset; //Event that deals with playing the reset aniamtion for the puzzle levers
    public void PuzzleReset(int puzzleNumber, int leverNumber, bool animationState){
        if(puzzleReset != null)
            puzzleReset(puzzleNumber, leverNumber, animationState);
    }
}
