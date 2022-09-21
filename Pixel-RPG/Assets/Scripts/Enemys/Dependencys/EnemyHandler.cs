using System.Collections.Generic;
using RiptideNetworking;
using UnityEngine;

public enum EnemyTypeID : ushort {
    GoldenUmbala = 1,
    Wolf = 2,
    Death = 3,
    GreenUmbala = 4,
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
        Debug.Log("hi");
        GameObject enemyPrefab = type switch {
            (int)EnemyTypeID.GoldenUmbala => EnemyLogic.Singleton.GoldenUmbalaPrefab,
            (int)EnemyTypeID.Wolf => EnemyLogic.Singleton.WolfPrefab,
            (int)EnemyTypeID.Death => EnemyLogic.Singleton.DeathPrefab,
            (int)EnemyTypeID.GreenUmbala => EnemyLogic.Singleton.GreenUmbalaPrefab,
            _ => EnemyLogic.Singleton.GoldenUmbalaPrefab
        };
        Debug.Log("hi2");
        string enemyName = type switch {
            (int)EnemyTypeID.GoldenUmbala => "Golden Umbala",
            (int)EnemyTypeID.Wolf => "Wolf",
            (int)EnemyTypeID.Death => "Death",
            (int)EnemyTypeID.GreenUmbala => "Green Umbala",
            _ => "ERROR name not found;"
        };
        Debug.Log("hi3");
        Enemy enemy = Instantiate(enemyPrefab, position, Quaternion.identity).GetComponent<Enemy>(); Debug.Log("hi4");
        enemy.header = Instantiate(GameLogic.Singleton.PlayerHeader, position + new Vector3(0,0.15f,0), Quaternion.identity).GetComponent<TextMesh>();
        enemy.header.text = enemyName;
        enemy.header.color = Color.red;
        enemy.header.transform.parent = enemy.transform;
        enemy.type = type;
        enemy.enemyID = id;
        list.Add(id, enemy);
    }
}
