using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class MouseItemData : MonoBehaviour
{
    private Transform _playerTransform;
    public Image ItemSprite;
    public static InventorySlot_UI prevSlot;
    public TextMeshProUGUI ItemCount;
    public InventorySlot AssignedInventorySlot;
   // public InventorySlot_UI tempSlot;
   // public Transform lastItemsPosition;

   // public InventorySlot_UI previouslyClickedSlot;
    // Start is called before the first frame update

    private void Awake()
    {
        _playerTransform = GameManager.Instance.player.transform;
        ItemSprite.preserveAspect = true;
        ItemSprite.color = Color.clear;
        ItemCount.text = "";
    }



    public void UpdateMouseSlot(InventorySlot invSlot)
    {
        if(invSlot.itemData.type == ItemType.Equipable)
        {
            GameManager.Instance.equipItemButton.gameObject.SetActive(true);
        }
        GameManager.Instance.dropItemButton.gameObject.SetActive(true);
        AssignedInventorySlot.AssignItem(invSlot);
        UpdateMouseSlot();
    }
    public void UpdateMouseSlot(InventorySlot invSlot, InventorySlot_UI clickedSlot)
    {
       
        //previouslyClickedSlot = clickedSlot;
       /* if (invSlot.itemData.type == ItemType.Equipable)
        {
            GameManager.Instance.equipItemButton.gameObject.SetActive(true);
        }

        if (invSlot.itemData is ConsumableItemData)
        {
            UIManager.instance.consumeItemButton.gameObject.SetActive(true);
        }*/
        GameManager.Instance.dropItemButton.gameObject.SetActive(true);
        //tempSlot = clickedSlot;
        AssignedInventorySlot.AssignItem(invSlot);
        UpdateMouseSlot();
    }
    public void UpdateMouseSlot()
    {
     
       // ItemSprite.color = Color.white;
        //ItemSprite.sprite = AssignedInventorySlot.itemData.itemIcon;
       // ItemCount.text = AssignedInventorySlot.stackSize.ToString();
    }

   
    public void ConsumeItem()
    {
        if (AssignedInventorySlot.itemData != null && AssignedInventorySlot.itemData is ConsumableItemData)
        {
            var temp = (ConsumableItemData)AssignedInventorySlot.itemData;
            Player.player.playerStamina += temp.energyValue;
            Player.player.playerHunger += temp.foodValue;
            if (Player.player.playerHunger >= 101) Player.player.playerHunger = 100;
            Player.player.playerThirst += temp.drinkValue;
            if (Player.player.playerThirst >= 101) Player.player.playerThirst = 100;
            UIManager.instance.UpdatePlayerStatistics();
            
        }
        UIManager.instance.consumeItemButton.gameObject.SetActive(false);
        prevSlot.ClearSlot();
        prevSlot.UpdateUISlot();
        ClearSlot();
        /*if (tempSlot != null)
        {
            tempSlot.ClearSlot();
        }*/
    }
    public void EquipItem()
    {
      //  Debug.Log("equipped");
        if (AssignedInventorySlot.itemData != null && AssignedInventorySlot.itemData.type == ItemType.Equipable)
        {
            // transform.position = Input.mousePosition;


            // if (AssignedInventorySlot.itemData.prefab != null)

            GameManager.Instance.equipItemButton.gameObject.SetActive(false);
        GameManager.Instance.dropItemButton.gameObject.SetActive(false);
        GameManager.Instance.weaponHolder.EquipToHands(AssignedInventorySlot);
        
            // GameManager.Instance.OnWeaponDisabled.Invoke();
            // Instantiate(AssignedInventorySlot.itemData.prefab, _playerTransform.position + _playerTransform.forward * 1f, Quaternion.identity);
            /*   if (AssignedInventorySlot.stackSize > 1)
               {
                   AssignedInventorySlot.AddToStack(-1);
                   tempSlot.UpdateUISlot(AssignedInventorySlot);
                   //tempSlot.
                   UpdateMouseSlot();
               }
               else
               {
                   ClearSlot();
                   if (tempSlot != null)
                   {
                       tempSlot.ClearSlot();
                   }
               }*/


        }
    }
    public void Dropitem()
    {
        if (AssignedInventorySlot.itemData != null)
        {
            // transform.position = Input.mousePosition;


            if (AssignedInventorySlot.itemData.prefab != null) { 

               // Debug.Log("asigned: " + AssignedInventorySlot.itemData);
               // Debug.Log("current: "  + GameManager.Instance.weaponHolder.currentItem);
                if (GameManager.Instance.weaponHolder.currentItem != null && AssignedInventorySlot.itemData == GameManager.Instance.weaponHolder.currentItem.GetComponent<WeaponHolderItem>().weapon)
                {
                    GameManager.Instance.weaponHolder.currentItem.gameObject.SetActive(false);
                    GameManager.Instance.weaponHolder.currentItemSlot = null;

                }
                MouseItemData.prevSlot.ClearSlot();
                Instantiate(AssignedInventorySlot.itemData.prefab, _playerTransform.position + _playerTransform.forward * 1f, Quaternion.identity);
            }
            if (AssignedInventorySlot.stackSize > 1)
            {

               // Debug.Log("xDdada");
                AssignedInventorySlot.AddToStack(-1);
                //tempSlot.UpdateUISlot(AssignedInventorySlot);
                //tempSlot.
                UpdateMouseSlot();
                prevSlot.AssignedItemSlot.AssignItem(AssignedInventorySlot);
                prevSlot.UpdateUISlot();
                ClearSlot();
            }

            else
            {
               // Debug.Log("cleamn");
               ClearSlot();
               
            }


        }
    }
    public void ClearSlot()
    {
        AssignedInventorySlot.ClearSlot();
        ItemCount.text = "";
        ItemSprite.color = Color.clear;
        ItemSprite.sprite = null;
        GameManager.Instance.dropItemButton.gameObject.SetActive(false);
        UIManager.instance.consumeItemButton.gameObject.SetActive(false);
        GameManager.Instance.equipItemButton.gameObject.SetActive(false);
        
    }
    public static bool IsPointerOverUIObject()
    {
       // bool isOverUI = false;
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = Mouse.current.position.ReadValue();
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        foreach(var ray in results)
        {
            if(ray.gameObject.CompareTag("DropItem") == true)
            {
                return true;
            }
        }
        /* foreach (var ray in results)
         {
             if (ray.gameObject.CompareTag("UI"))
             {
                 return false;
             }
         }*/
        return false;

       

    }
}