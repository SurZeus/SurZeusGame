using System.Collections.Generic;
using UnityEngine;
using TouchControlsKit;
using System;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;
    public static event Action<bool> isSprinting;
    Rigidbody rigidbody;
    public static event Action<int> PlayerIsMoving;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();


    private void OnEnable()
    {
        isSprinting += ManageStamina;
    }
    private void OnDisable()
    {
       isSprinting -= ManageStamina;
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

        if (IsRunning) isSprinting(true);
        else isSprinting(false);
        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
       // Vector2 targetVelocity =new Vector2( Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);
        Vector2 targetVelocity =new Vector2( TCKInput.GetAxis("Joystick",EAxisType.Horizontal) * targetMovingSpeed, TCKInput.GetAxis("Joystick", EAxisType.Vertical) * targetMovingSpeed);
        /*if(TCKInput.GetAxis("Joystick", EAxisType.Horizontal) *targetMovingSpeed ==0 && TCKInput.GetAxis("Joystick", EAxisType.Vertical) * targetMovingSpeed == 0)
         {
             Debug.Log("is standing");
             Player.player.WeaponHolder.GetComponent<WeaponController>().anim.SetBool("isWalking", false);
         }
         else
         {
             Player.player.WeaponHolder.GetComponent<WeaponController>().anim.SetBool("isWalking", true);
         }*/
        // Apply movement.

        if (rigidbody.velocity.magnitude > 1)
        {
            Player.player.WeaponHolder.GetComponent<WeaponController>().anim.SetBool("isWalking", true);
            PlayerIsMoving(1);
        }
        else
        {
            Player.player.WeaponHolder.GetComponent<WeaponController>().anim.SetBool("isWalking", false);
            PlayerIsMoving(-1);
        }
        rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);
    }


    public void ManageStamina(bool isRuning)
    {
        if(isRuning && Player.player.playerStamina!=0)
        Player.player.playerStamina -= Time.deltaTime *5;
        else if(isRuning==false && Player.player.playerStamina <=100)
        {
            Player.player.playerStamina += Time.deltaTime *2;
        }
        else if (Player.player.playerStamina <= 0)
        {
            Player.player.playerStamina = 0;
        }

    }

   
}
