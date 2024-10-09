using UnityEngine;
using UnityEngine.UI;

public class ThemeManager : MonoBehaviour
{
    public Image backgroundImage;
    public GameObject bombObject;
    public GameObject[] regularObjects;
    public GameObject[] zigzagObjects;
    public Theme[] themes;
    
    private int currentThemeIndex = 0;

    void Start()
    {
        // Load the saved theme index (default to 0 if no theme is saved)
        int savedThemeIndex = PlayerPrefs.GetInt("SelectedTheme", 0);

        // Apply the saved theme
        ApplyTheme(savedThemeIndex);
    }
    

    public void ApplyTheme(int themeIndex)
    {
        if (themeIndex >= 0 && themeIndex < themes.Length)
        {
            Theme selectedTheme = themes[themeIndex];

            // Change the background image
            backgroundImage.sprite = selectedTheme.backgroundImage;
            
            Button[] allButtons = FindObjectsOfType<Button>();
            for (int i = 0; i < allButtons.Length; ++i)
            {
                allButtons[i].image.color = selectedTheme.buttonColor;
            }
            
            if (bombObject != null)
            {
                SpriteRenderer spriteRenderer = bombObject.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.sprite = selectedTheme.bombObject;
                    spriteRenderer.color = selectedTheme.bombColor;
                }

            }

            if (regularObjects != null)
            {
                for (int i = 0; i < regularObjects.Length; i++)
                {
                    GameObject obj = regularObjects[i];
                    if (obj != null)
                    {
                        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
                        if (spriteRenderer != null)
                        {
                            spriteRenderer.sprite = selectedTheme.regularObjects[Random.Range(0, selectedTheme.regularObjects.Length-1)];
                        }
                    }
                }
            }

            if (zigzagObjects != null)
            {
                for (int i = 0; i < zigzagObjects.Length; i++)
                {
                    GameObject obj = zigzagObjects[i];
                    if (obj != null)
                    {
                        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
                        if (spriteRenderer != null)
                        {
                            spriteRenderer.sprite = selectedTheme.zigzagObjects[Random.Range(0, selectedTheme.zigzagObjects.Length-1)];
                        }
                    }
                }
            }
 
            currentThemeIndex = themeIndex;
            PlayerPrefs.SetInt("SelectedTheme", themeIndex);
            PlayerPrefs.Save();  // Ensure the data is saved immediately
        }
    }
}