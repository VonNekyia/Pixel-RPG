using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmbalaAnimationManager : MonoBehaviour
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
        Umbala umbala = gameObject.GetComponent<Umbala>();
        
        EnemyHandler.list.Remove(umbala.enemyID);
        Destroy(umbala.gameObject);
    }
    
    public void AnimateBasedOnSpeed(Vector3 lastPosition, Vector3 newPosition)
    {
        
        float distanceMoved = Vector3.Distance(lastPosition, newPosition);
        bool d = distanceMoved > 0.001f;
        Debug.Log( distanceMoved);
        animator.SetBool("isWalking", distanceMoved > 0.001f);
    }
    
}
