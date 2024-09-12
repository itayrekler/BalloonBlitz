using UnityEngine;

public class Balloon : MonoBehaviour
{
    public int pointValue = 1;
    public float speed = 1f;
    public bool isZigzag = false;
    public bool isBomb = false;

    private float zigzagTimer = 0f;
    private float zigzagInterval = 1f;
    private int zigzagDirection = 1;
    //scxvcx

    void Update()
    {
        if (isZigzag)
        {
            ZigzagMovement();
        }
        else
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }

        if (transform.position.y > Camera.main.orthographicSize + 1f)
        {
            Destroy(gameObject);
        }
    }

    void ZigzagMovement()
    {
        zigzagTimer += Time.deltaTime;
        if (zigzagTimer >= zigzagInterval)
        {
            zigzagDirection *= -1;
            zigzagTimer = 0f;
        }

        Vector3 movement = new Vector3(zigzagDirection, 1, 0).normalized;
        transform.Translate(movement * speed * Time.deltaTime);
    }

    void OnMouseDown()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            if (isBomb)
            {
                int score = gameManager.GetScore();
                if (score < pointValue)
                    gameManager.IncrementScore(-1*score);
                else
                    gameManager.IncrementScore(-1*pointValue);
            }
            else
            {
                gameManager.IncrementScore(pointValue);
            }
        }
        Destroy(gameObject);
    }
}