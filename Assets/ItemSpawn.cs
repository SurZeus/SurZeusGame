using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{

    public List<InventoryItemData> itemsThatCanSpawnAtThisSpawn;
 
    public InventoryItemData tempItem;
   
    // Start is called before the first frame update
    void Start()
    {
        
        GetComponentInParent<ItemSpawnArea>().itemSpawns.Add(gameObject.GetComponent<ItemSpawn>());
      //  InventoryItemData tempItem = new InventoryItemData();
       
    }

    // Update is called once per frame
 
    public void SpawnItem()
    {
        int isActive; //1 yes 2 no
        isActive = Random.Range(0, 2);

        if (isActive == 1)
        {
            var itemToSpawn = SelectRandomItem();
            if (itemToSpawn != null)
            {
                var spawnedItem = Instantiate(itemToSpawn.prefab, gameObject.transform.position, Quaternion.identity);
                spawnedItem.gameObject.transform.SetParent(gameObject.transform);
            }
            else Debug.Log("no items to spawn");

        }
        Debug.Log("spawn deactivated");

    }

   
    
   

   


    public void ClearItem()
    {
        if(transform.childCount !=0 )
        {
            DestroyObject(gameObject.transform.GetChild(0).gameObject);
        }
    }

    public InventoryItemData SelectRandomItem()
    {
        int randomNumber = Random.Range(0, 101);
        List<InventoryItemData> possibleItemsToAdd = new List<InventoryItemData>();
        foreach(InventoryItemData item in itemsThatCanSpawnAtThisSpawn)
        {
            if(randomNumber<= item.rarityLevel)
            {
                possibleItemsToAdd.Add(item);
            }
        }

        if (possibleItemsToAdd.Count > 0)
        {
            return possibleItemsToAdd[Random.Range(0, possibleItemsToAdd.Count)];
        }

        else return null;

    }
}
