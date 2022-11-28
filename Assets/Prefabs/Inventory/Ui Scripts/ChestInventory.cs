using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(UniqueID))]
public class ChestInventory : InventoryHolder, IInteractable
{

    private void Start()
    {
        
        var chestSavedData = new InventorySaveData(primaryInventorySystem, transform.position, transform.rotation);
        SaveManager.data.chestDictionary.Add(GetComponent<UniqueID>().ID, chestSavedData);
    }
    public UnityAction<IInteractable> OnInteractionComplete { get; set; }
    protected override void Awake()
    {
        base.Awake();
        SaveLoad.OnLoadGame += LoadInventory;
    }

    protected override void LoadInventory(SaveData data)
    {
        if(data.chestDictionary.TryGetValue(GetComponent<UniqueID>().ID,out InventorySaveData chestData))
        {
            this.primaryInventorySystem = chestData.invSystem;
            this.transform.position = chestData.position;
            this.transform.rotation = chestData.rotation;
        }
    }


    public void Interact(Interactor interactor, out bool interactSuccesful)
    {
        OnDynamicInventoryDisplayRequested?.Invoke(primaryInventorySystem,0);
        interactSuccesful = true;
    }

    public void EndInteraction()
    {
        
    }


}

