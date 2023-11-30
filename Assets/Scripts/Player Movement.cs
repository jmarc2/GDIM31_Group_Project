using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxisRaw("Horizontal");


        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            rb.velocity = transform.up * upForce;
        }

        if (rb.position.x < -17.4f)
        {
            rb.position = new Vector2(rb.position.x + 34.8f, rb.position.y);
        }

        if (rb.position.x > 17.4f)
        {
            rb.position = new Vector2(rb.position.x - 34.8f, rb.position.y);
        }



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Game Over"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Point score"))
        {
           
        }
    }

}