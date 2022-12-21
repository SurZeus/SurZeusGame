using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnArea : MonoBehaviour
{
    public List<ItemSpawn> itemSpawns;
    // Start is called before the first frame update
    void Start()
    {
       

        ItemManager.itemManager.itemSpawnAreas.Add(gameObject.GetComponent<ItemSpawnArea>());
    }

    private void Awake()
    {
      
    }

    // Update is called once per frame
   

   public void SpawnOnStart()
    {
        foreach(ItemSpawn x in itemSpawns)
        {
            x.SpawnItem();
        }
    }

    public void RespawnItems()
    {
        foreach (ItemSpawn x in itemSpawns)
        {
            x.ClearItem();
        }

        foreach (ItemSpawn x in itemSpawns)
        {
            x.SpawnItem();
        }
    }
}
