using System.Collections.Generic;
using UnityEngine;

///<summary>
///This class is the main controller of the puzzle (the "brains"). 
///It checks if the lever combination was correct and then sends events
///to lock the puzzle to them, if incorrect it clears the puzzle.
///</summary>
public class PuzzleDoor : Interact
{   
    [Header("Puzzle")]
    public int puzzleNumber; //What puzzle this is
    public int leverAmount = 0; //How many levers
    
    private List<int> leverCombination;
    private bool finished = false; //If the puzzle is complete
    private AudioManager audioMngr;

    [Header("Door")]
    private bool canOpen = false;

    // Called once before the first frame
    private void Awake() {
        //Inistalises the list
        leverCombination = new List<int>();
    }
    
    // Called once before the first frame
    private void Start() {
        description = "Press 'E' To Open";

        audioMngr = AudioManager.mngInstance;
        EventManager.eventMngr.leverPuzzle += AddToList;
    }

    /* Deals with opening the door, if it is closed, the door will be moved upwards */
    public override void DoInteract(){
        //Moves the door up and makes it open
        if(canOpen)
            transform.position = new Vector3(transform.position.x, transform.position.y+4, transform.position.z);
    }

    ///<summary>
    ///Checks if the correct order of levers was pressed. Done by checking if the previous number in the list
    ///is less than the one infront of it. If so, it becomes the old, if the old number is great then the puzzle resets.
    ///Afte all necessary levers have been pressed then the puzzle is complete
    ///</summary>
    private void CheckCombination(){
        int lastNumber = 0;

        foreach(int combination in leverCombination){
            
            //Check if current number is greater than the last number
            if(lastNumber < combination)
                lastNumber = combination;
            else{

                ResetLeverTransform();
                leverCombination = new List<int>(); //Clears the list (Resets all levers)
                
                if(audioMngr !=null) //Fail sound
                    audioMngr.PlaySound("Incorrect", audioMngr.sounds);

                return;
            }
        }

        if(audioMngr !=null) //CORRECT SOUND
            audioMngr.PlaySound("Correct", audioMngr.sounds);

        //If correct combination then open the door
        if(leverCombination.Count >= leverAmount){

            canOpen = true;
            LockThisPuzzle();
            finished = true;
            
            //DING DING DING CORRECT
            if(audioMngr !=null)
                audioMngr.PlaySound("CompletePuzzle", audioMngr.sounds);
        }
    }

    ///<summary>
    ///Iterates through the list and tells animator to play the rest animation
    ///</summary>
    private void ResetLeverTransform(){
        foreach(int combination in leverCombination)
            EventManager.eventMngr.PuzzleReset(puzzleNumber, combination, false);
    }
    
    ///<summary>
    ///Calls an event that makes it impossible to play this puzzle
    ///</summary>
    private void LockThisPuzzle(){
        for(int i = 1; i < leverCombination.Count+1; i++){
            EventManager.eventMngr.LockPuzzle(puzzleNumber, i);
        }
    }

    ///<summary>
    ///Checks if the correct puzzle is at play and then adds a number to its list
    ///</summary>
    ///<param name ="puzzleNumber"> Which puzzle this is.</param>
    ///<param name ="newInput"> What lever was pressed </param>
    public void AddToList(int puzzleNumber, int newInput){
        //If correct puzzle add to list
        if(puzzleNumber == this.puzzleNumber)
            leverCombination.Add(newInput);

        if(leverCombination !=null && !finished)
            CheckCombination();
    }

    //Called once when the object is destroyed
    private void OnDestroy() {
        EventManager.eventMngr.leverPuzzle += AddToList;
    }
}
