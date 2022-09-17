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
    
    
    
}
