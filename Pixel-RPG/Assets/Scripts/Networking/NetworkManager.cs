using System;
using System.Net.Sockets;
using RiptideNetworking;
using RiptideNetworking.Utils;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

public enum ServerToClientID : ushort
{
    playerSpawned = 1,
    playerMovement = 2,
    playerMessage = 3,
    playerAttack = 4,
}

public enum ClientToServerID : ushort
{
    name = 1,
    input = 2,
    playerMessageSender = 3,
    isAttacking = 4,
}

public class NetworkManager : MonoBehaviour
{
    private static NetworkManager _singleton;

    public static NetworkManager Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
                _singleton = value;
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(NetworkManager)} instance already exists, destroying duplicate!");
                Destroy(value);
            }
        }
    }
    public Client Client { get; private set; }
    [Header("Connection")]
    [SerializeField] private string ip;
    [SerializeField] private ushort port;
    [SerializeField] private InputField ipOverwrite;
    [Header("Error")]
    [SerializeField] private Text ErrorText;
    public void Awake()
    {
        Singleton = this;
    }
    private void Start()
    {
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);
        
        Client = new Client();
        Client.Connected += DidConnect;
        Client.ConnectionFailed += FailedToConnect;
        Client.ClientDisconnected += PlayerLeft;
        Client.Disconnected += DidDisconnect;
    }
    private void FixedUpdate()
    {
        Client.Tick();
    }

    private void OnApplicationQuit()
    {
        Client.Disconnect();
    }

    public void Connect()
    {
        if (ipOverwrite.text != "")
            ip = ipOverwrite.text;
        Client.Connect($"{ip}:{port}");
        HomeCanvas.Singleton.SetErrorText($"STATUS: Connecting to {ip}:{port}");
    }

    private void DidConnect(object sender, EventArgs e)
    {
        HomeCanvas.Singleton.SendName();
        HomeCanvas.Singleton.disableCanvas();
    }

    private void FailedToConnect(object sender, EventArgs e)
    {
        HomeCanvas.Singleton.BackToMain();
        HomeCanvas.Singleton.SetErrorText("ERROR: Connection to Server failed! Please try again or contact a server administrator.");
    }

    private void PlayerLeft(object sender, ClientDisconnectedEventArgs e)
    {
        if(Player.list.TryGetValue(e.Id, out Player player))
            Destroy(Player.list[e.Id].gameObject);
    }
    
    private void DidDisconnect(object sender, EventArgs e)
    {
        HomeCanvas.Singleton.BackToMain();
        foreach (Player player in Player.list.Values)
            Destroy(player.gameObject);
        
    }
}
