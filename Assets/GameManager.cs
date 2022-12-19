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


    //UI
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

    public UnityAction OnWeaponDisabled;
    // Start is called before the first frame update
    private void Awake()
    {

        menuCamera.gameObject.SetActive(true);
        _menuCanvas.gameObject.SetActive(true);
       // Screen.SetResolution(2080, 960, true);
       // Screen.SetResolution(2080, 960, true);
        gamePostFX.enabled = false;
        audioManager.playAudio(audioManager.mainMenu);
        playerCamera.GetComponent<AudioListener>().enabled = false;
        player.GetComponent<FirstPersonMovement>().enabled = false;
        playerCamera.gameObject.SetActive(false);
       // player.gameObject.SetActive(false);
        Instance = this;
        _playerMovementControlls.SetActive(false);
       _playerUI.SetActive(false);

    }
   

    // Update is called once per frame
    

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
        menuPostFX.enabled = false;
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
  
}
