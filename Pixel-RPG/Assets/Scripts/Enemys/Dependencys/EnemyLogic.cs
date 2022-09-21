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
    

    public GameObject GoldenUmbalaPrefab => goldenUmbalaPrefab;
    public GameObject WolfPrefab => wolfPrefab;
    public GameObject DeathPrefab => deathPrefab;
    public GameObject GreenUmbalaPrefab => greenUmbalaPrefab;

   
    
    
    

    [Header("Enemy Prefabs")]
    [SerializeField] private GameObject goldenUmbalaPrefab;
    [SerializeField] private GameObject wolfPrefab;
    [SerializeField] private GameObject deathPrefab;
    [SerializeField] private GameObject greenUmbalaPrefab;


 
  
}
