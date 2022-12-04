using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasingPlayerState : StateMachineBehaviour
{

    NavMeshAgent enemyAgent;
    Enemy enemy;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyAgent = animator.GetComponentInParent<Enemy>().enemyAgent;
        enemy = animator.GetComponentInParent<Enemy>();

        enemyAgent.SetDestination(GameManager.Instance.player.transform.position);
        //Debug.Log(enemyAgent.destination.t);
        enemyAgent.speed = 2;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemyAgent.isActiveAndEnabled)
        {
            enemyAgent.SetDestination(GameManager.Instance.player.transform.position);
            Debug.Log("Distance to player" + enemyAgent.remainingDistance);
            if (enemyAgent.remainingDistance <= enemyAgent.stoppingDistance)
            {
                animator.SetBool("isAttacking", true);
                // animator.SetBool("isWalking", false);

            }

            if (enemyAgent.remainingDistance >= 60)
            {
                animator.SetBool("isChasingPlayer", false);
                // animator.SetBool("isWalking", false);

            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
           animator.SetBool("isChasingPlayer", false);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
