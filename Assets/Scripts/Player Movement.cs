using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
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
    [SerializeField]
    private Text scoreDisplay;

    private int score;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        score = 0;
        scoreDisplay.text = "Score: " + score;
    }

    // Update is called once per frame
    void Update()
    {
        //Allows to take A,D, <-, and -> as inputs for moving left and right
        move = Input.GetAxisRaw("Horizontal");

        //Moves the player left and right
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.velocity = transform.up * upForce;
        }

        //These two if statements check for when the player moves past the
        //camera and teleports the player to the other side of the map.
        if (rb.position.x < -18.4f)
        {
            rb.position = new Vector2(rb.position.x + 35.8f, rb.position.y);
        }

        if (rb.position.x > 18.4f)
        {
            rb.position = new Vector2(rb.position.x - 35.8f, rb.position.y);
        }


        //ya mama
    }

    //deletes the player object if contact between the player and bottom side of an enemy is met
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //add 10 points when player collides with enemy's top side
        if (collision.gameObject.CompareTag("Point score"))
        {
            score += 10;
            scoreDisplay.text = "Score: " + score;
        }

        if (collision.gameObject.CompareTag("Game Over"))
        {
            Destroy(gameObject);
        }

    }

}