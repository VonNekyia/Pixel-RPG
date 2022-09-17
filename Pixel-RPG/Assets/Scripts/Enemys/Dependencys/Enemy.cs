using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    public ushort enemyID { get; set; }
    public String enemyName { get; set; }
    public int livePoints { get; set; }

    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void Move(Vector3 newPosition, Umbala umbala)
    {
        Vector3 lastPosition = umbala.transform.position;
        transform.position = newPosition;

        if (newPosition.x < lastPosition.x)
            spriteRenderer.flipX = true;
        if (newPosition.x > lastPosition.x)
            spriteRenderer.flipX = false;

        umbala.animationManager.AnimateBasedOnSpeed(lastPosition, newPosition);
    }
}
