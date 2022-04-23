using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompleteMenu : MenuParent
{
    [Header("Buttons")]
    public Button mainMenuBtn;
    public Button nexLevelBtn;
    public Button quitBtn;


    private void Start() {
        
        //=====Add listeners for buttons=====//
        //Main Menu
        Button mainMenuButton = mainMenuBtn.GetComponent<Button>();
        mainMenuButton.onClick.AddListener(() => { LoadScene("MainMenu"); });

        //Next Level
        Button nextLevelButton = nexLevelBtn.GetComponent<Button>();
        nextLevelButton.onClick.AddListener(() => { LoadScene("SampleScene"); }); //Change to level name

        // Exit App
        Button quitButton = quitBtn.GetComponent<Button>();
        quitButton.onClick.AddListener(() => { EventManager.eventMngr.CloseGame(); });

        EventManager.eventMngr.levelComplete += ShowCompleteMenu;
    }

    ///<summary>
    /// Shows the win menu
    ///</summary>
    public void ShowCompleteMenu(){
        //Shows the death screen
        if(panel !=null){
            panel.SetActive(true);
            Time.timeScale = 0f; //Pauses the game
            Cursor.lockState = CursorLockMode.None; //Unlocks the cursor
        } 
    }







    /*
        ADD LEADERBOARD STUFF HERE

    */

    private void OnDestroy() {
        EventManager.eventMngr.levelComplete -= ShowCompleteMenu;
    }






}
