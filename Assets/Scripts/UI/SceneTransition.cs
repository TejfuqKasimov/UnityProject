using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneTransition : MonoBehaviour
{
    [SerializeField] private int sceneNumber; 
    public void transition()
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
