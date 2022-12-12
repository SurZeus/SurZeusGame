using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    public static UIManager instance;
    public Image staminaBar;
    public Image hungerBar;
    public Image thirstBar;
    public GameObject consumeItemButton;
    void Start()
    {
        
        instance = this;
        staminaBar.fillAmount = Player.player.playerStamina;
        thirstBar.fillAmount = Player.player.playerThirst;
        hungerBar.fillAmount = Player.player.playerHunger;
       
}


    private void OnEnable()
    {
        FirstPersonMovement.isSprinting += UpdateStaminaUI;
        Player.isStarving += UpdateHungerAndThirstUI;

    }
    private void OnDisable()
    {
       
        FirstPersonMovement.isSprinting -= UpdateStaminaUI;
        Player.isStarving -= UpdateHungerAndThirstUI;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowHideWindow(GameObject window)
    {
        if(!window.activeInHierarchy)
        window.SetActive(true);
        else
        {
            window.SetActive(false);
        }

    }

    public void UpdateStaminaUI(bool nothing)
    {
        staminaBar.fillAmount = Player.player.playerStamina/100;
    }
    public void UpdateHungerAndThirstUI(bool nothing)
    {
        thirstBar.fillAmount = Player.player.playerThirst / 100;
        hungerBar.fillAmount = Player.player.playerHunger / 100;
    }
}
