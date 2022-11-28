using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
public class MenuButtons : MonoBehaviour
{
    public GameObject fadeScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {

        Debug.Log("New Game Pressed");
        FadeToBlack();
    }

    public void FadeToBlack()
    {
        fadeScreen.GetComponent<Fade>().FadeScreen();
    }
    
    
}
