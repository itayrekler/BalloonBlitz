using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
            SoundManager soundManager = FindObjectOfType<SoundManager>();
            soundManager.isSoundEnabled = !soundManager.isSoundEnabled;
            checkbox.isOn = soundManager.isSoundEnabled;
            Debug.Log("Toggle value changed: " + checkbox.isOn);
        }
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}