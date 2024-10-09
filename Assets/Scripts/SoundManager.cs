using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource audioSource;
    public AudioClip buttonClickSound;
    public bool isSoundEnabled;

    private void Awake()
    {
        // Ensure the SoundManager persists between scenes
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        isSoundEnabled = true;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayButtonClickSound()
    {
        if (isSoundEnabled)
            audioSource.PlayOneShot(buttonClickSound);
    }
}