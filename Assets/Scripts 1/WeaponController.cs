using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchControlsKit;

public class WeaponController : MonoBehaviour
{
   // public InventoryItemData currentItemData;
    public GameObject currentItem;
    public InventorySlot currentItemSlot;
    public Camera camera;
    public bool isAttacking;
    public int damage;
    public GameObject Axe;
    public bool CanAttack = true;
    public float AttackCooldown = 1.0f;
    public AudioClip AttackSound;
    public AudioSource audioSource;
    public Animator anim;
    public WeaponHolderItem[] weaponsList;
    // Start is called before the first frame update

    [Header("Weapon Sway")]
    public Transform weaponSwayObject;
    public float swayAmountA=5;
    public float swayAmountB=10;
    public float swayScale = 100;
    public float swayLerpSpeed = 14;
    public float swayTime;
    public Vector3 swayPosition;
    private void CalculateWeaponSway()
    {
        var targetPosition = LissajousCurve(swayTime, swayAmountA, swayAmountB) / swayScale;
        swayTime += Time.deltaTime;
        swayPosition = Vector3.Lerp(swayPosition, targetPosition, Time.smoothDeltaTime * swayLerpSpeed) ;
        weaponSwayObject.localPosition = swayPosition;
            
    }
    private Vector3 LissajousCurve(float Time,float A, float B)
    {
        return new Vector3(Mathf.Sin(Time), A *  Mathf.Sin(B * Time * Mathf.PI));
    }

    void Start()
    {

        camera = Camera.main;
        damage = 25;
        anim = Axe.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
       if( TCKInput.GetAction("useBtn", EActionEvent.Down))
        {
            if (CanAttack)
            {
                AxeAttack();
            }
        }

        CalculateWeaponSway();
    }

    public void AxeAttack()
    {

        Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f); // center of the screen
        float rayLength = 2f; //zasieg broni
        // actual Ray
        Ray ray = Camera.main.ViewportPointToRay(rayOrigin);
        // debug Ray
        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayLength))
        {
            if(hit.collider != null && hit.collider.CompareTag("Enemy"))
            {
                hit.collider.gameObject.GetComponent<Enemy>().getDamage(damage);
            }
           
           
        }
        CanAttack = false;
        isAttacking = true;
        anim.SetTrigger("Attack");
        StartCoroutine(ResetAttackCooldown());
        audioSource.PlayOneShot(AttackSound);

    }

    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(AttackCooldown);
        StartCoroutine(ResetAttackBool());

        CanAttack = true;
    }
    IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(1.0f);
        isAttacking = false;
    }

    public void Test1()
    {
        Debug.Log("xD");
        Player.player.WeaponHolder.GetComponent<WeaponController>().anim.SetBool("isWalking", true);
    }

    public void Test2()
    {
        Debug.Log("xD");
        Player.player.WeaponHolder.GetComponent<WeaponController>().anim.SetBool("isWalking", false);
    }


    public void EquipToHands( InventorySlot item)
    {
        Debug.Log("etest");

        if(currentItem!= null)
        {
            currentItem.gameObject.SetActive(false);
            currentItem = null;
        }
        for (int i = 0; i < weaponsList.Length; i++)
        {
            if(weaponsList[i].GetComponent<WeaponHolderItem>().weapon == item.itemData)
            {
                weaponsList[i].gameObject.SetActive(true);
                currentItem = weaponsList[i].gameObject;
                currentItemSlot = item;
                if (item is RangeWeaponData)
                {
                    weaponsList[i].GetComponent<GunSystem>().bulletsLeft = item.loadedAmmo;
                }

            }
        }
        
        

        //temp.gameObject.transform.SetParent(this.transform);
        //temp.GetComponent<ItemPickUp>().enabled = false;
        
    }
}
