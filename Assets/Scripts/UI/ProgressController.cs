using UnityEngine;
using System.IO;



public class SaveLoadManager : MonoBehaviour
{
    public static SaveData currentSaveData;

    public static void LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "/save.json"))
        {
            string jsonData = File.ReadAllText(Application.persistentDataPath + "/save.json");
            currentSaveData = JsonUtility.FromJson<SaveData>(jsonData);
        }
        else
        {
            currentSaveData = new SaveData();
        }
    }
    public static void NewData(int LevelIndex)
    {
        SaveLoadManager.currentSaveData.maxLevelReached = LevelIndex-1;
        SaveLoadManager.SaveData();
    }

    public static void UpdateData(int LevelIndex)
    {
        SaveLoadManager.currentSaveData.maxLevelReached = System.Math.Max(SaveLoadManager.currentSaveData.maxLevelReached, LevelIndex);
        SaveLoadManager.SaveData();
    }

    public static void SaveData()
    {
        string jsonData = JsonUtility.ToJson(currentSaveData);
        File.WriteAllText(Application.persistentDataPath + "/save.json", jsonData);
    }
}

