using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[System.Serializable]
public class HighScore
{
    public string playerName;
    public int score;
    public Difficulty difficulty;

    public HighScore(string name, int score, Difficulty difficulty)
    {
        this.playerName = name;
        this.score = score;
        this.difficulty = difficulty;
    }
}

public class HighScoreManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI[] scores;
    public TMPro.TextMeshProUGUI[] names;
    public TMPro.TextMeshProUGUI[] difficulties;
    private const string HighScoreKey = "HighScores";
    private const int MaxHighScores = 5;
    
    private void Start()
    {
        DisplayHighScores();
    }

    public static void AddHighScore(string playerName, int score, Difficulty difficulty)
    {
        List<HighScore> highScores = GetHighScores();
        highScores.Add(new HighScore(playerName, score, difficulty));
        highScores.Sort((a, b) => b.score.CompareTo(a.score)); // Sort in descending order

        if (highScores.Count > MaxHighScores)
        {
            highScores = highScores.GetRange(0, MaxHighScores);
        }

        SaveHighScores(highScores);
    }

    private static List<HighScore> GetHighScores()
    {
        string scoresString = PlayerPrefs.GetString(HighScoreKey, "");

        if (string.IsNullOrEmpty(scoresString))
        {
            Debug.Log("No high scores found.");
            return new List<HighScore>();
        }

        string[] scoreStrings = scoresString.Split('|');
        List<HighScore> scores = new List<HighScore>();
        foreach (string scoreString in scoreStrings)
        {
            string[] parts = scoreString.Split(',');
            if (parts.Length >= 2 && int.TryParse(parts[1], out int score))
            {
                Difficulty difficulty = Difficulty.Medium; // Default to Medium for old data
                if (parts.Length >= 3 && System.Enum.TryParse(parts[2], out Difficulty parsedDifficulty))
                {
                    difficulty = parsedDifficulty;
                }
                scores.Add(new HighScore(parts[0], score, difficulty));
            }
            else
            {
                Debug.LogWarning($"Failed to parse score: {scoreString}");
            }
        }

        return scores;
    }

    private static void SaveHighScores(List<HighScore> highScores)
    {
        string scoresString = string.Join("|", highScores.ConvertAll(score => $"{score.playerName},{score.score},{score.difficulty}"));
        PlayerPrefs.SetString(HighScoreKey, scoresString);
        PlayerPrefs.Save();
    }

 private void DisplayHighScores()
    {
        // This funciton sets the existing player name and score into the Text parts of the leaderboard.
        List<HighScore> highScores = GetHighScores();
        for (int i = 0; i < scores.Length; i++)
        {
            if (i < highScores.Count)
            {
                scores[i].text = $"{highScores[i].score}";
                names[i].text = $"{highScores[i].playerName}";
                difficulties[i].text = $"{highScores[i].difficulty}";
            }
            else
            {
                scores[i].text = "";
                names[i].text = "";
                difficulties[i].text = "";
            }
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
