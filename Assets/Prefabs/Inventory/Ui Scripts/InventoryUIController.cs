using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
public class InventoryUIController : MonoBehaviour
{

    public static UnityAction<InventorySystem, int> OnPlayerInventoryDisplayRequested;
    private void Awake()
    {
        inventoryPanel.gameObject.SetActive(false);
        playerBackpackPanel.gameObject.SetActive(false);
    }
    public DynamicInventoryDisplay playerBackpackPanel;
    
   [FormerlySerializedAs("chestPanel")] public DynamicInventoryDisplay inventoryPanel;

    // Update is called once per frame
    private void OnEnable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested += DisplayChestInventory;
        PlayerInventoryHolder.OnPlayerInventoryDisplayRequested += DisplayPlayerInventory;
       
    }
    private void OnDisable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested -= DisplayChestInventory;
        PlayerInventoryHolder.OnPlayerInventoryDisplayRequested -= DisplayPlayerInventory;
    }
    void Update()
    {

       
           
            if(inventoryPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                inventoryPanel.gameObject.SetActive(false);
            }
            if(playerBackpackPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                 playerBackpackPanel.gameObject.SetActive(false);
            }
        
    }

   public void DisplayInventory(InventorySystem invToDisplay, int offset)
    {
        inventoryPanel.gameObject.SetActive(true);
        inventoryPanel.RefreshDynamicInventory(invToDisplay, offset);
    }
   public void DisplayPlayerInventory(InventorySystem invToDisplay, int offset)
    {
        if (playerBackpackPanel.gameObject.activeInHierarchy)
        {
            playerBackpackPanel.gameObject.SetActive(false);
        }
        else
        {

            playerBackpackPanel.gameObject.SetActive(true);
            playerBackpackPanel.RefreshDynamicInventory(invToDisplay, offset);
        }

    }public void DisplayChestInventory(InventorySystem invToDisplay, int offset)
    {
        if (inventoryPanel.gameObject.activeInHierarchy)
        {
            inventoryPanel.gameObject.SetActive(false);
        }
        else
        {

            inventoryPanel.gameObject.SetActive(true);
            inventoryPanel.RefreshDynamicInventory(invToDisplay, offset);
        }

    }
    

    

   

   
   
}
