using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    
    [SerializeField] private Animator animator;
    [SerializeField] private float playerMoveSpeed;


    public void AnimateBasedOnSpeed(Vector3 lastPosition, Vector3 newPosition)
    {
        float distanceMoved = Vector3.Distance(lastPosition, newPosition);
        animator.SetBool("isMoving", distanceMoved > 0.001f);
    }

    public void Attacking()
    {
        animator.SetTrigger("onAttack");
    }
}

