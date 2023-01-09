using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(UniqueID))]
public class ChestInventory : InventoryHolder, IInteractable
{

    public bool isOpen;
    public UnityAction<IInteractable> OnInteractionComplete { get; set; }
    private void Start()
    {
        DontDestroyOnLoad(this);
        isOpen = false;
      var chestSavedData = new InventorySaveData(primaryInventorySystem, transform.position, transform.rotation);
       SaveManager.data.chestDictionary.Add(GetComponent<UniqueID>().ID, chestSavedData);
    }

    private void Update()
    {
        if (isOpen)
        {
            CheckDistanceToPlayer();
        }
    }
    protected override void Awake()
    {
        base.Awake();
        SaveLoad.OnLoadGame += LoadInventory;
    }

    private void OnDestroy()
    {
        SaveLoad.OnLoadGame -= LoadInventory;
    }

    public void CheckDistanceToPlayer()
    {
        if(Vector3.Distance(gameObject.transform.position,GameManager.Instance.player.transform.position) > 3f)
        {
            OnDynamicInventoryDisplayRequested?.Invoke(primaryInventorySystem, 0);
            isOpen = false;
        }
    }
    protected override void LoadInventory(SaveData data)
    {
        //Debug.Log(data.chestDictionary.)
        if (data.chestDictionary.Count != 0)
        {
            if (data.chestDictionary.TryGetValue(GetComponent<UniqueID>().ID, out InventorySaveData chestData))
            {
                this.primaryInventorySystem = chestData.invSystem;
                this.transform.position = chestData.position;
                this.transform.rotation = chestData.rotation;
            }
        }
    }


    public void Interact(Interactor interactor, out bool interactSuccesful)
    {
        if (isOpen)
        {
            isOpen = false;
        }
        else isOpen = true;
        OnDynamicInventoryDisplayRequested?.Invoke(primaryInventorySystem,0);
        interactSuccesful = true;
    }

    public void EndInteraction()
    {
        
    }


}

