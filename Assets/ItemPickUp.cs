using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(UniqueID))]
public class ItemPickUp : MonoBehaviour
{

  // public InventoryHolder ih;
    public InventoryItemData item;
    public int quantity;
    public int loadedAmmo; //ammo count,
    [SerializeField] 
    private ItemPickUpSaveData itemSaveData;
    private string id;

    private void Awake()
    {
  
        SaveLoad.OnLoadGame += LoadGame;
        itemSaveData = new ItemPickUpSaveData(item, transform.position, transform.rotation);
    }
    // Start is called before the first frame update
    void Start()
    {
        id = GetComponent<UniqueID>().ID;
        SaveManager.data.activeItems.Add(id, itemSaveData);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        if (SaveManager.data.activeItems.ContainsKey(id)) SaveManager.data.activeItems.Remove(id);
        SaveLoad.OnLoadGame -= LoadGame;
    }
    private void LoadGame(SaveData data)
    {
        Debug.Log("cos nie dziala");
        if (data.collectedItems.Contains(id)) Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        var inventory = other.transform.GetComponent<PlayerInventoryHolder>();

        if (!inventory) return;
        Debug.Log("set ikmn");

        
            if (inventory.AddToInventory((InventoryItemData)item, quantity))
            {
               
                SaveManager.data.collectedItems.Add(id);
                Destroy(this.gameObject);

            }
        }


       // ih.PrimaryInventorySystem.AddToInventory(item, quantity);
    
}

[System.Serializable]
public struct ItemPickUpSaveData
{
    public InventoryItemData itemData;
    public Vector3 Position;
    public Quaternion Rotation;
    public ItemPickUpSaveData(InventoryItemData _itemData ,Vector3 _position, Quaternion _rotation)
    {
        itemData = _itemData;
        Position = _position;
        Rotation = _rotation;
        
    }
}
