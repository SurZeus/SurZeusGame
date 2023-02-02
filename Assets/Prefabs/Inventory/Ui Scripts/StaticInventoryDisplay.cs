using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInventoryDisplay : InventoryDisplay
{
    [SerializeField] private InventoryHolder inventoryHolder;
    [SerializeField] private InventorySlot_UI[] slots;
    public override void AssignSlot(InventorySystem invToDisplay, int offset)
    {
        slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();

        for (int i = 0; i < inventoryHolder.Offset; i++)
        {
            slotDictionary.Add(slots[i], inventorySystem.inventorySlots[i]);
            slots[i].Init(inventorySystem.inventorySlots[i]);
        }
    }

    private void OnEnable()
    {
        //  DontDestroyOnLoad(this.gameObject);
        PlayerInventoryHolder.OnPlayerInventoryChange += RefreshStaticDisplay;
    }

    private void OnDisable()
    {
        PlayerInventoryHolder.OnPlayerInventoryChange -= RefreshStaticDisplay;
    }

    protected override void Start()
    {
        base.Start();
        RefreshStaticDisplay();
    }
    // Start is called before the first frame update

    private void RefreshStaticDisplay()
    {
        if (inventoryHolder != null)
        {
            inventorySystem = inventoryHolder.PrimaryInventorySystem;
            inventorySystem.OnItemSlotChanged += UpdateSlot;
        }
        else Debug.LogWarning($"No inventory assigned to {this.gameObject}");
        AssignSlot(inventorySystem, 0);
    }
}