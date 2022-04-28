using UnityEngine;
using UnityEngine.SceneManagement;
using System;

///<summary>
/// Parent class to all the menus, it deals with the things that all menus have in common, such as loading a new scene
///</summary>
public class MenuParent : MonoBehaviour
{
    public GameObject panel; // The pause menu UI

    ///<summary>
    /// Tries to load the main menu scene
    ///</summary>
    public void LoadScene(string sceneName){
        try{
            Time.timeScale = 1f; //Unpauses the game
            Cursor.lockState = CursorLockMode.Locked; //Locks the cursor and makes it invisible.
            StopAllSound();
            SceneManager.LoadScene(sceneName); 
        }catch(Exception e){
            Debug.Log(e);
        }
    }

    ///<summary>
    /// Stops every sound in the curent scene by looping through the whole sound list
    ///</summary>
    public void StopAllSound(){
        //Adds each audio source in our array, including its volume properties
        foreach(Sounds i in AudioManager.mngInstance.sounds){
            AudioManager.mngInstance.StopSound(i.name, AudioManager.mngInstance.sounds);//Stop the sound
        }
    }
}
