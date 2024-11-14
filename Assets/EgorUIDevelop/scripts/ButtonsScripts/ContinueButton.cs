using UnityEngine;
using UnityEngine.UI;
public class ContinueButton : MonoBehaviour
{
    public Button myButton; // Переменная для хранения ссылки на кнопку

    void Start()
    {
        // Подписка на событие нажатия кнопки
        myButton.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        // Логика, которая будет выполняться при нажатии на кнопку
        Debug.Log("Кнопка была нажата!");

        // Можно изменять текст кнопки, если это необходимо
        Text buttonText = myButton.GetComponentInChildren<Text>();
        if (buttonText != null)
        {
            buttonText.text = "Нажата!";
        }
    }
}
