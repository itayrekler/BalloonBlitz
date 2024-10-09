using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Toggle checkbox;
    private bool toggleState;

    private void Awake()
    {
        toggleState = true;
        Debug.Log($"awake {toggleState}");
    }
    
    private void Start()
    {
        Debug.Log($"start {toggleState}");
        checkbox.isOn = toggleState;
    }
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