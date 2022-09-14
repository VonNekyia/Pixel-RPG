using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    
    private static PlayerAnimationManager _singleton;
    public static PlayerAnimationManager Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
                _singleton = value;
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(PlayerAnimationManager)} instance already exists, destroying duplicate!");
                Destroy(value);
            }
        }
    }
    
    [SerializeField] private Animator animator;
    [SerializeField] private float playerMoveSpeed;
    private Vector3 lastPosition;
    
    public void Awake()
    {
        Singleton = this;
    }
    
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

