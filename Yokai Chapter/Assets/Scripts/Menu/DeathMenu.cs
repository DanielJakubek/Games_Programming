using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

///<summary>
/// The menu that is shown upon player death
///</summary>
public class DeathMenu : MenuParent
{
    [Header("Buttons")]
    public Button restartBtn;
    public Button mainMenuBtn;
    public Button quitBtn;

    
    private void Start() {
        AssignButtons();
        EventManager.eventMngr.playerDeath += deathScreen;
    }

    ///<summary>
    /// Assigns listeners to each button on the panel
    ///</summary>
    private void AssignButtons(){
         //=====Add listeners for buttons=====//

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


