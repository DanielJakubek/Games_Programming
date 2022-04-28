using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LootLocker.Requests;

public class CompleteMenu : MenuParent
{
    [Header("Buttons")]
    public Button mainMenuBtn;
    public Button nexLevelBtn;
    public Button quitBtn;

    public string nextLevel;

    //https://www.youtube.com/watch?v=pp8Vl4cKLdc
    [Header("Leaderboard")]
    public int boardID;
    public int MaxTimes = 10;
    public TMP_InputField playerName; //Name of the player
    public TextMeshProUGUI timeValue; //Holds the timer value on canvas

    public TextMeshProUGUI leaderboardName; //Holds the player name form the leaderboard
    public TextMeshProUGUI leaderBoardTime; //Holds the timer value from the leaderboard

    private float startTime; //The time at the start of the level
    private float finalTime; //The final time they got

    [Header("Leaderboard Buttons")]
    public Button submitBtn;

    //Called once before the first frame
    private void Start() {

        startTime = Time.time; //What time the level started at
        AddButtonListeners();

        StartLootLockerSession();
  
        //Subscribes the function to the event
        EventManager.eventMngr.levelComplete += ShowCompleteMenu;
    }

    ///<summary>
    /// Adds listeners for buttons
    ///</summary>
    public void AddButtonListeners(){

        //Main Menu
        Button mainMenuButton = mainMenuBtn.GetComponent<Button>();
        mainMenuButton.onClick.AddListener(() => { LoadScene("MainMenu"); });

        //Next Level
        Button nextLevelButton = nexLevelBtn.GetComponent<Button>();
        nextLevelButton.onClick.AddListener(() => { LoadScene(nextLevel); }); //Change to level name

        // Exit App
        Button quitButton = quitBtn.GetComponent<Button>();
        quitButton.onClick.AddListener(() => { EventManager.eventMngr.CloseGame(); });

        // Submit leaderboard
        Button submitButton = submitBtn.GetComponent<Button>();
        submitButton.onClick.AddListener(() => { SubmitTime(); });
    }

    //Will show the updated leaderboard on enable
    private void OnEnable() {
        DisplayLeaderBoard();
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

        SetTimerValue(); //Sets how long it took to beat the level
    }

    ///<summary>
    /// Sets tht time how long the user took to beat the level
    ///</summary>
    private void SetTimerValue(){
        if(timeValue != null){
            finalTime = Time.time - startTime;  
            timeValue.text = finalTime.ToString();
        }   
    }

    ////////////////LEADERBOARD////////////////////

    ///<summary>
    ///Starts the loot locker session
    ///</summary>
    public void StartLootLockerSession(){
        LootLockerSDKManager.StartGuestSession("Player", (response) => {
            if(response.success)
                Debug.Log("Connected to LootLocker");
            else
                Debug.Log("Did not connect to LootLocker");
        });
    }

    ///<summary>
    ///Submits the player's name and time to the loot locker leaderboard
    ///</summary>
    public void SubmitTime(){
        //The completion time in ints. This is bad because we're dealing with time and therefore this is not going to be precise but loot locker doesn't like floats here
        int playerTimeInt = (int)finalTime; 

        LootLockerSDKManager.SubmitScore(playerName.text, playerTimeInt, boardID, (response) =>{
            if(response.success)
                Debug.Log("Connected to LootLocker");
            else
                Debug.Log("Did not connect to LootLocker");
        });
        DisplayLeaderBoard();
    }

    ///<summary>
    ///Loads the leaderboard. THis is done by connecting to loot locker and then checking if it was successful, if so it 
    ///it will check if there are leaderboard text fields and then loop through x items from the leaderboard adding each 
    // to the text field
    ///</summary>
    public void DisplayLeaderBoard(){
        LootLockerSDKManager.GetScoreList(boardID, MaxTimes, (response) =>{
            if(response.success){
                if(leaderboardName !=null && leaderBoardTime !=null){

                    //Empties the board
                    leaderboardName.text = null;
                    leaderBoardTime.text = null;

                    //Gets all the items from the leaderboard
                    LootLockerLeaderboardMember[] leaderboardItems = response.items;

                    //Loops through all the items in the leaderboard and adds them to the text fields
                    foreach(LootLockerLeaderboardMember item in leaderboardItems){
                        leaderboardName.text += item.member_id.ToString() + "\n";
                        leaderBoardTime.text += item.score.ToString() + "\n";
                    }
                }
            }
        });
    }

    private void OnDestroy() {
        EventManager.eventMngr.levelComplete -= ShowCompleteMenu;
    }
}
