using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData 
{
    [SerializeField]
   // public List<string> collectedItems;
   // public SerializableDictionary<string, InventorySaveData> chestDictionary;
   // public SerializableDictionary<string, ItemPickUpSaveData> activeItems;
    public InventorySaveData playerInventory;
    public PlayerSaveData playerSaveData;

    public SaveData()
    {
      //  collectedItems = new List<string>();
      //  activeItems = new SerializableDictionary<string, ItemPickUpSaveData>();
      //  chestDictionary = new SerializableDictionary<string, InventorySaveData>();
        playerInventory = new InventorySaveData();
        playerSaveData = new PlayerSaveData();


    }
}
