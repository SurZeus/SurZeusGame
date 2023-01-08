using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    public static SaveData data;

    private void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        SaveLoad.OnLoadGame += LoadData;
       // DontDestroyOnLoad(gameObject);
        data = new SaveData();
    }

    

    private void LoadData(SaveData _data)
    {
        data = _data;
    }

    public static void SaveData()
    {
        var saveData = data;
      //  Debug.Log("sd " + saveData.chestDictionary.Count);
        SaveLoad.SaveGame(saveData);
    }

    public void DeleteData()
    {
        SaveLoad.DeleteSaveData();
    }


    public static void TryLoadData()
    {
        SaveLoad.LoadGame();
    }

    public void GenerateNewSaveData()
    {

    }
}
