using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonToTransitionLVL : MonoBehaviour
{
    public void LoadLevels(int LevelIndex)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(LevelIndex);
    }

}
