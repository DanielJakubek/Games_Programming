using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackingState : EnemyState
{
    [Header("Enemy Properties")]
    public bool isAttacking = true; // For checking if the animation has finished
    
    
    //Setter for the is attacking bool
    public void setIsAttacking(bool temp){
        isAttacking = temp;
    }
}
