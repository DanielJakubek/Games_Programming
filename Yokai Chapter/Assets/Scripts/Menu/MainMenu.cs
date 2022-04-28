using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

///<summary>
///Deals with traversing the main menu
///</summary>
public class MainMenu : MonoBehaviour
{

    [Header("Start")]
    public GameObject startMenu;
    public Button startBackBtn;
    public Button levelOne;
    public Button levelTwo;
    public Button tutorial;


    [Header("Main Menu")]
    public GameObject mainMenu;
    public Button startBtn;
    public Button settingsBtn;
    public Button creditsBtn;
    public Button exitBtn;

    [Header("Settings")]
    public GameObject settingsMenu;
    public Button settingsBackBtn;

    [Header("Mouse Settings")]
    public Slider mouseSlider;
    public TextMeshProUGUI mouseValue;

    [Header("Volume Settings")]
    public Slider volumeSlider;
    public TextMeshProUGUI volumeValue;

    /*  Start is called before the first frame update

        The method to assign two functions to the addListener method was taken from stackoverflow on the 30th of March 2022
        https://answers.unity.com/questions/1288510/buttononclickaddlistener-how-to-pass-parameter-or.html
    */
    private void Start(){
        AssignButtons();
        AudioManager.mngInstance.PlaySound("MenuMusic", AudioManager.mngInstance.sounds);
        Cursor.lockState = CursorLockMode.None; //Locks the cursor and makes it invisible.
    }

    //Called once every frame
    private void Update() {
        UpdateMouseSen();
        UpdateVolume();
    }

    ///<summary>
    ///Deals with changing the mouse sen. It does this by making sure the value cannot be
    ///less than 0.5f and then sets the display to show current value and saves that value
    ///in PlayerPref that is used later.
    ///</summary>
    private void UpdateMouseSen(){
        if(mouseSlider !=null){

            if(mouseSlider.value < 0.5f)
                mouseSlider.value = 500f;
            
            mouseValue.text = mouseSlider.value.ToString("F2");
            PlayerPrefs.SetFloat("MouseSen", mouseSlider.value);
        }
    }

    ///<summary>
    ///Deals with changing the mouse sen. It does this by making sure the value cannot be
    ///less than 0.5f and then sets the display to show current value and saves that value
    ///in PlayerPref that is used later.
    ///</summary>
    private void UpdateVolume(){
        if(volumeSlider !=null){
            volumeValue.text = volumeSlider.value.ToString("F2");
            PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        }
    }

    ///<summary>
    ///Deals with loading a level, taking a string which tells it what scene to load
    ///</summary>
    private void LoadNewScene(string level){
        Time.timeScale = 1f; //Unpauses the game
        Cursor.lockState = CursorLockMode.Locked; //Locks the cursor and makes it invisible.

        if(SceneManager.GetSceneByName(level) !=null)
            SceneManager.LoadScene(level); 
    }

    ///<summary>
    ///Assigns listeners to buttons
    ///</summary>
    private void AssignButtons(){

        //Main Menu - Start
        Button startButton = startBtn.GetComponent<Button>();
        startButton.onClick.AddListener(() => { SwitchMenus(mainMenu, startMenu);});
        
        //Settings
        Button settingButton = settingsBtn.GetComponent<Button>();
        settingButton.onClick.AddListener(() => { SwitchMenus(mainMenu, settingsMenu);});

        Button creditsButton = creditsBtn.GetComponent<Button>();
        creditsButton.onClick.AddListener(() => { LoadNewScene("Credits") ;});

        //Exit
        Button exitButton = exitBtn.GetComponent<Button>();
        exitButton.onClick.AddListener(() => { EventManager.eventMngr.CloseGame();});

        //============================================================================================

        //Start Menu 
        Button loadLevelOneBtn = levelOne.GetComponent<Button>();
        loadLevelOneBtn.onClick.AddListener(() => { LoadNewScene("Level1") ;});
            
        Button loadLevelTwoBtn = levelTwo.GetComponent<Button>();
        loadLevelTwoBtn.onClick.AddListener(() => { LoadNewScene("Level2") ;});

        Button tutorialButton = tutorial.GetComponent<Button>();
        tutorialButton.onClick.AddListener(() => { LoadNewScene("Tutorial") ;});

        //Back Button
        Button startBackButton = startBackBtn.GetComponent<Button>();
        startBackButton.onClick.AddListener(() => { SwitchMenus(startMenu, mainMenu);});

        //============================================================================================

        //Settings menu
        Button settingBackButton = settingsBackBtn.GetComponent<Button>();
        settingBackButton.onClick.AddListener(() => { SwitchMenus(settingsMenu, mainMenu);});
    }


    ///<summary>
    ///Deals with switching menu scenes, this is done by calling an action to disable and enable game objects.
    ///</summary>
    ///<param name="currentMenu">A GameObject that stores the ui items for the current menu </param>>
    ///<param name="nextMenu">A GameObject that stores the ui items for the next desired menu </param>>
    private void SwitchMenus(GameObject currentMenu, GameObject nextMenu){
        EventManager.eventMngr.CloseMenuPanel(currentMenu);
        EventManager.eventMngr.OpenMenuPanel(nextMenu);
    }
}
