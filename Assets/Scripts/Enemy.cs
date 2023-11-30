using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float movement;
    public float speed;
    public float upForce;
    private Rigidbody2D rb;
    private float newTime;
    public float right;
    public float left;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Chooses which side the enemy is going to move
        float mo = Random.Range(1, 3);

        if (mo == 1)
        {
            movement = left;
        }

        if (mo == 2)
        {
            movement = right;
        }

        //Decides how often the enemy wants to turn around
        if (Time.time > newTime)
        {
            rb.velocity = new Vector2(movement * speed, upForce);
            newTime = Time.time + Random.Range(0, 4);
        }
        

        //Allows the enemy to go outside the view of the camera to show up on the other side of the map.
        if (rb.position.x < -17.4f)
        {
            rb.position = new Vector2(rb.position.x + 34.8f, rb.position.y);
        }

        if (rb.position.x > 17.4f)
        {
            rb.position = new Vector2(rb.position.x - 34.8f, rb.position.y);
        }

        if (rb.position.x > 16f && rb.position.x < 18f) 
        {
            rb.position = new Vector2(movement * speed, upForce);
        }

        if (rb.position.x > -16f && rb.position.x < -14f)
        {
            rb.position = new Vector2(movement * speed, upForce);
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Kill Floor"))
        {
            Destroy(gameObject);
        }

    }

}
