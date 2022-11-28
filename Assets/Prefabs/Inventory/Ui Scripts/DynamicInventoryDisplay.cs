using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DynamicInventoryDisplay : InventoryDisplay
{
    [SerializeField] protected InventorySlot_UI slotPrefab;
    public override void AssignSlot(InventorySystem invToDisplay,int offset)
    {
        ClearSlots();
        slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();
        if (invToDisplay == null) return;

        for(int i = offset; i < invToDisplay.InventorySize; i++)
        {
            var uiSlot = Instantiate(slotPrefab, transform);
            slotDictionary.Add(uiSlot, invToDisplay.inventorySlots[i]);
            uiSlot.Init(invToDisplay.inventorySlots[i]);
        }
    }

    protected override void Start()
    {
       // InventoryHolder.OnDynamicInventoryDisplayRequested += RefreshDynamicInventory;
        base.Start();
       // AssignSlot(inventorySystem);

    }
    private void OnDestroy()
    {
        //InventoryHolder.OnDynamicInventoryDisplayRequested -= RefreshDynamicInventory;
    }

    public void RefreshDynamicInventory(InventorySystem invToDisplay, int offset)
    {
        ClearSlots();
        inventorySystem = invToDisplay;
        if(inventorySystem != null) inventorySystem.OnItemSlotChanged += UpdateSlot;
        AssignSlot(invToDisplay, offset);
    }

    private void ClearSlots()
    {
        foreach(var item in transform.Cast<Transform>())
        {
            Destroy(item.gameObject);
        }

        if(slotDictionary != null)
        {
            slotDictionary.Clear();
        }
    }

    private void OnDisable()
    {
        if (inventorySystem != null) inventorySystem.OnItemSlotChanged -= UpdateSlot;
    }
}
