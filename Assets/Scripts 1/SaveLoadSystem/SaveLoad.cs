using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Events;

public static class SaveLoad 
{
    public static SaveData CurrentSaveData = new SaveData();
    private static string SaveDirectory = "/SaveData2/";
    private static string FileName = "SaveGame2.sav";


    public static UnityAction OnSaveGame;
    public static UnityAction<SaveData> OnLoadGame;
    
    
    public static bool SaveGame(SaveData data)
    {

        OnSaveGame?.Invoke();
        var dir = Application.persistentDataPath + SaveDirectory;
        Debug.Log(data.chestDictionary.Count);
        GUIUtility.systemCopyBuffer = dir;
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(dir + FileName,json);
       
       
        return true;
    }

    public static SaveData LoadGame()
    {
        string fullPath = Application.persistentDataPath + SaveDirectory + FileName;
        SaveData tempData = new SaveData();

        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            tempData = JsonUtility.FromJson<SaveData>(json);
            OnLoadGame?.Invoke(tempData);
           
        }

        else
        {
            Debug.LogError("Save File doesnt exist!");
           
        }

        return tempData;
    }
    // Update is called once per frame
    public static void DeleteSaveData()
    {
        string fullPath = Application.persistentDataPath + SaveDirectory + FileName;
        if (File.Exists(fullPath)) File.Delete(fullPath);
    }
}
   
    
   
