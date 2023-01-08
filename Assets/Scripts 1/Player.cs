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
    public PlayerInventoryHolder playerInventory;
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
    private PlayerSaveData playerSaveData = new PlayerSaveData(false);
    public bool isDyingOfStarvation;
    public int playerState = 0; //0 prone  , 1 crouch, 2 stay, 3 walking, 4 runing;

    private void OnEnable()
    {
        EventManager.changePlayerState += ChangePlayerState;
        SaveLoad.OnSaveGame += SavePlayerData;
        SaveLoad.OnLoadGame += LoadPlayerData;
    }

    private void OnDisable()
    {
        EventManager.changePlayerState -= ChangePlayerState;
        SaveLoad.OnSaveGame -= SavePlayerData;
        SaveLoad.OnLoadGame -= LoadPlayerData;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = this;
        // var inventorySaveData = new PlayerSaveData(transform,rota)

        playerInventory = GetComponent<PlayerInventoryHolder>();
        //StartCoroutine(DrainHungerAndThirst());
        IsActive = true;
        playerHunger = 100;
        playerThirst = 100;
        maxStamina = 100;
        isDyingOfStarvation = false;
        playerStamina = maxStamina;
        playerHealth = 100;
       
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {

        playerSaveData.position = transform.position;
        playerSaveData.rotation = transform.rotation;
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


        if (playerHunger <= 0 || playerThirst <= 0)
        {
            if (playerHunger <= 0 && playerThirst >= 0)
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
        if (playerHealth >= 100)
        {
            playerHealth = 100;
        }

    }

    public void ChangePlayerState(int state_id) //0,1,2,3,4,5
    {
        playerState = state_id;
    }

    public void SavePlayerData()
    {
        SaveManager.data.playerSaveData = playerSaveData;
    }

    public void LoadPlayerData(SaveData data)
    {
        transform.position = data.playerSaveData.position;
        transform.rotation = data.playerSaveData.rotation;
      //  SaveManager.data.playerSaveData = playerSaveData;
    }
}
[System.Serializable]
public struct PlayerSaveData
{
   
    public Vector3 position;
    public Quaternion rotation;

    public PlayerSaveData( Vector3 _position, Quaternion _rotation)
    {
        position = _position;
        rotation = _rotation;
    }

    public PlayerSaveData(bool none)
    {
        position = Vector3.zero;
        rotation = Quaternion.identity;
    }

}