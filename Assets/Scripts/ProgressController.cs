using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData
{
    public int maxLevelReached; 
}

public class SaveLoadManager : MonoBehaviour
{
    // Путь к файлу сохранения
    private string savePath = Application.persistentDataPath + "/save.json";

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

    public static void SaveData()
    {
        string jsonData = JsonUtility.ToJson(currentSaveData);
        File.WriteAllText(Application.persistentDataPath + "/save.json", jsonData);
    }
}

