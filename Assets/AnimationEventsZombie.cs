using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventsZombie : MonoBehaviour
{
    public Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    // Update is called once per frame
   
    public void AttackPlayerAnimationEvent()
    {
        enemy.AttackPlayer();
    }
}
