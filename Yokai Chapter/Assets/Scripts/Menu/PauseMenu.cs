using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

///<summary>
/// This Class deals with enabling the disabling the pause menu
///</summary>
public class PauseMenu : MonoBehaviour
{
    private bool isPaused = false; // Is the game paused
    public GameObject pausePanel; // The pause menu UI

    [Header("Start")]
    public Button restartBtn;
    public Button mainMenuBtn;
    public Button resumeBtn;
    public Button quitBtn;
    
    private void Start() {
        
        //=====Add listeners for buttons=====//
        
        //Restart
        Button restartButton = restartBtn.GetComponent<Button>();
        restartButton.onClick.AddListener(() => { LoadScene("SampleScene"); });

        //Main Menu
        Button mainMenuButton = mainMenuBtn.GetComponent<Button>();
        mainMenuButton.onClick.AddListener(() => { LoadScene("MainMenu"); });
        
        // Resume
        Button resumeButton = resumeBtn.GetComponent<Button>();
        resumeButton.onClick.AddListener(() => { isPaused = false; Resume(); });

        // Exit App
        Button quitButton = quitBtn.GetComponent<Button>();
        quitButton.onClick.AddListener(() => { EventManager.eventMngr.CloseGame(); });
    }


    // Update is called once per frame
    private void Update(){

        //When the "ESC" key is pressed
        if(Input.GetKeyDown(KeyCode.Escape)){
            isPaused = !isPaused;

            if(isPaused)
                PauseGame();
            else
                Resume();
        }
    }

    ///<summary>
    /// Deals with unpausing the game. Achieves this by setting the time scale to one (making the time go back to normal)
    /// and then locking  the curosr and disabling the pause menu panel
    ///</summary>
    private void Resume(){
        if(pausePanel !=null){
            Time.timeScale = 1f; //Unpauses the game
            Cursor.lockState = CursorLockMode.Locked; //Locks the cursor and makes it invisible.
            pausePanel.SetActive(false);
        }
    }  

    ///<summary>
    /// Deals with pausing the game. Achieves this by setting the time scale to zero (making everything halt)
    /// and then unlocking the curosr and enabling the pause menu panel
    ///</summary>
    private void PauseGame(){
        if(pausePanel !=null){
            Time.timeScale = 0f; //Pauses the game
            Cursor.lockState = CursorLockMode.None; //Unlocks the cursor
            pausePanel.SetActive(true);
        }
    }

    ///<summary>
    /// Tries to load the main menu scene
    ///</summary>
    private void LoadScene(string sceneName){
        try{
            Time.timeScale = 1f; //Unpauses the game
            Cursor.lockState = CursorLockMode.Locked; //Locks the cursor and makes it invisible.
            SceneManager.LoadScene(sceneName); 
        }catch(Exception e){
            Debug.Log(e);
        }
    }
}
