using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    
    /*
     *  Singelton stellt sicher, dass von der Klasse nur ein Object existiert
     */
    
    private static GameLogic _singleton;
    public static GameLogic Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
                _singleton = value;
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(GameLogic)} instance already exists, destroying duplicate!");
                Destroy(value);
            }
        }
        
    }
    
    /*
     *  Events
     */
    
    public void Awake()
    {
        Singleton = this;
    }
    
    /*
     *  Zuordnung der Prefabs für den loken Spieler und externe Spieler
     */
    
    public GameObject LocalPlayerPrefab => localPlayerPrefab;
    public GameObject PlayerPrefab => playerPrefab;
    public TextMesh PlayerHeader => playerHeader;
    public Text TextPrefab => textMessage;
    
    

    [Header("Prefabs")] 
    [SerializeField] private GameObject localPlayerPrefab;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private TextMesh playerHeader;
    [Header("ChatSystem")] 
    [SerializeField] private Text textMessage;
    
    

}
