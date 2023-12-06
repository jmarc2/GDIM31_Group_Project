using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public float movement;
    [SerializeField]
    public float speed;
    [SerializeField]
    public float upForce;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float newTime;
    [SerializeField]
    public float right;
    [SerializeField]
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

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Kill Floor"))
        {
            rb.position = new Vector2(rb.position.x, 7);
        }

    }

    public static void SpawnE(GameObject Enemy)
    {
        Instantiate(rb);
        float[] spawnx = { -12, 13, -9, 11 };
        float[] spawny = { 4, 4, 1, 1 };
        int spawn = Random.Range(0, 3);

        rb.position = new Vector2(spawnx[spawn], spawny[spawn]);
    }

}
