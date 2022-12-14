using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawnManager : MonoBehaviour
{
    public List<GameObject> spawnPoints;
    public GameObject enemyPrefab;
    public GameObject areaCenterPoint;
    public static UnityAction<int> enemyHasDied;
    public EnemyManager enemyManager;
    public int id = -1;
    private WaitForSeconds waitForSeconds;
    public float SpawnDelay;

    // Start is called before the first frame update
    void Start()
    {
        if (id == -1)
        {
            Debug.Log("Id not set");
        }
        enemyHasDied += SpawnEnemy;
        foreach (Transform child in transform)
        {
            spawnPoints.Add(child.gameObject);
        }
     

        Invoke("SpawnOnStart", 5f);
    }


    private void Awake()
    {
        waitForSeconds = new WaitForSeconds(SpawnDelay);
    }

    public void SpawnOnStart()
    {
        foreach(GameObject i in spawnPoints)
        {

            var tempEnemy = InstantiateEnemy(i);
            tempEnemy.GetComponent<Enemy>().centrePoint = areaCenterPoint.transform;
            tempEnemy.gameObject.transform.SetParent(enemyManager.transform);
            tempEnemy.GetComponent<Enemy>().ParentSpawnAreaID = this.id;

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
        temp1.GetComponent<Enemy>().ParentSpawnAreaID = this.id;
    }

    public void SpawnEnemy(int id)
    {
        if(id ==this.id)
        StartCoroutine(spawnEnemyCorutine());
    }

    public IEnumerator spawnEnemyCorutine()
    {
        yield return waitForSeconds;
        SpawnNewEnemy();
    }
}
