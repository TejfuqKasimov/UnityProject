using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public GameObject[] levelButtons; 

    void Start()
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

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex + 1);
    }
}

