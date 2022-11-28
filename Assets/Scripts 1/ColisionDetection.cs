using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionDetection : MonoBehaviour
{
    // Start is called before the first frame update
    public WeaponController weaponController;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy") && weaponController.isAttacking == true)
        {
            other.GetComponent<Enemy>().getDamage(weaponController.damage);
            Destroy(other.gameObject, 10);
            Debug.Log("hit");
           
        }
    }

    
    
       
    
    
}
