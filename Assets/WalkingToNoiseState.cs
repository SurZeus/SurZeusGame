using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class WalkingToNoiseState : StateMachineBehaviour
{
    public Transform centrePoint;
    public float range;
    NavMeshAgent enemyAgent;
    Enemy enemy;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)

    {

        enemyAgent = animator.GetComponentInParent<Enemy>().enemyAgent;
        //enemyAgent.speed = 1;
        enemy = animator.GetComponentInParent<Enemy>();
        enemy.currentDestination = GameManager.Instance.player.transform.position;


        range = animator.GetComponentInParent<Enemy>().range;
        centrePoint = enemy.centrePoint;





        enemy.EnemyGoTo(GameManager.Instance.player.transform.localPosition);
            //  Debug.Log(enemy.remainingDistance);
        

    }




    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

      //  Debug.Log(enemyAgent.remainingDistance);

        if(enemyAgent.destination == enemy.currentDestination)
        {
            if (enemyAgent.isActiveAndEnabled)
            {
                if (enemyAgent.remainingDistance <= enemyAgent.stoppingDistance)
                {
                    // animator.SetBool("isWalking", false);

                }
            }
        }

        else
        {
            enemyAgent.SetDestination(enemy.currentDestination);
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
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 10.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            /* if(Vector3.Distance(hit.position,enemy.transform.position) <=3)
                 {
                     hit.position += new Vector3(5, 5, 5);
                 }*/
            return true;
        }

        result = Vector3.zero;
        return false;
    }


}

