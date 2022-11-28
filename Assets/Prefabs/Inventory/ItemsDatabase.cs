using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Item Database")]
public class ItemsDatabase : ScriptableObject
{

    public List<InventoryItemData> itemDatabase;
    // Start is called before the first frame update
    void Start()
    {
      //  itemDatabase = new List<ItemScriptable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
