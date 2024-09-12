using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public GameObject[] balloonPrefabs;
    public float spawnRate = 1f;
    public float minX, maxX;
    public float spawnY;
    public TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI timerText;
    public GameObject gameOverPanel;
    public TMPro.TextMeshProUGUI finalScoreText;

    
    private float screenLeft;
    private float screenRight;
    private float screenBottom;
    private Camera mainCamera;
    private int score;
    private float timeLeft = 60f;
    private bool isGameActive;
    


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
        
        Debug.Log("Screen bounds: Left " + screenLeft + ", Right " + screenRight + ", Bottom " + screenBottom);
        StartGame();
    }

    void Update()
    {
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
        score = 0;
        timeLeft = 60f;
        isGameActive = true;
        UpdateScoreDisplay();
        UpdateTimerDisplay();
        StartCoroutine(SpawnBalloons());
    }
    void SpawnBalloon()
    {
        Debug.Log("Inside spawn balloon");
        int index = Random.Range(0, balloonPrefabs.Length);
        float randomX = Random.Range(screenLeft+minX, screenRight-maxX);
        Vector3 spawnPos = new Vector3(randomX, screenBottom+spawnY, 0);
        GameObject balloon = Instantiate(balloonPrefabs[index], spawnPos, Quaternion.identity);
    }
    
    IEnumerator SpawnBalloons()
    {
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

    void UpdateScoreDisplay()
    {
        scoreText.text = "Score: " + score;
    }

    void UpdateTimerDisplay()
    {
        int seconds = Mathf.FloorToInt(timeLeft);
        timerText.text = "Time: " + seconds;
    }

    void EndGame()
    {
        isGameActive = false;
        gameOverPanel.SetActive(true);
        finalScoreText.text = "Final Score: " + score;
        // TODO: Check for high score and update if necessary
    }
}