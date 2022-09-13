using System;
using System.Collections;
using System.Collections.Generic;
using RiptideNetworking;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private bool[] inputs;


    private void Start()
    {
        inputs = new bool[5];
    }

    private void Update()
    {

        if (!ChatSystem.Singleton.chatInputField.isFocused)
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
    }

    private void FixedUpdate()
    {
        SendInput();

        for (int i = 0; i < inputs.Length; i++ ) 
            inputs[i] = false;
        
    }

    #region Messages

    private void SendInput()
    {
        Message message = Message.Create(MessageSendMode.unreliable, ClientToServerID.input);
        message.AddBools(inputs, false);
        NetworkManager.Singleton.Client.Send(message);
    }

    #endregion
}
