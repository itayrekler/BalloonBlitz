using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * This script is used for mainly storing the state of the sound checkbox
 */
public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Toggle checkbox;
    private const string ToggleStateKey = "ToggleState";
    private bool isInitializing = false;

    void Start()
    {
        LoadToggleState();
    }

    void OnDisable()
    {
        SaveToggleState();
    }

    private void SaveToggleState()
    {
        PlayerPrefs.SetInt(ToggleStateKey, checkbox.isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void LoadToggleState()
    {
        isInitializing = true;
        if (PlayerPrefs.HasKey(ToggleStateKey))
        {
            checkbox.isOn = PlayerPrefs.GetInt(ToggleStateKey) == 1;
        }
        else
        {
            PlayerPrefs.SetInt(ToggleStateKey, checkbox.isOn ? 0 : 1);
        }
        isInitializing = false;
    }

    public void OnToggleValueChanged()
    {
        if (!isInitializing)
        {
            // Inverts soundManager state when the toggle is clicked
            SoundManager soundManager = FindObjectOfType<SoundManager>();
            soundManager.isSoundEnabled = !soundManager.isSoundEnabled;
            checkbox.isOn = soundManager.isSoundEnabled;
        }
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}