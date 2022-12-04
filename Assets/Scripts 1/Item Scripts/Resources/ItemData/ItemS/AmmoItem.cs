using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AmunitionType
{
    STANAG, AK, SHEELS
}

[CreateAssetMenu(menuName = "Amunition")]
public class AmmoItem : InventoryItemData
{
    
    
    public AmunitionType amunitionType;


     
    

}
