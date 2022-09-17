using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    public ushort enemyID { get; set; }
    public String enemyName { get; set; }
    public int livePoints { get; set; }
    
    public void Move(Vector3 newPosition, Umbala umbala)
    {
        //SpriteRenderer spriteRenderer = umbala.GetComponent<SpriteRenderer>();
        Vector3 lastPosition = umbala.transform.position;
        
        transform.position = newPosition;
/*
        if (newPosition.x < lastPosition.x)
            spriteRenderer.flipX = true;
        if (newPosition.x > lastPosition.x)
            spriteRenderer.flipX = false;
*/
        //animationManager.AnimateBasedOnSpeed(lastPosition, newPosition);
    }
}
