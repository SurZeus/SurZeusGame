using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AmunitionType
{
    STANAG,AK,SHEELS
}
[CreateAssetMenu(menuName = "Range Weapon Data")]

public class RangeWeaponData : WeaponInventoryData
{

    public float timeBetweenShooting;
    public float spread;
    public float reloadTime;
    public float timeBetweenShots;
    public int magazineSize;
    public int bulletsPerTap;
   
 
    public AmunitionType amunitionType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
