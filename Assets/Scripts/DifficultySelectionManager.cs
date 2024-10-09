using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DifficultySelectionManager : MonoBehaviour
{
    public Button easyButton;
    public Button mediumButton;
    public Button hardButton;
    public Button backButton;

    private void Start()
    {
        easyButton.onClick.AddListener(() => StartGame(Difficulty.Easy));
        mediumButton.onClick.AddListener(() => StartGame(Difficulty.Medium));
        hardButton.onClick.AddListener(() => StartGame(Difficulty.Hard));
        backButton.onClick.AddListener(ReturnToMainMenu);
    }

    private void StartGame(Difficulty difficulty)
    {
        PlayerPrefs.SetInt("Difficulty", (int)difficulty);
        SceneManager.LoadScene("Game");
    }

    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

public enum Difficulty
{
    Easy,
    Medium,
    Hard
}