using System;
using System.Collections;
using System.Collections.Generic;
using RiptideNetworking;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class ChatSystem : MonoBehaviour
{
    
    private static ChatSystem _singleton;
    public static ChatSystem Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
                _singleton = value;
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(ChatSystem)} instance already exists, destroying duplicate!");
                Destroy(value);
            }
        }
    }

    
    public int maxMessages = 25;
    public GameObject chatPanel, textObject, header;
    public InputField chatInputField;
    public Color PLAYER, INFO, LOCALPLAYER;
    public CanvasGroup canvasGroup;
    public bool fadeIn = false, fadeOut = false;

    [SerializeField]
    List<ClientMessage> messageList = new List<ClientMessage>();


    
    public void Awake()
    {
        Singleton = this;
        canvasGroup = chatInputField.transform.parent.gameObject.GetComponent<CanvasGroup>();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !Singleton.header.activeSelf)
        {
            header.SetActive(true);
            fadeChatIn();
            
        }
        if (Input.GetKeyDown(KeyCode.Return) && !chatInputField.isFocused)
        {
            chatInputField.gameObject.SetActive(true);
        }
        if(chatInputField.text != "")
            if (Input.GetKeyDown(KeyCode.Return))
            {
                sendMessage();
                chatInputField.text = "";
                chatInputField.gameObject.SetActive(false);
            }
            else
        if(!chatInputField.isFocused && Input.GetKeyDown(KeyCode.Return))
            chatInputField.ActivateInputField();

        if (fadeIn)
            if (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.deltaTime;
                if (canvasGroup.alpha >= 1) fadeIn = false;
            }
        if (fadeOut)
            if (canvasGroup.alpha >= 0)
            {
                canvasGroup.alpha -= Time.deltaTime;
                if (canvasGroup.alpha == 0) fadeOut = false;
            }
    }
    
    public void SendMessageToChat(string text, ClientMessage.MessageType messageType)
    {
        if (messageList.Count >= maxMessages)
        {
            Destroy(messageList[0].textObject.gameObject);
            messageList.Remove(messageList[0]);
        }
            
        ClientMessage newMessage = new ClientMessage();
        newMessage.text = text;

        GameObject newText = Instantiate(textObject, chatPanel.transform);

        newMessage.textObject = newText.GetComponent<Text>();
        newMessage.textObject.text = newMessage.text;
        newMessage.textObject.color = MessageTypeColor(messageType);
        
        messageList.Add(newMessage);
    }
    
    Color MessageTypeColor(ClientMessage.MessageType messageType)
    {
        Color color = Color.white;
        switch (messageType)
        {
            case ClientMessage.MessageType.INFO:
                color = INFO;
                break;
            case ClientMessage.MessageType.PLAYER:
                color = PLAYER;
                break;
            case ClientMessage.MessageType.LOCALPLAYER:
                color = LOCALPLAYER;
                break;
        }
        return color;
    }

    public void sendMessage()
    {
        Message message = Message.Create(MessageSendMode.reliable, ClientToServerID.playerMessageSender);
        message.AddString(chatInputField.text);
        NetworkManager.Singleton.Client.Send(message);
    }
    
    [MessageHandler((ushort)ServerToClientID.playerMessage)]
    private static void GetMessage(Message message)
    {
        if (Player.list.TryGetValue(message.GetUShort(), out Player player))
        {
            String messageString = message.GetString();
            messageString = player.username + ": " + messageString;
            if (!Singleton.header.activeSelf)
            {
                Singleton.header.SetActive(true);
                Singleton.fadeChatIn();
            }
            Singleton.StartCoroutine(Singleton.FadeWindowOut(8,Singleton.header));
            Singleton.SendMessageToChat(messageString, player.IsLocal ? ClientMessage.MessageType.LOCALPLAYER : ClientMessage.MessageType.PLAYER );
        }
    }
    
    IEnumerator FadeWindowOut(int seconds, GameObject gameObject)
    {
        yield return new WaitForSeconds(seconds);
        fadeChatOut();
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
    
    public void fadeChatIn()
    {
        fadeIn = true;
    }
    
    public void fadeChatOut()
    {
        fadeOut = true;
    }
    
}

[System.Serializable]
public class ClientMessage
{
    public string text;
    public Text textObject;
    public MessageType messageType;
    public enum MessageType
    {
        LOCALPLAYER,
        PLAYER,
        INFO
    }
}

