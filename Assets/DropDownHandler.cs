using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropDownHandler : MonoBehaviour
{
    public TextMeshProUGUI TextBox;
    public Database DB;
    public List<InventoryItemData> items;
    // Start is called before the first frame update
    void Start()
    {

     
        var dropdown = transform.GetComponent<TMP_Dropdown>();
        dropdown.options.Clear();

        
       items = new List<InventoryItemData>();
       
        foreach (var dbitem in DB._itemDatabase)
        {
            items.Add(dbitem);
           
        }

        
        foreach (var item in items)
        {
           
            dropdown.options.Add(new TMP_Dropdown.OptionData() { text = item.itemName });
           
            
        }
        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
    }

    void DropdownItemSelected(TMP_Dropdown dropdown)
    {
        int index = dropdown.value;
       TextBox.text = dropdown.options[index].text;
        DevMenu.devMenu.itemToSpawn = ItemSelected(dropdown);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public InventoryItemData ItemSelected(TMP_Dropdown dropdown)
    {
        for(int i = 0; i < DB._itemDatabase.Count; i++)
        {
            if(DB._itemDatabase[i].itemName == dropdown.options[dropdown.value].text)
            {
                return DB._itemDatabase[i];
            }
        }
        return null;
    }
   
    
}
