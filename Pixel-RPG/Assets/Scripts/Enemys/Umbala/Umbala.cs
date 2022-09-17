using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Umbala : Enemy
{
    [SerializeField] public UmbalaAnimationManager animationManager;

    public static ushort ID = 0;

    public TextMesh header { get; set; }

    private void OnDestroy()
    {
        EnemyHandler.list.Remove(enemyID);
    }
    
  

}
