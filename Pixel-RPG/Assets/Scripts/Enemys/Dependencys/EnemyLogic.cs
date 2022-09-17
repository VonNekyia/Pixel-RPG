using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    private static EnemyLogic _singleton;
    public static EnemyLogic Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
                _singleton = value;
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(EnemyLogic)} instance already exists, destroying duplicate!");
                Destroy(value);
            }
        }
        
    }
    public void Awake()
    {
        Singleton = this;
    }
    

    public GameObject UmbalaPrefab => umbalaPrefab;

   
    
    
    

    [Header("Enemy Prefabs")]
    [SerializeField] private GameObject umbalaPrefab;


 
  
}
