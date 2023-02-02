using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchControlsKit;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

  //  MainManager mainManager;
    private bool isGamePaused;
    [SerializeField]
    private GameObject _playerMovementControlls;
    [SerializeField]
    private GameObject _playerUI;
    public PlayerInventoryHolder playerInventory;
    [SerializeField]
    private GameObject _menuCanvas;
    public WeaponController weaponHolder;
    public GameObject fadeScreen;

    public PostProcessVolume gamePostFX;
    public PostProcessVolume menuPostFX;


    public GameObject dropItemButton;
    public GameObject equipItemButton;
    //Cameras
    public Camera playerCamera;
    public Camera menuCamera;
    public AudioManager audioManager;

    public static GameManager Instance;
    [SerializeField]
    public Player player;
    [SerializeField] float playerScore;
    public  Database Database;
    public UnityAction OnWeaponDisabled;
    // Start is called before the first frame update
    private void Awake()
    {
        InitializePlayer();
        isGamePaused = false;
        Instance = this;
        Screen.SetResolution(1655, 764, true);
      //  menuCamera.gameObject.SetActive(true);
      //  _menuCanvas.gameObject.SetActive(true);
       // Screen.SetResolution(2080, 960, true);
       // Screen.SetResolution(2080, 960, true);
        gamePostFX.enabled = true;
        audioManager.playAudio(audioManager.natureClip);
        playerCamera.GetComponent<AudioListener>().enabled = true;
        player.GetComponent<FirstPersonMovement>().enabled = true;
        playerCamera.gameObject.SetActive(true);
       // player.gameObject.SetActive(false);
       
        Database = Resources.Load<Database>("Database");
        _playerMovementControlls.SetActive(true);
       _playerUI.SetActive(true);

    }

    private void OnEnable()
    {
        SaveLoad.OnLoadGame += LoadGame;
    }

    private void OnDisable()
    {
        SaveLoad.OnLoadGame -= LoadGame;
    }
    // Update is called once per frame
    private void Update()
    {
       
            /*if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
            {
                Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit raycastHit;
            //if (Physics.Raycast(raycast, out raycastHit))
                if (Physics.Raycast(raycast, out raycastHit, 3f, LayerMask.GetMask("Item")))
                {
                if (raycastHit.collider.CompareTag("Item"))
                {

                    raycastHit.collider.gameObject.GetComponent<ItemPickUp>().PickUpItem(player.playerInventory);
                }
                }
                
            }*/
        
    }

    /*public void SaveGame()
    {
        SaveGameManager.CurrentSaveData.index = 10;
        SaveGameManager.SaveGame();
    }

    public void LoadGame()
    {
        SaveGameManager.CurrentSaveData.index = 20;
        SaveGameManager.LoadGame();
    }*/


    public void StartGame()
    {
      
        gamePostFX.enabled = true;
       // menuPostFX.enabled = false;
        InitializePlayer();
        //tworze gracza
        DeactivateMainMenu();
        // ActivatePlayerUI();
        audioManager.playAudio(audioManager.natureClip);

    }


    public void InitializePlayer()
    {

        //player.gameObject.SetActive(true);//tworze gracza
        playerCamera.gameObject.SetActive(true);//ustawiam jego kamere na aktywna
        player.GetComponent<FirstPersonMovement>().enabled = true;
        _playerUI.SetActive(true);
        _playerMovementControlls.SetActive(true);
        playerCamera.GetComponent<AudioListener>().enabled = true;
        player.IsActive = true;
    }

    public void DisablePlayer()
    {

        //player.gameObject.SetActive(true);//tworze gracza
        playerCamera.gameObject.SetActive(false);//ustawiam jego kamere na aktywna
        player.GetComponent<FirstPersonMovement>().enabled = false;
        _playerUI.SetActive(false);
        _playerMovementControlls.SetActive(false);
        playerCamera.GetComponent<AudioListener>().enabled = false;
        player.IsActive = false;
    }

    public void HideUI(GameObject _uiElement)
    {
        _uiElement.SetActive(false);
    }

    public void ShowUI(GameObject _uiElement)
    {
        _uiElement.SetActive(true);
    }

    public void DeactivateMainMenu()
    {
        menuCamera.gameObject.SetActive(false);
        HideUI(_menuCanvas);
    }

    public void  NewGameFromDeathScreen()
    {
        SceneManager.LoadScene("Underground", LoadSceneMode.Single);
    }

    
 public void PauseOrResumeGame()
 {
        if (!isGamePaused)
        {
            Time.timeScale = 0;
            isGamePaused = true;
            UIManager.instance.ShowHideWindow(UIManager.instance.pauseScreen);
        }

        else
        {
            Time.timeScale = 1;
            isGamePaused = false;
            UIManager.instance.ShowHideWindow(UIManager.instance.pauseScreen);
        }

            
 }

 

    public void LoadGame(SaveData data)
    {
        StartGame();
       
    }

    public void GoToMainMenu()
    {
        MainManager.Instance.QuitToMenu();
    }
}
