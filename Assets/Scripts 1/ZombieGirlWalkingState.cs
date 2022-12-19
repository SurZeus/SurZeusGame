using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ZombieGirlWalkingState : StateMachineBehaviour
{
    public Transform centrePoint;
    public float range;
    NavMeshAgent enemyAgent;
    Enemy enemy;
    float timer = 0;
    Vector3 destination;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)

    {
        //Debug.Log("Entered walking state");
        enemyAgent = animator.GetComponentInParent<Enemy>().enemyAgent;
        enemy = animator.GetComponentInParent<Enemy>();
        enemy.GoToRandomPoint();
       


    }




    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        Debug.DrawRay(destination, Vector3.up, Color.blue, 1.0f);
        Debug.DrawLine(enemy.gameObject.transform.position,enemy.currentDestination,Color.red);
        //jezeli patroluje i nie slyszal wystrzalu
        if (enemy.heardNoise == false)
        {

            if (enemyAgent.isActiveAndEnabled && timer >=2f)
            {
                if (enemyAgent.remainingDistance <= enemyAgent.stoppingDistance)
                {
                    animator.SetBool("isWalking", false);

                }
            }
        }
        else
        {
            enemyAgent.SetDestination(GameManager.Instance.player.transform.position);
            enemy.heardNoise = false;
        }




    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isWalking", false);
        // animator.GetComponentInParent<NavMeshAgent>().SetDestination(animator.transform.position);

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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