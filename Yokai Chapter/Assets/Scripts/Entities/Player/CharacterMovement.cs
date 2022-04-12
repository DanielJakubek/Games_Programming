using UnityEngine;

/*
    This class is all about the player's movement.
*/
public class CharacterMovement : MonoBehaviour{

    [Header("Player movement")]
    public float walkVelocity = 10f; // Walking speed
    public float sprintVelocity = 17f; // Sprinting speed

    float currentVelocity; //Stores the current speed of player
    private CharacterController chrController; //Controller.

    [Header("Headbob")]
    public float bobSpeed = 15f; //How fast the bob happens
    public float bobValue = 0.1f; //How many times the bob occurs

    private float startingPosition = 0f; //The default camera y position
    private float metronome; //Used to keep the beat of the 

    [Header("Sounds")]
    public float runningStepRate; //Rate at which to play foot sounds
    public float walkingStepRate; //Rate at which to play foot sounds
    private float stepRate;

    private AudioManager audioMng; //Audio manager
    private float waitTime; //How long it has been between steps
    
    public Camera playerCam; //Stores the player camera
    
    private Gravity gravityTemp; //Gravity class

    // Start is called at the start of the program.
    void Awake() {
        Cursor.lockState = CursorLockMode.Locked; //Locks the cursor and makes it invisible.

        

        chrController = GetComponent<CharacterController>(); //Gets the character controller
        gravityTemp = GetComponent<Gravity>(); //Gets the gravity script
        startingPosition = playerCam.transform.localPosition.y;
    }

    private void Start() {
        audioMng = AudioManager.mngInstance;
    }


    //Called every frame before update, mostly used for physics
    private void FixedUpdate() {
        MoveCharacter(); // Allows for movement.
    }

    private void Update() {
        gravityTemp.playerJump(); //Calls the jump function from the gravity class
    }


    /* Function deals with the character horizontal movement */
    private void MoveCharacter(){

        //Gets user input and changes the vector 3 variable accordingly.
        Vector3 moveInput =( Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") *transform.forward);

        //Decides the velocity of the character depending if the run key is pressed.
        if(Input.GetKey(KeyCode.LeftShift)){
            currentVelocity = walkVelocity;
            stepRate = walkingStepRate;
        }
        else{
            currentVelocity =sprintVelocity;
            stepRate = runningStepRate;
        }

        // If there is input move the character if not go back to idle state.
        if(moveInput.magnitude >= 0.01f){
            DoHeadbob(gravityTemp.getGrounded());
            chrController.Move(moveInput * currentVelocity * Time.deltaTime); // Moves the object/player.  
        }   
    }

    /*
        Deals with the headbobbing movement (Up and down) when the character is walking.

        Parameters: Bool grounded, if the player is in the air or not
    */
    public void DoHeadbob(bool grounded){

        if(grounded){

            //Updates the timer to make sure the speed is consistent
            metronome+= Time.deltaTime * bobSpeed;

            //Calculates the position of the bob via sine wave and then stores creates a new vector 3 with the new y position (1 or -1)
            float angle = startingPosition + Mathf.Sin(metronome) * bobValue;
            Vector3 temp = new Vector3(playerCam.transform.localPosition.x, angle, playerCam.transform.localPosition.z);

            //Moves the camera up or down
            playerCam.transform.localPosition = temp;

            //Plays sound to go in tune with the headbob
            HandleFootSteps(); 
        }
    }

    /*
        Deals with playing the foot step sounds every few miliseconds
    */
    private void HandleFootSteps(){
        
         //Gets the user input
        if(Time.time > waitTime){

            //Assigns the time to see if the player can shoot again
            waitTime = Time.time + 1f/stepRate;

            //Generates a random number to choose a random foot step sound to play.
            audioMng.PlaySound("Step"+Random.Range(0, 3), audioMng.sounds);
        }
    }
}
