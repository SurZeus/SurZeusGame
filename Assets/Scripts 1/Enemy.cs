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
    public Animator animator;
    public AudioSource audioSource;
    public int health;
    

    
    // Start is called before the first frame update
    private void Awake()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
        health = 100;
        heardNoise = false;
        enemyWalkTime = Random.Range(50, 100);
        range = Random.Range(25, 100);
    }
    void Start()
    {
     audioSource = GetComponent<AudioSource>();
     isAlive = true;
     StartCoroutine(AddToEnemyManager());
     currentDestination = centrePoint;
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
        EnemySpawnManager.enemyHasDied.Invoke();

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
}
    //AI



