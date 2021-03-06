using UnityEngine;

/*
    Deals with player and boss pillar interaction
*/
public class Pillars : Interact
{   
    public GameObject pillars; //The pillars to move
    public float shakeStrength = 0.2f; //How powerful the camera shake will be
    private bool shouldRaise = false; //Should the pillars go up
    private Vector3 newLocation; //Location of the pillars

    public static Pillars pillarInstace;  //Instance of this object

    private void Awake() {
        //Makes sure there is a single pillar
        if(pillarInstace == null){
            pillarInstace = this;
        }else{
            Destroy(gameObject);
            return; 
        }
    }

    //Called once at the start before the frame
    private void Start() {
        description = "Press 'E' To Start...";
    }

    //Called once every frame
    private void Update() {
        if(shouldRaise)
            RaisePillars();
    }

    /* Deals with opening the door, if it is closed, the door will be moved upwards */
    public override void DoInteract(){
        transform.position =  new Vector3(transform.position.x, transform.position.y-10f, transform.position.z);  //Move the button down
        GetNewPillarPosition(53.1f);
    }

    //Sets the position to which the pillars should move to
    public void GetNewPillarPosition(float yMovement){
        //Location to move the pillars to and says that the pillars should move up or down
        newLocation = new Vector3(pillars.transform.position.x, pillars.transform.position.y + yMovement, pillars.transform.position.z);
        shouldRaise = true;
    }

    bool playSound = true;

    /*
        Deals with moving the pillars and lava up to 
        the top platform (changing the level). This is done by
        setting the pillar game object to active and moving it 
        upwards until it has been reached
    */
    private void RaisePillars(){
        if(pillars != null){

            //Make active if not already
            if(!pillars.activeSelf)
                pillars.SetActive(true);
            
            //Move the pillars to their location and start camera shake
            pillars.transform.position = Vector3.MoveTowards(pillars.transform.position, newLocation, 5f*Time.deltaTime);
            EventManager.eventMngr.StartCameraShake(shakeStrength);

            //Plays sound once (looped)
            if(playSound){
                AudioManager.mngInstance.PlaySound("StoneFriction", AudioManager.mngInstance.sounds); //Play sound
                playSound = false;
            }
                
            //If the pillar have reached their new location stop moving them, also stop camera shake
            if(pillars.transform.position == newLocation){
                pillars.transform.position = newLocation;

                EventManager.eventMngr.StopCameraShake();
                AudioManager.mngInstance.StopSound("StoneFriction", AudioManager.mngInstance.sounds); //Stop sound

                shouldRaise = false;
            }
        }
    }
}
