using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawnManager : MonoBehaviour
{
    public List<GameObject> spawnPoints;
    public GameObject enemyPrefab;
    public GameObject areaCenterPoint;
    public static UnityAction enemyHasDied;
    // Start is called before the first frame update
    void Start()
    {

        enemyHasDied += SpawnEnemy;
        foreach (Transform child in transform)
        {
            spawnPoints.Add(child.gameObject);
        }
        //child is your child transform

        spawnOnStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnOnStart()
    {
        foreach(GameObject i in spawnPoints)
        {

         
            var temp1 = InstantiateEnemy(i);
            temp1.GetComponent<Enemy>().centrePoint = areaCenterPoint.transform;


        }
    }

    public GameObject InstantiateEnemy(GameObject spawnPoint)
    {
       return  Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);
    }

    public void SpawnNewEnemy()
    {
        int temp = Random.Range(0, spawnPoints.Count);
        var temp1 = InstantiateEnemy(spawnPoints[temp]);
        temp1.GetComponent<Enemy>().centrePoint = areaCenterPoint.transform;
    }

    public void SpawnEnemy()
    {
        StartCoroutine(spawnEnemyCorutine());
    }

    public IEnumerator spawnEnemyCorutine()
    {
        yield return new WaitForSeconds(10f);
        SpawnNewEnemy();
    }
}
