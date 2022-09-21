using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    public int enemyID { get; set; }
    public String enemyName { get; set; }
    public int livePoints { get; set; }
    public TextMesh header { get; set; }

    public SpriteRenderer spriteRenderer;
    public int type { get; set; }

    public EnemyAnimationManager animationManager;

   private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animationManager = gameObject.GetComponent<EnemyAnimationManager>();
    }

   private void OnDestroy()
   {
       EnemyHandler.list.Remove(enemyID);
   }
   
    public void Move(Vector3 newPosition, Enemy enemy)
    {
        Vector3 lastPosition = enemy.transform.position;
        transform.position = newPosition;

        if (newPosition.x < lastPosition.x)
            spriteRenderer.flipX = true;
        if (newPosition.x > lastPosition.x)
            spriteRenderer.flipX = false;

        enemy.animationManager.AnimateBasedOnSpeed(lastPosition, newPosition);
    }
}
