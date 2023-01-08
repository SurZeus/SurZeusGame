using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(UniqueID))]
[RequireComponent(typeof(Outline))]
//[RequireComponent(typeof(SphereCollider))]
public class ItemPickUp : MonoBehaviour
{

    // public InventoryHolder ih;
    public InventoryItemData item;
    public int quantity;
    private Outline outline;
    public int loadedAmmo; //ammo count,
    [SerializeField]
    private ItemPickUpSaveData itemSaveData;
    private string id;

    private void Awake()
    {
        outline = GetComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.OutlineWidth = 5;
       // GetComponent<SphereCollider>().radius = 2f;
       // GetComponent<SphereCollider>().isTrigger = true;
        gameObject.tag = "Item";
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
        gameObject.layer = LayerMask.NameToLayer("Item");
        //gameObject.layer
        // SaveLoad.OnLoadGame += LoadGame;
        // itemSaveData = new ItemPickUpSaveData(item, transform.position, transform.rotation);
    }
    // Start is called before the first frame update
    void Start()
    {
        id = GetComponent<UniqueID>().ID;
        // SaveManager.data.activeItems.Add(id, itemSaveData);
        if (item is AmmoItem)
        {
            quantity = Random.Range(1, 10);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        // if (SaveManager.data.activeItems.ContainsKey(id)) SaveManager.data.activeItems.Remove(id);
        // SaveLoad.OnLoadGame -= LoadGame;
    }
    private void LoadGame(SaveData data)
    {
        Debug.Log("cos nie dziala");
        // if (data.collectedItems.Contains(id)) Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {

        var inventory = other.transform.GetComponent<PlayerInventoryHolder>();

        if (!inventory) return;
        gameObject.GetComponent<BoxCollider>().enabled = false;


        if (inventory.AddToInventory((InventoryItemData)item, quantity))
        {

            //SaveManager.data.collectedItems.Add(id);
            Destroy(this.gameObject);

        }
    }


    // ih.PrimaryInventorySystem.AddToInventory(item, quantity);


    public void PickUpItem(PlayerInventoryHolder playerInventoryHolder)
    {
        var inventory = playerInventoryHolder;

        if (!inventory) return;
        Debug.Log("set ikmn");


        if (inventory.AddToInventory((InventoryItemData)item, quantity))
        {

            //SaveManager.data.collectedItems.Add(id);
            Destroy(this.gameObject);

        }
    }
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
