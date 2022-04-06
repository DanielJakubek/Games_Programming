using UnityEngine;

public class Fireball : MonoBehaviour
{   
    private RaycastHit hitTarget;
    private bool hasCollided = false;

    public float dmg;

    //called once at the start
    private void Start() {
        AudioManager.mngInstance.PlaySound("Fireball", AudioManager.mngInstance.sounds);
        transform.LookAt(Player.playerInstance.transform.position);
    }

    // Update is called once per frame
    private void Update(){
        //Moves the fireball forward
        transform.position += transform.forward*10f*Time.deltaTime;
    }

    //Setter for the damage
    public void SetDmg(float dmg){
        this.dmg = dmg;
    }

    /*
        Function used to say when to damage the player. Therefore, when
        this object collides with something, checks it has collided before, 
        if not damage the player and destroy it self.

        If it does not collide with anything for 10 seconds, destroy it self  
    */    
    private void OnCollisionEnter(Collision collider) {

        if(collider.transform.tag == "Player" && !hasCollided){
               Player.playerInstance.UpdateHealth(dmg);
                hasCollided= true;
        }

        Destroy(gameObject);

        //Makes sure that the object is destroyed after 10 seconds if nothing hit
        if(gameObject)
            Destroy(gameObject,10);
    }
}
