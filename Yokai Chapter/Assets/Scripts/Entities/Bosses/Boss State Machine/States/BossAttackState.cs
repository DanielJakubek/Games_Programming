using UnityEngine;

/*  
    Deals with the attacks of the boss. This is the
    beam attack and fireball attack
*/
public class BossAttackState : BossState
{
    public BossWeapons bossWeapons; //The weapons of this boss
    private float waitTimeFireBall, waitTimeBeam; //How long to wait before next attack
    private int ballCount; //Counts how many fireballs have been shot
    private float rotation; //How much the object should rotate

     private bool resetRotation = true;

    //Constructor Paramters: The ones under "Enemy Properties" above this method
    public BossAttackState(BossTemplate bossTemplate, GameObject target, GameObject itSelf, Animator enemyAnimator, BossWeapons bossWeapons){
        this.bossTemplate = bossTemplate;
        this.target = target;
        this.itSelf = itSelf;
        this.enemyAnimator = enemyAnimator;
        this.bossWeapons = bossWeapons;
    }

    // Start is called before the first frame update
    public override void StartState(BossContext context){
        ballCount = 0;
        rotation = 720f;

        if(enemyAnimator != null)
            enemyAnimator.SetInteger("Transition", 1);
    }
            

    // Update is called once per frame
    public override void UpdateState(BossContext context){

        //Switch to different attack after x fire balls have been shot
        if(ballCount >= 20){
            bossWeapons.weaponThree.SetActive(true);
            RotateBoss(context);
            RenderBeam(); //Attack with beam
        }
        else   
            FireBallAttack(context); //Attack with fireballs
    }

    /*
        Line renderer: https://docs.unity3d.com/ScriptReference/LineRenderer.html

        This function deals with rendering the beam that comes from the Boss' chest.
        This is done by getting the line renderer component from the boss' weapon
        and then setting the end of the beam to an arbitrary vector. From there
        the start of the beam is the weapon (middle of chest) and end is that that arbitrary vector.
        Then the vector btween the start and end are calculated and passed to the damage function.
    */
    private void RenderBeam(){

        LineRenderer laserBeam = bossWeapons.weaponThree.GetComponent<LineRenderer>();

        if(laserBeam != null){
    
            //Sets arbitrary vector for end of the beam
            Vector3 laserEnd = bossWeapons.weaponThree.transform.forward*600f;

            //Sets the laser beam start and end locations
            laserBeam.SetPosition(0, bossWeapons.weaponThree.transform.position);
            laserBeam.SetPosition(1, bossWeapons.weaponThree.transform.forward*600f);

            //gets the vector between the end of the laser beam and the start.
            var rayCastDirection = laserEnd-bossWeapons.weaponThree.transform.position;
            BeamDamage(rayCastDirection);
        }
    }

   
    /*
        Deals with dealing damage to the player if they enter
        the beam. Raycasts, if encounters the tag "player", do 
        damage to it.

        Prameter: beamEnd, the direction of the raycast
    */
    private void BeamDamage(Vector3 beamEnd){

        RaycastHit hit;

        AudioManager.mngInstance.PlaySound("Beam", AudioManager.mngInstance.sounds); //Play sound

        //Checks if the beam hits anything
        if(Physics.Raycast(bossWeapons.weaponThree.transform.position, beamEnd, out hit, 100f)){

            //If player is hit, do damage to player
            if(hit.transform.tag == "Player"){

                //Can do damage every x seconds
                if(Time.time > waitTimeBeam){
                    waitTimeBeam = Time.time + 1f/bossTemplate.attackSpeed;
                    Player.playerInstance.UpdateHealth(bossTemplate.damageTwo);
                }
            }
        }
    }

   
    /*
        Deals with rotating the boss while it beams the
        player. After the boss has rotated enough it will
        switch to the rest state.
    */
    private void RotateBoss(BossContext context){
        
        ResetRotation();

        //Rotates the boss
        itSelf.transform.Rotate(Vector3.up * Time.deltaTime* 100f); //Rotates the boss

        //Subtracts the rotation that has occured already
        rotation -= Time.deltaTime* 50f;
      
        //Switches to the rest phase after the rotation has finished
        if(rotation <= 0){
           

            bossWeapons.weaponThree.SetActive(false);
            AudioManager.mngInstance.StopSound("Beam", AudioManager.mngInstance.sounds); //Stop sound
             
            //Switch to rest phase
            context.SwitchStates(context.bossRestState);
        }
    }

    /*
        Deals with resetting the boss' rotation angles
        so that it parallel to the pillars. 
    */
    private void ResetRotation(){
        if(resetRotation){
            itSelf.transform.rotation = Quaternion.Euler(Vector3.zero);
            resetRotation = false;
        }
    }

    /*
        Deals with shooting the fireball every x seconds
        at the target (the player). THis is done by seeing 
        if the current time is greater than the time to wait
        for, if it is, then update the wait time to be greater again.
        Check if the weapon exsits (cannon) and then call a function
        from another class to deal with instatinating the fireball.
    */
    private void FireBallAttack(BossContext context){

        FaceTarget();

        //If is able to shoot
        if(Time.time > waitTimeFireBall){

            //Update wait timer
            waitTimeFireBall = Time.time + 1f/bossTemplate.attackSpeed;

            //If the weapon exits, then shoot
            if(bossWeapons.weaponOne != null && bossWeapons.weaponTwo !=null){
                ballCount++;

                context.InstantiateObject(bossWeapons.Fireball, bossWeapons.weaponOne, bossTemplate.damageOne);
                context.InstantiateObject(bossWeapons.Fireball, bossWeapons.weaponTwo, bossTemplate.damageOne);
            }  
        }
    }

    /*  
        It rotates the enemy to face the target (locking the y rotation). 
        Parameter: playerPosition, vector3 that sends the information  where to rotate towards
    */
    private void FaceTarget(){
        
        var lookPosition = target.transform.position;

        //Rotates the enemy to face the player/object it is tracking
        Vector3 rotationLocation = new Vector3(lookPosition.x, 0f, lookPosition.z);
        itSelf.transform.LookAt(lookPosition);
  
    }
}
