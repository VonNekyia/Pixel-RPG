using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationManager : MonoBehaviour
{

    [SerializeField] private Animator animator;
    
    
    public void ReceiveDamage()
    {
        animator.SetTrigger("reveiveDamage");
    }
    
    public void DamagedTrigger()
    {
        animator.SetTrigger("damaged");
    }
    
    public void DefeatedTrigger()
    {
        animator.SetTrigger("defeated");
    }
    
    public void Defeated()
    {
        Enemy enemy = gameObject.GetComponent<Enemy>();
        
        EnemyHandler.list.Remove(enemy.enemyID);
        Destroy(enemy.gameObject);
    }
    
    public void AnimateBasedOnSpeed(Vector3 lastPosition, Vector3 newPosition)
    {
        float distanceMoved = Vector3.Distance(lastPosition, newPosition);
        bool d = distanceMoved > 0.001f;
        Debug.Log( distanceMoved);
        animator.SetBool("isWalking", distanceMoved > 0.001f);
    }
    
}
