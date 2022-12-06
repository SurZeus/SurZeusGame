using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponAnimations : MonoBehaviour
{
    public Animator animator;
    private void OnEnable()
    {
        FirstPersonMovement.PlayerIsMoving += WalkingWithTheGun;
    } private void OnDisable()
    {
        FirstPersonMovement.PlayerIsMoving -= WalkingWithTheGun;
    }
    private void Awake()
    {
        animator = this.GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WalkingWithTheGun(int state)
    {
        if(state == 1)
        animator.SetBool("isWalking", true);
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}
