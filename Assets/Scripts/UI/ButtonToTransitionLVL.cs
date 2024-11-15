using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonToTransitionLVL : MonoBehaviour
{
    public GameObject[] levelButtons;
    int lenght;
    void Awake()
    {
        if (levelButtons != null)
        {
            lenght = levelButtons.Length;
        }
        UpdateButtonStates();
    }

    private void UpdateButtonStates()
    {

        SaveLoadManager.LoadData();

        for (int i = 0; i < lenght; i++)
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
        SaveLoadManager.UpdateData(LevelIndex-1);
        SceneManager.LoadScene(LevelIndex);
    }

}
