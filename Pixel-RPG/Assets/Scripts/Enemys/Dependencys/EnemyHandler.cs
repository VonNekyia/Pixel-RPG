using System.Collections.Generic;
using RiptideNetworking;
using UnityEngine;

public enum EnemyTypeID : ushort {
    Umbala = 1,
    Wolf = 2,
    Death = 3,
}

public class EnemyHandler : MonoBehaviour {
    [SerializeField] private GameObject umbalaPrefab;
    
    public static Dictionary<int, Enemy> list = new Dictionary<int, Enemy>();
    

    [MessageHandler((ushort)ServerToClientID.enemySpawned)]
    private static void SpawnEnemy(Message message) {
        Spawn(message.GetInt(), message.GetInt(), message.GetVector3());
    }
    
    [MessageHandler((ushort)ServerToClientID.enemyMovement)]
    private static void MoveEnemy(Message message) {
        if (list.TryGetValue(message.GetInt(), out Enemy enemy)) {
            Vector3 pos = message.GetVector3();
            enemy.Move(pos, enemy);
        }
        
    }

    //private static int i; 
    [MessageHandler((ushort)ServerToClientID.enemyDied)]
    private static void KillEnemy(Message message) {
        if (list.TryGetValue(message.GetInt(), out Enemy enemy)) {
            bool state = message.GetBool();
            if (state) {
                enemy.animationManager.DefeatedTrigger();
            } else if (!state) {
                enemy.animationManager.DamagedTrigger();
            }
            
        }
    }
    
    public static void Spawn(int id, int type, Vector3 position) {
        
        GameObject enemyPrefab = type switch {
            (int)EnemyTypeID.Umbala => EnemyLogic.Singleton.UmbalaPrefab,
            (int)EnemyTypeID.Wolf => EnemyLogic.Singleton.WolfPrefab,
            (int)EnemyTypeID.Death => EnemyLogic.Singleton.DeathPrefab,
            _ => EnemyLogic.Singleton.UmbalaPrefab
        };
        string enemyName = type switch {
            (int)EnemyTypeID.Umbala => "Umbala",
            (int)EnemyTypeID.Wolf => "Wolf",
            (int)EnemyTypeID.Death => "Death",
            _ => "ERROR name not found;"
        };
        Enemy enemy = Instantiate(enemyPrefab, position, Quaternion.identity).GetComponent<Enemy>();
        enemy.header = Instantiate(GameLogic.Singleton.PlayerHeader, position + new Vector3(0,0.15f,0), Quaternion.identity).GetComponent<TextMesh>();
        enemy.header.text = enemyName;
        enemy.header.color = Color.red;
        enemy.header.transform.parent = enemy.transform;
        enemy.type = type;
        enemy.enemyID = id;
        list.Add(id, enemy);
    }
}
