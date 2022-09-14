using System;
using System.Collections;
using System.Collections.Generic;
using RiptideNetworking;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Dictionary<ushort, Player> list = new Dictionary<ushort, Player>();
    public ushort Id { get; private set; }
    public bool IsLocal { get; private set; }
    public TextMesh header { get; private set; }

    [SerializeField] private UI_Inventory uiInventory;
    private Inventory inventory;
    
    [SerializeField] private PlayerAnimationManager animationManager;
    private Camera mainCamera;

    public string username { get; private set;}

    private void Awake()
    {
        inventory = new Inventory();
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnDestroy()
    {
        list.Remove(Id);
    }

    private void LocalMove(Vector3 newPosition)
    {
        Vector3 modPosition = newPosition;
        modPosition.z = -10;
        mainCamera.transform.position = modPosition;
    }
    private void Move(Vector3 newPosition, Player player)
    {
        SpriteRenderer spriteRenderer = player.GetComponent<SpriteRenderer>();
        Vector3 lastPosition = player.transform.position;
        
        transform.position = newPosition;

        if (newPosition.x < lastPosition.x)
            spriteRenderer.flipX = true;
        if (newPosition.x > lastPosition.x)
            spriteRenderer.flipX = false;
        //Custom Axis makes this part useless @deprecated
        //spriteRenderer.sortingOrder = (int)(((-newPosition.y) * 100));
        
        animationManager.AnimateBasedOnSpeed(lastPosition, newPosition);
    }
    
    public static void Spawn(ushort id, string username, Vector3 position)
    {
        Player player;

        if (id == NetworkManager.Singleton.Client.Id)
        {
            player = Instantiate(GameLogic.Singleton.LocalPlayerPrefab, position, Quaternion.identity).GetComponent<Player>();
            player.IsLocal = true;
            player.header = Instantiate(GameLogic.Singleton.PlayerHeader, position + new Vector3(0,0.09f,0), Quaternion.identity).GetComponent<TextMesh>();
            player.header.color = Color.green;
        }
        else
        {
            player = Instantiate(GameLogic.Singleton.PlayerPrefab, position, Quaternion.identity).GetComponent<Player>();
            player.IsLocal = false;
            player.header = Instantiate(GameLogic.Singleton.PlayerHeader, position + new Vector3(0,0.09f,0), Quaternion.identity).GetComponent<TextMesh>();
            //player.header.color = Color.yellow;
        }
        
        player.header.transform.parent = player.transform;
        player.name = $"Player {id} ({(string.IsNullOrEmpty(username) ? "Guest" : username)})";
        player.Id = id;
        player.username = username;
        player.header.text = username;
        

        list.Add(id,player);
    }

    public void lockMovement()
    {
        PlayerController.Singleton.canMove = false;
    }
    
    public void unLockMovement()
    {
        PlayerController.Singleton.canMove = true;
    }

    #region Messages
    [MessageHandler((ushort)ServerToClientID.playerSpawned)]
    private static void SpawnPlayer(Message message)
    {
        Spawn(message.GetUShort(), message.GetString(), message.GetVector3());
    }

    [MessageHandler((ushort)ServerToClientID.playerMovement)]
    private static void PlayerMovement(Message message)
    {
     

        if (list.TryGetValue(message.GetUShort(), out Player player))
        {
            Vector3 pos = message.GetVector3();
            player.Move(pos, player);
            if (player.IsLocal)
                player.LocalMove(pos);
        }
                
   
    }


    #endregion
    

}
