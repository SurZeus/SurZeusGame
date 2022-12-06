using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.HealthSystemCM;
using UnityEngine.AI;

public class Enemy : MonoBehaviour,IDamagable
{
    public bool heardNoise;
    public bool isChasingPlayer;
    public float walkSpeed;
    public NavMeshAgent enemyAgent;
    public GameObject detectionArea;
    [SerializeField]
    public float distanceToTarget;
    public float range; //radius of sphere
    [SerializeField]
   public float enemyWalkTime;
    public Transform centrePoint;
    public Transform currentDestination;

    public int Health { get ; set; }
  


    // Start is called before the first frame update
    private void Awake()
    {
       
        heardNoise = false;
        enemyWalkTime = Random.Range(10, 30);
        Debug.Log("TIMER: " + enemyWalkTime);
        range = Random.Range(25, 60);
    }
    void Start()
    {

        StartCoroutine(AddToEnemyManager());
        currentDestination = centrePoint;
        isChasingPlayer = false;
       enemyAgent = GetComponent<NavMeshAgent>();
       
       detectionArea =gameObject.transform.Find("DetectionArea").gameObject;

       
    }

    
    void Update()
    {

       // Debug.Log("odleglosc do gracza: "  + getDistanceToPlayer());

    }
    

   

    public void EnemyGoTo(Vector3 position )
    {
        enemyAgent.SetDestination(position);
    }

    public void EnemyPatroling()
    {
      
    }
 









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

    public void getDamage(int damageAmount)
    {
       

        Health = Health - damageAmount;
        if (Health <= 0)
        {
            gameObject.GetComponentInChildren<Animator>().SetTrigger("gotKilled");
          
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            EnemyManager.instance.enemies.Remove(this);
            Destroy(gameObject, 5);
            
          
        }
    }

    public float getDistanceToPlayer()
    {
        float distance = Vector3.Distance(GameManager.Instance.player.transform.position, this.transform.position);
        return distance;
    }

    private void OnDestroy()
    {
        EnemySpawnManager.enemyHasDied.Invoke();

    }
    public IEnumerator AddToEnemyManager()
    {
        yield return new  WaitForSeconds(5f);
        EnemyManager.instance.enemies.Add(this);
    }
}
    //AI



