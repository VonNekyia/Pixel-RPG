using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using RiptideNetworking;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HomeCanvas : MonoBehaviour
{
    
    
    
    private static HomeCanvas _singleton;

    public static HomeCanvas Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
                _singleton = value;
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(HomeCanvas)} instance already exists, destroying duplicate!");
                Destroy(value);
            }
        }
    }

    [Header("Connect")] 
    [SerializeField] private GameObject homeCanvas;
    [SerializeField] private InputField usernameField;
    [Header("UI")] 
    [SerializeField] private Text ErrorText;

    private void Awake()
    {
        Singleton = this;
    }

    public void ConnectClicked()
    {
        if (usernameField.text.Length == 0) SetErrorText("ERROR: Bitte geben Sie einen Benutzernamen an!");
        else if (usernameField.text.Length >= 20 ) SetErrorText("ERROR: Ihr Nutzername darf nicht länger als 20 Zeichen sein");
        else if (usernameField.text.Contains(" ")) SetErrorText("ERROR: Keine Leerzeichen.");
        else 
        {
            usernameField.interactable = false;
            NetworkManager.Singleton.Connect();    
        }
    }
    
    public void ExitClicked()
    {
        Application.Quit();
    }

    public void BackToMain()
    {
        usernameField.interactable = true;
        homeCanvas.SetActive(true);
    }

    public void SendName()
    {
        Message message = Message.Create(MessageSendMode.reliable, ClientToServerID.name);
        message.AddString(usernameField.text);
        NetworkManager.Singleton.Client.Send(message);
    }

    public void disableCanvas()
    {
        homeCanvas.SetActive(false);
    }
    
    
    public void SetErrorText(String message)
    {
        ErrorText.text = message;
        if (message.StartsWith("STATUS:"))
        {
            ErrorText.color = Color.yellow;
        }
        else if (message.StartsWith("ERROR:"))
        {
            ErrorText.color = Color.red;
        }
        else
        {
            ErrorText.color = Color.magenta;
        }
        
    }
    

    
}
