using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public Image healthBar;
    public GameObject consumeItemButton;
    public TextMeshProUGUI healthPercentage;
    public TextMeshProUGUI weaponAmmunitionUI;
    public GameObject deathScreen;
    void Start()
    {
        
        instance = this;
        staminaBar.fillAmount = Player.player.playerStamina;
        thirstBar.fillAmount = Player.player.playerThirst;
        hungerBar.fillAmount = Player.player.playerHunger;
        healthPercentage.text = Player.player.playerHealth.ToString();


}


    private void OnEnable()
    {
        EventManager.playerDied += ShowDeathScreen;
        EventManager.isSprinting += UpdateStaminaUI;
        EventManager.isStarving += UpdateHungerAndThirstUI;
        EventManager.isLosingHealth += UpdateHealth;

    }
    private void OnDisable()
    {

        EventManager.isSprinting -= UpdateStaminaUI;
        EventManager.isStarving -= UpdateHungerAndThirstUI;
        EventManager.isLosingHealth -= UpdateHealth;
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

    public void UpdateHealth()
    {
        healthBar.fillAmount = Player.player.playerHealth / 100;
        healthPercentage.text = Player.player.playerHealth.ToString();
    }

    public void UpdatePlayerStatistics()
    {
        UpdateHealth();
        UpdateHungerAndThirstUI(true);
        UpdateStaminaUI(true);
    }

    public void ShowDeathScreen()
    {
        if(deathScreen!=null)
        deathScreen.SetActive(true);
    }
    
}
