using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerSaveData : MonoBehaviour
{
    // Start is called before the first frame update
    public int currentHealth;
    private PlayerData MyData = new PlayerData();
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /* MyData.PlayerPosition = transform.position;
         MyData.PlayerRotation = transform.rotation;

         MyData.CurrentHealth = currentHealth;

         if (Keyboard.current.rKey.wasPressedThisFrame)
         {
             SaveGameManager.CurrentSaveData.PlayerData = MyData;
             SaveGameManager.SaveGame();
         }
         if (Keyboard.current.tKey.wasPressedThisFrame)
         {
             SaveGameManager.LoadGame();
             MyData = SaveGameManager.CurrentSaveData.PlayerData;
             transform.position = MyData.PlayerPosition;
             transform.rotation = MyData.PlayerRotation;
             currentHealth = MyData.CurrentHealth;

         }
     }*/
    }
}
[System.Serializable]
public struct PlayerData
{
    public Vector3 PlayerPosition;
    public Quaternion PlayerRotation;
    public int CurrentHealth;
}