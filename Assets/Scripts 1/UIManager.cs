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
    public GameObject consumeItemButton;
    void Start()
    {
        
        instance = this;
        staminaBar.fillAmount = Player.player.playerStamina;
    }


    private void OnEnable()
    {
        FirstPersonMovement.isSprinting += UpdateStaminaUI;
    }
    private void OnDisable()
    {
        FirstPersonMovement.isSprinting -= UpdateStaminaUI;
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
}
