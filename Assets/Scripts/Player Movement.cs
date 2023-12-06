using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed;
    [SerializeField]
    public float upForce;
    [SerializeField]
    public float fuel;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float move;
    [SerializeField] public float xspd;
    [SerializeField] public float yspd;
    [SerializeField] public float mxspd = 8;
    [SerializeField] public float accel = 5;
    [SerializeField] public float decel = 2;
    [SerializeField] public float grav = 5;
    [SerializeField] public float jumpstr = 10;
    [SerializeField] private Text scoreDisplay;

    private int score;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        // Player movement left and right
        if (horizontalInput > 0)
        {
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x + accel, -mxspd, mxspd), rb.velocity.y);
        }
        else if (horizontalInput < 0)
        {
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x - accel, -mxspd, mxspd), rb.velocity.y);
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
        float verticalInput = Input.GetAxisRaw("Vertical");
        if (verticalInput > 0)
        {
            rb.AddForce(Vector2.up * (yspd + jumpstr), ForceMode2D.Impulse);
        }
        

        // The rest of your code...

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
            Destroy(gameObject);
        }
    }
}
