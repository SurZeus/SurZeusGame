using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchControlsKit;
using System;

public class Player : MonoBehaviour
{
    [SerializeField]
    public int playerHealth;
    public GameObject WeaponHolder;
    public static Player player;
    public float maxStamina;
    public float playerStamina;
    [HideInInspector]
    public float maxHunger = 100;
    public float playerHunger;
    [HideInInspector]
    public float maxThirst = 100;
    public float playerThirst;
    public float timer = 0;
    public static event Action<bool> isStarving;

    
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(DrainHungerAndThirst());
        playerHunger = 100;
        playerThirst = 100;
        maxStamina = 100;
        playerStamina = maxStamina;
        playerHealth = 100;
        player = this;
        Application.targetFrameRate =60;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>=1)
        {
            DrainHungerAndThirst();
        }
        /* Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
         RaycastHit hit;
         if (Physics.Raycast(ray, out hit, 5f) && hit.collider.tag == "Usable" && TCKInput.GetAction("useBtn", EActionEvent.Down))
         {
             Debug.Log("dadad");
             Debug.Log(hit.collider.transform.gameObject.name);

             //Ambulans

                 Debug.Log("key: " + hit.transform.parent.gameObject.name);
             hit.transform.parent.gameObject.GetComponent<Ambulance>().Use(hit.collider.transform.gameObject);


         }
     }*/

    }

    public void DrainHungerAndThirst()
    {
        Debug.Log("xD");
        playerHunger -= 0.1f;
        playerThirst -= 0.1f;
        timer = 0;
        isStarving(true);
       
        
    }
}
