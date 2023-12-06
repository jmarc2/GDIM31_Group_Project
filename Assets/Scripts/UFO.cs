using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : Enemy
{
    [SerializeField]
    private float lowgrav;
    [SerializeField]
    private float normgrav;

    // Update is called once per frame
    public override void AfterUpdate()
    {
        //base.AfterUpdate();

        float mo = Random.Range(1, 3);

        if (mo == 1)
        {
            movement = left;
        }

        if (mo == 2)
        {
            movement = right;
        }

        if (rb.velocity.y < 0)
        {
            rb.gravityScale = lowgrav;
        }

        else
            rb.gravityScale = normgrav;

        //Decides how often the enemy wants to turn around
        if (Time.time > newTime)
        {
            rb.velocity = new Vector2(movement * speed, upForce);
            newTime = Time.time + Random.Range(0, 1);

            //Allows the enemy to go outside the view of the camera to show up on the other side of the map.
            if (rb.position.x < -18.4f)
            {
                rb.position = new Vector2(rb.position.x + 35.8f, rb.position.y);
            }

            if (rb.position.x > 18.4f)
            {
                rb.position = new Vector2(rb.position.x - 35.8f, rb.position.y);
            }

        }
    }





}