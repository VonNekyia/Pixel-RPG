using System;
using System.Collections;
using System.Collections.Generic;
using RiptideNetworking;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using MouseButton = Unity.VisualScripting.MouseButton;

public class PlayerController : MonoBehaviour
{
    
    private static PlayerController _singleton;
    public static PlayerController Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
                _singleton = value;
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(PlayerController)} instance already exists, destroying duplicate!");
                Destroy(value);
            }
        }
    }

    private bool[] inputs;
    bool isAttacking = true;
    public bool canMove;

    public void Awake()
    {
        Singleton = this;
    }
    
    private void Start()
    {
        inputs = new bool[5];
        canMove = true;
    }

    private void Update()
    {
        if (canMove && !ChatSystem.Singleton.chatInputField.isFocused) // Wenn der Spieler nicht im Chat ist
        {
            if (Input.GetKey(KeyCode.W)) 
                inputs[0] = true;
            if (Input.GetKey(KeyCode.S))
                inputs[1] = true;
            if (Input.GetKey(KeyCode.A))
                inputs[2] = true;
            if (Input.GetKey(KeyCode.D))
                inputs[3] = true;
            if (Input.GetKey(KeyCode.LeftShift))
                inputs[4] = true;
        }
        if (Input.GetMouseButtonDown(0)) // LeftClick Attack
        {
            SendAttack();
        }
    }

    private void FixedUpdate()
    {
        SendInput(); // Sendet Movement
        for (int i = 0; i < inputs.Length; i++ ) // resettet Movement boolean
            inputs[i] = false;
    }

    public void lockMovement()
    {
        canMove = false;
    }
    
    public void unLockMovement()
    {
        canMove = true;
    }
    
    #region Messages

    private void SendInput()
    {
        Message message = Message.Create(MessageSendMode.unreliable, ClientToServerID.input);
        message.AddBools(inputs, false);
        NetworkManager.Singleton.Client.Send(message);
    }
    
    private void SendAttack()
    {
        Message message = Message.Create(MessageSendMode.reliable, ClientToServerID.isAttacking);
        message.AddBool(isAttacking);
        NetworkManager.Singleton.Client.Send(message);
    }

    #endregion
}
