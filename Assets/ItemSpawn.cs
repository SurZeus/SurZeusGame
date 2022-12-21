using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{

    public List<InventoryItemData> itemsThatCanSpawnAtThisSpawn;
    public List<int> pool1;
    public List<int> pool2;
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
        Debug.Log("xD");
      var itemToSpawn = PickItemToSpawn();
        var spawnedItem = Instantiate(itemToSpawn.prefab, gameObject.transform.position, Quaternion.identity);
        spawnedItem.gameObject.transform.SetParent(gameObject.transform);

    


    }

    private InventoryItemData PickItemToSpawn()
    {
        
        List<int> pool = new List<int>();
        for(int i = 0; i < itemsThatCanSpawnAtThisSpawn.Count; i++)
        {
            for(int j = 0; j < itemsThatCanSpawnAtThisSpawn[i].rarityLevel; j++)
            {
                pool.Add(itemsThatCanSpawnAtThisSpawn[i].id);
            }
        }
        pool1 = pool;


        pool = shuffleGOList(pool);
        pool2 = pool;
        int index = Random.Range(0, pool.Count);

        
       
        tempItem = itemsThatCanSpawnAtThisSpawn.Find((x) => x.id == pool[index]);
        return tempItem;
    }

    
   

    private List<int> shuffleGOList(List<int> inputList)
    {    //take any list of GameObjects and return it with Fischer-Yates shuffle
        int i = 0;
        int t = inputList.Count;
        int r = 0;
        int p = 0;
        List<int> tempList = new List<int>();
        tempList.AddRange(inputList);

        while (i < t)
        {
            r = Random.Range(i, tempList.Count);
            p = tempList[i];
            tempList[i] = tempList[r];
            tempList[r] = p;
            i++;
        }

        return tempList;
    }


    public void ClearItem()
    {
        if(transform.childCount !=0 )
        {
            DestroyObject(gameObject.transform.GetChild(0).gameObject);
        }
    }
}