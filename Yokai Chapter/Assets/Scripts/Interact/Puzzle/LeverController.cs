using UnityEngine;

///<summary>
///This class deals with the lever controls, such as sending what lever
///was pressed and after the puzzle is complete locking the puzzle
///</summary>
public class LeverController : Interact
{
    public int leverNumber; //which lever was pressed
    public int puzzleNumber; //What puzzle
    public Animator levelAnimator; //The animator


    // Called once before the first frame
    private void Start() {
        EventManager.eventMngr.lockPuzzle += LockLever;
        EventManager.eventMngr.puzzleReset += PlayLeverAnimation;
    }

    /* Deals with opening the door, if it is closed, the door will be moved upwards */
    public override void DoInteract(){
        //Animation pushing lever
        PlayLeverAnimation(puzzleNumber,leverNumber, true);        
        EventManager.eventMngr.LeverPuzzle(puzzleNumber, leverNumber);
    }

    ///<summary>
    ///Locks the lever by getting rid of the tag and locking the interact function
    ///</summary>
    public void LockLever(int puzzleNumber, int newInput){
        if(puzzleNumber == this.puzzleNumber && leverNumber == newInput){
            description = "".ToString();
            transform.tag = "Untagged"; 
        }  
    }

    ///<summary>
    ///Deals with changing the aniamtion for this entity
    ///</summary>
    public void PlayLeverAnimation(int puzzleNumber, int leverNumber, bool animationState){
        if(puzzleNumber == this.puzzleNumber && leverNumber == this.leverNumber){
            if(levelAnimator !=null)
                levelAnimator.SetBool("Open", animationState);
        } 
    }

    //Called once when the object is destroyed
    private void OnDestroy() {
        EventManager.eventMngr.lockPuzzle -= LockLever;
        EventManager.eventMngr.puzzleReset -= PlayLeverAnimation;
    }
}

