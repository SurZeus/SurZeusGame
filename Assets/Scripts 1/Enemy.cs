using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.HealthSystemCM;
using UnityEngine.AI;

public class Enemy : MonoBehaviour,IDamagable
{
    public bool isAlive;
    public bool heardNoise;
    public bool isEngagingPlayer;
    public bool isChasingPlayer;
   
    public NavMeshAgent enemyAgent;
    public GameObject detectionArea;
    [SerializeField]
    public float distanceToTarget;
    public float range; //radius of sphere
    [SerializeField]
   public float enemyWalkTime;
    public Transform centrePoint;
    public Vector3 currentDestination;
    public Animator animator;
    public AudioSource audioSource;
    public int health;
    public float Damage;
    public GameObject bloodParticle;
    public int ParentSpawnAreaID;

    
    // Start is called before the first frame update
    private void Awake()
    {
        
        animator = gameObject.GetComponentInChildren<Animator>();
        health = 100;
        heardNoise = false;
        enemyWalkTime = Random.Range(10, 100);
        range = Random.Range(50, 100);
       
    }
    void Start()
    {
    
     audioSource = GetComponent<AudioSource>();
     isAlive = true;
     StartCoroutine(AddToEnemyManager());
     isChasingPlayer = false;
     enemyAgent = GetComponent<NavMeshAgent>();
     detectionArea =gameObject.transform.Find("DetectionArea").gameObject;

       
    }

    
   
    

   

    public void EnemyGoTo(Vector3 position )
    {
        enemyAgent.SetDestination(position);
    }

    
    

    public void getDamage(int damageAmount)
    {
        health = health - damageAmount;
        if (health <= 0)
        {
            if(isAlive)
            {
                animator.SetTrigger("gotKilled");
                isAlive = false;
            }
           

            
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
        EnemySpawnManager.enemyHasDied.Invoke(ParentSpawnAreaID);

    }
    public IEnumerator AddToEnemyManager()
    {
        yield return new  WaitForSeconds(2f);
        EnemyManager.instance.enemies.Add(this);
    }

    IEnumerator playSoundWithDelay(AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.PlayOneShot(clip);
    }

  

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 20.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            
                return true;
        }

    

        result = Vector3.zero;
        return false;
    }
    public void GoToRandomPoint()
    {
        Vector3 point;
        int tries = 0;
        bool foundPoint = false;
      while(foundPoint != true && tries<=10) //zabezpieczenie przed infinity loopem
        {
            if (RandomPoint(centrePoint.position, range, out point))
            {
                enemyAgent.SetDestination(point);
                foundPoint = true;
            }
            tries++;
        }
       
          
          
            currentDestination = enemyAgent.destination;
      
       
    }

    public IEnumerator IdleThenGo()
    {
        yield return new WaitForSeconds(5f);
        animator.SetBool("isWalking", true);
    }


    public void AttackPlayer()
    {
        Debug.Log("xD");
        if (getDistanceToPlayer() <= 2f)
        Player.player.DecreaseHealth(Damage);
        else Debug.Log("Player TOO FAR");
    }
}

    //AI



