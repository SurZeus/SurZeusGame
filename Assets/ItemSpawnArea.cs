using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnArea : MonoBehaviour
{
    public List<ItemSpawn> itemSpawns;
    // Start is called before the first frame update
    void Start()
    {

       
       
    }

    private void Awake()
    {
        Invoke("AddToItemManager", 2f);  
    }

    private void OnEnable()
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
    public void AddToItemManager()
    {
        ItemManager.itemManager.itemSpawnAreas.Add(gameObject.GetComponent<ItemSpawnArea>());
    }
}
