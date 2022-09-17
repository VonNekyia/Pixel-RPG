using System;
using System.Collections;
using System.Collections.Generic;
using RiptideNetworking;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    [SerializeField] private GameObject umbalaPrefab;
    
    public static Dictionary<ushort, Umbala> list = new Dictionary<ushort, Umbala>();
    
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            foreach (Umbala umbala in list.Values)
            {
                Debug.Log(umbala.enemyID);
            }
        }
    }

    [MessageHandler((ushort)ServerToClientID.enemySpawned)]
    private static void SpawnEnemy(Message message)
    {
        Spawn(message.GetUShort(),  message.GetVector3());
    }
    
    [MessageHandler((ushort)ServerToClientID.enemyMovement)]
    private static void MoveEnemy(Message message)
    {
        if (list.TryGetValue(message.GetUShort(), out Umbala umbala))
        {
            Vector3 pos = message.GetVector3();
            umbala.Move(pos, umbala);
        }
        
    }

    //private static int i;
    [MessageHandler((ushort)ServerToClientID.enemyDied)]
    private static void KillEnemy(Message message)
    {
        if (list.TryGetValue(message.GetUShort(), out Umbala umbala))
        {
            bool state = message.GetBool();
            if (state)
            {
                umbala.animationManager.DefeatedTrigger();
            } else if (!state)
            {
                umbala.animationManager.DamagedTrigger();
            }
            
        }
    }
    
    public static void Spawn(ushort id, Vector3 position)
    {
        //Umbala wird net zum umbala
        Umbala umbala = Instantiate(EnemyLogic.Singleton.UmbalaPrefab, position, Quaternion.identity).GetComponent<Umbala>();
        umbala.header = Instantiate(GameLogic.Singleton.PlayerHeader, position + new Vector3(-0.145f,0.15f,0), Quaternion.identity).GetComponent<TextMesh>();
        umbala.header.text = "Umbala";
        umbala.header.color = Color.red;
        umbala.header.transform.parent = umbala.transform;
        umbala.enemyID = id;
        list.Add(id,umbala);
    }
}
