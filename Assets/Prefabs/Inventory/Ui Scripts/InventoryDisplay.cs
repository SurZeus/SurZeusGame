using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public abstract class InventoryDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] MouseItemData mouseInventoryItem;
    protected Dictionary<InventorySlot_UI, InventorySlot> slotDictionary; //pair up the UI slots with the system slots;
    protected InventorySystem inventorySystem;
    public InventorySystem InventorySystem => inventorySystem;
    public Dictionary<InventorySlot_UI, InventorySlot> SlotDictionary => slotDictionary;

    protected virtual void Start()
    {

    }

    public abstract void AssignSlot(InventorySystem invToDisplay,int offset);//implemented in child classes.
    protected virtual void UpdateSlot(InventorySlot updatedSlot)
    {
        foreach(var slot in SlotDictionary)
        {
            if(slot.Value == updatedSlot) //Slot value - backend inventory slot
            {
                slot.Key.UpdateUISlot(updatedSlot); //slot key - the ui representaion of the value;
            }
        }
    }

    public void SlotClicked(InventorySlot_UI clickedSlot)
    {
        
        clickedSlot.isSlotLocked = true;
        bool isShiftPressed = Keyboard.current.leftShiftKey.isPressed;
        Debug.Log("slot clicked");

        //does the

        //clicked slot has an item - mouse doesnt have an i tem - pick up that item

        if(clickedSlot.AssignedItemSlot.itemData != null && mouseInventoryItem.AssignedInventorySlot.itemData == null)
        {
            if (isShiftPressed && clickedSlot.AssignedItemSlot.SplitStack(out InventorySlot halfStackSlot)) //split stakc
            {
                mouseInventoryItem.UpdateMouseSlot(halfStackSlot);
                clickedSlot.UpdateUISlot();
                return;
            }
            else //pick up the item in the clicked slot.
            {
                 mouseInventoryItem.UpdateMouseSlot(clickedSlot.AssignedItemSlot,clickedSlot);
                   // clickedSlot.ClearSlot();
                return;
            }
           
        }

        if(clickedSlot.AssignedItemSlot.itemData == null && mouseInventoryItem.AssignedInventorySlot.itemData  != null)
        {
            clickedSlot.AssignedItemSlot.AssignItem(mouseInventoryItem.AssignedInventorySlot);
            clickedSlot.UpdateUISlot();
            mouseInventoryItem.ClearSlot();
            mouseInventoryItem.tempSlot.ClearSlot();
            return;
        }
        //clicked slot doesnbt but mouse does so place item in the empty slot

        if(clickedSlot.AssignedItemSlot.itemData != null && mouseInventoryItem.AssignedInventorySlot.itemData != null && mouseInventoryItem.tempSlot != clickedSlot)
        {
            bool isSameItem = clickedSlot.AssignedItemSlot.itemData == mouseInventoryItem.AssignedInventorySlot.itemData;

            if (isSameItem && mouseInventoryItem.AssignedInventorySlot.itemData && clickedSlot.AssignedItemSlot.EnoughRoomLeftInStack(mouseInventoryItem.AssignedInventorySlot.stackSize) && mouseInventoryItem.tempSlot != clickedSlot)
            {
                clickedSlot.AssignedItemSlot.AssignItem(mouseInventoryItem.AssignedInventorySlot);
                clickedSlot.UpdateUISlot();
                mouseInventoryItem.ClearSlot();
                mouseInventoryItem.tempSlot.ClearSlot();
            }

            else if(isSameItem && !clickedSlot.AssignedItemSlot.EnoughRoomLeftInStack(mouseInventoryItem.AssignedInventorySlot.stackSize,out int leftInStack) && mouseInventoryItem.tempSlot != clickedSlot)
            {
                if (leftInStack < 1) SwapSlots(clickedSlot);
                else
                {
                    int remainingOnMouse = mouseInventoryItem.AssignedInventorySlot.stackSize - leftInStack;
                    clickedSlot.AssignedItemSlot.AddToStack(leftInStack);
                    clickedSlot.UpdateUISlot();

                    var newItem = new InventorySlot(mouseInventoryItem.AssignedInventorySlot.itemData, remainingOnMouse);
                    mouseInventoryItem.ClearSlot();
                    mouseInventoryItem.UpdateMouseSlot(newItem);
                    return;
                }
            }

            else if (clickedSlot.AssignedItemSlot.itemData != mouseInventoryItem.AssignedInventorySlot.itemData && mouseInventoryItem.tempSlot != clickedSlot)
            {
                SwapSlots(clickedSlot);
                return;
            }
        }

        //both slots have an item - what next
    }

    private void SwapSlots(InventorySlot_UI clickedUISlot)
    {
        var clonedSlot = new InventorySlot(mouseInventoryItem.AssignedInventorySlot.itemData, mouseInventoryItem.AssignedInventorySlot.stackSize);
        mouseInventoryItem.ClearSlot();
        mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedItemSlot);
        clickedUISlot.ClearSlot();

        clickedUISlot.AssignedItemSlot.AssignItem(clonedSlot);
        clickedUISlot.UpdateUISlot();
    }
}
