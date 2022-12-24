using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchControlsKit;
using System;

public class Player : MonoBehaviour
{
    [SerializeField]
    public bool IsActive;
    public float playerHealth;
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
   
    public bool isDyingOfStarvation;
    public int playerState =0; //0 prone  , 1 crouch, 2 stay, 3 walking, 4 runing;

    private void OnEnable()
    {
        EventManager.changePlayerState += ChangePlayerState;
    }

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(DrainHungerAndThirst());
        IsActive = false;
        playerHunger = 100;
        playerThirst = 100;
        maxStamina = 100;
        isDyingOfStarvation = false;
        playerStamina = maxStamina;
        playerHealth = 100;
        player = this;
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 5 && IsActive)
        {
            DrainHungerAndThirst();
        }
       

    }

    public void DrainHungerAndThirst()
    {
     
        if (playerHunger >= 0)
        {
            playerHunger -= 1f;
        }

        if (playerThirst >= 0)
        {
            playerThirst -= 1f;
        }


        if (playerHunger <=0 || playerThirst <=0)
        {
            if(playerHunger <=0 && playerThirst >=0)
            DecreaseHealth(0.25f);
            else if (playerHunger >= 0 && playerThirst <= 0)
            DecreaseHealth(0.5f);
            else DecreaseHealth(1f);



        }


        timer = 0;
        EventManager.OnIsStarving();


    }

    public void DecreaseHealth(float value)
    {
        playerHealth -= value;
        EventManager.OnIsLosingHealth();
        if (playerHealth <= 0)
        {
            EventManager.OnPlayerDied();
        }
        
    }

    public void IncreaseHealth(float value)
    {
        playerHealth += value;
        EventManager.OnIsLosingHealth();
        if (playerHealth >=100)
        {
            playerHealth = 100;
        }

    }

    public void ChangePlayerState(int state_id) //0,1,2,3,4,5
    {
        playerState = state_id;
    }
}
