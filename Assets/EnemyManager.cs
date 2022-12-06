using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public List<Enemy> enemies;
    public static EnemyManager instance;

    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<Enemy>();
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void attractNearbyZombies()
    {
        foreach(Enemy enemy in enemies)
        {
            if (enemy != null  && enemy.gameObject.activeInHierarchy)
            {
                if (enemy.enemyAgent.isActiveAndEnabled && enemy.getDistanceToPlayer() <= 200)
                {
                    if (enemy.getDistanceToPlayer() <= 50f)
                    {
                        Debug.Log("co jest kjurwa");
                        enemy.GetComponentInChildren<Animator>().SetBool("isChasingPlayer", true);
                    }
                    else if (enemy.getDistanceToPlayer() >= 51f)
                    {
                        Debug.Log("kurwaaa");
                        enemy.heardNoise = true;
                    }
                }
            }
        }
    }
}
