using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;


public class DeathMenu : MenuParent
{

    [Header("Buttons")]
    public Button restartBtn;
    public Button mainMenuBtn;
    public Button quitBtn;

  
    private void Start() {
    
        //=====Add listeners for buttons=====//

        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        //Restart
        Button restartButton = restartBtn.GetComponent<Button>();
        restartButton.onClick.AddListener(() => {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
            Time.timeScale = 1f; //Unpauses the game
        });

        //Main Menu
        Button mainMenuButton = mainMenuBtn.GetComponent<Button>();
        mainMenuButton.onClick.AddListener(() => { LoadScene("MainMenu"); });
        
        // Exit App
        Button quitButton = quitBtn.GetComponent<Button>();
        quitButton.onClick.AddListener(() => { EventManager.eventMngr.CloseGame(); });

        EventManager.eventMngr.playerDeath += deathScreen;
    }

    ///<summary>
    /// Shows the death menu
    ///</summary>
    public void deathScreen(){
        //Shows the death screen
        if(panel !=null){
            panel.SetActive(true);
            Time.timeScale = 0f; //Pauses the game
            Cursor.lockState = CursorLockMode.None; //Unlocks the cursor
        } 
    }



    private void OnDestroy() {
        EventManager.eventMngr.playerDeath -= deathScreen;
    }
}


