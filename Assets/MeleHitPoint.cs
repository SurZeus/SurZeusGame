using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleHitPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public BoxCollider hitPoint;
    public float damage;
    void Start()
    {
       hitPoint = GetComponent<BoxCollider>();
        damage = GetComponentInParent<MeleAttack>().damage;
        
    }

    // Update is called once per frame


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
       // Debug.Log("testuje hita");
            GetComponentInParent<MeleAttack>().MeleDamage(other);
        Instantiate(other.GetComponentInParent<Enemy>().bloodParticle, gameObject.transform.position,Quaternion.identity);
        
    }
}
