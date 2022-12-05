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
    public TextMeshProUGUI ItemCount;
    public InventorySlot AssignedInventorySlot;
    public InventorySlot_UI tempSlot;
    public Transform lastItemsPosition;
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
        if (invSlot.itemData.type == ItemType.Equipable)
        {
            GameManager.Instance.equipItemButton.gameObject.SetActive(true);
        }
        GameManager.Instance.dropItemButton.gameObject.SetActive(true);
        tempSlot = clickedSlot;
        AssignedInventorySlot.AssignItem(invSlot);
        UpdateMouseSlot();
    }
    public void UpdateMouseSlot()
    {
     
       // ItemSprite.color = Color.white;
        //ItemSprite.sprite = AssignedInventorySlot.itemData.itemIcon;
       // ItemCount.text = AssignedInventorySlot.stackSize.ToString();
    }

    private void Update()
    {
       

    }
    public void EquipItem()
    {
        Debug.Log("equipped");
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
            

            if (AssignedInventorySlot.itemData.prefab != null)
            {
                if(AssignedInventorySlot.itemData == GameManager.Instance.weaponHolder.currentItem)
                {
                    GameManager.Instance.weaponHolder.currentItem.gameObject.SetActive(false);
                    GameManager.Instance.weaponHolder.currentItemSlot = null;

                }
                Instantiate(AssignedInventorySlot.itemData.prefab, _playerTransform.position + _playerTransform.forward * 1f, Quaternion.identity);
            }
            if (AssignedInventorySlot.stackSize > 1)
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