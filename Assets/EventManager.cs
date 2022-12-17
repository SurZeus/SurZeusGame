using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    public static event Action<bool> isStarving;
    public static void OnIsStarving() => isStarving?.Invoke(false);



    public static event Action isLosingHealth;
    public static void OnIsLosingHealth() => isLosingHealth?.Invoke();


    public static event Action<bool> isSprinting;
    public static void OnIsSprinting(bool param)
    {
        if(isSprinting!= null)
        {
            isSprinting.Invoke(param);
            if (param == true)
                OnChangePlayerState(4);
            else
                OnChangePlayerState(3);

        }
    }

   // => isSprinting?.Invoke(param);
    public static event Action<int> changePlayerState;
    public static void OnChangePlayerState(int param) => changePlayerState?.Invoke(param);




    public static event Action<bool> playerIsWalking;
    public static void OnPLayerIsWalking(bool param)
    {
        playerIsWalking?.Invoke(param);
        if (param)
            OnChangePlayerState(3);
        else
            OnChangePlayerState(2);
    }
    private void OnEnable()
    {
       // isSprinting += OnChangePlayerState(true);
    }


    // Start is called before the first frame update

}
