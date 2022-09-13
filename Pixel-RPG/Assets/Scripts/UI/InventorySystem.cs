using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{

    [Header("ChatComponents")] [SerializeField] public GameObject InventoryWindowImage;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !InventoryWindowImage.activeSelf)
            InventoryWindowImage.SetActive(true);
        else if(Input.GetKeyDown(KeyCode.I) && InventoryWindowImage.activeSelf)
            InventoryWindowImage.SetActive(false);
    }
}
