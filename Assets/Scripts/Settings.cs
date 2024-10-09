using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void setSound()
    {
        SoundManager soundManager = FindObjectOfType<SoundManager>();
        soundManager.isSoundEnabled = !soundManager.isSoundEnabled;
    }
    
}