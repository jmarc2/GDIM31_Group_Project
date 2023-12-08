using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public float xspd;
    [SerializeField] public float yspd;
    [SerializeField] public float mxspd = 8;
    [SerializeField] public float accel = 5;
    [SerializeField] public float decel = 2;
    [SerializeField] public float jumpstr = 10;
    [SerializeField] private Text scoreDisplay;
    [SerializeField] TextMeshProUGUI highScoreText;

    private int score;
    public float flip;

    private bool isJumping = false;
    private float jumpTimer = 0f;
    public float maxJumpTime = 0.3f;

    private bool isJumpCooldown = false;
    private float jumpCooldownTimer = 0f;
    public float jumpCooldownDuration = 2.0f;

    void CheckHighScore()
    {
        if (score > PlayerPrefs.GetInt("High Score", 0))
        {
            PlayerPrefs.SetInt("High Score", score);
            UpdateHighScoreText();
        }
    }

    void UpdateHighScoreText()
    {
        highScoreText.text = $"High Score: {PlayerPrefs.GetInt("High Score", 0)}";
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        score = 0;
        scoreDisplay.text = "Score: " + score;
        flip = transform.localScale.x;
        UpdateHighScoreText();
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Player movement left and right
        if (horizontalInput > 0)
        {
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x + accel, -mxspd, mxspd), rb.velocity.y);
            transform.localScale = new Vector3(flip, transform.localScale.y, transform.localScale.z);
        }
        else if (horizontalInput < 0)
        {
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x - accel, -mxspd, mxspd), rb.velocity.y);
            transform.localScale = new Vector3((-1) * flip, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            // Decelerate when not pressing left or right
            if (rb.velocity.x > 0)
            {
                rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x - decel, 0, mxspd), rb.velocity.y);
            }
            else if (rb.velocity.x < 0)
            {
                rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x + decel, -mxspd, 0), rb.velocity.y);
            }
        }

        // Jumping
        if (verticalInput > 0 && !isJumpCooldown)
        {
            if (!isJumping)
            {
                isJumping = true;
                jumpTimer = 0f;
            }

            // Check if jump duration is within the allowed limit
            if (jumpTimer < maxJumpTime)
            {
                rb.AddForce(Vector2.up * (yspd + jumpstr), ForceMode2D.Impulse);
                jumpTimer += Time.fixedDeltaTime;
            }
        }
        else
        {
            isJumping = false;
            jumpTimer = 0f;
        }

        // Cooldown for jump
        if (isJumpCooldown)
        {
            jumpCooldownTimer += Time.fixedDeltaTime;

            if (jumpCooldownTimer >= jumpCooldownDuration)
            {
                isJumpCooldown = false;
                jumpCooldownTimer = 0f;
            }
        }


        // Screen edge teleport
        if (transform.position.x < -17.4f)
        {
            transform.position = new Vector2(transform.position.x + 34.8f, transform.position.y);
        }
        else if (transform.position.x > 17.4f)
        {
            transform.position = new Vector2(transform.position.x - 34.8f, transform.position.y);
        }
    }

    //deletes the player object if contact between the player and bottom side of an enemy is met
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Game Over"))
        {
            GameStateManager.GameOver();
        }

        if (collision.gameObject.CompareTag("Kill Floor"))
        {
            GameStateManager.GameOver();
        }

        //add 10 points when player collides with enemy's top side
        if (collision.gameObject.CompareTag("Point score"))
        {
            score += 10;
            scoreDisplay.text = "Score: " + score;
            CheckHighScore();
        }

        // Start jump cooldown when colliding with specific objects
        if (collision.gameObject.CompareTag("Platform"))
        {
            StartJumpCooldown();
        }
    }

    // Start jump cooldown
    private void StartJumpCooldown()
    {
        isJumpCooldown = true;
    }
}
