using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    public GameObject finishMenuUI; // Присоедините сюда ваше меню финиша

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, является ли объект игроком
        if (other.CompareTag("Player"))
        {
            // Открываем меню финиша
            finishMenuUI.SetActive(true);
            Time.timeScale = 0f; // Приостанавливаем игру
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f; // Возобновляем игру
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Перезагрузка текущей сцены
    }

    public void NextLevel(string levelName)
    {
        Time.timeScale = 1f; // Возобновляем игру
        SceneManager.LoadScene(levelName); // Переход на следующий уровень
    }
}
