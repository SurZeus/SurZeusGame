using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DetectionArea : MonoBehaviour
{
    public Enemy enemy;
    public SphereCollider sphereCollider;
    public bool playerNear;
    public float distanceToPlayer;
    // Start is called before the first frame update
    void Start()
    {
        distanceToPlayer = 999;
        playerNear = false;
        enemy = GetComponentInParent<Enemy>();
        sphereCollider = GetComponent<SphereCollider>();
       // Debug.Log(Vector3.Distance(sphereCollider.center, sphereCollider.ClosestPoint(sphereCollider.gameObject.transform.position)));
    

        

        
    }


    private void Update()
    {
        if (playerNear == true)
        {
            distanceToPlayer = Vector3.Distance(sphereCollider.transform.position, Player.player.transform.position);
        }
      //  Debug.DrawLine(sphereCollider.transform.position,  Color.red);
    }

    private void OnTriggerEnter(Collider other)
    {
        playerNear = true;
     
       // Debug.Log(other.name);
        if (other.CompareTag("Player"))
        {
           
           // enemy.GetComponentInChildren<Animator>().

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (enemy.isChasingPlayer == false) //jezeli przeciwnik juz sciga gracza to nie ma sensu tutaj wchodzic
        {
            if (other.CompareTag("Player"))
            {
                if (Player.player.playerState == 0)//lezy
                {
                   // Debug.Log("stoi");
                    if (distanceToPlayer >2f)
                    {
                       // Debug.Log("lezy jest save");

                    }
                    else if (distanceToPlayer <= 2f)
                    {
                       // Debug.Log("not Save range");
                        enemy.GetComponentInChildren<Animator>().SetBool("isChasingPlayer", true);
                    }
                }

                else if (Player.player.playerState == 1) // kuca
                {
                    if (distanceToPlayer >= 3f)
                    {
                       // Debug.Log("kuca jest save");
                    }
                    else
                    {
                       // Debug.Log("kuca nie jest save");
                        enemy.GetComponentInChildren<Animator>().SetBool("isChasingPlayer", true);
                    }
                }

                else if (Player.player.playerState == 2) // stoi
                {
                    if (playerNear && distanceToPlayer >= 5f)
                    {
                       // Debug.Log("Save rangesss");
                    }
                    else
                    {
                        enemy.GetComponentInChildren<Animator>().SetBool("isChasingPlayer", true);
                    }
                }
                else if (Player.player.playerState == 3) // idzie
                {
                    if (playerNear && distanceToPlayer >= 15f)
                    {
                       // Debug.Log("Save rangesss");
                    }
                    else
                    {
                        enemy.GetComponentInChildren<Animator>().SetBool("isChasingPlayer", true);
                    }
                }

                else if (Player.player.playerState == 4) // biegnie
                {
                    if (playerNear && distanceToPlayer >= 25f)
                    {
                        //Debug.Log("Save rangesss");
                    }
                    else
                    {
                        enemy.GetComponentInChildren<Animator>().SetBool("isChasingPlayer", true);
                    }
                }

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        playerNear = false;
    }
    public IEnumerator trackingPlayer()
    {
        
        yield return null;
    }
    
}
