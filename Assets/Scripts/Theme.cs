using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


[CreateAssetMenu(fileName = "Theme", menuName = "Themes/Theme", order = 1)]
public class Theme : ScriptableObject
{
    // We have create a ScriptableObject to represent a theme
    public Sprite backgroundImage;
    public Sprite bombObject;

    public Sprite[] regularObjects;

    public Sprite[] zigzagObjects;
    public Color buttonColor;
    public Color bombColor;
}
