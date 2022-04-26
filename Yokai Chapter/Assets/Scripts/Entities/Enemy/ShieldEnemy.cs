using UnityEngine;

///<summary>
///Keeps track of enemy properties such as their health. Enemy only has one state
///Therefore decided to keep it in here
///</summary>
public class ShieldEnemy : NewEnemy
{   
    private float waitTime;
    public LineRenderer laserBeam;
    public GameObject weapon;

    //Called once before the first frame
    private void Start() { 
        if(target == null){
            if(GameObject.Find("BossTwo(Clone)").transform !=null)
                target = GameObject.Find("BossTwo(Clone)").transform;
        }
    }

    //Called every frame before Update
    private void FixedUpdate() {
        if(target !=null)
            LookAt();   
    }

    //called once everyframe
    private void Update() {
        FireHealthBeam();
    }

 
    ///<summary>
    ///Improved on FaceTarget function as started using Rigidbodies. Makes entity face their target
    ///</summary>
    public void LookAt(){

        //Vector 3 between the target and this entity
        Vector3 lookAt = target.transform.position - transform.position;
        lookAt = new Vector3(lookAt.x, 0f, lookAt.z);
        
        //Where to rotate
        Quaternion rotate = Quaternion.LookRotation(lookAt);
        
        var temp = gameObject.GetComponent<Rigidbody>();
        if(temp !=null)
            temp.transform.rotation = rotate;
    }


    ///<summary>
    ///Keeps track of enemy properties such as their health. Enemy only has one state
    ///Therefore decided to keep it in here
    ///</summary>
    private void FireHealthBeam(){

        RenderHealthBeam();

        /* If it can see the target then every few seconds see if it hits the target (boss) in order to heal it */
        if(Physics.Raycast(weapon.transform.position, transform.forward, out hitTarget, enemyTemplate.range)){

            //Only heal every x seconds
            if(Time.time > waitTime){

                //Assigns the time to see if the enemy can heal again
                waitTime = Time.time + 1f/enemyTemplate.bps;

                //If it hits the boss, heal it
                if(hitTarget.transform.tag == "Boss"){
                    BossEnemy boss = hitTarget.transform.GetComponent<BossEnemy>();
                    if(boss != null)
                        boss.UpdateHealth(-enemyTemplate.damage);
                }
            } 
        }
    }

    ///<summary>
    ///Renders the beam coming from the entity
    ///</summary>
    private void RenderHealthBeam(){
        //Sets the laser beam start and end locations
        if(laserBeam != null){
            if(weapon !=null && target !=null){
                laserBeam.SetPosition(0, weapon.transform.position);
                laserBeam.SetPosition(1, new Vector3(target.transform.position.x, target.transform.position.y+1f, target.transform.position.z));      
            }    
        }
    }
}