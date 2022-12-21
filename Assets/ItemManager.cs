using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager itemManager;
    public List<ItemSpawnArea> itemSpawnAreas;
    public float timer;
    public float respawnTime;
    private void Start()
    {
        timer = -5;
        respawnTime = 10f;
        itemManager = this;
    }

    private void Awake()
    {
        Invoke("SpawnOnStart", 5f);
    }
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= respawnTime)
        {
            timer = 0;
            RespawnItems();
        }
    }


    public void SpawnOnStart()
    {

       
        foreach (ItemSpawnArea x in itemSpawnAreas)
        {
            x.Invoke("SpawnOnStart", 1f);
        }
    }

    public void RespawnItems()
    {
        foreach (ItemSpawnArea x in itemSpawnAreas)
        {
            x.RespawnItems();
        }
    }
}

