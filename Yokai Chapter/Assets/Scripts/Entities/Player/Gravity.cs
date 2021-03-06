using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    [Header("Falling/Gravity physics")]
    public float gravity = -35f; //Gravity of the world.
    public float height = 4f; // The jumping height.
    public float distanceFromGround = 1f; // How far the player needs to be from the ground.

    public Transform groundChecker; // The object that checks if the player is on any ground.
    public LayerMask groundLayerMask; // What layers the checker should look for.

    public LayerMask fallFromMask; 
    private bool canFall;

    private bool grounded; //Checks if the player is grounded or not.
    private Vector3 gravityStore; // Used to store the gravity vectors.
    private CharacterController chrController; // The character controller

    
    ///<summary>
    ///Called once before the start
    ///</summary>
    private void Awake() {
        //Gets and sets t character controller
        chrController = GetComponent<CharacterController>();
    }

    ///<summary>
    ///Called once before update - used for physical items
    ///</summary>
    private void FixedUpdate() {
        gravityPhysics();
    }

    ///<summary>
    ///Deals with the gravity of the player - Basically allows the player to fall
    ///</summary>
    private void gravityPhysics(){

        //Creates a sphere around the object and checks if it collides with anything, if so then changes the grounded bool
        grounded = Physics.CheckSphere(groundChecker.position, distanceFromGround, groundLayerMask); 
        canFall = Physics.CheckSphere(groundChecker.position, distanceFromGround, fallFromMask); 

        // Used to make sure that the gravity y doesn't keep on increasing
        if((grounded || canFall) && gravityStore.y < 0){
            gravityStore.y = -2f;
            
            if(grounded)
                playerJump();
        }
        
        //Changes the y axis movement to V = 0.5g * t^(2)
        gravityStore.y += (gravity * Time.deltaTime);
        chrController.Move(gravityStore * Time.deltaTime);
    }

    ///<summary>
    ///Deals with the player jumping. When the jump key is pressed and player is grounded (not in air), 
    ///the jump speed (going up speed) is calculated using the physics equation: v^(2) = u^(2)+ 2as
    ///</summary>
    public void playerJump(){
        if(Input.GetButtonDown("Jump") && grounded){
            gravityStore.y = Mathf.Sqrt(height * -2f * gravity); 
        }   
    }
    
    ///<summary>
    ///Getter for the bool grounded - to check if player is in the air or not
    ///</summary>
    public bool getGrounded(){
       return grounded;
    }
}
