using System.Collections.Generic;
using UnityEngine;
using TouchControlsKit;
using System;
using System.Collections;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;
    
    
    Rigidbody rigidbody;
   
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();


    private void OnEnable()
    {
       // EventManager.PlayerIsWalking += this.PlayerIsWalking;
        EventManager.isSprinting += ManageStamina;
        
    }
    private void OnDisable()
    {
        EventManager.isSprinting -= ManageStamina;
    } 
    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Update IsRunning from input.
        IsRunning = Player.player.playerStamina >0 && canRun && TCKInput.GetAction("sprintBtn", EActionEvent.Press);

        if (IsRunning)EventManager.OnIsSprinting(true);
        else EventManager.OnIsSprinting(false);
   


        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

       
        Vector2 targetVelocity =new Vector2( TCKInput.GetAxis("Joystick",EAxisType.Horizontal) * targetMovingSpeed, TCKInput.GetAxis("Joystick", EAxisType.Vertical) * targetMovingSpeed);
      

        if (rigidbody.velocity.magnitude > 1)
        {  
            EventManager.OnPLayerIsWalking(true);
            if (TCKInput.GetAction("sprintBtn", EActionEvent.Press)) EventManager.OnIsSprinting(true);
            else if (TCKInput.GetAction("crouchBtn", EActionEvent.Press)) EventManager.OnChangePlayerState(1);
          

            Player.player.WeaponHolder.GetComponent<WeaponController>().anim.SetBool("isWalking", true);
          
        }
        else
        {
         //   Player.player.WeaponHolder.GetComponent<WeaponController>().anim.SetBool("isWalking", false);
            if (TCKInput.GetAction("crouchBtn", EActionEvent.Press))
            {
                EventManager.OnPLayerIsWalking(false);
                EventManager.OnChangePlayerState(1);
            }
            else EventManager.OnPLayerIsWalking(false);

        }
        rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);
    }


    public void ManageStamina(bool isRuning)
    {
      //  Debug.Log("c jest");
        if (isRuning && Player.player.playerStamina != 0)
        {
           // Debug.Log("1");
            Player.player.playerStamina -= Time.deltaTime * 5;
        }
        else if (isRuning == false && Player.player.playerStamina <= 100)
        {

           // Debug.Log("2 jest");
            if (Player.player.playerStamina <= 1)
            {

               // Debug.Log("3 jest");
                StartCoroutine(RuningCooldown());
                Player.player.playerStamina += Time.deltaTime * 2;
            }

            else
            {
                Player.player.playerStamina += Time.deltaTime * 2;
            }
           
        }
        else if (Player.player.playerStamina <= 1)
        {
           
                Player.player.playerStamina = 0;
                
            

        }

    }

    IEnumerator RuningCooldown()
    {
       // Debug.Log("corutya");
        canRun = false;
        yield return new WaitForSeconds(5f);
        canRun = true;
    }
   

    
}
