using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuManager : MonoBehaviour
{
    public Button playButton;
    public Button highScoresButton;
    public Button quitButton;
    public void Start()
    {
        playButton.onClick.AddListener(OpenDifficultySelection);
        highScoresButton.onClick.AddListener(ShowHighScores);
        quitButton.onClick.AddListener(QuitGame);
    }
    private void OpenDifficultySelection()
    {
        SceneManager.LoadScene("DifficultySelection");
    }
    public void ShowHighScores()
    {
        SceneManager.LoadScene("HighScore");
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}