using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
public class InventoryManager : MonoBehaviour
{
    public InventorySystem inventory = new InventorySystem(12);
    public GameObject itemSlotPrefab;
    public Transform inventoryUI;
   public static  InventoryManager inventoryManager;
    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
    public void RefreshInventory()
    {
        foreach (var _item in inventory.inventorySlots)
        {
            // ItemSlot tempSlot = new ItemSlot(_item.item,1);

            GameObject obj = Instantiate(itemSlotPrefab, inventoryUI);
            var itemImage = obj.transform.Find("Item/Image").GetComponent<Image>();
            var itemCount = obj.transform.Find("Item/itemCount").GetComponent<TextMeshProUGUI>();
           // itemImage.sprite = _item.item.itemScriptable.itemIcon;
            itemCount.text = _item.stackSize.ToString();
        }
    }
    public void ListItems2D()
    {
        foreach(Transform item in inventoryUI)
        {
            Destroy(item.gameObject);
        }
        foreach(var _item in inventory.inventorySlots)
        {
           // ItemSlot tempSlot = new ItemSlot(_item.item,1);

            /*GameObject obj = Instantiate(itemSlotPrefab, inventoryUI);
            var itemImage = obj.transform.Find("Item/Image").GetComponent<Image>();
            var itemCount = obj.transform.Find("Item/itemCount").GetComponent<TextMeshProUGUI>();
            itemImage.sprite = _item.item.ir.itemIcon;
            itemCount.text = _item.stackSize.ToString();*/
        }
    }
   
}

[System.Serializable]
public class InventorySystem
{
   
    public bool isFull;
    public int maxWeight;
    public List<InventorySlot> inventorySlots = new List<InventorySlot>();
    public int InventorySize => inventorySlots.Count;

    public UnityAction<InventorySlot> OnItemSlotChanged;
    public InventorySystem(int size)
    {
        inventorySlots = new List<InventorySlot>(size);

        for(int i =0; i< size; i++)
        {
            inventorySlots.Add(new InventorySlot());
        }
    }

    public bool AddToInventory(InventoryItemData itemToAdd, int amountToAdd)
    {
        Debug.Log("TESTUJE");
        if(ContainsItem(itemToAdd,out List<InventorySlot> itemSlot))
        {
            Debug.Log("CONTAINS");
            foreach (var slot in itemSlot)
            {
                if (slot.EnoughRoomLeftInStack(amountToAdd))
                {
                    slot.AddToStack(amountToAdd);
                    OnItemSlotChanged?.Invoke(slot);
                    return true;
                }
            }
           
        }
       
        if (HasFreeSlot(out InventorySlot freeSlot))
        {
            if (freeSlot.EnoughRoomLeftInStack(amountToAdd))
            {
                freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
                OnItemSlotChanged?.Invoke(freeSlot);
                return true;
            }

            //add implementation to only that waht can fill the stack, and chec another free slot to put remainder in.
        }
     
        return false;
       

    }

    public bool ContainsItem(InventoryItemData itemToAdd, out  List<InventorySlot> itemSlot)//do any of our slots have the item to add in them?
    {
        
        itemSlot = inventorySlots.Where(i => i.itemData== itemToAdd).ToList(); //if they do, get a list of all of them
        return itemSlot.Count > 1 ? true : false; //if so return true, if not return false;
        
    }

    public bool HasFreeSlot(out InventorySlot freeSlot)
    {

        freeSlot = inventorySlots.FirstOrDefault(I => I.itemData == null);//get the first free slot
        return freeSlot == null ? false : true;
    }
}




[System.Serializable]
public class InventorySlot : ISerializationCallbackReceiver
{
    [SerializeField] private int _itemID = -1;
    [NonSerialized] public InventoryItemData itemData; //reference to the data
    public bool isEquipped = false;
    public int stackSize; //current stack size - how many of the data do we have
    public InventorySlot(InventoryItemData item, int count)  //constructor to make a occupied inventory slot
    {
        this.itemData = item;
        _itemID = itemData.id;
        this.stackSize = count;
    }


    public bool SplitStack(out InventorySlot splitStack) //is th
    {
        if (stackSize <= 1)// is there enough to actually split. If not return false
        {
            splitStack = null;
            return false;
        }

        int halfStack = Mathf.RoundToInt(stackSize / 2);
        RemoveFromStack(halfStack); //get half the stack
        splitStack = new InventorySlot(itemData, halfStack); //creates a copy of this slot with half the stack size
        return true;

    }
    public void AddToStack(int value)
    {
        stackSize += value;
    } public void RemoveFromStack(int value)
    {
        stackSize -= value;
    }

    public InventorySlot() //Constructor to make empty inventory slot

    {
        ClearSlot();

    }

    public void UpdateInventorySlot(InventoryItemData data, int amount) //updates slot directly.
    {
        itemData = data;
        stackSize = amount;
        _itemID = itemData.id;
    }

    public void AssignItem(InventorySlot itemSlot) //assigns an item to the slot
    {
        if (itemData == itemSlot.itemData) //does the slot contain the same item? add to stack if so.
        {
            AddToStack(itemSlot.stackSize);

        }
        else// Overwrite slot with inventpory slot that we are passing in
        {
            itemData = itemSlot.itemData;
            _itemID = itemData.id;
            stackSize = 0;
            AddToStack(itemSlot.stackSize);

        }
    }
    public void ClearSlot() //clears the slot
    {
        itemData = null;
        _itemID = -1;
        stackSize = -1;
    }

    public bool EnoughRoomLeftInStack(int amountToAdd)
    {

        if (itemData == null || itemData != null && stackSize + amountToAdd <= itemData.MaxStackSize) return true;
        else return false;
    } 
    public bool EnoughRoomLeftInStack(int amountToAdd, out int amountRemaining) //would there be enpough room in the stack for the amount we are trying to add
    {
        amountRemaining = itemData.MaxStackSize - stackSize;
        return EnoughRoomLeftInStack(amountToAdd);
    }

    public void OnBeforeSerialize()
    {
       
    }

    public void OnAfterDeserialize()
    {
        if (_itemID == -1) return;
        var db = Resources.Load<Database>("Database");
        itemData = db.GetItem(_itemID);
    }
}