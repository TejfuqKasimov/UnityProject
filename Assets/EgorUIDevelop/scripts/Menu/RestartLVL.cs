using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLVL : MonoBehaviour
{


    public void RestartCurrentScene()
    {
        // get name current scene and reboot it
        Time.timeScale = 1;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        

    }
}
