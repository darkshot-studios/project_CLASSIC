﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class jumperMovement : MonoBehaviour 
{
	public bool locked = false;
	public float maxVelocity;

	public Rigidbody2D rb;

	public Vector2 initP;
	public Vector2 finalP;

	public float power;

	public GameObject anchor;
	public SpriteRenderer rend;

    private float rotSpeed;

    public bool landed = false;

	void start()
	{
		rend = anchor.GetComponent<SpriteRenderer>();
	}


	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.R))
			SceneManager.LoadScene("scn_game_jumper");
		
        if (Input.GetMouseButton(0) & landed)
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
					rend.transform.position = initP;
					rend.enabled = true;
				}
			} 
		}
		else
		{
			rend.enabled = false;
			gameObject.GetComponent<Rigidbody2D>().gravityScale = 8;
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

				if(initP.y > finalP.y)
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

				//Do physics
				gameObject.GetComponent<Rigidbody2D>().AddForce (supahPOWAH, ForceMode2D.Impulse);

				// Reset it
				initP = new Vector2 (0, 0);
			}
			locked = false;
		} 		
	}

	void FixedUpdate()
	{
		rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		landed = true;
	}

	void OnCollisionExit2D(Collision2D other)
	{
		landed = false;
	}
}

