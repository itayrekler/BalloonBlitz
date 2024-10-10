using UnityEngine;
using UnityEngine.UI;
/*
 * A script to attach a sound to buttons 
 */
public class ButtonSound : MonoBehaviour
{
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(PlayClickSound);
    }

    void PlayClickSound()
    {
        SoundManager.instance.PlayButtonClickSound();
    }
}