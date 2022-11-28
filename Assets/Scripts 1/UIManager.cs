using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    GameObject UI;
    void Start()
    {
        
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
}
