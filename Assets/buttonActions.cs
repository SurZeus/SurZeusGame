using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StarGame()
    {
        MainManager.Instance.StartNewGame();
    }
    public void LoadGame()
    {
        MainManager.Instance.LoadGame();
    } 
    public void QuitGame()
    {

    }
}
