using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] balloonPrefabs;
    public float spawnRate = 0.2f;
    public float minX, maxX;
    public float spawnY;
    public TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI timerText;
    public GameObject gameOverPanel;
    public TMPro.TextMeshProUGUI finalScoreText;

    public GameObject nameInputPanel;
    public TMPro.TMP_InputField nameInputField;
    public Button submitNameButton;
    
    private float screenLeft;
    private float screenRight;
    private float screenBottom;
    private Camera mainCamera;
    private int score;

    private float timeLeft = 30f;
    private bool isGameActive;
    private string playerName;
    private Collider2D[] allColliders;

    

    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("No main camera found!");
            return;
        }
        // Calculate screen boundaries
        screenLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane)).x;
        screenRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, mainCamera.nearClipPlane)).x;
        screenBottom = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane)).y;
        
        StartCoroutine(GameSetupCoroutine());

    }
    IEnumerator GameSetupCoroutine()
    /**
     * We wait for the player to enter his name and only then, start the game.
     */
    {
        yield return StartCoroutine(WaitForPlayerName());
        StartGame();
    }

    IEnumerator WaitForPlayerName()
    {
        nameInputPanel.SetActive(true);
        submitNameButton.onClick.AddListener(OnNameSubmitted);

        // Wait until the name is submitted
        yield return new WaitUntil(() => !string.IsNullOrEmpty(playerName));

        nameInputPanel.SetActive(false);
    }
    
    void OnNameSubmitted()
    {
        // Once the name was typed, turn off the nameInput panel and move on the start the game
        playerName = nameInputField.text;
        if (string.IsNullOrEmpty(playerName))
        {
            playerName = "Player";
        }
        nameInputPanel.SetActive(false);
    }
    
    void Update()
    {
        /**
         * We update the timeLeft on the screen.
         * When the game is over we call EndGame() that wil lbe decribed later on the code.
         */
        if (isGameActive)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 1)
            {
                EndGame();
            }
            UpdateTimerDisplay();
        }
    }

    void StartGame()
    {
        /**
         * Default params setup and reset.
         * This function is incharge on starting the balloon spawner and initial setup of dynamic text(score and timer).
         */
        score = 0;
        timeLeft = 30f;
        isGameActive = true;
        UpdateScoreDisplay();
        UpdateTimerDisplay();
        StartCoroutine(SpawnBalloons());
    }
    void SpawnBalloon()
    {
        /**
         * This method spawns balloons into the screen.
         * Balloon type and position are chosen randomly. 
         */
        if (isGameActive) {
            int index = Random.Range(0, balloonPrefabs.Length);
            float randomX = Random.Range(screenLeft+minX, screenRight-maxX);
            Vector3 spawnPos = new Vector3(randomX, screenBottom+spawnY, 0);
            GameObject balloon = Instantiate(balloonPrefabs[index], spawnPos, Quaternion.identity);
        }
    }
    
    IEnumerator SpawnBalloons()
    {
        // Spawn a balloon in every SPAWN_RATE seconds.
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            SpawnBalloon();
        }
    }
    
    public void IncrementScore(int points)
    {
        score += points;
        UpdateScoreDisplay();
    }

    public int GetScore()
    {
        return score;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void EnterMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    void UpdateScoreDisplay()
    {
        scoreText.text = $"{playerName}'s Score: {score}";
        
    }

    void UpdateTimerDisplay()
    {
        int seconds = Mathf.FloorToInt(timeLeft);
        timerText.text = "Time: " + seconds;
    }

    void EndGame()
    {
        /**
         * When the game is over, we disable the clickability of each existing balloon
         * to avoid drifts between the visible score and saved score.
         * At the end of a round, the final score will be displyed to the player and saved to the leaderbord.
         */
        allColliders = FindObjectsOfType<Collider2D>();
        isGameActive = false;
        foreach (Collider2D collider in allColliders) {
            collider.enabled = false;
        }
        gameOverPanel.SetActive(true);
        finalScoreText.text = $"{playerName}'s Final Score: {score}";

        HighScoreManager.AddHighScore(playerName, score);
    }

}