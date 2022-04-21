using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
    Shows an error with UI not existing in the namespace, however, it seems
    to work fine and compile fine.
*/
public class MainMenu : MonoBehaviour
{

    [Header("Start")]
    public GameObject startMenu;
    public Button startBackBtn;
    public Button levelOne;
    public Button levelTwo;
    public Button levelThree;


    [Header("Main Menu")]
    public GameObject mainMenu;
    public Button startBtn;
    public Button settingsBtn;
    public Button exitBtn;

    [Header("Settings")]
    public GameObject settingsMenu;
    public Button settingsBackBtn;



    /*  Start is called before the first frame update

        The method to assign two functions to the addListener method was taken from stackoverflow on the 30th of March 2022
        https://answers.unity.com/questions/1288510/buttononclickaddlistener-how-to-pass-parameter-or.html
    */
    private void Start(){

        AudioManager.mngInstance.PlaySound("MenuMusic", AudioManager.mngInstance.sounds);

        //=====Add listeners for buttons=====//
        //============================================================================================
        //Main Menu - Start
        Button startButton = startBtn.GetComponent<Button>();
        startButton.onClick.AddListener(() => { SwitchMenus(mainMenu, startMenu);});
        
        //Settings
        Button settingButton = settingsBtn.GetComponent<Button>();
        settingButton.onClick.AddListener(() => { SwitchMenus(mainMenu, settingsMenu);});

        //Exit
        Button exitButton = exitBtn.GetComponent<Button>();
        exitButton.onClick.AddListener(() => { EventManager.eventMngr.CloseGame();});

        //============================================================================================

        //Start Menu 
        Button loadLevelOneBtn = levelOne.GetComponent<Button>();
        loadLevelOneBtn.onClick.AddListener(LoadLevelOne);

        Button loadLevelTwoBtn = levelTwo.GetComponent<Button>();
        loadLevelTwoBtn.onClick.AddListener(LoadLevelOne);

        Button loadLevelThreeBtn = levelThree.GetComponent<Button>();
        loadLevelThreeBtn.onClick.AddListener(LoadLevelOne);

        //Back Button
        Button startBackButton = startBackBtn.GetComponent<Button>();
        startBackButton.onClick.AddListener(() => { SwitchMenus(startMenu, mainMenu);});

        //============================================================================================

        //Settings menu
        Button settingBackButton = settingsBackBtn.GetComponent<Button>();
        settingBackButton.onClick.AddListener(() => { SwitchMenus(settingsMenu, mainMenu);});
    }


    /* 
        Deals with loading the level
    */
    private void LoadLevelOne(){
        Time.timeScale = 1f; //Unpauses the game
        Cursor.lockState = CursorLockMode.Locked; //Locks the cursor and makes it invisible.
        SceneManager.LoadScene("SampleScene"); 
    }

    /*
        Deals with switching menu scenes, this is done by calling an action to disable and enable
        game objects. 

        Parameter:
        currentMenu: A GameObject that stores the ui items for the current menu
        nextMenu: A GameObject that stores the ui items for the next desired menu
    */
    private void SwitchMenus(GameObject currentMenu, GameObject nextMenu){
        EventManager.eventMngr.CloseMenuPanel(currentMenu);
        EventManager.eventMngr.OpenMenuPanel(nextMenu);
    }
}
