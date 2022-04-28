using UnityEngine;

///<summary>
/// Parent calss that deals with all boss attacks
///</summary>
public class BossAttackState : BossState
{
    ///<summary>
    ///It rotates the enemy to face the target (locking the y rotation). 
    ///Parameter: playerPosition, vector3 that sends the information  where to rotate towards
    ///</summary>
    public void FaceTarget(){
        
        var lookPosition = target.transform.position;

        //Rotates the enemy to face the player/object it is tracking
        Vector3 rotationLocation = new Vector3(lookPosition.x, 0f, lookPosition.z);
        itSelf.transform.LookAt(lookPosition);
    }
}
