using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.HealthSystemCM;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public bool isChasingPlayer;
    public float Health;
    public float walkSpeed;
    public NavMeshAgent enemyAgent;
    public GameObject detectionArea;
    [SerializeField]
    public float distanceToTarget;
    public float range; //radius of sphere
    [SerializeField]
   public float enemyWalkTime;
    public Transform centrePoint;
    // Start is called before the first frame update
    private void Awake()
    {
        enemyWalkTime = Random.Range(10, 30);
        Debug.Log("TIMER: " + enemyWalkTime);
        range = Random.Range(25, 60);
    }
    void Start()
    {
       
        isChasingPlayer = false;
       enemyAgent = GetComponent<NavMeshAgent>();
        Health = 100f;
       detectionArea =gameObject.transform.Find("DetectionArea").gameObject;

       
    }

    
    // Update is called once per frame
    void Update()
    {
      /*  if (!isChasingPlayer)
        {
            EnemyPatroling();
        }
        else
        {
            distanceToTarget = Vector3.Distance(enemy.transform.position, GameManager.Instance.player.transform.position);
            if (distanceToTarget <=20)
            {
                EnemyGoTo(GameManager.Instance.player.transform.position);
            }
            else
            {
                isChasingPlayer = false ;
            }
        }*/
        

    }
    public void getDamage(float damage)
    {
        Health = Health - damage;
        if (Health <= 0)
        {
            gameObject.GetComponentInChildren<Animator>().SetTrigger("gotKilled");
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            Destroy(gameObject, 5);
        }

    }

   

    public void EnemyGoTo(Vector3 position )
    {
        enemyAgent.SetDestination(position);
    }

    public void EnemyPatroling()
    {
       /* if (enemy.remainingDistance <= enemy.stoppingDistance) //done with path
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point)) //pass in our centre point and radius of area
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                enemy.SetDestination(point);
            }
        }*/
    }
    //if you use this code you are contractually obligated to like the YT video









    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }


}
    //AI



