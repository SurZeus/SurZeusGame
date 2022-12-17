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
    public InventorySlot_UI prevSlot;
   
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
   
    
        if(clickedSlot.AssignedItemSlot.itemData != null)
        {
            GameManager.Instance.dropItemButton.gameObject.SetActive(true);
            if (clickedSlot.AssignedItemSlot.itemData.type == ItemType.Equipable)
            {
                GameManager.Instance.equipItemButton.gameObject.SetActive(true);
            }
            if (clickedSlot.AssignedItemSlot.itemData is ConsumableItemData)
            {
                UIManager.instance.consumeItemButton.gameObject.SetActive(true);
            }
        }
      

        
        clickedSlot.isSlotLocked = true;
       // bool isShiftPressed = Keyboard.current.leftShiftKey.isPressed;
        Debug.Log("slot clicked");

        //does the

        //clicked slot has an item - mouse doesnt have an i tem - pick up that item

        if(clickedSlot.AssignedItemSlot.itemData != null && mouseInventoryItem.AssignedInventorySlot.itemData == null)
        {
            /*if (isShiftPressed && clickedSlot.AssignedItemSlot.SplitStack(out InventorySlot halfStackSlot)) //split stakc
            {
                mouseInventoryItem.UpdateMouseSlot(halfStackSlot);
                clickedSlot.UpdateUISlot();
                return;
            }*/
            //pick up the item in the clicked slot.
            
                 mouseInventoryItem.UpdateMouseSlot(clickedSlot.AssignedItemSlot,clickedSlot);
            MouseItemData.prevSlot = clickedSlot;
               
                return;
            
           
        }

       else if(clickedSlot.AssignedItemSlot.itemData == null && mouseInventoryItem.AssignedInventorySlot.itemData  != null)
        {
            clickedSlot.AssignedItemSlot.AssignItem(mouseInventoryItem.AssignedInventorySlot);
            clickedSlot.UpdateUISlot();
            MouseItemData.prevSlot.ClearSlot();
            mouseInventoryItem.ClearSlot();
            //mouseInventoryItem.tempSlot.ClearSlot();
            return;
        }
        //clicked slot doesnbt but mouse does so place item in the empty slot

       else if(clickedSlot.AssignedItemSlot.itemData != null && mouseInventoryItem.AssignedInventorySlot.itemData != null && clickedSlot != MouseItemData.prevSlot)
        {
            bool isSameItem = clickedSlot.AssignedItemSlot.itemData == mouseInventoryItem.AssignedInventorySlot.itemData;
           
            if (isSameItem && mouseInventoryItem.AssignedInventorySlot.itemData && clickedSlot.AssignedItemSlot.EnoughRoomLeftInStack(mouseInventoryItem.AssignedInventorySlot.stackSize) )
            {
                clickedSlot.AssignedItemSlot.AssignItem(mouseInventoryItem.AssignedInventorySlot);
                clickedSlot.UpdateUISlot();
                Debug.Log("co sie odpierdala");
                mouseInventoryItem.ClearSlot();
                MouseItemData.prevSlot.ClearSlot();
               // mouseInventoryItem.tempSlot.ClearSlot();
            }

            else if(isSameItem && !clickedSlot.AssignedItemSlot.EnoughRoomLeftInStack(mouseInventoryItem.AssignedInventorySlot.stackSize,out int leftInStack))
            {
               // if (leftInStack < 1) SwapSlots(clickedSlot);
              
                
                    int remainingOnMouse = mouseInventoryItem.AssignedInventorySlot.stackSize - leftInStack;
                    clickedSlot.AssignedItemSlot.AddToStack(leftInStack);
                    clickedSlot.UpdateUISlot();

                    var newItem = new InventorySlot(mouseInventoryItem.AssignedInventorySlot.itemData, remainingOnMouse);
                    mouseInventoryItem.ClearSlot();
                    mouseInventoryItem.UpdateMouseSlot(newItem);
                MouseItemData.prevSlot.ClearSlot();
                return;
                
            }

            else if (clickedSlot.AssignedItemSlot.itemData != mouseInventoryItem.AssignedInventorySlot.itemData )
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
        // mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedItemSlot);
        // mouseInventoryItem.previouslyClickedSlot.Up
        //dateUISlot(clickedUISlot.AssignedItemSlot);
        //clickedUISlot.ClearSlot();
        MouseItemData.prevSlot.AssignedItemSlot.AssignItem(clickedUISlot.AssignedItemSlot);
        MouseItemData.prevSlot.UpdateUISlot();
        clickedUISlot.AssignedItemSlot.AssignItem(clonedSlot);
        clickedUISlot.UpdateUISlot();
    }
}
