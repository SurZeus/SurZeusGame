using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponAnimations : MonoBehaviour
{
    public Animator animator;
    private void OnEnable()
    {
        EventManager.playerIsWalking += WalkingWithTheGun;
    } private void OnDisable()
    {
        EventManager.playerIsWalking -= WalkingWithTheGun;
    }
    private void Awake()
    {
        animator = this.GetComponent<Animator>();
    }
 

    // Update is called once per frame
   

    public void WalkingWithTheGun(bool state)
    {
        if(state == true)
        animator.SetBool("isWalking", true);
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}
