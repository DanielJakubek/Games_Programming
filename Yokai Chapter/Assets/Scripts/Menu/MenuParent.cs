using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using System;

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
            SceneManager.LoadScene(sceneName); 
        }catch(Exception e){
            Debug.Log(e);
        }
    }
}
