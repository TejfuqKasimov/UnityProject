using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonToTransitionLVL : MonoBehaviour
{
    public GameObject[] levelButtons; 
    void Start()
    {
        UpdateButtonStates();
    }

    private void UpdateButtonStates()
    {
    SaveLoadManager.LoadData();

    for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i <= SaveLoadManager.currentSaveData.maxLevelReached)
            {
                levelButtons[i].SetActive(true);
            }
            else
            {
                levelButtons[i].SetActive(false);
            }
        }
    }
    public void LoadLevels(int LevelIndex)
    {
        Time.timeScale = 1;
        if (SaveLoadManager.currentSaveData.maxLevelReached != null)
        {
            SaveLoadManager.currentSaveData.maxLevelReached = System.Math.Max(SaveLoadManager.currentSaveData.maxLevelReached, LevelIndex - 1);
        }
        else
        {
            SaveLoadManager.currentSaveData.maxLevelReached = 0;
        }

        SaveLoadManager.SaveData();
        SceneManager.LoadScene(LevelIndex);

    }

}
