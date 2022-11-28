using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
  
    public static SaveData data;

    private void Awake()
    {
        data = new SaveData();
        SaveLoad.OnLoadGame += LoadData;
    }

    private void LoadData(SaveData _data)
    {
        data = _data;
    }

    public static void SaveData()
    {
        var saveData = data;
        Debug.Log("sd " + saveData.chestDictionary.Count);
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
}
