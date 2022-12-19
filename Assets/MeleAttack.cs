using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchControlsKit;

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





    void Start()
    {

        weaponHolderItem = GetComponent<WeaponHolderItem>();
        damage = weaponHolderItem.weapon.damage;
        anim = Axe.GetComponent<Animator>();
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
        if (Physics.Raycast(ray, out hit, rayLength,layer))
        {
            if (hit.collider != null && hit.collider.CompareTag("EnemyHead"))
            {
                Debug.Log("EnemyHit");
                StartCoroutine(damageEnemyWithDelay(0.5f, hit,true));
                StartCoroutine(AudioManager.instance.playSoundWithDelay(HitSound, 0.5f));

            }
            else if (hit.collider != null && hit.collider.CompareTag("EnemyBody"))
            {
                Debug.Log("EnemyHit");
                StartCoroutine(damageEnemyWithDelay(0.5f, hit, false));
                StartCoroutine(AudioManager.instance.playSoundWithDelay(HitSound, 0.5f));
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

   


    IEnumerator damageEnemyWithDelay(float delay,RaycastHit hit,bool oneShootKill)
    {
        yield return new WaitForSeconds(delay);
        if (oneShootKill) hit.collider.gameObject.GetComponentInParent<Enemy>().getDamage(100);
        else hit.collider.gameObject.GetComponentInParent<Enemy>().getDamage(damage);
    }

}
