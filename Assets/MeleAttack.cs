using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchControlsKit;
using Unity.VisualScripting;
using UnityEngine.InputSystem.HID;

public class MeleAttack : MonoBehaviour
{




    public bool isAttacking;
    public int damage;
    public LayerMask layer;
    public GameObject Axe;
    public bool CanAttack = true;
    public float AttackCooldown = 1.0f;
    public AudioClip AttackSound;
    public AudioClip HitSound;
    public AudioSource audioSource;
    public Animator anim;
    public WeaponHolderItem weaponHolderItem;
    public BoxCollider hitPoint;
    public bool canDoDamage;
    



    void Start()
    {
        canDoDamage = false;
        hitPoint = GetComponentInChildren<MeleHitPoint>().gameObject.GetComponent<BoxCollider>();
        weaponHolderItem = GetComponent<WeaponHolderItem>();
        damage = weaponHolderItem.weapon.damage;
        anim = gameObject.GetComponentInParent<Animator>();
        audioSource = GetComponent<AudioSource>();
        isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();


    }
    public void MyInput()
    {
        if (TCKInput.GetAction("shootBtn", EActionEvent.Down) || TCKInput.GetAction("shootBtn2", EActionEvent.Down))
        {
            if (CanAttack)
            {
                AxeAttack();
            }
        }
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

   


    IEnumerator damageEnemyWithDelay(float delay,RaycastHit hit,bool oneShootKill)
    {
        yield return new WaitForSeconds(delay);
        if (oneShootKill) hit.collider.gameObject.GetComponentInParent<Enemy>().getDamage(100);
        else hit.collider.gameObject.GetComponentInParent<Enemy>().getDamage(damage);
    }

    public void MeleDamage(Collider enemy)
    {

        if (canDoDamage)
        {
            StartCoroutine(AudioManager.instance.playSoundWithDelay(HitSound, 0f));
            Debug.Log("damagfe");
            if (enemy.CompareTag("EnemyBody"))
                enemy.GetComponent<Collider>().gameObject.GetComponentInParent<Enemy>().getDamage(damage);
            else if (enemy.CompareTag("EnemyHead"))
                enemy.GetComponent<Collider>().gameObject.GetComponentInParent<Enemy>().getDamage(100);
        }
    }

    
    public void SetHitPoint()
    {
        if (hitPoint.enabled == true)hitPoint.enabled = false;
        else hitPoint.enabled = true;
        if (canDoDamage)canDoDamage = false;
        else canDoDamage = true;

    }

}
