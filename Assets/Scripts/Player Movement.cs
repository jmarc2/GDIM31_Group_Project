using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float xspd;
    [SerializeField] private float yspd;
    [SerializeField] private float mxspd = 8;
    [SerializeField] private float accel = 5;
    [SerializeField] private float decel = 2;
    [SerializeField] private float jumpstr = 10;
    [SerializeField] private Text scoreDisplay;
    [SerializeField] TextMeshProUGUI highScoreText;

    private int score;
    private float flip;

    private float jumpTimer = 0f;
    public float maxJumpTime = 0.3f;

    private float JetpackFuel = 50f;

    private bool OnFloor = false;
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
        if (verticalInput > 0 && JetpackFuel > 0)
        {
            JetpackFuel -= 1;
            rb.AddForce(Vector2.up * (yspd + jumpstr), ForceMode2D.Impulse);
            jumpTimer += Time.fixedDeltaTime;
        }

        // Cooldown for jump
        if (OnFloor)
        {
            if(JetpackFuel < 50)
                JetpackFuel++;
        }
        OnFloor = false;


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
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Start jump cooldown when colliding with specific objects
        if (collision.gameObject.CompareTag("Platform"))
        {
            SetOnFloor();
        }
    }

    // Start jump cooldown
    private void SetOnFloor()
    {
        OnFloor = true;
    }
}
