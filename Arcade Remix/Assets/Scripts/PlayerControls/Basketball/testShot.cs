﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testShot : MonoBehaviour
{

    public bool locked = false;
    public float maxVelocity;

    public Rigidbody2D rb;

    public Vector2 initP;
    public Vector2 finalP;

    public float power;


    private float rotSpeed;

    public bool touching;

    public GameObject results;
    public int secs;

    public GameObject lr;
    private LineRenderer lr2;

    // player jump
    public GameObject player;

    void Start()
    {
        lr2 = lr.GetComponent<LineRenderer>();
        global.winner = false;
        global.goalCounter = 1;
    }

    void Update()
    {
        if (global.goalCounter ==0)
        {
            global.winner = true;
            results.SetActive(true);
            Destroy(this);
        }
        if (Input.GetMouseButton(0) && (gameObject.transform.position.y <= -10.5f))
        {
            // This 'lock' allows the logic for dragging... since only when you let go, does it 
            // turn false
            locked = true;
            // Stop ball from moving as player calculates shot
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            rb.velocity = new Vector2(0, 0);

            if (locked)
            {
                // We can get initial point
                if ((initP.x == 0) && (initP.y == 0))
                {
                    initP = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    lr2.SetPosition(0, new Vector3(initP.x-7.35f, initP.y,-1));
                }
                finalP = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                lr2.SetPosition(1,   new Vector3(finalP.x-7.35f, finalP.y,-1));
            }
        }
        else
        {
            lr2.SetPosition(0,new Vector3(100f, 100f,-1));
            lr2.SetPosition(1,new Vector3(100f, 100f,-1));
            if (!touching)
                gameObject.GetComponent<Rigidbody2D>().gravityScale = 10;

            if (locked)
            {
                finalP = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // Find the direction
                int xDir;
                int yDir;
                if (initP.x > finalP.x)
                    xDir = 1;
                else
                    xDir = -1;

                if (initP.y > finalP.y)
                    yDir = 1;
                else
                    yDir = -1;

                // Find the real power (distance between x, and y vectors
                // subtract the x, and y
                float xDist = initP.x - finalP.x;
                float yDist = initP.y - finalP.y;

                xDist = Mathf.Abs(xDist);
                yDist = Mathf.Abs(yDist);
                // Can also set a max distance to avoid overpowering

                ///  direction (probably 1, or -1... calculated by if statements of initP and finalP)* magnitude * power(which is a public float) 
                Vector2 supahPOWAH = new Vector2(xDir * power * xDist, yDir * power * yDist);
                if (yDist > 12)
                    yDist = 12;
                Vector2 jumpPOWAH = new Vector2(0, (yDir * power * yDist)/4);
                // Do physics
                gameObject.GetComponent<Rigidbody2D>().AddForce(supahPOWAH, ForceMode2D.Impulse);
                player.GetComponent<Rigidbody2D>().AddForce(jumpPOWAH, ForceMode2D.Impulse);
                // Reset it
                initP = new Vector2(0, 0);
                touching = false;
            }
            locked = false;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "stop") // For the opponents hitbox
        {
            touching = true;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            rb.velocity = new Vector2(0, 0);
        }
    }
}