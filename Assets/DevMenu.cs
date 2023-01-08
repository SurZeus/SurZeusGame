using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevMenu : MonoBehaviour
{
    public static DevMenu devMenu;
    public InventoryItemData itemToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        devMenu = this;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnItem()
    {
        if(itemToSpawn != null)
        Instantiate(itemToSpawn.prefab, GameManager.Instance.player.transform.position + GameManager.Instance.player.transform.forward * 2, GameManager.Instance.player.transform.rotation);
    }
}
