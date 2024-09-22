using UnityEngine;
/**
 * Ballon Object:
 * This class represents a single balloon object
 * and contains the mandatory attributes and defaults
 * for the balloon life-cycle in the game.
 */
public class Balloon : MonoBehaviour
{
    public int pointValue = 1;
    public float speed = 1f;
    public bool isZigzag = false;
    public bool isBomb = false;

    private float zigzagTimer = 0f;
    private float zigzagInterval = 1f;
    private int zigzagDirection = 1;
    
    private float rightBorder = 0f;
    private float leftBorder = 0f;
    private float rightPos;
    private float leftPos;

    void Start()
    {
        // At first, we set the screen borders for the object 
        rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, Camera.main.nearClipPlane)).x;
        leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).x;
    }
    
    void Update()
    {
        /**
         * At every update we chack the bounds of the object to make sure its bouncing back to the screen
         * when the borders are crossed. Moreover, We assign the movement functionality upon the relevant needs
         */
        Bounds bounds = transform.GetComponent<Collider2D>().bounds;
        leftPos = bounds.min.x;
        rightPos = bounds.max.x;
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
        if (rightPos >= rightBorder)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            isZigzag = true;
        }
        
        if (leftPos <= leftBorder)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            isZigzag = true;
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
    /**
     * Upon click we check the type of the balloon to update the score and destory(pop) the balloon
     */
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