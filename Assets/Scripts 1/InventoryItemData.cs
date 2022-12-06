using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// thats a scriptable object, that defines what an it ies in my game
/// it could be inherited from to have branched version of items, for example otinos and equipment
/// </summary>
public enum ItemType
{
Weapon,Food,Ammunition,Equipable
}
[CreateAssetMenu(menuName ="Inventory Item Data")]
public class InventoryItemData : ScriptableObject
{
    public int id = -1;
    public ItemType type;
    public int MaxStackSize;
    public string itemName;
    public Sprite itemIcon;
    public GameObject prefab;
   
  
}
