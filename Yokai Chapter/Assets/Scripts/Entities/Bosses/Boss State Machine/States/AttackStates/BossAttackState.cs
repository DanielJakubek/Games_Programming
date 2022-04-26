using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  
    Deals with the attacks of the boss. This is the
    beam attack and fireball attack
*/
public class BossAttackState : BossState
{
    /*  
        It rotates the enemy to face the target (locking the y rotation). 
        Parameter: playerPosition, vector3 that sends the information  where to rotate towards
    */
    public void FaceTarget(){
        
        var lookPosition = target.transform.position;

        //Rotates the enemy to face the player/object it is tracking
        Vector3 rotationLocation = new Vector3(lookPosition.x, 0f, lookPosition.z);
        itSelf.transform.LookAt(lookPosition);
  
    }
}
