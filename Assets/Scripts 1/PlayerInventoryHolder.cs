using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
public class PlayerInventoryHolder : InventoryHolder
{

  
    public static UnityAction OnPlayerInventoryChange;
    public static UnityAction<InventorySystem, int> OnPlayerInventoryDisplayRequested;
    // public static UNityAction
    // Update is called once per frame
   

    private void Start()
    {
        SaveManager.data.playerInventory = new InventorySaveData(primaryInventorySystem);
    }
    protected override void LoadInventory(SaveData data)
    {
        if (data.playerInventory.invSystem != null)
        {
            this.primaryInventorySystem = data.playerInventory.invSystem;
            OnPlayerInventoryChange?.Invoke();
        }
    }
    public bool AddToInventory(InventoryItemData data, int amount)
    {
        if (primaryInventorySystem.AddToInventory(data, amount))
        {
            return true;
        }

       
        

        return false;
    }

    

    public void OpenInventory()
    {
         OnPlayerInventoryDisplayRequested?.Invoke(primaryInventorySystem, offset);
    }
}
