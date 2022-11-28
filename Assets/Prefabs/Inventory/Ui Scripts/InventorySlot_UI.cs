using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InventorySlot_UI : MonoBehaviour
{

    [SerializeField] private Image itemSprite;
    [SerializeField] public TextMeshProUGUI itemCount;
    [SerializeField] private InventorySlot assignedItemSlot;
    public bool isSlotLocked = false;

    public Button button;
    public InventorySlot AssignedItemSlot => assignedItemSlot;
    public InventoryDisplay ParentDisplay { get; private set; }

    private void Awake()
    {
        ClearSlot();
        itemSprite.preserveAspect = true;
        button.GetComponent<Button>();
        button?.onClick.AddListener(OnUISlotClick);
        ParentDisplay = transform.parent.GetComponent<InventoryDisplay>();

    }

    public void OnUISlotClick()
    {
        ParentDisplay?.SlotClicked(this);
    }

    public void Init(InventorySlot itemSlot)
    {
        assignedItemSlot = itemSlot;
        UpdateUISlot(itemSlot);
    }

    public void UpdateUISlot(InventorySlot slot)
    {
        if (slot.itemData != null)
        {
            itemSprite.sprite = slot.itemData.itemIcon;
            itemSprite.color = Color.white;

            if (slot.stackSize > 1) itemCount.text = slot.stackSize.ToString();
            else itemCount.text = "";
        }
        else
        {
            ClearSlot();
        }



    }

    public void UpdateUISlot()
    {
        if (assignedItemSlot != null)
        {
            UpdateUISlot(assignedItemSlot);
        }
    }
    public void ClearSlot()
    {
        assignedItemSlot?.ClearSlot();
        itemSprite.sprite = null;
        itemSprite.color = Color.clear;
        itemCount.text = "";

    }
    public void ClearSlot(int i)
    {
        assignedItemSlot?.ClearSlot();
       

    }
    // Start is called before the first frame update

}