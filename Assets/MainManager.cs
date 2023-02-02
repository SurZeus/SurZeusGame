using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public bool isLoadRequired = false;


    void OnEnable()
    {

        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {

        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (isLoadRequired == true)
        {
            isLoadRequired = false;
            SaveLoad.LoadGame();
        }
        Debug.Log("Level Loaded");
        Debug.Log(scene.name);
        Debug.Log(mode);
    }
    // Start is called before the first frame update
    private void Awake()
    {
        Time.timeScale = 1;
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public void StartNewGame()
    {
        Debug.Log("xD");
        isLoadRequired = false;
        SceneManager.LoadScene("Underground", LoadSceneMode.Single);
    }

    public void LoadGame()
    {
        isLoadRequired = true;
        SceneManager.LoadScene("Underground", LoadSceneMode.Single);

    }

    public void QuitToMenu()
    {

        SceneManager.LoadScene("MainMenuNew", LoadSceneMode.Single);
        SceneManager.UnloadScene("Underground");
    }

}
